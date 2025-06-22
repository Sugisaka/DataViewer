using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MyMath;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Interop;
using System.IO.Compression;
using System.Drawing;

namespace DataViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// プレビュー用の画像保存場所
        /// </summary>
        string dirwork = @"C:\DataViewerWorkSpace";

        ObservableCollection<DataView> DV = new ObservableCollection<DataView>();

        plot2d plt = new plot2d();

        /// <summary>
        /// 開いているメニュー
        /// </summary>
        int openmenu = 1;

        /// <summary>
        /// 読み込み済みのファイル
        /// </summary>
        string LoadedFile = "";

        /// <summary>
        /// グラフの表示倍率
        /// </summary>
        double mag;

        public MainWindow()
        {
            InitializeComponent();
            //コンボボックスに選択できる項目のリストを追加
            cmb_colmap.ItemsSource = ColorMaps;
            cb_plottype.ItemsSource = DataTypes;
            DataGrid1.ItemsSource = DV;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(dirwork))
            {
                var ret = MessageBox.Show("作業フォルダ「" + dirwork + "」を作成してもよろしいですか？", "作業フォルダ作成", MessageBoxButton.YesNo);
                if (ret == MessageBoxResult.Yes)
                {
                    Directory.CreateDirectory(dirwork);
                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (Directory.Exists(dirwork))
            {
                string[] files = Directory.GetFiles(dirwork, "*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void listwindow_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void listwindow_PreviewDrop(object sender, DragEventArgs e)
        {
            // Mark the event as handled, so TextBox's native Drop handler is not called.
            e.Handled = true;
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            string ext = System.IO.Path.GetExtension(filenames[0]);
            if (ext == ".xml")
            {
                //DataView.LoadSettingXML(DV, filenames[0]);
            }
            else
            {
                DataView.LoadFiles(DV, filenames);
            }
            //listView.SelectedIndex = DV.Count - 1;

            //listView.SelectedIndex = DV.Count - 1;
            //MenuSetting();
        }

        // Enumの定義名変更プロパティ
        public Dictionary<DataType, string> DataTypes { get; } = new Dictionary<DataType, string>
        {
            [DataType.Re] = "re",
            [DataType.Im] = "im",
            [DataType.Abs] = "abs",
            [DataType.Pha] = "pha",
            [DataType.Pow] = "pow",
            [DataType.Cpx] = "cpx"
        };
        // Enumの定義名変更プロパティ
        public Dictionary<ColorMap, string> ColorMaps { get; } = new Dictionary<ColorMap, string>
        {
            [ColorMap.Gray] = "Gray",
            [ColorMap.RedPower] = "RedPower",
            [ColorMap.BluePower] = "BulePower",
            [ColorMap.Horizon] = "Horizon",
            [ColorMap.Rainbow] = "Rainbow",
            [ColorMap.RainbowCycle] = "RainbowCycle",
            [ColorMap.RainbowRGB] = "RainbowRGB",
            [ColorMap.RainbowDark] = "RainbowDark",
            [ColorMap.RainbowPower] = "RainbowPower",
            [ColorMap.Wave] = "Wave",
            [ColorMap.WaveLight] = "WaveLight",
            [ColorMap.WaveDark] = "WaveDark",
            [ColorMap.WaveNight] = "WaveNight",
            [ColorMap.WavePower] = "WavePower",
            [ColorMap.NightScapeRed] = "NightScapeRed",
            [ColorMap.NightScapeOrange] = "NightScapeOrange",
            [ColorMap.NightScapeYellow] = "NightScapeYellow",
            [ColorMap.NightScapeGreen] = "NightScapeGreen",
            [ColorMap.NightScapeCyan] = "NightScapeCyan",
            [ColorMap.NightScapeBlue] = "NightScapeBlue",
            [ColorMap.NightScapeViolet] = "NightScapeViolet",
            [ColorMap.LandScapeRed] = "LandScapeRed",
            [ColorMap.LandScapeBlue] = "LandScapeBlue",
            [ColorMap.ComplexLightRainbowCycle] = "ComplexLight",
            [ColorMap.ComplexVividRainbowCycle] = "ComplexVivid"
        };

        /// <summary>
        /// idで指定したメニューを開く
        /// id=0ならば全メニューを閉じる
        /// </summary>
        /// <param name="id"></param>
        private void openclosemenu(int id)
        {
            if (id == 0)
            {
                //メニューを閉じる
                menu1.Width = new GridLength(0.0);
                openmenu = 0;
            }
            else if (id == 1)
            {
                //メニュー1を開く
                menu1.Width = new GridLength(200.0);
                openmenu = 1;
            }
        }

        private void b_info_Click(object sender, RoutedEventArgs e)
        {
            int menuid = 1;
            if (openmenu == menuid)
            {
                openclosemenu(0);
            }
            else
            {
                openclosemenu(menuid);
            }
        }

        private string SetOutputFileName(string outputdir, string filename, string addfilename)
        {
            //プレビューに表示するビットマップファイルを作成
            var index = 0;
            string file = "";
            string filename_ = System.IO.Path.GetFileNameWithoutExtension(filename);
            if (addfilename == "")
            {
                file = outputdir + "\\" + filename_ + ".bmp";
            }
            else
            {
                file = outputdir + "\\" + filename_ + "_" + addfilename + ".bmp";
            }
            while (File.Exists(file))
            {
                index++;
                if (addfilename == "")
                {
                    file = outputdir + "\\" + filename_ + "_" + index + ".bmp";
                }
                else
                {
                    file = outputdir + "\\" + filename_ + "_" + addfilename + "_" + index + ".bmp";
                }
            }
            return file;
        }
        private (bool, double, double) scaleSetting(DataView d)
        {
            if (d.ZMin == "auto" || d.ZMax == "auto")
            {

                return (true, 0.0, 0.0);
            }
            else
            {
                double min, max;
                try
                {
                    min = Double.Parse(d.ZMin);
                }
                catch
                {
                    return (true, 0.0, 0.0); ;
                }
                try
                {
                    max = Double.Parse(d.ZMax);
                }
                catch
                {
                    return (true, 0.0, 0.0); ;
                }
                return (false, min, max);
            }
        }
        /// <summary>
        /// データのプレビュー
        /// </summary>
        /// <param name="outputdir"></param>
        /// <param name="addfilename"></param>
        /// <param name="zmax"></param>
        /// <param name="zmin"></param>
        /// <param name="phaseshift"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private string preview(string outputdir, string addfilename, double phaseshift, DataView d)
        {
            var file = SetOutputFileName(outputdir, d.FileName, addfilename);
            (bool autoScale, double zmin, double zmax) = scaleSetting(d);
            plt.writeBMP24(DataView.DataTypeIndex(d.Type), phaseshift, d.ColMap.ToString(), file, autoScale, zmin, zmax);
            //ビットマップファイルを表示
            BitmapImage bmp;
            try
            {
                bmp = new BitmapImage(new Uri(file, System.UriKind.Absolute));
            }
            catch (FileNotFoundException)
            {
                return "";
            }
            catch (FileFormatException)
            {
                MessageBox.Show("画像ファイルの生成に失敗しました", "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);

                return "";
            }
            mag = slider1.Value;
            t_mag.Text = ((int)mag).ToString();
            PreviewGrid.Width = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelWidth);
            PreviewGrid.Height = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelHeight);
            image1.Width = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelWidth);
            image1.Height = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelHeight);
            image1.Source = bmp;
            t_Nx.Text = plt.nx.ToString();
            t_Ny.Text = plt.ny.ToString();
            if (d.Type == DataType.Re)
            {
                lb_zmin.Content = "min(Re) = ";
                lb_zmax.Content = "max(Re) = ";
                t_zmin.Text = plt.minRe.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxRe.ToString("0.0#######e+00");
            }
            else if (d.Type == DataType.Im)
            {
                lb_zmin.Content = "min(Im) = ";
                lb_zmax.Content = "max(Im) = ";
                t_zmin.Text = plt.minIm.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxIm.ToString("0.0#######e+00");
            }
            else if (d.Type == DataType.Abs)
            {
                lb_zmin.Content = "min(Abs) = ";
                lb_zmax.Content = "max(Abs) = ";
                t_zmin.Text = plt.minAbs.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxAbs.ToString("0.0#######e+00");
            }
            else if (d.Type == DataType.Pha)
            {
                lb_zmin.Content = "min(Pha) = ";
                lb_zmax.Content = "max(Pha) = ";
                t_zmin.Text = plt.minPha.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxPha.ToString("0.0#######e+00");
            }
            else if (d.Type == DataType.Pow)
            {
                lb_zmin.Content = "min(Pow) = ";
                lb_zmax.Content = "max(Pow) = ";
                t_zmin.Text = (plt.minAbs * plt.minAbs).ToString("0.0#######e+00");
                t_zmax.Text = (plt.maxAbs * plt.maxAbs).ToString("0.0#######e+00");
            }
            else if (d.Type == DataType.Cpx)
            {
                lb_zmin.Content = "min(Abs) = ";
                lb_zmax.Content = "max(Abs) = ";
                t_zmin.Text = plt.minAbs.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxAbs.ToString("0.0#######e+00");
            }
            return file;
        }
        private void b_prev_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is DataView d)
            {
                if (!File.Exists(d.FileFullPath))
                {
                    MessageBox.Show("ファイル「" + d.FileFullPath + "」は存在しません", "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (d.IsBinaryData)
                    {
                        plt.FileRead(d.FileFullPath);
                    }
                    else
                    {
                        plt.FileRead(d.FileFullPath, d.ColX, d.ColY, d.ColZ, d.ColI);
                    }
                    LoadedFile = d.FileFullPath;
                    preview(this.dirwork, "", 0.0, d);
                    if (plt.error != "")
                    {
                        MessageBox.Show(plt.error, "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void plot(DataView d)
        {
            if (!File.Exists(d.FileFullPath))
            {
                MessageBox.Show("ファイル「" + d.FileFullPath + "」は存在しません", "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (d.IsBinaryData)
                {
                    plt.FileRead(d.FileFullPath);
                }
                else
                {
                    plt.FileRead(d.FileFullPath, d.ColX, d.ColY, d.ColZ, d.ColI);
                }
                LoadedFile = d.FileFullPath;
                var file1 = preview(this.dirwork, "", 0.0, d);
                var file2 = SetOutputFileName(d.FileDir, d.FileName, "");
                if (file1 != "")
                {
                    var cbout = (cb_colmap.IsChecked != null && (bool)cb_colmap.IsChecked);
                    //プロットファイルをコピー
                    File.Copy(file1, file2);
                    //再プロット用スクリプト
                    (bool autoScale, double zmin, double zmax) = scaleSetting(d);
                    DataView.OutputSource(d, file2, autoScale, zmin, zmax, cbout);
                    //カラーバー
                    if (cbout)
                    {
                        plt.writeColorBar(DataView.DataTypeIndex(d.Type), d.ColMap.ToString(), file2.Replace(".bmp", "_colorbar.bmp"), autoScale, zmin, zmax);
                    }
                }
                return;
            }
        }
        private void b_plot_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is DataView d)
            {
                plot(d);
            }
            else
            {
                return;
            }
        }

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is DataView d)
            {
                DV.Remove(d);
            }
        }

        private void Lwhite_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer2.Background = System.Windows.Media.Brushes.White;
        }

        private void Lred_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer2.Background = System.Windows.Media.Brushes.LightPink;
        }

        private void Lgreen_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer2.Background = System.Windows.Media.Brushes.LimeGreen;
        }

        private void Lblue_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer2.Background = System.Windows.Media.Brushes.LightSkyBlue;
        }

        private void Ldark_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer2.Background = System.Windows.Media.Brushes.Gray;
        }
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (LoadedFile != "")
            {
                mag = slider1.Value;
                t_mag.Text = ((int)mag).ToString();
                PreviewGrid.Width = (int)Math.Floor(0.5 + (0.01 * mag) * plt.nx);
                PreviewGrid.Height = (int)Math.Floor(0.5 + (0.01 * mag) * plt.ny);
                image1.Width = (int)Math.Floor(0.5 + (0.01 * mag) * plt.nx);
                image1.Height = (int)Math.Floor(0.5 + (0.01 * mag) * plt.ny);
            }
        }
        private void image1_MouseMove(object sender, MouseEventArgs e)
        {
            int ix = (int)Math.Floor(e.GetPosition(image1).X / (0.01 * mag));
            int iy = (int)Math.Floor((image1.Height - e.GetPosition(image1).Y) / (0.01 * mag));

            t_ix.Text = (ix + 1).ToString();
            t_iy.Text = (iy + 1).ToString();

            if (0 <= ix && ix < plt.nx && 0 <= iy && iy < plt.ny)
            {
                if (plt.isCPXdata)
                {
                    var re = plt.Re(ix, iy);
                    var im = plt.Im(ix, iy);
                    t_re.Text = re.ToString("0.0#######e+00");
                    t_im.Text = im.ToString("0.0#######e+00");
                    t_abs.Text = Math.Sqrt(re * re + im * im).ToString("0.0#######e+00");
                    t_power.Text = (re * re + im * im).ToString("0.0#######e+00");
                    t_phase.Text = (Math.Atan2(im, re) * 180.0 / Math.PI).ToString("0.0#######");
                }
                else
                {
                    t_re.Text = plt.Re(ix, iy).ToString("0.0#######e+00");
                    t_im.Text = "-";
                    t_abs.Text = "-";
                    t_power.Text = "-";
                    t_phase.Text = "-";
                }
            }
        }
        private void b_clear_Click_1(object sender, RoutedEventArgs e)
        {
            t_note.Text = "";
        }
        private void b_copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(t_note.Text);
        }
        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (t_note.Text != "")
            {
                t_note.Text = t_note.Text + "\r\n";
            }
            if (t_re.Text != "-" && cb_re.IsChecked != null && (bool)cb_re.IsChecked)
            {
                if (!t_note.Text.EndsWith("\r\n") && t_note.Text != "")
                {
                    t_note.Text = t_note.Text + ",";
                }
                t_note.Text = t_note.Text + t_re.Text;
            }
            if (t_im.Text != "-" && cb_im.IsChecked != null && (bool)cb_im.IsChecked)
            {
                if (!t_note.Text.EndsWith("\r\n") && t_note.Text != "")
                {
                    t_note.Text = t_note.Text + ",";
                }
                t_note.Text = t_note.Text + t_im.Text;
            }
            if (t_abs.Text != "-" && cb_abs.IsChecked != null && (bool)cb_abs.IsChecked)
            {
                if (!t_note.Text.EndsWith("\r\n") && t_note.Text != "")
                {
                    t_note.Text = t_note.Text + ",";
                }
                t_note.Text = t_note.Text + t_abs.Text;
            }
            if (t_power.Text != "-" && cb_pow.IsChecked != null && (bool)cb_pow.IsChecked)
            {
                if (!t_note.Text.EndsWith("\r\n") && t_note.Text != "")
                {
                    t_note.Text = t_note.Text + ",";
                }
                t_note.Text = t_note.Text + t_power.Text;
            }
            if (t_phase.Text != "-" && cb_pha.IsChecked != null && (bool)cb_pha.IsChecked)
            {
                if (!t_note.Text.EndsWith("\r\n") && t_note.Text != "")
                {
                    t_note.Text = t_note.Text + ",";
                }
                t_note.Text = t_note.Text + t_phase.Text;
            }
        }

        private void b_plotall_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataView d in DV)
            {
                plot(d);
            }
        }
        
        private void b_applyall_Click(object sender, RoutedEventArgs e)
        {
            var index = DataGrid1.SelectedIndex;
            if (index != -1)
            {
                for (int i = 0; i < DV.Count; i++)
                {
                    if (i != index)
                    {
                        DV[i].SetCondition(DV[index]);
                    }
                }
            }
            DataGrid1.Items.Refresh();
        }

        private void b_deleteall_Click(object sender, RoutedEventArgs e)
        {
            DV.Clear();
        }
    }
}