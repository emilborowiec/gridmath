using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class CoordTest
    {
        [Fact]
        public void TestEquality()
        {
            var c1 = new IntCoord(2, 2);
            var c2 = new IntCoord(2, 2);
            var c3 = new IntCoord();
            var c4 = new IntCoord();
            Assert.Equal(c1, c2);
            Assert.Equal(c3, c4);
            Assert.NotEqual(c1, c3);
        }
    }
}
