using Xunit;

namespace GridMath.Tests
{
    public class GridRotationTest
    {
        [Theory]
        [InlineData(1, 4, Directions.Degree90)]
        [InlineData(1, 8, Directions.Degree45)]
        [InlineData(6, 12, Directions.Degree180)]
        public void TestToRadians(int ticks, int subdivision, double expected)
        {
            var rot = new GridRotation(ticks);
            Assert.Equal(expected, rot.ToRadians(subdivision));
        }

        [Fact]
        public void TestEquality()
        {
            var rot1 = new GridRotation(1, true);
            var rot2 = new GridRotation(1, true);
            var rot3 = new GridRotation(1);
            
            Assert.Equal(rot1, rot2);
            Assert.True(rot1 == rot2);
            Assert.NotEqual(rot1, rot3);
            Assert.True(rot1 != rot3);
        }
    }
}