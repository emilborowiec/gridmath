using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridCoordinateTest
    {
        [Fact]
        public void TestEquality()
        {
            var c1 = new GridCoordinate(2, 2);
            var c2 = new GridCoordinate(2, 2);
            var c3 = new GridCoordinate();
            var c4 = new GridCoordinate();
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
            var c = GridCoordinate.FromReal(x, y);
            Assert.Equal(expectedX, c.X);
            Assert.Equal(expectedY, c.Y);
        }
    }
}
