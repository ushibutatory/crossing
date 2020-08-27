using System.Collections.Generic;

namespace Crossing.Counter
{
    /// <summary>
    /// 基底カウンタクラス
    /// </summary>
    internal abstract class BaseCounter
    {
        /// <summary>
        /// ファイルを読み込んで交差数を返します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>交差数</returns>
        /// <remarks>
        /// 派生クラスで交差数のカウント処理を実装します。
        /// </remarks>
        public abstract long Count(string filePath);

        /// <summary>
        /// ファイルを読み込んで数値リストを返します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>数値リスト</returns>
        protected List<int> _ReadFile(string filePath)
        {
            var list = new List<int>();

            using (var reader = new System.IO.StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    // 1行読み込み
                    var line = reader.ReadLine();

                    // 数値変換してリストに追加
                    list.Add(int.Parse(line));
                }
            }

            return list;
        }
    }
}
