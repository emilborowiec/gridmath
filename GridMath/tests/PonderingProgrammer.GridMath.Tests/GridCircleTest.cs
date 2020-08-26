using System.Linq;
using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridCircleTest
    {
        [Fact]
        public void TestEdgeAndInterior()
        {
            var c = new GridCircle(0, 0, 1);
            Assert.Equal(4, c.Edge.Distinct().Count());
            Assert.Equal(5, c.Interior.Distinct().Count());
            c.Radius = 2;
            Assert.Equal(12, c.Edge.Distinct().Count());
            Assert.Equal(21, c.Interior.Distinct().Count());
        }
    }
}