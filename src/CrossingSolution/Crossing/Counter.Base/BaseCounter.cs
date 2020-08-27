using System.Collections.Generic;

namespace Crossing.Counter
{
    /// <summary>
    /// 基底カウンタクラス
    /// </summary>
    internal abstract class BaseCounter : ICounter
    {
        public abstract long Count(string filePath);

        /// <summary>
        /// ファイルを読み込んで数値リストを返します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>数値リスト</returns>
        protected IEnumerable<int> _ReadFile(string filePath)
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
