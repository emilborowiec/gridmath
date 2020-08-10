using Xunit;

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridIntervalsTest
    {
        [Fact]
        public void SeparateTest()
        {
            var i1 = new GridInterval(0, 2);
            var i2 = new GridInterval(1, 3);
            var i3 = new GridInterval(2, 4);
            var i4 = new GridInterval(-1, 0);
            var i12 = GridIntervals.Separate(i1, i2);
            Assert.Equal(-1, i12[0].Min);
            Assert.Equal(2, i12[1].Min);
            var i13 = GridIntervals.Separate(i1, i3);
            Assert.Equal(-1, i13[0].Min);
            Assert.Equal(2, i13[1].Min);
            var i14 = GridIntervals.Separate(i1, i4);
            Assert.Equal(0, i14[0].Min);
            Assert.Equal(-2, i14[1].Min);
        }        
    }
}