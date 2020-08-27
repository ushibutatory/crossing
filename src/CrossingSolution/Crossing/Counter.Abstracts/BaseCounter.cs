using System.Collections.Generic;

namespace Crossing.Counter.Abstracts
{
    /// <summary>
    /// 基底カウンタクラス
    /// </summary>
    internal abstract class BaseCounter : ICounter
    {
        public abstract long Count(string filePath);

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
    }
}
