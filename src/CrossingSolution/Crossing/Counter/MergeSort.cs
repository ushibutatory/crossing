using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crossing.Counter
{
	/// <summary>
	/// マージソートによるカウント
	/// </summary>
	internal class MergeSort : BaseCounter
	{
		#region Publicメソッド
		/// <summary>
		/// 交差数をカウントします。
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>交差数</returns>
		public override Int64 Count(String filePath)
		{
			// ファイル読み込み
			var list = this._ReadFile(filePath);

			// マージソートによる演算結果を取得
			var result = this._MergeSort(list);

			// 交差数を返す
			return result.Count;
		}
		#endregion

		#region Privateメソッド
		/// <summary>
		/// マージソートを実行します。
		/// </summary>
		/// <param name="list">ソート対象リスト</param>
		/// <returns>ソート結果</returns>
		private Result _MergeSort(List<Int32> list)
		{
			Result result;

			if (list.Count == 1)
			{
				// 要素が1個の場合はソートしない
				result = new Result();
				result.Count = 0;
				result.List = list;
			}
			else
			{
				// リストの中間のインデックス
				var half = list.Count / 2;

				// リストの前半と後半をそれぞれマージソート
				Result subResult1 = null;
				Result subResult2 = null;
				using (var task1 = Task.Run(() => { subResult1 = this._MergeSort(list.Take(half).ToList()); }))
				using (var task2 = Task.Run(() => { subResult2 = this._MergeSort(list.Skip(half).ToList()); }))
				{
					// 待機
					Task.WaitAll(task1, task2);
				}

				// それぞれの結果をマージ
				result = new Result();
				result.List = new List<Int32>();
				result.Count = subResult1.Count + subResult2.Count;

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
		#endregion

		#region Privateクラス
		/// <summary>
		/// マージソート結果クラス
		/// </summary>
		private class Result
		{
			/// <summary>
			/// 交差数
			/// </summary>
			public Int64 Count { get; set; }

			/// <summary>
			/// ソート後リスト
			/// </summary>
			public List<Int32> List { get; set; }
		}
		#endregion
	}
}
