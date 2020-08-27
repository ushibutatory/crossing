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
        private static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 1:
                    // ファイルパス取得
                    var filePath = args[0];

                    // 検証
                    var errorList = _Validate(filePath);
                    if (errorList.Count > 0)
                    {
                        Console.Write(string.Join(Environment.NewLine, errorList));
                    }
                    else
                    {
                        // 演算
                        _Execute((new Inversion()), filePath);
                        _Execute((new MergeSort()), filePath);
                        //_Execute((new BubbleSort()), filePath); // あまりに遅すぎるのでコメントアウト
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
        /// <summary>
        /// ファイルパスを検証します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>エラーリスト</returns>
        private static List<string> _Validate(string filePath)
        {
            var errorList = new List<string>();

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
        private static void _Execute(BaseCounter counter, string filePath)
        {
            // 開始日時
            var start = DateTime.Now;

            // カウント結果を取得
            var count = counter.Count(filePath);

            // 終了日時
            var end = DateTime.Now;

            // 結果表示
            Console.WriteLine($"Algorithm:{counter.GetType().Name}, Count:{count:#,##0}, Time:{end - start:hh\\:mm\\:ss\\.fff}");
        }
    }
}
