#region

using System;
using Xunit;

#endregion

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridPolarCoordinatesTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(Math.PI, Math.PI)]
        [InlineData(2 * Math.PI, 0)]
        [InlineData(-Math.PI, Math.PI)]
        [InlineData(3 * Math.PI, Math.PI)]
        public void WrapAngleTest(double angle, double expected)
        {
            Assert.Equal(expected, Directions.WrapAngle(angle));
        }

        [Fact]
        public void TestRotation()
        {
            var pc = new GridPolarCoordinates(Directions.BottomRight, 10);
            pc = pc.Rotation(Directions.Degree45);
            Assert.Equal(Directions.Bottom, pc.Theta);
            pc = pc.Rotation(Directions.Degree90);
            Assert.Equal(Directions.Left, pc.Theta);
        }

        [Fact]
        public void TestConversionFromGridCartesian()
        {
            var pc = GridPolarCoordinates.FromGridCartesian(new GridCoordinatePair(10, 0));
            Assert.Equal(Directions.Right, pc.Theta);
            Assert.Equal(10, pc.Radius);
            pc = GridPolarCoordinates.FromGridCartesian(new GridCoordinatePair(0, 10));
            Assert.Equal(Directions.Bottom, pc.Theta);
            Assert.Equal(10, pc.Radius);
        }

        [Theory]
        [InlineData(Directions.Right, 10, 0)]
        [InlineData(Directions.BottomRight, 7, 7)]
        [InlineData(Directions.Bottom, 0, 10)]
        [InlineData(Directions.BottomLeft, -7, 7)]
        [InlineData(Directions.Left, -10, 0)]
        [InlineData(Directions.TopLeft, -7, -7)]
        [InlineData(Directions.Top, 0, -10)]
        [InlineData(Directions.TopRight, 7, -7)]
        public void TestConversionToCartesian(double theta, int expectedX, int expectedY)
        {
            var pc = new GridPolarCoordinates(theta, 10);
            var c = pc.ToGridCartesian();
            Assert.Equal(expectedX, c.X);
            Assert.Equal(expectedY, c.Y);
        }

        [Fact]
        public void TestEquality()
        {
            var pc1 = new GridPolarCoordinates(Math.PI, 1);
            var pc2 = new GridPolarCoordinates(Math.PI*3, 1);
            var pc3 = new GridPolarCoordinates(Math.PI+2, 1);
            
            Assert.Equal(pc1, pc2);
            Assert.True(pc1 == pc2);
            Assert.NotEqual(pc1, pc3);
            Assert.True(pc1 != pc3);
        }
    }
}