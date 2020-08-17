using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests.Shapes
{
    public class GridRectangleTest
    {
        [Fact]
        public void TestUpdateContainedCoords()
        {
            var rect = new GridRectangle(GridBoundingBox.FromMinMax(2, 2, 4, 4));
            Assert.Equal(9, rect.Coordinates.Count);
            Assert.Contains(new GridCoordinatePair(2, 2), rect.Coordinates);
            Assert.Contains(new GridCoordinatePair(3, 3), rect.Coordinates);
            Assert.Contains(new GridCoordinatePair(4, 4), rect.Coordinates);
            rect.Rectangle = rect.Rectangle.Translation(10, 0);
            Assert.Contains(new GridCoordinatePair(12, 2), rect.Coordinates);
            Assert.Contains(new GridCoordinatePair(13, 3), rect.Coordinates);
            Assert.Contains(new GridCoordinatePair(14, 4), rect.Coordinates);
        }

        [Fact]
        public void TestRotate()
        {
            var rect = new GridRectangle(GridBoundingBox.FromMinMax(0, 0, 1, 0));
            rect.Rotate(Grid4Rotation.Ccw90);
            Assert.Equal(0, rect.Rectangle.MinX);
            Assert.Equal(-1, rect.Rectangle.MinY);
            Assert.Equal(0, rect.Rectangle.MaxX);
            Assert.Equal(0, rect.Rectangle.MaxY);
            rect.Rotate(Grid4Rotation.Cw90);
            Assert.Equal(-1, rect.Rectangle.MinX);
            Assert.Equal(-1, rect.Rectangle.MinY);
            Assert.Equal(0, rect.Rectangle.MaxX);
            Assert.Equal(-1, rect.Rectangle.MaxY);
        }
    }
}