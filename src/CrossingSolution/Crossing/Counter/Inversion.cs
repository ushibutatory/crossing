using Crossing.Counter.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossing.Counter
{
    /// <summary>
    /// 転倒
    /// </summary>
    internal class Inversion : BaseCounter
    {
        public override long Count(IEnumerable<long> values)
        {
            // 交差点の数を初期化
            var count = (long)0;

            // 最大値（桁数を数えるため）
            var maxValue = values.Max();

            // 最大長（最大値を2進数にした時の桁数）
            var maxLength = Convert.ToString(maxValue, 2).Length;

            // 転倒数リストを初期化
            var inversionList = new Dictionary<string, int>();

            foreach (var value in values)
            {
                // 2進数に変換（最大長まで0埋め）
                var valueString = Convert.ToString(value, 2).PadLeft(maxLength, '0');

                // 1桁ずつチェック
                // 例）0,00,000,0000,00000,...
                for (var i = 0; i < maxLength; i++)
                {
                    // キー生成
                    var key = valueString.Substring(0, i);

                    if (!inversionList.ContainsKey(key))
                    {
                        // 初回生成
                        inversionList.Add(key, 0);
                    }

                    switch (valueString[i])
                    {
                        case '0':
                            // 交差数に転倒数を加算
                            count += inversionList[key];
                            break;
                        case '1':
                            // 転倒数を加算
                            inversionList[key]++;
                            break;
                    }
                }
            }

            return count;
        }
    }
}
