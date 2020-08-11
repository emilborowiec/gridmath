using System.Linq;
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

        [Fact]
        public void FindOverlappingIntervalsTest()
        {
            var i1 = new GridInterval(0, 10);
            var i2 = new GridInterval(1, 3);
            var i3 = new GridInterval(2, 3);
            var i4 = new GridInterval(4, 13);
            var i5 = new GridInterval(12, 14);
            var i6 = new GridInterval(15, 16);

            var overlappingGroups = GridIntervals.FindOverlappingIntervals(new [] {i1, i2, i3, i4, i5, i6});

            Assert.Equal(3, overlappingGroups.Count);
            Assert.Equal(3, overlappingGroups[0].Count);
            Assert.Equal(2, overlappingGroups[1].Count);
            Assert.Equal(2, overlappingGroups[2].Count);
            Assert.False(overlappingGroups.SelectMany(g => g).Any(index => index == 5));
        }
        
        [Fact]
        public void FindOverlappingIntervalsTest2()
        {
            var i1 = new GridInterval(1, 3);
            var i2 = new GridInterval(3, 5);
            var i3 = new GridInterval(1, 4);

            var overlappingGroups = GridIntervals.FindOverlappingIntervals(new [] {i1, i2, i3});

            Assert.Equal(1, overlappingGroups.Count);
        }

        [Fact]
        public void TestFindCenterOfMass()
        {
            var i1 = new GridInterval(-2, -2);
            var i2 = new GridInterval(-2, -2);
            var i3 = new GridInterval(0, 0);
            var i4 = new GridInterval(4, 4);

            var center = GridIntervals.FindCenterOfMass(new[] {i1, i2, i3, i4});
            
            Assert.Equal(0, center);
        }
    }
}