using Crossing.Counter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    var errors = _Validate(filePath);
                    if (errors.Count() > 0)
                    {
                        Console.Write(string.Join(Environment.NewLine, errors));
                    }
                    else
                    {
                        // 測定対象のアルゴリズム
                        var algorithms = new ICounter[] {
                            new Inversion(),
                            new MergeSort(),
                            //new BubbleSort(), // あまりに遅すぎるのでコメントアウト
                        };

                        // 測定
                        algorithms.ToList().ForEach((counter) =>
                        {
                            // 開始日時
                            var start = DateTime.Now;

                            // カウント結果を取得
                            var count = counter.Count(filePath);

                            // 終了日時
                            var end = DateTime.Now;

                            // 結果表示
                            Console.WriteLine($"Algorithm:{counter.GetType().Name}, Count:{count:#,##0}, Time:{end - start:hh\\:mm\\:ss\\.fff}");
                        });
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
        private static IEnumerable<string> _Validate(string filePath)
        {
            if (!File.Exists(filePath))
            {
                yield return "指定されたファイルが存在しません。";
            }
            else
            {
                // ファイル情報取得
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Extension.ToLower() != ".txt")
                {
                    yield return ".txtファイルを指定してください。";
                }
            }
        }
    }
}
