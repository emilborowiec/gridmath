using System;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class DirectionsTest
    {
        [Theory]
        [InlineData(Grid4Direction.Left, Directions.Degree180)]
        [InlineData(Grid4Direction.Right, Directions.Degree0)]
        [InlineData(Grid4Direction.Top, Directions.Degree270)]
        [InlineData(Grid4Direction.Bottom, Directions.Degree90)]
        public void TestCardinalDirectionToAngle(Grid4Direction direction, double expected)
        {
            var angle = Directions.DirectionToAngle(direction);
            Assert.Equal(expected, angle);
        }

        [Theory]
        [InlineData(Grid8Direction.Left, Directions.Degree180)]
        [InlineData(Grid8Direction.Right, Directions.Degree0)]
        [InlineData(Grid8Direction.Top, Directions.Degree270)]
        [InlineData(Grid8Direction.Bottom, Directions.Degree90)]
        [InlineData(Grid8Direction.TopLeft, Directions.Degree225)]
        [InlineData(Grid8Direction.TopRight, Directions.Degree315)]
        [InlineData(Grid8Direction.BottomLeft, Directions.Degree135)]
        [InlineData(Grid8Direction.BottomRight, Directions.Degree45)]
        public void TestIntermediateDirectionToAngle(Grid8Direction direction, double expected)
        {
            var angle = Directions.DirectionToAngle(direction);
            Assert.Equal(expected, angle);
        }

        [Theory]
        [InlineData(Directions.Degree0, Grid4Direction.Right)]
        [InlineData(Directions.Degree180, Grid4Direction.Left)]
        [InlineData(Directions.Degree270, Grid4Direction.Top)]
        [InlineData(Directions.Degree90, Grid4Direction.Bottom)]
        public void TestAngleToCardinalDirection(double degree, Grid4Direction expected)
        {
            Assert.Equal(expected, Directions.AngleToDirection4(degree));
        }

        [Theory]
        [InlineData(Directions.Degree0, Grid8Direction.Right)]
        [InlineData(Directions.Degree180, Grid8Direction.Left)]
        [InlineData(Directions.Degree270, Grid8Direction.Top)]
        [InlineData(Directions.Degree90, Grid8Direction.Bottom)]
        [InlineData(Directions.Degree315, Grid8Direction.TopRight)]
        [InlineData(Directions.Degree225, Grid8Direction.TopLeft)]
        [InlineData(Directions.Degree135, Grid8Direction.BottomLeft)]
        [InlineData(Directions.Degree45, Grid8Direction.BottomRight)]
        public void TestAngleToIntermediateDirection(double degree, Grid8Direction expected)
        {
            Assert.Equal(expected, Directions.AngleToDirection8(degree));
        }

        [Theory]
        [InlineData(Math.PI, Math.PI)]
        [InlineData(-Math.PI, Math.PI)]
        [InlineData(3 * Math.PI, Math.PI)]
        public void TestWrapAngle(double angle, double expected)
        {
            Assert.Equal(expected, Directions.WrapAngle(angle));
        }

        [Fact]
        public void TestRotate()
        {
            Assert.Equal(Grid4Direction.Bottom, Directions.Rotate(Grid4Direction.Right, new GridRotation(1)));
            Assert.Equal(Grid4Direction.Top, Directions.Rotate(Grid4Direction.Right, new GridRotation(1, true)));
            Assert.Equal(Grid8Direction.BottomRight, Directions.Rotate(Grid8Direction.Right, new GridRotation(1)));
            Assert.Equal(Grid8Direction.TopRight, Directions.Rotate(Grid8Direction.Right, new GridRotation(1, true)));
        }
    }
}