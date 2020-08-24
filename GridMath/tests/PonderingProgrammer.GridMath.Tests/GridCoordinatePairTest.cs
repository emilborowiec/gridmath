#region

using Xunit;

#endregion

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridCoordinatePairTest
    {
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

        [Fact]
        public void TestMappingToReal()
        {
            var c = new GridCoordinatePair(0, -1);
            var (x, y) = GridCoordinatePair.ToReal(c);
            Assert.Equal(0.5, x);
            Assert.Equal(-0.5, y);
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
        [InlineData(3, 0, 3.0)]
        [InlineData(0, 3, 3.0)]
        [InlineData(3, 3, 4.243)]
        [InlineData(2, 1, 2.236)]
        [InlineData(-1, -1, 1.414)]
        public void TestEuclideanDistance(int x, int y, double expected)
        {
            var c = new GridCoordinatePair(0, 0);
            Assert.Equal(expected, c.EuclideanDistance(x, y), 3);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(-11, 1)]
        public void TestTranslation(int x, int y)
        {
            var c = new GridCoordinatePair(10, 10);
            var c2 = c.Translation(x, y);
            Assert.Equal(10 + x, c2.X);
            Assert.Equal(10 + y, c2.Y);
        }

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
    }
}