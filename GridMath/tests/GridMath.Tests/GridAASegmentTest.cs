using GridMath.Shapes;
using Xunit;

namespace GridMath.Tests
{
    public class GridAASegmentTest
    {
        [Fact]
        public void TestRotation()
        {
            var s = new GridAASegment(new GridCoordinatePair(0, 0), new GridCoordinatePair(10, 0));
            s.Rotate(new GridRotation(1));
            Assert.Equal(10, s.B.Y);
            s.Rotate(new GridRotation(1, true));
            Assert.Equal(0, s.B.Y);
        }
    }
}