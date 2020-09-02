# Crossing問題に挑戦してみた

結城浩先生が出題されたクロッシング問題に挑戦しました。

- [CodeIQの問題に挑戦しよう！](http://www.hyuki.com/codeiq/#c12)
- [問題ページ（※リンク切れ）](https://codeiq.jp/ace/yuki_hiroshi/q432)

> 問題発表当時（2013年頃）に既に実装済みでしたが、.NETのバージョンアップや細かい記法の見直しなどを2020年に行いました。

## 実装言語、実行環境

- C#
- .NET Core 3.1

## 実行方法

```console
$ dotnet Crossing.dll {filepath}
```

または

```console
$ Crossing.exe {filepath}
```

## データファイル

- crossing.txt
    - 問題で出題されたデータ。1,048,576行。
- sample5.txt
- sample3000.txt
- sample10000.txt
    - 検証用に作成したデータ。末尾の数値が行数。

## 結果

※提示されているフォーマットではなく、検証のため色々情報を出力しています。

- `Algorythm` ... 採用したアルゴリズム
- `Count` ... 交差点の個数
- `Count Time` ... 交差点を数えるのにかかった時間
- `Total Time` ... ファイル読み込みを含めてかかった総処理時間

### サンプルデータ（10,000件）

```console
# dotnet Crossing.dll data/sample10000.txt
Algorythm: Inversion
  Count: 25,029,255
  Count Time: 00:00.035
  Total Time: 00:00.037

Algorythm: MergeSort
  Count: 25,029,255
  Count Time: 00:00.136
  Total Time: 00:00.136

Algorythm: BubbleSort
  Count: 25,029,255
  Count Time: 00:00.100
  Total Time: 00:00.100
```

### 本番データ

```console
# dotnet Crossing.dll data/crossing.txt
Algorythm: Inversion
  Count: 275,115,831,438
  Count Time: 00:07.144
  Total Time: 00:07.146

Algorythm: MergeSort
  Count: 275,115,831,438
  Count Time: 42:10.429
  Total Time: 42:10.429
```

※バブルソートは時間がかかりすぎたので除外

### 所感、当時の思い出など

問題を読んでしばらく悩みました。結城先生の問題は「パッと聞いた感じでは解けそうなのに、ちゃんと解こうとすると存外難しい」という、素敵な問題が多くて楽しめます。

絵を描いたりしてようやく、ソートの計算量（入れ替えた手数＝交差点数）だろうことに思い至ることができました。
本番データ（crossing.txt）が重すぎるので、軽量なデータを作って検証し、合っていることを確認しました。

まずバブルソートとマージソートで実装・計測したところ、時間がかかりすぎて問題外でした。
ちなみに、サンプル（10,000件）の動作確認時はマージソートよりバブルソートの方が早かったので、逆転したのはちょっと意外でした。

仕方なくカンニングして、転倒という手法を知りました。実装して計測したところ、何とかそれらしい時間内で完了できるようになりました。（それでもまだ遅いが。）

細かい処理や記述を見直せばミリ秒単位で早くできるかも知れません。しかし、個人的な興味から逸れそうだったので、ここまでとしました。

2013, 2020 - @ushibutatory
