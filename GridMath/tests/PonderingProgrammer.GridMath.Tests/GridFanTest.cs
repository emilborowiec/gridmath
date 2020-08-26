#region

using System.Linq;
using PonderingProgrammer.GridMath.Shapes;
using Xunit;

#endregion

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridFanTest
    {
        [Fact]
        public void TestEdgeAndInterior()
        {
            var fan = new GridFan(new GridCoordinatePair(), 1, Grid8Direction.Top);
            Assert.Equal(1, fan.BoundingBox.Width);
            Assert.Equal(4, fan.Edge.Distinct().Count());
            Assert.Equal(4, fan.Interior.Distinct().Count());
            fan.Radius = 2;
            Assert.Equal(6, fan.Edge.Distinct().Count());
            Assert.Equal(7, fan.Interior.Distinct().Count());
        }

        [Fact]
        public void TestRotation()
        {
            var fan = new GridFan(new GridCoordinatePair(), 1, Grid8Direction.Top);
            fan.Rotate(new GridRotation(1));
            Assert.Equal(Grid8Direction.TopRight, fan.Direction);
        }
    }
}