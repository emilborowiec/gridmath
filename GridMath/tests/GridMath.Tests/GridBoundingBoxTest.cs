#region

using System;
using Xunit;

#endregion

namespace GridMath.Tests
{
    public class GridBoundingBoxTest
    {
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(2, -3)]
        [InlineData(0, 1)]
        [InlineData(2, 0)]
        public void TestSizeAssertions(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => GridBoundingBox.FromSize(0, 0, width, height));
        }

        [Fact]
        public void TestMax()
        {
            var bounds = GridBoundingBox.FromSize(1, 2, 3, 4);
            Assert.Equal(4, bounds.MaxXExcl);
            Assert.Equal(6, bounds.MaxYExcl);
        }

        [Fact]
        public void TestPlaceBeside()
        {
            var box1 = GridBoundingBox.FromMinMaxExcl(0, 0, 1, 3);
            var box2 = GridBoundingBox.FromMinMaxExcl(10, 5, 20, 15);
            var onTop = box1.PlaceBeside(box2, Grid4Direction.Top);
            var onRight = box1.PlaceBeside(box2, Grid4Direction.Right);
            var under = box1.PlaceBeside(box2, Grid4Direction.Bottom);
            var onLeft = box1.PlaceBeside(box2, Grid4Direction.Left);

            Assert.Equal(14, onTop.MinX);
            Assert.Equal(5, onTop.MaxYExcl);
            Assert.Equal(14, under.MinX);
            Assert.Equal(15, under.MinY);
            Assert.Equal(20, onRight.MinX);
            Assert.Equal(10, onLeft.MaxXExcl);
        }

        [Fact]
        public void TestEquality()
        {
            var box1 = GridBoundingBox.FromMinMaxExcl(0, 0, 1, 3);
            var box2 = GridBoundingBox.FromMinMax(0, 0, 0, 2);
            var box3 = GridBoundingBox.FromMinMax(0, 0, 1, 3);
            
            Assert.Equal(box1, box2);
            Assert.True(box1 == box2);
            Assert.NotEqual(box1, box3);
            Assert.True(box1 != box3);
        }
    }
}