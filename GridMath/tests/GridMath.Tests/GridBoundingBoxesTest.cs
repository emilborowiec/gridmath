#region

using Xunit;

#endregion

namespace GridMath.Tests
{
    public class GridBoundingBoxesTest
    {
        [Fact]
        public void TestFindCenterOfMass()
        {
            var b1 = GridBoundingBox.FromMinMax(1, 1, 3, 3);
            var b2 = GridBoundingBox.FromMinMax(3, 3, 5, 5);
            var b3 = GridBoundingBox.FromMinMax(1, 5, 3, 7);
            var b4 = GridBoundingBox.FromMinMax(1, 10, 3, 12);

            var boxes = new[] {b1, b2, b3, b4};

            var center = GridBoundingBoxes.FindCenterOfMass(boxes);

            Assert.Equal(2, center.X);
            Assert.Equal(5, center.Y);
        }

        [Fact]
        public void TestFindOverlappingBoxes()
        {
            var b1 = GridBoundingBox.FromMinMax(1, 1, 3, 3);
            var b2 = GridBoundingBox.FromMinMax(3, 3, 5, 5);
            var b3 = GridBoundingBox.FromMinMax(1, 5, 3, 7);
            var b4 = GridBoundingBox.FromMinMax(1, 10, 3, 12);

            var boxes = new[] {b1, b2, b3, b4};

            var overlappingGroups = GridBoundingBoxes.FindOverlappingBoxes(boxes);

            Assert.Equal(2, overlappingGroups.Count);
            Assert.Equal(2, overlappingGroups[0].Count);
            Assert.Equal(2, overlappingGroups[1].Count);
        }

        [Fact]
        public void TestPack()
        {
            var b1 = GridBoundingBox.FromMinMax(1, 1, 3, 3);
            var b2 = GridBoundingBox.FromMinMax(3, 3, 5, 5);
            var b3 = GridBoundingBox.FromMinMax(1, 5, 3, 7);
            var b4 = GridBoundingBox.FromMinMax(1, 10, 3, 12);

            var boxes = new[] {b1, b2, b3, b4};

            GridBoundingBoxes.Pack(boxes);
            Assert.Empty(GridBoundingBoxes.FindOverlappingBoxes(boxes));
        }
    }
}