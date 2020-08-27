using System.Collections.Generic;

namespace Crossing.Counter.Abstracts
{
    /// <summary>
    /// カウンタインタフェース
    /// </summary>
    public interface ICounter
    {
        /// <summary>
        /// ファイルを読み込んで数値リストを返します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>数値リスト</returns>
        public IEnumerable<long> ReadFile(string filePath);

        /// <summary>
        /// ファイルを読み込んで交差数を返します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>交差数</returns>
        public long Count(string filePath);
    }
}
