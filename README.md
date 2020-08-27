# Crossing問題に挑戦してみた

結城浩先生が出題されたクロッシング問題に挑戦しました。

- [CodeIQの問題に挑戦しよう！](http://www.hyuki.com/codeiq/#c12)
- [問題ページ（※リンク切れ）](https://codeiq.jp/ace/yuki_hiroshi/q432)に挑戦しました。

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

- `Count Time` ... 交差点を数えるのにかかった時間
- `Total Time` ... ファイル読み込みを含めてかかった総処理時間

### 当時の思い出など

ソートの計算量なんだろうとアタリをつけ、最初はバブルソートで計測したところ、時間がかかりすぎて問題外でした。

マージソートで実装してみてもやはり遅すぎました（既定の3秒を大幅に超える）。

仕方なくカンニングして転倒という手法で計算したところ、何とかそれらしい時間内で計測できるようになりました。（それでもまだ遅いが。）

細かい処理や記述を見直せばミリ秒単位で早くできるかも知れませんが、個人的な興味から逸れているので、ここまでとしました。

2013, 2020 - @ushibutatory
