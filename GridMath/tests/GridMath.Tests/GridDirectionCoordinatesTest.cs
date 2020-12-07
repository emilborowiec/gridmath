using Xunit;

namespace GridMath.Tests
{
    public class GridDirectionCoordinatesTest
    {
        [Fact]
        public void TestConversionToGridCoordinates()
        {
            var dc1 = new GridDirectionCoordinates(Grid8Direction.TopRight, 3);
            var c = dc1.ToGridCartesian();
            Assert.Equal(3, c.X);
            Assert.Equal(-3, c.Y);
        }
        
        [Fact]
        public void TestEquality()
        {
            var dc1 = new GridDirectionCoordinates(Grid8Direction.Top, 1);
            var dc2 = new GridDirectionCoordinates(Grid8Direction.Top, 1);
            var dc3 = new GridDirectionCoordinates(Grid8Direction.Bottom, 1);
            var dc4 = new GridDirectionCoordinates(Grid8Direction.Top, 2);
            
            Assert.Equal(dc1, dc2);
            Assert.True(dc1 == dc2);
            Assert.NotEqual(dc1, dc3);
            Assert.True(dc1 != dc3);
            Assert.NotEqual(dc1, dc4);
            Assert.True(dc1 != dc4);
        }
    }
}