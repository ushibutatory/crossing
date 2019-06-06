using System;
using System.Collections.Generic;

namespace Crossing.Counter
{
	/// <summary>
	/// 基底カウンタクラス
	/// </summary>
	internal abstract class BaseCounter
	{
		#region Publicメソッド
		/// <summary>
		/// ファイルを読み込んで交差数を返します。
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>交差数</returns>
		/// <remarks>
		/// 派生クラスで交差数のカウント処理を実装します。
		/// </remarks>
		public abstract Int64 Count(String filePath);
		#endregion

		#region Protectedメソッド
		/// <summary>
		/// ファイルを読み込んで数値リストを返します。
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>数値リスト</returns>
		protected List<Int32> _ReadFile(String filePath)
		{
			var list = new List<Int32>();

			using (var reader = new System.IO.StreamReader(filePath))
			{
				while (!reader.EndOfStream)
				{
					// 1行読み込み
					var line = reader.ReadLine();

					// 数値変換してリストに追加
					list.Add(Int32.Parse(line));
				}
			}

			return list;
		}
		#endregion
	}
}
