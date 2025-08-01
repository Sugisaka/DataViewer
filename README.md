# DataViewer
このソフトウェアは、2次元分布データをビットマップファイルにプロットします。フェーザ表示の電磁界分布などの複素数データのプロットに適しています。

## 使い方 

1. [https://github.com/Sugisaka/DataViewer](https://github.com/Sugisaka/DataViewer)にあるRelease「DataViewer_(バージョン番号：数字が最も大きいもの).zip」をダウンロード
2. zipファイルを展開し、DataViewer.exeを起動（初回起動時は作業フォルダ作成の確認メッセージが表示される）
3. データファイルを右下のリストにドラッグ＆ドロップ
	- テキスト形式の場合は、各行にx座標、y座標、実部、虚部がスペースまたはタブで区切られたファイルを読み込み可能。虚部の列は無くても可能
	- バイナリファイルの場合は「bin.」にチェックが入る（バイナリデータのフォーマットは、下記参照）
5. 「Type」にプロットするデータの種類を選択
	- re：実数データをそのままプロット。複素数データの場合は、実部をプロット
	- im：複素数データの虚部をプロット（複素数データのみ指定可能）
	- abs：複素数データの絶対値をプロット（複素数データのみ指定可能）
	- pha：複素数データの偏角をプロット（複素数データのみ指定可能）
	- pow：複素数データの絶対値の2乗をプロット（複素数データのみ指定可能）
	- cpx：複素数データのプロット（複素数データのみ指定可能、カラーマップComplexLight、ComplexVividのみ有効）
6. テキストファイルの場合はデータ列を指定
	- x：x座標が記されたデータ列
	- y：y座標が記されたデータ列
	- re：プロットする値（複素数の場合は実部）が記されたデータ列
	- im：プロットする値の虚部が記されたデータ列（複素数でない場合は0を指定）
7. 「Color map」からカラーマップを選択
8. 結果の確認
	- ファイルリストの\[Preview\]をクリックするとプロット結果が画面に表示される（データファイルが書き換えられた場合はもう一度押すと更新される）
	- ファイルリストの\[Save\]をクリックすると右下のリストで選択したファイルのみプロットされ、データファイルと同じ場所にbmpファイルが出力される。同時に出力されるfsxファイルを実行すると、同じ設定で再プロット可能（要F#実行環境）。
	- ファイルリストの\[Delete\]をクリックすると選択した項目を削除する 
9. プロット詳細設定
	- \[Output color bar\]をチェックした上で\[Save\]をクリックすると、bmpファイルと同時にカラーバーのbmpファイルも出力される 
	- \[Plot all\]をクリックすると、ファイルリストのデータすべてプロットされ、データファイルと同じ場所にbmpファイルが出力される
	- \[Apply to Others\]をクリックすると、選択されたデータ項目のプロット設定が他のすべての項目に反映される
	- \[Delete all\]をクリックすると、リストのデータ項目をすべて削除する
10. 左サイドバーにデータ情報が確認できる（「i」をクリックすると閉じる）
	- Nx,Ny：データのサンプリング点数
	- x,y：マウスポインタの位置
	- re：マウスポインタ位置のデータ値
	- im：マウスポインタ位置のデータ値（虚部）
	- abs：マウスポインタ位置のデータ値（絶対値）
	- pow：マウスポインタ位置のデータ値（絶対値の２乗）
	- pha：マウスポインタ位置のデータ値（偏角）
	- min：データの最小値
	- max：データの最大値
	- プレビュー画像内でマウスをクリックすると、その位置のデータ値（上記のうち、チェックされたもの）がテキストボックスに追加される。
