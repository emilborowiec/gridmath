using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests.Shapes
{
    public class GridCircleTest
    {
        [Fact]
        public void TestUpdatingCircleCoordinates()
        {
            var c = new GridCircle(new GridCoordinatePair(0, 0), 0);
            Assert.Single(c.ContainedCoordinates);
            Assert.Equal(0, c.ContainedCoordinates[0].X);
            Assert.Equal(0, c.ContainedCoordinates[0].Y);
            c.Radius = 2;
            Assert.Equal(13, c.ContainedCoordinates.Count);
            c.Radius = 3;
            Assert.Equal(29, c.ContainedCoordinates.Count);
            c.Radius = 4;
            Assert.Equal(49, c.ContainedCoordinates.Count);
        }
    }
}