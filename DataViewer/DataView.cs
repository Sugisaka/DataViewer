using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.WebRequestMethods;

namespace DataViewer
{
    public enum DataType
    {
        Re,
        Im,
        Abs,
        Pha,
        Pow,
        Cpx
    }
    public enum ColorMap
    {
        Gray,
        RedPower,
        BluePower,
        Horizon,
        Rainbow,
        RainbowCycle,
        RainbowRGB,
        RainbowDark,
        RainbowPower,
        Wave,
        WaveLight,
        WaveDark,
        WaveNight,
        WavePower,
        NightScapeRed,
        NightScapeOrange,
        NightScapeYellow,
        NightScapeGreen,
        NightScapeCyan,
        NightScapeBlue,
        NightScapeViolet,
        LandScapeRed,
        LandScapeBlue,
        ComplexLightRainbowCycle,
        ComplexVividRainbowCycle
    }
    internal class DataView
    {
        /// <summary>
        /// ファイルのディレクトリ
        /// </summary>
        public string FileDir { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// フォーマット
        /// </summary>
        public bool IsBinaryData { get; set; }

        /// <summary>
        /// プロットするデータの種類
        /// 0: 実部
        /// 1: 虚部
        /// 2: 複素数
        /// 3: 絶対値
        /// 4: 位相
        /// 5: 強度
        /// </summary>
        public DataType Type { get; set; }

        /// <summary>
        /// データ列インデックス(x)
        /// </summary>
        public int ColX { get; set; }

        /// <summary>
        /// データ列インデックス(y)
        /// </summary>
        public int ColY { get; set; }

        /// <summary>
        /// データ列インデックス(実部)
        /// </summary>
        public int ColZ { get; set; }

        /// <summary>
        /// データ列インデックス(虚部)
        /// </summary>
        public int ColI { get; set; }

        /// <summary>
        /// カラーマップ
        /// </summary>
        public ColorMap ColMap { get; set; }

        /// <summary>
        /// データ範囲(最大値)
        /// </summary>
        public string ZMax { get; set; }

        /// <summary>
        /// データ範囲(最小値)
        /// </summary>
        public string ZMin { get; set; }

        public DataView(string filename,string filedir)
        {
            FileDir = filedir;
            FileName = filename;
            ZMin = "auto";
            ZMax = "auto";
            Type = DataType.Re;
            if (filename.EndsWith(".bin"))
            {
                IsBinaryData = true;
                ColX = 0;
                ColY = 0;
                ColZ = 0;
                ColI = 0;
            }
            else
            {
                IsBinaryData = false;
                ColX = 1;
                ColY = 2;
                ColZ = 3;
                ColI = 0;
            }
        }
        /// <summary>
        /// 複数のファイル名をリストDVに追加
        /// </summary>
        /// <param name="DV"></param>
        /// <param name="filenames"></param>
        public static void LoadFiles(ObservableCollection<DataView> DV, string[] filenames)
        {
            foreach (var filename in filenames)
            {
                if (System.IO.File.Exists(filename))
                {
                    var dir = Path.GetDirectoryName(filename);
                    if (dir != null)
                    {
                        var name = Path.GetFileName(filename);
                        DataView d = new DataView(name, dir);
                        DV.Add(d);
                    }
                }
            }
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

        public static void OutputSource(DataView d, string bmpfilename, bool autoscale, double zmin, double zmax, bool IsOutputColBar)
        {
            var file = bmpfilename.Replace(".bmp", ".fsx");
            var idxlabel = 0;
            while (System.IO.File.Exists(file))
            {
                file = bmpfilename.Replace(".bmp", "_"+idxlabel.ToString()+".fsx");
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
            wr.WriteLine("let dir = @\""+d.FileDir+"\"");
            wr.WriteLine("let datafile = \""+d.FileName+"\"");
            wr.WriteLine("let bmpfile = \""+System.IO.Path.GetFileName(bmpfilename)+"\"");
            wr.WriteLine("let dataType = " + DataTypeIndex(d.Type));
            if(!d.IsBinaryData)
            {
                wr.WriteLine("let colx = " + d.ColX.ToString());
                wr.WriteLine("let coly = " + d.ColY.ToString());
                wr.WriteLine("let colre = " + d.ColZ.ToString());
                wr.WriteLine("let colim = " + d.ColI.ToString());
            }
            wr.WriteLine("let phaseShift = 0.0");
            wr.WriteLine("let colorMap = \""+d.ColMap.ToString()+"\"");
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
            if (!d.IsBinaryData)
            {
                wr.WriteLine("p.FileRead(dir + \"\\\\\" + datafile, colx, coly, colre, colim)");
            }
            else
            {
                wr.WriteLine("p.FileRead(dir + \"\\\\\" + datafile)");
            }
            wr.WriteLine("p.writeBMP24(dataType, phaseShift, colorMap, bmpfile, autoScale, min, max)");
            if(IsOutputColBar)
            {
                wr.WriteLine("p.writeColorBar(dataType, colorMap, bmpfile.Replace(\".bmp\", \"_colorbar.bmp\"), autoScale, min, max)");
            }
            wr.Close();
        }

        public static int DataTypeIndex(DataType x)
        {
            if (x == DataType.Im)
            {
                return 1;
            }
            else if (x == DataType.Abs)
            {
                return 2;
            }
            else if (x == DataType.Pha)
            {
                return 3;
            }
            else if (x == DataType.Pow)
            {
                return 4;
            }
            else if (x == DataType.Cpx)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ファイル名はそのまま、プロット条件を引数と同じものに設定
        /// </summary>
        /// <param name="D"></param>
        public void SetCondition(DataView D)
        {
            this.IsBinaryData = D.IsBinaryData;
            this.Type = D.Type;
            this.ColX = D.ColX;
            this.ColY = D.ColY;
            this.ColZ = D.ColZ;
            this.ColI = D.ColI;
            this.ColMap = D.ColMap;
            this.ZMax = D.ZMax;
            this.ZMin = D.ZMin;
        }
    }
}
