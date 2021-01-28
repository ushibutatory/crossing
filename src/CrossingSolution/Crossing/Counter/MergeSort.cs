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
            return result.CrossingCount;
        }

        /// <summary>
        /// マージソートを実行します。
        /// </summary>
        /// <param name="values">ソート対象リスト</param>
        /// <returns>ソート結果</returns>
        private Result _MergeSort(IEnumerable<long> values)
        {
            // 要素が1個以下の場合はソート不要
            if (values.Count() <= 1) return Result.New(values);

            // リストの中間のインデックス
            var half = values.Count() / 2;

            // リストの前半と後半をそれぞれマージソート
            Result subResult1 = null;
            Result subResult2 = null;
            Task.WaitAll(new[]
            {
                Task.Run(() => { subResult1 = _MergeSort(values.Take(half).ToArray()); }),
                Task.Run(() => { subResult2 = _MergeSort(values.Skip(half).ToArray()); }),
            });

            // それぞれの結果をマージ
            var result = Result
                .New()
                .Countup(subResult1.CrossingCount + subResult2.CrossingCount);

            while (subResult1.Values.Count > 0 && subResult2.Values.Count > 0)
            {
                // 2つのリストのそれぞれ先頭を比較
                if (subResult1.Values.First() > subResult2.Values.First())
                {
                    // 交差あり（交差数を加算）
                    result.AddValue(subResult2.PullAt(0))
                        .Countup(subResult1.Values.Count);
                }
                else
                {
                    // 交差なし
                    result.AddValue(subResult1.PullAt(0));
                }
            }

            // 残ったリストを末尾に結合
            if (subResult1.Values.Count > 0) result.AddValues(subResult1.Values);
            if (subResult2.Values.Count > 0) result.AddValues(subResult2.Values);

            return result;
        }

        /// <summary>
        /// マージソート結果
        /// </summary>
        private class Result
        {
            /// <summary>
            /// 交差数
            /// </summary>
            public long CrossingCount { get; private set; }

            /// <summary>
            /// 値リスト
            /// </summary>
            public IReadOnlyCollection<long> Values => _values;

            /// <summary>
            /// 値リスト（操作用）
            /// </summary>
            private List<long> _values;

            /// <summary>
            /// 新しいインスタンスを生成します。
            /// </summary>
            public static Result New() => New(new List<long>());

            /// <summary>
            /// 新しいインスタンスを生成します。
            /// </summary>
            public static Result New(IEnumerable<long> values) => new Result
            {
                CrossingCount = 0,
                _values = values.ToList(),
            };

            /// <summary>
            /// 交差数を加算します。
            /// </summary>
            public Result Countup(long count)
            {
                CrossingCount += count;
                return this;
            }

            /// <summary>
            /// 値リストに値を追加します。
            /// </summary>
            public Result AddValue(long value)
            {
                _values.Add(value);
                return this;
            }

            /// <summary>
            /// 値リストに値を追加します。
            /// </summary>
            public Result AddValues(IEnumerable<long> values)
            {
                _values.AddRange(values);
                return this;
            }

            /// <summary>
            /// 値リストから指定したインデックス位置の値を取得します。
            /// 取得した値はリストから除去されます。
            /// </summary>
            public long PullAt(int index)
            {
                var value = _values[index];
                _values.RemoveAt(index);
                return value;
            }
        }
    }
}
