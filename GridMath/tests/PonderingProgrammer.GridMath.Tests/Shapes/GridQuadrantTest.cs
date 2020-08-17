using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests.Shapes
{
    public class GridQuadrantTest
    {
        [Fact]
        public void TestUpdatingCircleCoordinates()
        {
            var c = new GridQuadrant(new GridCoordinatePair(), 0, Grid8Direction.TopRight);
            Assert.Single(c.Coordinates);
            Assert.Equal(0, c.Coordinates[0].X);
            Assert.Equal(0, c.Coordinates[0].Y);
            c.Radius = 2;
            Assert.Equal(6, c.Coordinates.Count);
            c.Radius = 3;
            Assert.Equal(11, c.Coordinates.Count);
            c.Radius = 4;
            Assert.Equal(17, c.Coordinates.Count);
            c.Rotate(Grid4Rotation.Ccw90);
            Assert.Equal(17, c.Coordinates.Count);
            c = new GridQuadrant(new GridCoordinatePair(), 4, Grid8Direction.Right);
            Assert.Equal(15, c.Coordinates.Count);
            c.Rotate(Grid4Rotation.Ccw90);
            Assert.Equal(15, c.Coordinates.Count);
        }

    }
}