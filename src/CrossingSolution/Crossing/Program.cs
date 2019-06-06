using Crossing.Counter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crossing
{
	/// <summary>
	/// クロッシング問題への回答
	/// </summary>
	public class Program
	{
		/// <summary>
		/// エントリポイント
		/// </summary>
		/// <param name="args">
		/// 1: ファイルパス
		/// </param>
		public static void Main(string[] args)
		{
			try
			{
				switch (args.Length)
				{
					case 1:
						// ファイルパス取得
						String filePath = args[0];

						// 検証
						var errorList = _Validate(filePath);
						if (errorList.Count > 0)
						{
							Console.Write(String.Join(Environment.NewLine, errorList));
						}
						else
						{
							// 演算
							_Execute((new Inversion()), filePath);
							_Execute((new MergeSort()), filePath);
							//_Execute((new BubbleSort()), filePath); // あまりに遅すぎるのでコメントアウト

							// 待機
							Console.WriteLine("--press key");
							Console.ReadLine();
						}
						break;

					default:
						// ヘルプ表示
						var text = new StringBuilder();
						text.AppendLine("ファイルパスを指定してください。");
						text.AppendLine("example> crossing.exe crossing.txt");
						Console.Write(text.ToString());
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// ファイルパスを検証します。
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>エラーリスト</returns>
		private static List<String> _Validate(String filePath)
		{
			var errorList = new List<String>();

			if (!File.Exists(filePath))
			{
				errorList.Add("指定されたファイルが存在しません。");
			}
			else
			{
				// ファイル情報取得
				var fileInfo = new FileInfo(filePath);
				if (fileInfo.Extension.ToLower() != ".txt")
				{
					errorList.Add(".txtファイルを指定してください。");
				}
			}

			return errorList;
		}

		/// <summary>
		/// 交差点をカウントします。
		/// </summary>
		/// <param name="counter">カウンターオブジェクト</param>
		/// <param name="filePath">対象ファイルパス</param>
		private static void _Execute(BaseCounter counter, String filePath)
		{
			// 開始日時
			var start = DateTime.Now;

			// カウント結果を取得
			var count = counter.Count(filePath);

			// 終了日時
			var end = DateTime.Now;

			// 結果表示
			Console.WriteLine(@"Algorithm:{0}, Count:{1:#,##0}, Time:{2:hh\:mm\:ss\.fff}", counter.GetType().Name, count, end - start);
		}
	}
}