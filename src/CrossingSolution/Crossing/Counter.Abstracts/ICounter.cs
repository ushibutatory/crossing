namespace Crossing.Counter.Abstracts
{
    /// <summary>
    /// カウンタインタフェース
    /// </summary>
    public interface ICounter
    {
        /// <summary>
        /// ファイルを読み込んで交差数を返します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>交差数</returns>
        public long Count(string filePath);
    }
}
