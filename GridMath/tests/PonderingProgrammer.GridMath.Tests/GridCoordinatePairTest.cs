using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridCoordinatePairTest
    {
        [Fact]
        public void TestEquality()
        {
            var c1 = new GridCoordinatePair(2, 2);
            var c2 = new GridCoordinatePair(2, 2);
            var c3 = new GridCoordinatePair();
            var c4 = new GridCoordinatePair();
            Assert.Equal(c1, c2);
            Assert.Equal(c3, c4);
            Assert.NotEqual(c1, c3);
        }

        [Theory]
        [InlineData(1, 2, 1, 2)]
        [InlineData(1.1, 2.9, 1, 2)]
        [InlineData(-0.1, 0.1, -1, 0)]
        [InlineData(-2.1, -1.9, -3, -2)]
        public void TestMappingFromReal(double x, double y, int expectedX, int expectedY)
        {
            var c = GridCoordinatePair.FromReal(x, y);
            Assert.Equal(expectedX, c.X);
            Assert.Equal(expectedY, c.Y);
        }

        [Theory]
        [InlineData(3, 0, 3)]
        [InlineData(0, 3, 3)]
        [InlineData(3, 3, 6)]
        [InlineData(2, 1, 3)]
        [InlineData(-1, -1, 2)]
        public void TestManhattanDistance(int x, int y, int expected)
        {
            var c = new GridCoordinatePair(0, 0);
            Assert.Equal(expected, c.ManhattanDistance(x, y));
        }

        [Theory]
        [InlineData(3, 0, 3)]
        [InlineData(0, 3, 3)]
        [InlineData(3, 3, 3)]
        [InlineData(2, 1, 2)]
        [InlineData(-1, -1, 1)]
        public void TestChebyshevDistance(int x, int y, int expected)
        {
            var c = new GridCoordinatePair(0, 0);
            Assert.Equal(expected, c.ChebyshevDistance(x, y));
        }
    }
}
