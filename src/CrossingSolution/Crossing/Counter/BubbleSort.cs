using Crossing.Counter.Abstracts;
using System.Linq;

namespace Crossing.Counter
{
    /// <summary>
    /// バブルソート
    /// </summary>
    internal class BubbleSort : BaseCounter
    {
        /// <summary>
        /// 交差数をカウントします。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>交差数</returns>
        public override long Count(string filePath)
        {
            // ファイル読み込み
            var list = ReadFile(filePath).ToArray();

            // 交差数を初期化
            var count = (long)0;

            for (var endIndex = list.Count() - 1; endIndex > 0; endIndex--)
            {
                // 先頭から走査
                for (var startIndex = 0; startIndex < endIndex; startIndex++)
                {
                    // 隣同士を取得
                    var x = list[startIndex];
                    var y = list[startIndex + 1];

                    if (x > y)
                    {
                        // 入れ替える
                        list[startIndex] = y;
                        list[startIndex + 1] = x;

                        // 交差数を加算
                        count++;
                    }
                }
            }

            return count;
        }
    }
}