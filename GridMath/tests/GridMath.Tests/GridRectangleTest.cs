using System.Linq;
using GridMath.Shapes;
using Xunit;

namespace GridMath.Tests
{
    public class GridRectangleTest
    {
        [Fact]
        public void TestEdgeAndInterior()
        {
            var r = new GridRectangle(0, 0, 4, 3);
            Assert.Equal(10, r.Edge.Count());
            Assert.Equal(12, r.Interior.Count());
        }

        [Fact]
        public void TestRotation()
        {
            var r = new GridRectangle(0, 0, 2, 1);
            Assert.Equal(0, r.MinX);
            Assert.Equal(0, r.MinY);
            Assert.Equal(1, r.MaxX);
            Assert.Equal(0, r.MaxY);
            r.Rotate(new GridRotation(1));
            Assert.Equal(0, r.MinX);
            Assert.Equal(0, r.MinY);
            Assert.Equal(0, r.MaxX);
            Assert.Equal(1, r.MaxY);
            r.Rotate(new GridRotation(1));
            Assert.Equal(-1, r.MinX);
            Assert.Equal(0, r.MinY);
            Assert.Equal(0, r.MaxX);
            Assert.Equal(0, r.MaxY);
            r.Rotate(new GridRotation(1));
            Assert.Equal(-1, r.MinX);
            Assert.Equal(0, r.MinY);
            Assert.Equal(-1, r.MaxX);
            Assert.Equal(1, r.MaxY);
            r.Rotate(new GridRotation(1, true));
            Assert.Equal(-1, r.MinX);
            Assert.Equal(0, r.MinY);
            Assert.Equal(0, r.MaxX);
            Assert.Equal(0, r.MaxY);
        }
    }
}