#region

using System;
using Xunit;

#endregion

namespace PonderingProgrammer.GridMath.Tests
{
    public class GridIntervalTest
    {
        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void TestContains(int value, bool expected)
        {
            var interval = new GridInterval(0, 1);
            Assert.Equal(expected, interval.Contains(value));
        }

        [Theory]
        [InlineData(-1, 2, false)]
        [InlineData(-1, 1, false)]
        [InlineData(0, 2, false)]
        [InlineData(0, 0, true)]
        public void TestContainsInterval(int min, int max, bool expected)
        {
            var interval = new GridInterval(0, 1);
            Assert.Equal(expected, interval.Contains(new GridInterval(min, max)));
        }

        [Theory]
        [InlineData(-10, -3, false)]
        [InlineData(-10, 0, false)]
        [InlineData(-10, 1, true)]
        [InlineData(-10, 3, true)]
        [InlineData(0, 3, true)]
        [InlineData(4, 6, true)]
        [InlineData(0, 13, true)]
        [InlineData(9, 13, true)]
        [InlineData(-10, 13, true)]
        [InlineData(10, 13, false)]
        public void TestOverlaps(int min, int maxExcl, bool expected)
        {
            var interval = GridInterval.FromExclusiveMax(0, 10);
            var other = GridInterval.FromExclusiveMax(min, maxExcl);
            Assert.Equal(expected, interval.Overlaps(other));
        }

        [Theory]
        [InlineData(-2, -1, false)]
        [InlineData(-2, 0, true)]
        [InlineData(0, 10, false)]
        [InlineData(9, 10, false)]
        [InlineData(10, 12, true)]
        [InlineData(12, 32, false)]
        public void TestTouches(int min, int maxExcl, bool expected)
        {
            var interval = GridInterval.FromExclusiveMax(0, 10);
            var other = GridInterval.FromExclusiveMax(min, maxExcl);
            Assert.Equal(expected, interval.Touches(other));
        }


        [Theory]
        [InlineData(2, 4, true)]
        [InlineData(1, 5, true)]
        [InlineData(-3, 3, true)]
        [InlineData(1, 4, false)]
        [InlineData(-2, 3, false)]
        [InlineData(-4, -1, false)]
        public void TestIsEven(int min, int maxExcl, bool expected)
        {
            var interval = GridInterval.FromExclusiveMax(min, maxExcl);
            Assert.Equal(expected, interval.IsEven());
        }

        [Theory]
        [InlineData(2, 4, 3)]
        [InlineData(2, 5, 3)]
        [InlineData(-5, -2, -4)]
        public void TestCenter(int min, int max, int expected)
        {
            var interval = new GridInterval(min, max);
            Assert.Equal(expected, interval.Center);
        }

        [Theory]
        [InlineData(0, 3, IntervalAnchor.Start, 20)]
        [InlineData(0, 3, IntervalAnchor.Center, 19)]
        [InlineData(0, 3, IntervalAnchor.End, 18)]
        [InlineData(0, 2, IntervalAnchor.Start, 20)]
        [InlineData(0, 2, IntervalAnchor.Center, 20)]
        [InlineData(0, 2, IntervalAnchor.End, 19)]
        public void TestSetPosition(int min, int maxExcl, IntervalAnchor anchor, int expectedMin)
        {
            var interval = GridInterval.FromExclusiveMax(min, maxExcl);
            var translated = interval.SetPosition(20, anchor);
            Assert.Equal(expectedMin, translated.Min);
        }

        [Theory]
        [InlineData(0, 10, 0.01, null)]
        [InlineData(0, 10, 0.2, 2)]
        [InlineData(0, 10, 0.21, 2)]
        [InlineData(0, 10, 0.5, 5)]
        [InlineData(0, 10, 1.2, 12)]
        [InlineData(0, 10, 1.21, 12)]
        [InlineData(0, 10, -0.5, null)]
        public void TestMultiplication(int min, int maxExcl, double multiplier, int? expectedMax)
        {
            var interval = GridInterval.FromExclusiveMax(min, maxExcl);
            var result = interval.Multiplication(multiplier);
            Assert.Equal(expectedMax, result?.MaxExcl);
        }

        [Theory]
        [InlineData(-4, -3)]
        [InlineData(-3, -2)]
        [InlineData(-2, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void TestDistance(int value, int expected)
        {
            var interval = new GridInterval(-1, 1);
            Assert.Equal(expected, interval.Distance(value));
        }

        [Theory]
        [InlineData(-4, -3, -2)]
        [InlineData(-3, -2, -1)]
        [InlineData(-2, -1, 0)]
        [InlineData(-1, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 2, 1)]
        [InlineData(2, 3, 2)]
        [InlineData(3, 4, 3)]
        [InlineData(-2, 3, 0)]
        public void TestDistanceToOther(int otherMin, int otherMax, int expected)
        {
            var interval = new GridInterval(-1, 0);
            Assert.Equal(expected, interval.Distance(new GridInterval(otherMin, otherMax)));
        }

        [Theory]
        [InlineData(-2, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 3)]
        [InlineData(2, -2)]
        [InlineData(3, -1)]
        [InlineData(4, 0)]
        public void TestDepth(int value, int expected)
        {
            var interval = new GridInterval(-1, 3);
            Assert.Equal(expected, interval.Depth(value));
        }

        [Theory]
        [InlineData(-3, -2, 0)]
        [InlineData(-2, -1, 1)]
        [InlineData(-1, 0, 2)]
        [InlineData(0, 1, 3)]
        [InlineData(1, 2, -2)]
        [InlineData(2, 3, -1)]
        [InlineData(3, 4, 0)]
        public void TestDepthToSeparateOther(int otherMin, int otherMax, int expected)
        {
            var interval = new GridInterval(-1, 2);
            Assert.Equal(expected, interval.Depth(new GridInterval(otherMin, otherMax)));
        }

        [Fact]
        public void TestArgumentException()
        {
            Assert.Throws<ArgumentException>(() => GridInterval.FromExclusiveMax(2, 2));
        }

        [Fact]
        public void TestLength()
        {
            var interval = new GridInterval(2, 4);
            Assert.Equal(3, interval.Length);
        }

        [Fact]
        public void TestMapping()
        {
            var n1 = 1.1;
            var n2 = -0.9;
            Assert.Equal(1, (int) n1);
            Assert.Equal(0, (int) n2);
        }

        [Fact]
        public void TestRelate()
        {
            var i1 = GridInterval.FromExclusiveMax(0, 4);
            var i2 = GridInterval.FromExclusiveMax(10, 14);
            var related = i1.Relate(i2, Relation.StartToStart());
            Assert.Equal(10, related.Min);
            related = i1.Relate(i2, Relation.StartToCenter());
            Assert.Equal(11, related.Min);
            related = i1.Relate(i2, Relation.StartToEnd());
            Assert.Equal(13, related.Min);
            related = i1.Relate(i2, Relation.EndToStart());
            Assert.Equal(7, related.Min);
            related = i1.Relate(i2, Relation.EndToCenter());
            Assert.Equal(8, related.Min);
            related = i1.Relate(i2, Relation.EndToEnd());
            Assert.Equal(10, related.Min);
        }

        [Fact]
        public void TestSetMaxExcl()
        {
            var i1 = GridInterval.FromExclusiveMax(20, 30);
            var i2 = GridInterval.FromExclusiveMax(0, 4);
            var aligned = i2.SetMaxExcl(i1.MaxExcl);
            Assert.Equal(26, aligned.Min);
            Assert.Equal(30, aligned.MaxExcl);
        }

        [Fact]
        public void TestSetMin()
        {
            var i1 = GridInterval.FromExclusiveMax(20, 30);
            var i2 = GridInterval.FromExclusiveMax(0, 4);
            var aligned = i2.SetMin(i1.Min);
            Assert.Equal(20, aligned.Min);
            Assert.Equal(24, aligned.MaxExcl);
        }

        [Fact]
        public void TestSplitEven()
        {
            var interval = GridInterval.FromExclusiveMax(0, 2);
            var halves = interval.SplitEven();
            Assert.Equal(2, halves.Length);
            Assert.Equal(1, halves[0].Length);
            Assert.Equal(1, halves[1].Length);
            Assert.Equal(0, halves[0].Min);
            Assert.Equal(1, halves[0].MaxExcl);
            Assert.Equal(1, halves[1].Min);
            Assert.Equal(2, halves[1].MaxExcl);
        }

        [Fact]
        public void TestTranslate()
        {
            var interval = GridInterval.FromExclusiveMax(0, 2);
            var translated = interval.Translation(5);
            Assert.Equal(5, translated.Min);
            Assert.Equal(7, translated.MaxExcl);
        }

        [Fact]
        public void TestEquality()
        {
            var i1 = new GridInterval(0, 2);
            var i2 = GridInterval.FromExclusiveMax(0, 3);
            var i3 = GridInterval.FromLength(0, 2);
            
            Assert.Equal(i1, i2);
            Assert.True(i1 == i2);
            Assert.NotEqual(i1, i3);
            Assert.True(i1 != i3);
        }
    }
}