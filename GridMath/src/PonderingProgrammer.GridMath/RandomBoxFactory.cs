using System;

namespace PonderingProgrammer.GridMath
{
    public class RandomBoxFactory
    {
        private readonly Random _rand = new Random();
        
        public GridBoundingBox RandomSizeBox(int minSize, int maxSize)
        {
            if (minSize < 1)
            {
                throw new ArgumentException("min size must be > 0");
            }
            if (maxSize < minSize)
            {
                throw new ArgumentException("maxSize must be >= minSize");
            }

            return GridBoundingBox.FromSize(0, 0, _rand.RandRange(minSize, maxSize + 1), _rand.RandRange(minSize, maxSize + 1));
        }

        public GridBoundingBox RandomSizeBoxWithinBounds(GridBoundingBox bounds, int minSize, int maxSize)
        {
            if (minSize < 1)
            {
                throw new ArgumentException("size must be > 0");
            }
            if (maxSize < minSize)
            {
                throw new ArgumentException("maxSize must be >= minSize");
            }

            var width = Math.Min(_rand.RandRange(minSize, maxSize + 1), bounds.Width);
            var height = Math.Min(_rand.RandRange(minSize, maxSize + 1), bounds.Height);
            
            var minX = _rand.RandRange(bounds.MinX, bounds.MaxXExcl - width + 1);
            var minY = _rand.RandRange(bounds.MinY, bounds.MaxYExcl - height + 1);

            return GridBoundingBox.FromSize(minX, minY, width, height);
        }
    }
}
