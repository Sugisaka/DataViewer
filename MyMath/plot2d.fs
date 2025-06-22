namespace MyMath
    
    open System
    open System.IO
    
    module colorMap  =
        let Gradation : (string * (float*int*int*int) list) [] =
            [|
            ("Gray",      [(0.0, 0,0,0); (100.0, 255,255,255)] );
            
            ("RedPower",  [(0.0, 0,0,0); (1.0*100.0/3.0,255,0,0); (2.0*100.0/3.0,255,255,0); (100.0,255,255,255)]);
            
            ("BluePower", [(0.0 ,0,0,0); (1.0*100.0/3.0, 0,0,255); (2.0*100.0/3.0, 0,255,255); (100.0, 255,255,255)]);
            
            ("Horizon",   [(0.0, 0,0,0); (25.0, 0,0,255); (50.0, 255,0,255); (75.0, 255,255,0); (100.0, 255,255,255)]);
            
            ("Rainbow",   [(0.0, 255,0,255); (20.0, 0,0,255); (40.0, 0,255,255); (60.0, 0,255,0); (80.0, 255,255,0); (100.0, 255,0,0)]);
            
            ("RainbowCycle", [(0.0, 255,0,0); (1.0*100.0/6.0, 255,0,255); (2.0*100.0/6.0, 0,0,255); (3.0*100.0/6.0, 0,255,255); (4.0*100.0/6.0, 0,255,0); (5.0*100.0/6.0, 255,255,0); (100.0, 255,0,0)]);
            
            ("RainbowRGB",   [(0.0, 0,0,255); (25.0, 0,255,255); (50.0, 0,255,0); (75.0, 255,255,0); (100.0, 255,0,0)]);
            
            ("RainbowDark", [(0.0, 0,0,0); (1.0 * 100.0 / 7.0, 255,0,255); (2.0 * 100.0 / 7.0, 0,0,255); (3.0 * 100.0 / 7.0, 0,255,255); (4.0 * 100.0 / 7.0, 0,255,0); (5.0 * 100.0 / 7.0, 255,255,0); (6.0 * 100.0 / 7.0, 255,0,0); (100.0, 0,0,0)]);
            
            ("RainbowPower",[(0.0, 0,0,0); (1.0 * 100.0 / 7.0, 255,0,255); (2.0 * 100.0 / 7.0, 0,0,255); (3.0 * 100.0 / 7.0, 0,255,255); (4.0 * 100.0 / 7.0, 0,255,0); (5.0 * 100.0 / 7.0, 255,255,0); (6.0 * 100.0 / 7.0, 255,0,0); (100.0, 255,255,255)]);
            
            ("Wave", [(0.0, 0,  0,255); (50.0,  255,255,255); (100.0, 255,  0,  0)]);
            
            ("WaveLight",[(0.0, 0,255,255);(25.0, 0,0,255);(50.0, 255,255,255);(75.0, 255,0,0);(100.0, 255,0,255)]);
            
            ("WaveDark", [(0.0, 0,0,0); (25.0, 0,0,255); (50.0, 255,255,255); (75.0, 255,0,0); (100.0, 0,0,0)]);
            
            ("WaveNight", [(0.0, 0,0,255); (50.0, 0,0,0); (100.0, 255,0,0)] );
            
            ("WavePower", [(0.0, 255,255,255); (1.0*100.0/6.0, 0,255,255); (2.0*100.0/6.0, 0,0,255); (3.0*100.0/6.0, 0,0,0); (4.0*100.0/6.0, 255,0,0); (5.0*100.0/6.0, 255,255,0); (100.0, 255,255,255)]);
            
            ("NightScapeRed", [(0.0, 0,0,0); (50.0, 255,0,0); (100.0, 255,255,255)]);
            
            ("NightScapeOrange", [(0.0, 0,0,0); (50.0, 255,148,0); (100.0, 255,255,255)]);
            
            ("NightScapeYellow", [(0.0, 0,0,0); (50.0, 255,255,0); (100.0, 255,255,255)]);
            
            ("NightScapeGreen", [(0.0, 0,0,0); (50.0, 0,255,0); (100.0, 255,255,255)]);
            
            ("NightScapeCyan", [(0.0, 0,0,0); (50.0, 0,255,255); (100.0, 255,255,255)]);
            
            ("NightScapeBlue", [(0.0, 0,0,0); (50.0, 0,0,255); (100.0, 255,255,255)]);
            
            ("NightScapeViolet", [(0.0, 0,0,0); (50.0, 186,0,255); (100.0, 255,255,255)]);
            
            ("LandScapeRed", [(0.0, 255,255,255); (50.0, 255,0,255); (100.0, 255,0,0)]);
            
            ("LandScapeBlue", [(0.0, 255,255,255); (50.0, 0,255,255); (100.0, 0,0,255)]);
            
            ("ComplexLightRainbowCycle", [(0.0, 255,0,0); (1.0*100.0/6.0, 255,0,255); (2.0*100.0/6.0, 0,0,255); (3.0*100.0/6.0, 0,255,255); (4.0*100.0/6.0, 0,255,0); (5.0*100.0/6.0, 255,255,0); (100.0, 255,0,0)]);
            
            ("ComplexVividRainbowCycle", [(0.0, 255,0,0); (1.0*100.0/6.0, 255,0,255); (2.0*100.0/6.0, 0,0,255); (3.0*100.0/6.0, 0,255,255); (4.0*100.0/6.0, 0,255,0); (5.0*100.0/6.0, 255,255,0); (100.0, 255,0,0)]); |]
            
        /// <summary>
        /// 指定したカラーマップの位置に対する色情報を計算
        /// </summary>
        /// <param name="tp">0:実部、1:虚部、2:絶対値、3:位相、4:強度、5:複素数</param>
        /// <param name="re">実部</param>
        /// <param name="im">虚部</param>
        /// <param name="min0">位置pの最小値</param>
        /// <param name="max0">位置pの最大値</param>
        /// <param name="idx">カラーマップデータGradationの配列インデックス</param>
        let RGBColor(tp:int,re:double,im:double,min0:double,max0:double,idx:int) =
            let mutable RGB : int[] = Array.zeroCreate 3
            let (gname,list) = Gradation[idx]
            let amp = sqrt(re*re+im*im)
            let pha = atan2 im re
            let (min,max) =
                //波動系カラーマップの場合は最小値と最大値の絶対値が等しくなるようにする
                if gname.StartsWith("Wave") then
                    if Math.Abs(max0) > Math.Abs(min0) then
                        (-Math.Abs(max0),Math.Abs(max0))
                    else
                        (-Math.Abs(min0),Math.Abs(min0))
                //位相を色相環に対応させるカラーマップの場合は範囲を-π～πにする
                elif gname = "RainbowCycle" then
                    -Math.PI,Math.PI
                else
                    (min0,max0)
            /// 色に変換する値
            let p0 =
                match tp,gname with
                |1,_ -> im
                |2,_ -> amp
                |3,_ -> pha
                |4,_ -> amp*amp
                |5,("ComplexLightRainbowCycle"|"ComplexVividRainbowCycle") -> pha
                |5,_ -> re*re+im*im
                |_,_ -> re
            /// 色に変換する値を0～100に正規化
            let t = 
                match tp,gname with
                |3,_ |5,("ComplexLightRainbowCycle"|"ComplexVividRainbowCycle") ->
                    match p0 with
                    | _ when p0 <= -1.0*Math.PI ->
                        0.0
                    | _ when p0 >= Math.PI ->
                        100.0
                    | _ ->
                        100.0 * (p0 + Math.PI) / (2.0*Math.PI)
                |_ ->
                    match p0 with
                    | _ when p0 <= min ->
                        0.0
                    | _ when p0 >= max ->
                        100.0
                    | _ ->
                        100.0 * (p0 - min) / (max - min)
            let rec search list =
                match list with
                | (p1,r1,g1,b1)::(p2,r2,g2,b2)::_ when (p1<=t && t<=p2) -> 
                    let s = (t - p1) / (p2 - p1)
                    ((byte ((double r1) + s * (double (r2 - r1)))),
                    (byte ((double g1) + s * (double (g2 - g1)))),
                    (byte ((double b1) + s * (double (b2 - b1)))))
                | _::(p2,r2,g2,b2)::list1 -> 
                    search ((p2,r2,g2,b2)::list1)
                | _ ->
                    (0x00uy,0x00uy,0x00uy)
            let (r,g,b) = search list
            //複素数プロットは絶対値に応じて明度を設定
            if gname.StartsWith("ComplexVivid") then
                if amp > max then
                    (r,g,b)
                elif amp<min then
                    (0x00uy,0x00uy,0x00uy)
                else
                    ((byte ((double r) * ((amp - min) / (max - min)))),
                    (byte ((double g) * ((amp - min) / (max - min)))),
                    (byte ((double b) * ((amp - min) / (max - min)))))
            elif gname.StartsWith("ComplexLight") then
                if amp > max then
                    (0xFFuy,0xFFuy,0xFFuy)
                elif amp < 0.5 * (max - min) then
                    ((byte (((amp - min) / (0.5 * (max - min) - min)) * (double r))),
                    (byte (((amp - min) / (0.5 * (max - min) - min)) * (double g))),
                    (byte (((amp - min) / (0.5 * (max - min) - min)) * (double b))))
                elif amp >= 0.5 * (max - min) then
                    ((byte ((double r) + ((amp - 0.5 * (max - min)) / (max - 0.5 * (max - min))) * (255.0 - (double r)))),
                    (byte ((double g) + ((amp - 0.5 * (max - min)) / (max - 0.5 * (max - min))) * (255.0 - (double g)))),
                    (byte ((double b) + ((amp - 0.5 * (max - min)) / (max - 0.5 * (max - min))) * (255.0 - (double b)))))
                else
                    (0x00uy,0x00uy,0x00uy)
            else
                (r,g,b)
                
    type plot2d =
        
        val mutable public data : array<double>
        val mutable isCPXdata : bool
        val mutable isDataLoaded : bool
        val mutable nx : int
        val mutable ny : int
        val mutable minRe : double
        val mutable maxRe : double
        val mutable minIm : double
        val mutable maxIm : double
        val mutable minAbs : double
        val mutable maxAbs : double
        val mutable minPha : double
        val mutable maxPha : double
        val mutable error : string
        
        new() = {
            data = [||]
            isCPXdata = false
            isDataLoaded = false
            nx = 0
            ny = 0
            minRe=0.0
            maxRe=0.0
            minIm=0.0
            maxIm=0.0
            minAbs=0.0
            maxAbs=0.0
            minPha=0.0
            maxPha=0.0
            error = ""
        }
        
        /// データのx方向サンプリング点数
        member this.Nx with get() = this.nx
        
        /// データのy方向サンプリング点数
        member this.Ny with get() = this.ny
        
        /// 指定したインデックスの値(実部)を取得
        member public this.Re(i:int,j:int) =
            if this.isDataLoaded then
                if this.isCPXdata then
                    if (0 <= 2*(this.nx*j+i) && 2*(this.nx*j+i) <= this.data.GetUpperBound(0)) then
                        this.data[2*(this.nx*j+i)]
                    else
                        0.0
                else
                    if (0 <= this.nx*j+i && this.nx*j+i <= this.data.GetUpperBound(0)) then
                        this.data[this.nx*j+i]
                    else
                        0.0
            else
                0.0
                
        /// 指定したインデックスの値(虚部)を取得
        member public this.Im(i:int,j:int) =
            if this.isDataLoaded then
                if this.isCPXdata then
                    if (0 <= 2*(this.nx*j+i)+1 && 2*(this.nx*j+i)+1 <= this.data.GetUpperBound(0)) then
                        this.data[2*(this.nx*j+i)+1]
                    else
                        0.0
                else
                    0.0
            else
                0.0
                
        /// <summary>
        /// データ内の最小値と最大値を計算
        /// </summary>
        /// <param name="f">複素数→実数の関数</param>
        member private this.MinMax(f:(double*double)->double) =
            let zre = this.data[0]
            let zim = this.data[1]
            let mutable min = f(zre,zim)
            let mutable max = f(zre,zim)
            if this.isCPXdata then
                for i = 0 to this.nx-1 do
                    for j = 0 to this.ny-1 do
                        let zre = this.data[2*(this.nx*j+i)]
                        let zim = this.data[2*(this.nx*j+i)+1]
                        if max < f(zre,zim) then max <- f(zre,zim)
                        if min > f(zre,zim) then min <- f(zre,zim)
            else
                for i = 0 to this.nx-1 do
                    for j = 0 to this.ny-1 do
                        let zre = this.data[this.nx*j+i]
                        let zim = 0.0
                        if max < f(zre,zim) then max <- f(zre,zim)
                        if min > f(zre,zim) then min <- f(zre,zim)
            (min,max)
            
        /// <summary>
        /// ファイルからデータ読み込み(テキストデータ)
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="ix">x座標のデータ列</param>
        /// <param name="iy">y座標のデータ列</param>
        /// <param name="izre">実部のデータ列</param>
        /// <param name="izim">虚部のデータ列</param>
        member public this.FileRead(filename:string,ix:int,iy:int,izre:int,izim:int) =
            this.isDataLoaded <- false
            this.error <- "";
            // izim=-1の場合は実数データ
            this.isCPXdata <- not (izim = 0)
            // 区切り文字の判定
            let sep =
                let reader:StreamReader = new StreamReader(filename)
                let rec read n =
                    let tmp:string=reader.ReadLine()
                    match tmp with
                    | _ when (tmp.Contains("#")) -> 
                        read (n+1)
                    | _ when (tmp.Contains("\t")) -> 
                        reader.Close()
                        [| '\t' |]
                    | _ when (tmp.Contains(","))    -> 
                        reader.Close()
                        [| ',' |]
                    | _ when (tmp.Contains(" "))    -> 
                        reader.Close()
                        [| ' ' |]
                    | _ -> 
                        reader.Close()
                        [| ' ' |]
                read 0
                
            // データサイズの決定
            let (_, _, dx, dy, xmin, xmax, ymin, ymax) = 
                let reader = new StreamReader(filename)
                // 1行読み込んで離散間隔dx,dy、データ範囲xmin,xmax,ymin,ymaxを更新
                let rec read n x0 y0 dx dy xmin xmax ymin ymax =
                    let tmp=reader.ReadLine()
                    // ファイル末尾
                    if tmp=null then
                        reader.Close()
                        (x0, y0, dx, dy, xmin, xmax, ymin, ymax)
                    // コメント行の場合：そのまま次の行を読み込む
                    elif tmp.Contains("#") then
                        read n x0 y0 dx dy xmin xmax ymin ymax
                    // ファイル末尾でなく、コメント行でもない場合
                    else
                        // データのi列目を抽出
                        let extract(i:int) =
                            try
                                Double.Parse(tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[i - 1])
                            with
                            | :? System.FormatException ->
                                this.error <- (if this.error = "" then "" else this.error+"\r\n") + "データ" + (i.ToString()) + "列目「" + tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[i - 1] + "」を数値に変換できません"
                                0.0
                            | :? System.IndexOutOfRangeException ->
                                this.error <- (if this.error = "" then "" else this.error+"\r\n") + "データ" + (i.ToString()) + "列目は存在しません"
                                0.0
                        let x = extract(ix)
                        let y = extract(iy)
                        // エラー検出時：ファイル読み込み中断
                        if this.error<>"" then
                            reader.Close()
                            (x0, y0, -1.0, -1.0, xmin, xmax, ymin, ymax)
                        // 最初のデータ読み込み。各値に初期値を設定
                        elif n=0 then
                            read (n+1) x y 0.0 0.0 x x y y
                        // 2回目のデータ読み込み。離散間隔に初期値を設定
                        elif n=1 then
                            read (n+1) x0 y0 (abs(x-x0)) (abs(y-y0)) x x y y
                        // 3回目以降のデータ読み込み。データ範囲と離散間隔を更新
                        else
                            let xmin1 = if xmin > x then x else xmin
                            let xmax1 = if xmax < x then x else xmax
                            let ymin1 = if ymin > y then y else ymin
                            let ymax1 = if ymax < y then y else ymax
                            let dx1 = if ( dx=0.0 || (dx > abs(x-x0) && abs(x-x0)<>0.0)) then abs(x-x0) else dx
                            let dy1 = if ( dy=0.0 || (dy > abs(y-y0) && abs(y-y0)<>0.0)) then abs(y-y0) else dy
                            read (n+1) x0 y0 dx1 dy1 xmin1 xmax1 ymin1 ymax1
                            
                // read関数実行→sizeの返却値 *)
                read 0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0
                
            if dx = 0.0 then this.error <- (if this.error = "" then "" else this.error+"\r\n") + "xの値が一定です。1次元データの可能性があります。"
            if dy = 0.0 then this.error <- (if this.error = "" then "" else this.error+"\r\n") + "yの値が一定です。1次元データの可能性があります。"
            
            // 離散間隔とデータ範囲から離散点数を決定
            this.nx <- int (Math.Floor((xmax - xmin) / dx + 0.5) + 1.0)
            this.ny <- int (Math.Floor((ymax - ymin) / dy + 0.5) + 1.0)
            
            if this.error="" then
                // 配列初期化
                try
                    // 複素数データ
                    if this.isCPXdata then
                        this.data <- Array.zeroCreate(2*this.nx*this.ny)
                    // 実数データ
                    else
                        this.data <- Array.zeroCreate(this.nx*this.ny)
                with
                | :? System.OutOfMemoryException ->
                    this.error <- (if this.error = "" then "" else this.error+"\r\n") + "配列サイズ("+this.nx.ToString()+","+this.ny.ToString()+")を確保できません"
                // ここまでエラーなしの場合：データ読み込み
                if this.error = "" then
                    let reader = new StreamReader(filename)
                    let rec readD n =
                        match reader.ReadLine() with
                        |null ->
                            this.isDataLoaded <- true
                            let (minr,maxr) = this.MinMax(fun (re,im) -> re)
                            this.minRe <- minr
                            this.maxRe <- maxr
                            let (mini,maxi) = this.MinMax(fun (re,im) -> im)
                            this.minIm <- mini
                            this.maxIm <- maxi
                            let (mina,maxa) = this.MinMax(fun (re,im) -> sqrt(re*re+im*im))
                            this.minAbs <- mina
                            this.maxAbs <- maxa
                            let (minp,maxp) = this.MinMax(fun (re,im) -> atan2 im re)
                            this.minPha <- minp
                            this.maxPha <- maxp
                            reader.Close()
                        |tmp when tmp.Contains("#") ->
                            // コメント行なら次の行の読み込みに進む
                            readD (n+1)
                        |tmp ->
                            // ファイル末尾でなくコメント文でない場合
                            // x座標
                            let x = Double.Parse(tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[ix - 1])
                            // y座標
                            let y = Double.Parse(tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[iy - 1])
                            // x座標の配列インデックス
                            let ix = int (Math.Floor((x - xmin) / dx + 0.5))
                            // y座標の配列インデックス
                            let iy = int (Math.Floor((y - ymin) / dy + 0.5))
                            let k = tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries)
                            try
                                let re = Double.Parse(k.[izre - 1])
                                this.data[this.nx*iy+ix] <- re
                            with
                            | :? System.FormatException ->
                                this.error <- (if this.error = "" then "" else this.error+"\r\n") + ("データ" + (izre.ToString()) + "列目「" + k.[izre - 1] + "」を数値に変換できません")
                                reader.Close()
                            | :? System.IndexOutOfRangeException ->
                                this.error <- (if this.error = "" then "" else this.error+"\r\n") + ("データ" + (izre.ToString()) + "列目は存在しません")
                                reader.Close()
                            if this.error = "" then
                                readD (n+1)
                            else
                                ()
                    let rec readZ n =
                        match reader.ReadLine() with
                        // ファイル末尾の場合
                        |null ->
                            this.isDataLoaded <- true
                            let (minr,maxr) = this.MinMax(fun (re,im) -> re)
                            this.minRe <- minr
                            this.maxRe <- maxr
                            let (mini,maxi) = this.MinMax(fun (re,im) -> im)
                            this.minIm <- mini
                            this.maxIm <- maxi
                            let (mina,maxa) = this.MinMax(fun (re,im) -> sqrt(re*re+im*im))
                            this.minAbs <- mina
                            this.maxAbs <- maxa
                            let (minp,maxp) = this.MinMax(fun (re,im) -> atan2 im re)
                            this.minPha <- minp
                            this.maxPha <- maxp
                            reader.Close()
                        // コメント行なら次の行の読み込みに進む
                        |tmp when tmp.Contains("#") ->
                            readZ (n+1)
                        // ファイル末尾でなくコメント文でない場合
                        |tmp ->
                            // x座標
                            let x = Double.Parse(tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[ix - 1])
                            // y座標
                            let y = Double.Parse(tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[iy - 1])
                            // x座標の配列インデックス
                            let ix = int (Math.Floor((x - xmin) / dx + 0.5))
                            // y座標の配列インデックス
                            let iy = int (Math.Floor((y - ymin) / dy + 0.5))
                            let k = tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries)
                            try
                                let re = Double.Parse(k.[izre - 1])
                                this.data[2*(this.nx*iy+ix)] <- re
                            with
                            | :? System.FormatException ->
                                let k = tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries)
                                this.error <- (if this.error = "" then "" else this.error+"\r\n") + ("データ" + (izre.ToString()) + "列目「" + k.[izre - 1] + "」を数値に変換できません")
                                reader.Close()
                            | :? System.IndexOutOfRangeException ->
                                this.error <- (if this.error = "" then "" else this.error+"\r\n") + ("データ" + (izre.ToString()) + "列目は存在しません")
                                reader.Close()
                            if this.error = "" then
                                let h = tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries)
                                try
                                    let im = Double.Parse(h.[izim - 1])
                                    this.data[2*(this.nx*iy+ix)+1] <- im
                                with
                                | :? System.FormatException ->
                                    this.error <- (if this.error = "" then "" else this.error+"\r\n") + ("データ" + (izim.ToString()) + "列目「" + tmp.Split(sep, StringSplitOptions.RemoveEmptyEntries).[izim - 1] + "」を数値に変換できません")
                                    reader.Close()
                            if this.error = "" then
                                readZ (n+1)
                            else
                                ()
                    if this.isCPXdata then
                        readZ 0
                    else
                        readD 0
                        
        /// <summary>
        /// ファイルからデータ読み込み(バイナリデータ)
        /// </summary>
        member public this.FileRead(filename:string) =
            let fs = new FileStream(filename, FileMode.Open, FileAccess.Read)
            let rd = new BinaryReader(fs)
            let format = rd.ReadInt32()
            if format = 1 then
                let ntype = rd.ReadInt32()
                let dim = rd.ReadInt32()
                if dim > 0 then
                    let size1 = rd.ReadInt32()
                    this.nx <- size1
                if dim > 1 then
                    let size2 = rd.ReadInt32()
                    this.ny <- size2
                if dim > 2 then
                    ignore <| rd.ReadInt32()
                if ntype = 3000 then
                    this.data <- Array.zeroCreate(2*this.nx*this.ny)
                else
                    this.data <- Array.zeroCreate(this.nx*this.ny)
                match ntype with
                |1004 ->
                    this.isCPXdata <- false
                    for i = 0 to this.data.GetUpperBound(0) do
                        this.data[i] <- double (rd.ReadInt32())
                    this.isDataLoaded <- true
                    let (minr,maxr) = this.MinMax(fun (re,im) -> re)
                    this.minRe <- minr
                    this.maxRe <- maxr
                    let (mini,maxi) = this.MinMax(fun (re,im) -> im)
                    this.minIm <- mini
                    this.maxIm <- maxi
                    let (mina,maxa) = this.MinMax(fun (re,im) -> sqrt(re*re+im*im))
                    this.minAbs <- mina
                    this.maxAbs <- maxa
                    let (minp,maxp) = this.MinMax(fun (re,im) -> atan2 im re)
                    this.minPha <- minp
                    this.maxPha <- maxp
                |2000|3000 ->
                    this.isCPXdata <- true
                    for i = 0 to this.data.GetUpperBound(0) do
                        this.data[i] <- rd.ReadDouble()
                    this.isDataLoaded <- true
                    let (minr,maxr) = this.MinMax(fun (re,im) -> re)
                    this.minRe <- minr
                    this.maxRe <- maxr
                    let (mini,maxi) = this.MinMax(fun (re,im) -> im)
                    this.minIm <- mini
                    this.maxIm <- maxi
                    let (mina,maxa) = this.MinMax(fun (re,im) -> sqrt(re*re+im*im))
                    this.minAbs <- mina
                    this.maxAbs <- maxa
                    let (minp,maxp) = this.MinMax(fun (re,im) -> atan2 im re)
                    this.minPha <- minp
                    this.maxPha <- maxp
                |_ ->
                    ()
            else
                this.error <- (if this.error = "" then "" else this.error+"\r\n") + "unknown file format"
            rd.Close()
            
        /// <summary>
        /// 24ビットビットマップファイルを作成
        /// </summary>
        /// <param name="tp">0:実部、1:虚部、2:絶対値、3:位相、4:強度、5:複素数</param>
        /// <param name="phaseshift"></param>
        /// <param name="gradation"></param>
        /// <param name="filename"></param>
        /// <param name="autoscale"></param>
        /// <param name="z_min"></param>
        /// <param name="z_max"></param>
        member public this.writeBMP24(tp:int, phaseshift:double, gradation:string, filename:string, autoscale:bool, z_min:double, z_max:double ) =
            if not this.isDataLoaded then
                if this.error = "" then
                    this.error <- "data is not loaded"
            else
                let nx = this.nx
                let ny = this.ny
                let zabs(re,im) = sqrt(re*re+im*im)
                let zpha(re,im) = atan2 im re
                let pshift(re,im,ps) =
                    let a = zabs(re,im)
                    let p = zpha(re,im)+ps
                    a*cos(p),a*sin(p)
                let rest = if (3*nx)%4=0 then 0 else 4-(3*nx)%4
                //---データの規格化------------------------------------------------------
                let (min,max) =
                    match autoscale,this.isCPXdata,tp with
                    |false,_,_ ->
                        z_min,z_max
                    |true,_,3 ->
                        this.minPha,this.maxPha
                    |true,false,0 ->
                        this.minRe,this.maxRe
                    |true,false,2 ->
                        this.minAbs,this.maxAbs
                    |true,false,4 ->
                        this.minAbs**2.0,this.maxAbs**2.0
                    |true,true,2 ->
                        this.minAbs,this.maxAbs
                    |true,true,5 ->
                        this.minAbs,this.maxAbs
                    |true,true,0 ->
                        this.minRe,this.maxRe
                    |true,true,1 ->
                        this.minIm,this.maxIm
                    |true,true,4 ->
                        this.minAbs**2.0,this.maxAbs**2.0
                    |_ ->
                        0.0,0.0
                        
                //---ビットマップファイル生成---------------------------------------------
                let f_strm:FileStream = new FileStream(filename, FileMode.Create)
                let bw:BinaryWriter = new BinaryWriter(f_strm)
                //---BMPFILEHEADER構造体--------------------------------------------------
                let bfSize:int32 = 54 + (3 * nx + rest) * ny //ファイル全体のバイト数
                let bfReserved1:int16 = 0s          //常に0
                let bfReserved2:int16 = 0s          //常に0
                let bfOffBits:int32 = 54            //ファイルの最初から画像データまでのデータサイズ
                //---BITMAPINFOHEADER-----------------------------------------------------
                let biSize:int32 = 40               //情報ヘッダサイズ(40byte)
                let biWidth:int32 = nx              //画像の幅  [pixel]
                let biHeight:int32 = ny             //画像の高さ[pixel]
                let biPlanes:int16 = 1s             //常に1
                let biBitCount:int16 = 24s          //色ビット数[bit]
                let biCompression:int32 = 0         //圧縮形式
                let biSizeimage:int32 = 0           //ビットマップデータのサイズ(0でもよい)
                let biXPelsPerMeter:int32 = 0       //水平解像度(0でもよい)
                let biYPelsPerMeter:int32 = 0       //垂直解像度(0でもよい)
                let biClrUsed:int32 = 0             //ビットマップが実際に使用するカラーテーブルのエントリ数
                let biClrImportant:int32 = 0        //ビットマップの表示に必要な色数
                //---バイナリデータ書き込み-----------------------------------------------
                bw.Write('B') //BM
                bw.Write('M') //BM
                bw.Write(bfSize)        
                bw.Write(bfReserved1) 
                bw.Write(bfReserved2)
                bw.Write(bfOffBits)
                bw.Write(biSize)
                bw.Write(biWidth)
                bw.Write(biHeight)
                bw.Write(biPlanes)
                bw.Write(biBitCount)
                bw.Write(biCompression)
                bw.Write(biSizeimage)
                bw.Write(biXPelsPerMeter)
                bw.Write(biYPelsPerMeter)
                bw.Write(biClrUsed)
                bw.Write(biClrImportant)
                //---画像データ本体--------------------------------------------------------
                
                //指定されたグラデーションを探す
                let gradationindex:int = 
                    let det g =
                        let (name,list)=g
                        name=gradation
                    match Array.tryFindIndex det colorMap.Gradation with
                    |None -> 0
                    |Some i -> i
                    
                match this.isCPXdata,phaseshift with
                |true,0.0 ->
                    for  j = 0 to ny-1 do
                        for i = 0 to nx-1 do
                            let zre = this.data[2*(nx*j+i)]
                            let zim = this.data[2*(nx*j+i)+1]
                            let (r,g,b) = colorMap.RGBColor(tp, zre, zim, min, max, gradationindex)
                            bw.Write(b)    //青
                            bw.Write(g)    //緑
                            bw.Write(r)    //赤
                        for _ = 0 to rest-1 do
                            bw.Write(byte 0)
                |true,_ ->
                    for  j = 0 to ny-1 do
                        for i = 0 to nx-1 do
                            let zre = this.data[2*(nx*j+i)]
                            let zim = this.data[2*(nx*j+i)+1]
                            let (pre,pim) = pshift(zre,zim,phaseshift)
                            let (r,g,b) = colorMap.RGBColor(tp, pre, pim, min, max, gradationindex)
                            bw.Write(b)    //青
                            bw.Write(g)    //緑
                            bw.Write(r)    //赤
                        for _ = 0 to rest-1 do
                            bw.Write(byte 0)
                |false,_ ->
                    for  j = 0 to ny-1 do
                        for i = 0 to nx-1 do
                            let z = this.data[nx*j+i]
                            let (r,g,b) = colorMap.RGBColor(tp, z, 0.0, min, max, gradationindex)
                            bw.Write(b)    //青
                            bw.Write(g)    //緑
                            bw.Write(r)    //赤
                        for _ = 0 to rest-1 do
                            bw.Write(byte 0)
                bw.Close()
                
        /// <summary>
        /// カラーバー出力
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="phaseshift"></param>
        /// <param name="gradation"></param>
        /// <param name="filename"></param>
        /// <param name="autoscale"></param>
        /// <param name="z_min"></param>
        /// <param name="z_max"></param>
        member public this.writeColorBar(tp:int, gradation:string, filename:string, autoscale:bool, z_min:double, z_max:double ) =
            if not this.isDataLoaded then
                if this.error = "" then
                    this.error <- "data is not loaded"
            else
                let (nx,ny) = 
                    if gradation = "ComplexLightRainbowCycle" || gradation = "ComplexVividRainbowCycle" then
                        256,256
                    else
                        24,256
                let zabs(re,im) = sqrt(re*re+im*im)
                let zpha(re,im) = atan2 im re
                let pshift(re,im,ps) =
                    let a = zabs(re,im)
                    let p = zpha(re,im)+ps
                    a*cos(p),a*sin(p)
                let rest = if (3*nx)%4=0 then 0 else 4-(3*nx)%4
                //---データの規格化------------------------------------------------------
                let (min,max,rangemin,rangemax) =
                    match autoscale,this.isCPXdata,tp with
                    |false,_,0 ->
                        z_min,z_max,
                        this.minRe,this.maxRe
                    |false,_,1 ->
                        z_min,z_max,
                        this.minIm,this.maxIm
                    |false,_,2 ->
                        z_min,z_max,
                        this.minAbs,this.maxAbs
                    |false,_,3 ->
                        z_min,z_max,
                        this.minPha,this.maxPha
                    |false,_,4 ->
                        z_min,z_max,
                        this.minAbs**2.0,this.maxAbs**2.0
                    |false,_,5 ->
                        z_min,z_max,
                        this.minAbs,this.maxAbs
                    |true,_,3 ->
                        this.minPha,this.maxPha,
                        this.minPha,this.maxPha
                    |true,false,0 ->
                        this.minRe,this.maxRe,
                        this.minRe,this.maxRe
                    |true,false,2 ->
                        this.minAbs,this.maxAbs,
                        this.minAbs,this.maxAbs
                    |true,false,4 ->
                        this.minAbs**2.0,this.maxAbs**2.0,
                        this.minAbs,this.maxAbs
                    |true,true,2 ->
                        this.minAbs,this.maxAbs,
                        this.minAbs,this.maxAbs
                    |true,true,5 ->
                        this.minAbs,this.maxAbs,
                        this.minAbs,this.maxAbs
                    |true,true,0 ->
                        this.minRe,this.maxRe,
                        this.minRe,this.maxRe
                    |true,true,1 ->
                        this.minIm,this.maxIm,
                        this.minIm,this.maxIm
                    |true,true,4 ->
                        this.minAbs**2.0,this.maxAbs**2.0,
                        this.minAbs,this.maxAbs
                    |_ ->
                        0.0,0.0,
                        0.0,0.0
                        
                //---ビットマップファイル生成---------------------------------------------
                let f_strm:FileStream = new FileStream(filename, FileMode.Create)
                let bw:BinaryWriter = new BinaryWriter(f_strm)
                //---BMPFILEHEADER構造体--------------------------------------------------
                let bfSize:int32 = 54 + (3 * nx + rest) * ny //ファイル全体のバイト数
                let bfReserved1:int16 = 0s          //常に0
                let bfReserved2:int16 = 0s          //常に0
                let bfOffBits:int32 = 54            //ファイルの最初から画像データまでのデータサイズ
                //---BITMAPINFOHEADER-----------------------------------------------------
                let biSize:int32 = 40               //情報ヘッダサイズ(40byte)
                let biWidth:int32 = nx              //画像の幅  [pixel]
                let biHeight:int32 = ny             //画像の高さ[pixel]
                let biPlanes:int16 = 1s             //常に1
                let biBitCount:int16 = 24s          //色ビット数[bit]
                let biCompression:int32 = 0         //圧縮形式
                let biSizeimage:int32 = 0           //ビットマップデータのサイズ(0でもよい)
                let biXPelsPerMeter:int32 = 0       //水平解像度(0でもよい)
                let biYPelsPerMeter:int32 = 0       //垂直解像度(0でもよい)
                let biClrUsed:int32 = 0             //ビットマップが実際に使用するカラーテーブルのエントリ数
                let biClrImportant:int32 = 0        //ビットマップの表示に必要な色数
                //---バイナリデータ書き込み-----------------------------------------------
                bw.Write('B') //BM
                bw.Write('M') //BM
                bw.Write(bfSize)        
                bw.Write(bfReserved1) 
                bw.Write(bfReserved2)
                bw.Write(bfOffBits)
                bw.Write(biSize)
                bw.Write(biWidth)
                bw.Write(biHeight)
                bw.Write(biPlanes)
                bw.Write(biBitCount)
                bw.Write(biCompression)
                bw.Write(biSizeimage)
                bw.Write(biXPelsPerMeter)
                bw.Write(biYPelsPerMeter)
                bw.Write(biClrUsed)
                bw.Write(biClrImportant)
                //---画像データ本体--------------------------------------------------------
                
                //指定されたグラデーションを探す
                let gradationindex:int = 
                    let det g =
                        let (name,list)=g
                        name=gradation
                    match Array.tryFindIndex det colorMap.Gradation with
                    |None -> 0
                    |Some i -> i
                    
                match gradation with
                |"ComplexLightRainbowCycle" | "ComplexVividRainbowCycle" ->
                    for  j = 0 to ny-1 do
                        for i = 0 to nx-1 do
                            let a = rangemin + (rangemax-rangemin)*(double j)/double (ny-1)
                            let p = -Math.PI + 2.0*Math.PI*(double i)/double (nx-1)
                            let zre = a*cos(p)
                            let zim = a*sin(p)
                            let (r,g,b) = colorMap.RGBColor(tp, zre, zim, min, max, gradationindex)
                            bw.Write(b)    //青
                            bw.Write(g)    //緑
                            bw.Write(r)    //赤
                        for _ = 0 to rest-1 do
                            bw.Write(byte 0)
                |_ ->
                    for  j = 0 to ny-1 do
                        for i = 0 to nx-1 do
                            let z = rangemin + (rangemax-rangemin)*(double j)/double (ny-1)
                            let (r,g,b) = colorMap.RGBColor(tp, z, 0.0, min, max, gradationindex)
                            bw.Write(b)    //青
                            bw.Write(g)    //緑
                            bw.Write(r)    //赤
                        for _ = 0 to rest-1 do
                            bw.Write(byte 0)
                bw.Close()