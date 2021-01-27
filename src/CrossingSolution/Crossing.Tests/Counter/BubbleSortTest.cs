using Crossing.Counter;
using Crossing.Counter.Abstracts;
using Xunit;

namespace Crossing.Tests.Counter
{
    public class BubbleSortTest
    {
        private readonly ICounter _counter = new BubbleSort();

        [Theory]
        [InlineData("data/sample5.txt", 4)]
        [InlineData("data/sample3000.txt", 2217109)]
        [InlineData("data/sample10000.txt", 25029255)]
        //[InlineData("data/crossing.txt", 275115831438)] // 重すぎるのでコメントアウト
        public void Count(string filePath, long expected)
        {
            var actual = _counter.Count(filePath);

            Assert.Equal(expected, actual);
        }
    }
}
