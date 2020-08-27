using System.Collections.Generic;

namespace Crossing.Counter.Abstracts
{
    /// <summary>
    /// 基底カウンタクラス
    /// </summary>
    internal abstract class BaseCounter : ICounter
    {
        public IEnumerable<long> ReadFile(string filePath)
        {
            using var reader = new System.IO.StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                // 1行読み込み
                var line = reader.ReadLine();

                // 数値変換して返す
                yield return int.Parse(line);
            }
        }

        public long Count(string filePath)
        {
            // ファイル読み込み
            var values = ReadFile(filePath);

            // 交差数を返す
            return Count(values);
        }

        public abstract long Count(IEnumerable<long> values);
    }
}
