using Crossing.Counter.Abstracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crossing.Counter
{
    /// <summary>
    /// マージソートによるカウント
    /// </summary>
    public class MergeSort : BaseCounter
    {
        public override long Count(IEnumerable<long> values)
        {
            // マージソートによる演算結果を取得
            var result = _MergeSort(values);

            // 交差数を返す
            return result.Count;
        }

        /// <summary>
        /// マージソートを実行します。
        /// </summary>
        /// <param name="values">ソート対象リスト</param>
        /// <returns>ソート結果</returns>
        private Result _MergeSort(IEnumerable<long> values)
        {
            Result result;

            if (values.Count() == 1)
            {
                // 要素が1個の場合はソートしない
                result = new Result
                {
                    Count = 0,
                    List = values.ToList()
                };
            }
            else
            {
                // リストの中間のインデックス
                var half = values.Count() / 2;

                // リストの前半と後半をそれぞれマージソート
                Result subResult1 = null;
                Result subResult2 = null;
                using (var task1 = Task.Run(() => { subResult1 = _MergeSort(values.Take(half).ToList()); }))
                using (var task2 = Task.Run(() => { subResult2 = _MergeSort(values.Skip(half).ToList()); }))
                {
                    // 待機
                    Task.WaitAll(task1, task2);
                }

                // それぞれの結果をマージ
                result = new Result
                {
                    List = new List<long>(),
                    Count = subResult1.Count + subResult2.Count
                };

                while (subResult1.List.Count > 0 && subResult2.List.Count > 0)
                {
                    // 2つのリストのそれぞれ先頭を比較
                    if (subResult1.List.First() > subResult2.List.First())
                    {
                        // 交差あり
                        result.List.Add(subResult2.List.First());
                        subResult2.List.RemoveAt(0);
                        result.Count += subResult1.List.Count; // 交差数を加算
                    }
                    else
                    {
                        // 交差なし
                        result.List.Add(subResult1.List.First());
                        subResult1.List.RemoveAt(0);
                    }
                }

                // 残ったリストを末尾に結合
                if (subResult1.List.Count > 0)
                {
                    result.List.AddRange(subResult1.List);
                }
                if (subResult2.List.Count > 0)
                {
                    result.List.AddRange(subResult2.List);
                }
            }

            // 結果を返す
            return result;
        }

        /// <summary>
        /// マージソート結果クラス
        /// </summary>
        private class Result
        {
            /// <summary>
            /// 交差数
            /// </summary>
            public long Count { get; set; }

            /// <summary>
            /// ソート後リスト
            /// </summary>
            public List<long> List { get; set; }
        }
    }
}
