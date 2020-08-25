#region

using Xunit;

#endregion

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridConvertTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0.1, 0)]
        [InlineData(0.5, 0)]
        [InlineData(0.9, 0)]
        [InlineData(1, 1)]
        [InlineData(-0.9, -1)]
        public void TestToGrid(double real, int expected)
        {
            Assert.Equal(expected, GridConvert.ToGrid(real));
        }

        [Theory]
        [InlineData(0, 0.5)]
        [InlineData(1, 1.5)]
        [InlineData(-1, -0.5)]
        public void TestToReal(int grid, double expected)
        {
            Assert.Equal(expected, GridConvert.ToReal(grid));
        }
    }
}