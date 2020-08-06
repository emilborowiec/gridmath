using System;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridBoundingBoxTest
    {
        [Fact]
        public void TestMax()
        {
            var bounds = GridBoundingBox.FromSize(1, 2, 3, 4);
            Assert.Equal(4, bounds.MaxXExcl);
            Assert.Equal(6, bounds.MaxYExcl);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(2, -3)]
        [InlineData(0, 1)]
        [InlineData(2, 0)]
        public void TestSizeAssertions(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => GridBoundingBox.FromSize(0, 0, width, height));
        }
        
    }
}
