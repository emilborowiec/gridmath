using System.Linq;
using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridFanTest
    {
        [Fact]
        public void TestEdge()
        {
            var f = new GridFan(new GridCoordinatePair(), 5, Grid8Direction.Top);
            var bb = f.BoundingBox;
            var edge = f.Edge.OrderBy(e => e.X).ToArray();
        }
    }
}