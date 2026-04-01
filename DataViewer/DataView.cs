using MyMath;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DataViewer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
            => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter)
            => _execute(parameter);
    }

    public class DataView : INotifyPropertyChanged
    {
        private string _FileDir = "";
        private string _FileName = "";
        private string? _selectedFileFormat;
        private string? _selectedPlotType;
        private string? _selectedColorMap;
        private string _Min = "";
        private string _Max = "";
        private string _ColX = "";
        private string _ColY = "";
        private string _ColRe = "";
        private string _ColIm = "";

        public DataView(string dir, string filename, plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, CheckBox cb_colmap, TextBlock status, string dirwork, Action AnimationStop)
        {
            FileDir = dir;
            FileName = filename;
            FileFormat = "Text";
            PlotType = "Re";
            ColorMap = "Gray";
            Min = "auto";
            Max = "auto";
            ColX = "1";
            ColY = "2";
            ColRe = "3";
            ColIm = "-";
            Preview = new RelayCommand(_ => OnButtonPreviewClicked(plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, status, dirwork, AnimationStop));
            Plot = new RelayCommand(_ => OnButtonPlotClicked(plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, cb_colmap, status, dirwork, AnimationStop));
        }

        public string FileDir
        {
            get => _FileDir;
            set
            {
                _FileDir = value;
                OnPropertyChanged();
            }
        }

        public string FileName
        {
            get => _FileName;
            set
            {
                _FileName = value;
                OnPropertyChanged();
            }
        }

        public string? FileFormat
        {
            get => _selectedFileFormat;
            set
            {
                _selectedFileFormat = value;
                OnPropertyChanged();
            }
        }

        public string? PlotType
        {
            get => _selectedPlotType;
            set
            {
                _selectedPlotType = value;
                OnPropertyChanged();
            }
        }

        public string? ColorMap
        {
            get => _selectedColorMap;
            set
            {
                _selectedColorMap = value;
                OnPropertyChanged();
            }
        }

        public string Min
        {
            get => _Min;
            set
            {
                _Min = value;
                OnPropertyChanged();
            }
        }

        public string Max
        {
            get => _Max;
            set
            {
                _Max = value;
                OnPropertyChanged();
            }
        }

        public string ColX
        {
            get => _ColX;
            set
            {
                _ColX = value;
                OnPropertyChanged();
            }
        }

        public string ColY
        {
            get => _ColY;
            set
            {
                _ColY = value;
                OnPropertyChanged();
            }
        }

        public string ColRe
        {
            get => _ColRe;
            set
            {
                _ColRe = value;
                OnPropertyChanged();
            }
        }

        public string ColIm
        {
            get => _ColIm;
            set
            {
                _ColIm = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> FileFormatOptions { get; } = new()
        {
            "Text", "Binary"
        };

        public ObservableCollection<string> PlotTypeOptions { get; } = new()
        {
            "Re", "Im", "Abs", "Pha", "Pow", "Cpx"
        };

        public ObservableCollection<string> ColorMapOptions { get; } = new()
        {
            "Gray",
            "RedPower",
            "BulePower",
            "Horizon",
            "Rainbow",
            "RainbowCycle",
            "RainbowRGB",
            "RainbowDark",
            "RainbowPower",
            "Wave",
            "WaveLight",
            "WaveDark",
            "WaveNight",
            "WavePower",
            "NightScapeRed",
            "NightScapeOrange",
            "NightScapeYellow",
            "NightScapeGreen",
            "NightScapeCyan",
            "NightScapeBlue",
            "NightScapeViolet",
            "LandScapeRed",
            "LandScapeBlue",
            "ComplexLight",
            "ComplexVivid"
        };

        public ObservableCollection<string> DataColumnOptions { get; } = new()
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        public ObservableCollection<string> DataImColumnOptions { get; } = new()
        {
            "-", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };

        public ICommand Preview { get; }
        public ICommand Plot { get; }

        private void OnButtonPreviewClicked(plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, TextBlock StatusText, string dirwork, Action AnimationStop)
        {
            FileLoad(dirwork, "", 0.0, this, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, AnimationStop);
            preview(dirwork, "", 0.0, this, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, AnimationStop);
            StatusText.Text = "Preview:  " + FileDir + "\\" + FileName;
        }

        private void OnButtonPlotClicked(plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, CheckBox cb_colmap, TextBlock StatusText, string dirwork, Action AnimationStop)
        {
            plot(this, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, cb_colmap, dirwork, AnimationStop);
            StatusText.Text = "PLot:  " + FileDir + "\\" + FileName;
        }

        /// <summary>
        /// データファイルのフルパス
        /// </summary>
        /// <returns></returns>
        public string FileFullPath
        {
            get
            {
                return this.FileDir + "\\" + this.FileName;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// 複数のファイル名をリストDVに追加
        /// </summary>
        /// <param name="DV"></param>
        /// <param name="filenames"></param>
        public static void LoadFiles(ObservableCollection<DataView> DV, string[] filenames, plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, CheckBox cb_colmap, TextBlock status, string dirwork, Action AnimationStop)
        {
            foreach (var filename in filenames)
            {
                if (System.IO.File.Exists(filename))
                {
                    var dir = Path.GetDirectoryName(filename);
                    if (dir != null)
                    {
                        var name = Path.GetFileName(filename);
                        DataView d = new DataView(dir, name, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, cb_colmap, status, dirwork, AnimationStop);
                        DV.Add(d);
                    }
                }
            }
        }

        static private (bool, double, double) scaleSetting(DataView d)
        {
            if (d.Min == "auto" || d.Max == "auto")
            {

                return (true, 0.0, 0.0);
            }
            else
            {
                double min, max;
                try
                {
                    min = Double.Parse(d.Min);
                }
                catch
                {
                    return (true, 0.0, 0.0); ;
                }
                try
                {
                    max = Double.Parse(d.Max);
                }
                catch
                {
                    return (true, 0.0, 0.0); ;
                }
                return (false, min, max);
            }
        }

        private void FileLoad(string outputdir, string addfilename, double phaseshift, DataView d, plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, Action AnimationStop)
        {
            if (d != null)
            {
                if (!File.Exists(d.FileFullPath))
                {
                    MessageBox.Show("ファイル「" + d.FileFullPath + "」は存在しません", "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (d.FileFormat == "Binary")
                    {
                        plt.FileRead(d.FileFullPath);
                    }
                    else
                    {
                        int ix = Int32.Parse(d.ColX);
                        int iy = Int32.Parse(d.ColY);
                        int ire = Int32.Parse(d.ColRe);
                        int iim = 0;
                        try
                        {
                            iim = Int32.Parse(d.ColIm);
                        }
                        catch
                        {
                            iim = 0;
                        }
                        plt.FileRead(d.FileFullPath, ix, iy, ire, iim);
                    }
                    preview(outputdir, "", 0.0, d, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_Ny, t_zmin, t_zmax, lb_zmin, lb_zmax, AnimationStop);
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

        static private string SetOutputFileName(string outputdir, string filename, string addfilename)
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
        static private string preview(string outputdir, string addfilename, double phaseshift, DataView d, plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, Action AnimationStop)
        {
            AnimationStop();
            var file = SetOutputFileName(outputdir, d.FileName, addfilename);
            (bool autoScale, double zmin, double zmax) = scaleSetting(d);

            plt.writeBMP24(DataView.DataTypeIndex(d.PlotType), phaseshift, d.ColorMap, file, autoScale, zmin, zmax);
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
            var mag = slider1.Value;
            t_mag.Text = ((int)mag).ToString();
            PreviewGrid.Width = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelWidth);
            PreviewGrid.Height = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelHeight);
            image1.Width = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelWidth);
            image1.Height = (int)Math.Floor(0.5 + (0.01 * mag) * bmp.PixelHeight);
            image1.Source = bmp;
            t_Nx.Text = plt.nx.ToString();
            t_Ny.Text = plt.ny.ToString();
            if (d.PlotType == "Re")
            {
                lb_zmin.Content = "min(Re) = ";
                lb_zmax.Content = "max(Re) = ";
                t_zmin.Text = plt.minRe.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxRe.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Im")
            {
                lb_zmin.Content = "min(Im) = ";
                lb_zmax.Content = "max(Im) = ";
                t_zmin.Text = plt.minIm.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxIm.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Abs")
            {
                lb_zmin.Content = "min(Abs) = ";
                lb_zmax.Content = "max(Abs) = ";
                t_zmin.Text = plt.minAbs.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxAbs.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Pha")
            {
                lb_zmin.Content = "min(Pha) = ";
                lb_zmax.Content = "max(Pha) = ";
                t_zmin.Text = plt.minPha.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxPha.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Pow")
            {
                lb_zmin.Content = "min(Pow) = ";
                lb_zmax.Content = "max(Pow) = ";
                t_zmin.Text = (plt.minAbs * plt.minAbs).ToString("0.0#######e+00");
                t_zmax.Text = (plt.maxAbs * plt.maxAbs).ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Cpx")
            {
                lb_zmin.Content = "min(Abs) = ";
                lb_zmax.Content = "max(Abs) = ";
                t_zmin.Text = plt.minAbs.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxAbs.ToString("0.0#######e+00");
            }
            return file;
        }

        static public void animationPlot(string outputdir, List<BitmapImage> bmp, int Ndiv, DataView d, plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, Action AnimationStop)
        {
            AnimationStop();
            //ビットマップファイルを表示
            for (int i = 0; i < Ndiv; i++)
            {
                double phaseshift = 2.0 * Math.PI * (double)i / (double)Ndiv;
                string addfilename = i.ToString();
                var file = SetOutputFileName(outputdir, d.FileName, addfilename);
                (bool autoScale, double zmin, double zmax) = scaleSetting(d);
                plt.writeBMP24(DataView.DataTypeIndex(d.PlotType), phaseshift, d.ColorMap, file, autoScale, zmin, zmax);
                try
                {
                    bmp.Add(new BitmapImage(new Uri(file, System.UriKind.Absolute)));
                }
                catch (FileNotFoundException)
                {
                    return;
                }
                catch (FileFormatException)
                {
                    MessageBox.Show("画像ファイルの生成に失敗しました", "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            var mag = slider1.Value;
            t_mag.Text = ((int)mag).ToString();
            PreviewGrid.Width = (int)Math.Floor(0.5 + (0.01 * mag) * bmp[0].PixelWidth);
            PreviewGrid.Height = (int)Math.Floor(0.5 + (0.01 * mag) * bmp[0].PixelHeight);
            image1.Width = (int)Math.Floor(0.5 + (0.01 * mag) * bmp[0].PixelWidth);
            image1.Height = (int)Math.Floor(0.5 + (0.01 * mag) * bmp[0].PixelHeight);
            //image1.Source = bmp[0];
            t_Nx.Text = plt.nx.ToString();
            t_Ny.Text = plt.ny.ToString();
            if (d.PlotType == "Re")
            {
                lb_zmin.Content = "min(Re) = ";
                lb_zmax.Content = "max(Re) = ";
                t_zmin.Text = plt.minRe.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxRe.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Im")
            {
                lb_zmin.Content = "min(Im) = ";
                lb_zmax.Content = "max(Im) = ";
                t_zmin.Text = plt.minIm.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxIm.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Abs")
            {
                lb_zmin.Content = "min(Abs) = ";
                lb_zmax.Content = "max(Abs) = ";
                t_zmin.Text = plt.minAbs.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxAbs.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Pha")
            {
                lb_zmin.Content = "min(Pha) = ";
                lb_zmax.Content = "max(Pha) = ";
                t_zmin.Text = plt.minPha.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxPha.ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Pow")
            {
                lb_zmin.Content = "min(Pow) = ";
                lb_zmax.Content = "max(Pow) = ";
                t_zmin.Text = (plt.minAbs * plt.minAbs).ToString("0.0#######e+00");
                t_zmax.Text = (plt.maxAbs * plt.maxAbs).ToString("0.0#######e+00");
            }
            else if (d.PlotType == "Cpx")
            {
                lb_zmin.Content = "min(Abs) = ";
                lb_zmax.Content = "max(Abs) = ";
                t_zmin.Text = plt.minAbs.ToString("0.0#######e+00");
                t_zmax.Text = plt.maxAbs.ToString("0.0#######e+00");
            }
        }

        static public void plot(DataView d, plot2d plt, Slider slider1, TextBox t_mag, Grid PreviewGrid, System.Windows.Controls.Image image1, TextBox t_Nx, TextBox t_Ny, TextBox t_zmin, TextBox t_zmax, Label lb_zmin, Label lb_zmax, CheckBox cb_colmap, string dirwork, Action AnimationStop)
        {
            AnimationStop();
            if (!File.Exists(d.FileFullPath))
            {
                MessageBox.Show("ファイル「" + d.FileFullPath + "」は存在しません", "プロット失敗", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (d.FileFormat == "Binary")
                {
                    plt.FileRead(d.FileFullPath);
                }
                else
                {
                    int ix = Int32.Parse(d.ColX);
                    int iy = Int32.Parse(d.ColY);
                    int ire = Int32.Parse(d.ColRe);
                    int iim = 0;
                    try
                    {
                        iim = Int32.Parse(d.ColIm);
                    }
                    catch
                    {
                        iim = 0;
                    }
                    plt.FileRead(d.FileFullPath, ix, iy, ire, iim);
                }

                var file1 = preview(dirwork, "", 0.0, d, plt, slider1, t_mag, PreviewGrid, image1, t_Nx, t_zmin, t_zmax, t_Ny, lb_zmin, lb_zmax, AnimationStop);
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
                        plt.writeColorBar(DataView.DataTypeIndex(d.PlotType), d.ColorMap, file2.Replace(".bmp", "_colorbar.bmp"), autoScale, zmin, zmax);
                    }
                }
                return;
            }
        }

        public static void OutputSource(DataView d, string bmpfilename, bool autoscale, double zmin, double zmax, bool IsOutputColBar)
        {
            var file = bmpfilename.Replace(".bmp", ".fsx");
            var idxlabel = 0;
            while (System.IO.File.Exists(file))
            {
                file = bmpfilename.Replace(".bmp", "_" + idxlabel.ToString() + ".fsx");
                idxlabel++;
            }
            var wr = new StreamWriter(file);
            wr.WriteLine("//#############################################################################");
            wr.WriteLine("let projectname = \"dataviewer_plot\"");
            wr.WriteLine("let version = \"1.0.0\"");
            wr.WriteLine("//#############################################################################");
            wr.WriteLine("");
            wr.WriteLine("#I @\"" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\"");
            wr.WriteLine("#r \"MyMath.dll\"");
            wr.WriteLine("");
            wr.WriteLine("open MyMath");
            wr.WriteLine("");
            wr.WriteLine("let p = plot2d()");
            wr.WriteLine("let dir = @\"" + d.FileDir + "\"");
            wr.WriteLine("let datafile = \"" + d.FileName + "\"");
            wr.WriteLine("let bmpfile = \"" + System.IO.Path.GetFileName(bmpfilename) + "\"");
            wr.WriteLine("let dataType = " + DataTypeIndex(d.PlotType));
            if (d.FileFormat == "Text")
            {
                wr.WriteLine("let colx = " + d.ColX);
                wr.WriteLine("let coly = " + d.ColY);
                wr.WriteLine("let colre = " + d.ColRe);
                if (d.ColIm == "-")
                {
                    wr.WriteLine("let colim = 0");
                }
                else
                {
                    wr.WriteLine("let colim = " + d.ColIm);
                }
            }
            wr.WriteLine("let phaseShift = 0.0");
            wr.WriteLine("let colorMap = \"" + d.ColorMap + "\"");
            if (autoscale)
            {
                wr.WriteLine("let autoScale = true");
                wr.WriteLine("let min = 0.0");
                wr.WriteLine("let max = 0.0");
            }
            else
            {
                wr.WriteLine("let autoScale = false");
                wr.WriteLine("let min = " + zmin);
                wr.WriteLine("let max = " + zmax);
            }
            if (d.FileFormat == "Text")
            {
                wr.WriteLine("p.FileRead(dir + \"\\\\\" + datafile, colx, coly, colre, colim)");
            }
            else
            {
                wr.WriteLine("p.FileRead(dir + \"\\\\\" + datafile)");
            }
            wr.WriteLine("p.writeBMP24(dataType, phaseShift, colorMap, bmpfile, autoScale, min, max)");
            if (IsOutputColBar)
            {
                wr.WriteLine("p.writeColorBar(dataType, colorMap, bmpfile.Replace(\".bmp\", \"_colorbar.bmp\"), autoScale, min, max)");
            }
            wr.Close();
        }

        public static int DataTypeIndex(string? x)
        {
            if (x == null) return 0;
            if (x == "Im")
            {
                return 1;
            }
            else if (x == "Abs")
            {
                return 2;
            }
            else if (x == "Pha")
            {
                return 3;
            }
            else if (x == "Pow")
            {
                return 4;
            }
            else if (x == "Cpx")
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
    }

}
