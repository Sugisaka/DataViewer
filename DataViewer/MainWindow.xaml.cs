using MyMath;
using System.Collections.ObjectModel;
using System.IO;
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

namespace DataViewer
{
    public class MainViewModel
    {
        public ObservableCollection<DataView> DV { get; } = new() { };
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// プレビュー用の画像保存場所
        /// </summary>
        string dirwork = @"C:\DataViewerWorkSpace";

        MainViewModel vm1 = new MainViewModel();

        plot2d plt = new plot2d();

        List<BitmapImage> animationBMP = new List<BitmapImage>(0);

        int animationNdiv = 20;

        int animationCounter = 0;

        private TimeSpan _lastTime = TimeSpan.Zero;

        private TimeSpan _frameInterval = TimeSpan.FromMilliseconds(100);

        bool isAnimationEnabled = false;

        /// <summary>
        /// 開いているメニュー
        /// </summary>
        int openmenu = 1;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm1;
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
            e.Handled = true;
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            string ext = System.IO.Path.GetExtension(filenames[0]);
            if (ext == ".xml")
            {
                //DataView.LoadSettingXML(DV, filenames[0]);
            }
            else
            {
                DataView.LoadFiles(vm1.DV, filenames, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, cb_colmap, StatusText, dirwork, AnimationStop);
            }
        }

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
            var mag = slider1.Value;
            t_mag.Text = ((int)mag).ToString();
            PreviewGrid.Width = (int)Math.Floor(0.5 + (0.01 * mag) * plt.nx);
            PreviewGrid.Height = (int)Math.Floor(0.5 + (0.01 * mag) * plt.ny);
            image1.Width = (int)Math.Floor(0.5 + (0.01 * mag) * plt.nx);
            image1.Height = (int)Math.Floor(0.5 + (0.01 * mag) * plt.ny);
        }
        private void image1_MouseMove(object sender, MouseEventArgs e)
        {
            var mag = slider1.Value;
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
            foreach (var d in vm1.DV)
            {
                DataView.plot(d, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, cb_colmap, dirwork, AnimationStop);
            }
        }

        private void b_applyall_Click(object sender, RoutedEventArgs e)
        {
            var selected = listView1?.SelectedItem as DataView;
            if (selected != null)
            {
                foreach (var row in vm1.DV)
                {
                    row.FileFormat = selected.FileFormat;
                    row.PlotType = selected.PlotType;
                    row.ColX = selected.ColX;
                    row.ColY = selected.ColY;
                    row.ColRe = selected.ColRe;
                    row.ColIm = selected.ColIm;
                    row.ColorMap = selected.ColorMap;
                    row.Max = selected.Max;
                    row.Min = selected.Min;
                }
            }
        }

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = listView1?.SelectedItem as DataView;
            if (selected != null)
            {
                vm1.DV.Remove(selected);
            }
        }

        private void b_deleteall_Click(object sender, RoutedEventArgs e)
        {
            vm1.DV.Clear();
        }

        private void OnRendering(object? sender, EventArgs e)
        {
            var args = (RenderingEventArgs)e;

            if (args.RenderingTime - _lastTime < _frameInterval)
                return;

            _lastTime = args.RenderingTime;

            animationCounter = (animationCounter + 1) % animationNdiv;
            image1.Source = animationBMP[animationCounter];
        }

        private void b_phaseAnimation_Click(object sender, RoutedEventArgs e)
        {
            var selected = listView1?.SelectedItem as DataView;
            if (selected == null)
            {
                MessageBox.Show("プロットするファイルを選択してください");
            }
            else
            {
                animationBMP = new List<BitmapImage>(0);
                DataView.animationPlot(dirwork, animationBMP, animationNdiv, selected, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, AnimationStop);
                AnimationStart();
            }
        }

        private void AnimationStart()
        {
            if (!isAnimationEnabled)
            {
                CompositionTarget.Rendering += OnRendering;
                isAnimationEnabled = true;
            }
        }
        private void AnimationStop()
        {
            if (isAnimationEnabled)
            {
                CompositionTarget.Rendering -= OnRendering;
                isAnimationEnabled = false;
            }
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = listView1?.SelectedItem as DataView;
            if (selected != null)
            {
                if (File.Exists(selected.FileDir + "\\" + selected.FileName))
                {
                    StatusText.Text = "Selected: " + selected.FileDir + "\\" + selected.FileName;
                }
            }
        }
    }
}
