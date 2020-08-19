#region

using PonderingProgrammer.GridMath.Shapes;
using Xunit;

#endregion

namespace PonderingProgrammer.GridMath.Tests.Shapes
{
    public class GridPointTest
    {
        [Fact]
        public void TestUpdateBoundsAndCoords()
        {
            var point = new GridPoint(new GridCoordinatePair(0, 0));
            point.Translate(2, 5);
            Assert.Equal(2, point.BoundingBox.MinX);
            Assert.Equal(2, point.BoundingBox.MaxX);
            Assert.Equal(5, point.BoundingBox.MinY);
            Assert.Equal(5, point.BoundingBox.MaxY);
            Assert.Equal(5, point.Coordinates[0].Y);
            Assert.Equal(point.Position, point.Coordinates[0]);
        }
    }
}