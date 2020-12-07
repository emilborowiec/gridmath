using GridMath.Shapes;
using Xunit;

namespace GridMath.Tests
{
    public class GridPointTest
    {
        [Fact]
        public void TestEdgeAndInterior()
        {
            var p = new GridPoint(10, 10);
            Assert.Single(p.Edge);
            Assert.Single(p.Interior);
        }

        [Fact]
        public void TestTranslate()
        {
            var p = new GridPoint(10, 10);
            p.Translate(0, -5);            
            Assert.Equal(10, p.X);
            Assert.Equal(5, p.Y);
        }
    }
}