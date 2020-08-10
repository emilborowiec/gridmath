using System;
using System.Collections.Generic;
using System.Linq;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// A GridBoundingBox is an Axis-Aligned Bounding Box on a space of integer numbers.
    /// </summary>
    /// <remarks>
    /// It is modelled by a pair of <c>GridInteravl</c>s, one for X and one for Y axis.
    /// </remarks>
    public readonly struct GridBoundingBox
    {
        public readonly GridInterval XInterval;
        public readonly GridInterval YInterval;
        public readonly GridCoordinate TopLeft;
        public readonly GridCoordinate TopRight;
        public readonly GridCoordinate BottomRight;
        public readonly GridCoordinate BottomLeft;

        public static GridBoundingBox FromMinMax(int minX, int minY, int maxX, int maxY)
        {
            if (minX > maxX || minY > maxY) throw new ArgumentException($"min cannot be greater than max");
            var xInterval = new GridInterval(minX, maxX);
            var yInterval = new GridInterval(minY, maxY);
            return new GridBoundingBox(xInterval, yInterval);
        }

        public static GridBoundingBox FromMinMaxExcl(int minX, int minY, int maxXExcl, int maxYExcl)
        {
            if (maxXExcl <= minX || maxYExcl <= minY)
                throw new ArgumentException($"Max is exclusive and must be greater than Min");
            var xInterval = GridInterval.FromExclusiveMax(minX, maxXExcl);
            var yInterval = GridInterval.FromExclusiveMax(minY, maxYExcl);
            return new GridBoundingBox(xInterval, yInterval);
        }

        public static GridBoundingBox FromSize(int minX, int minY, int width, int height)
        {
            if (width < 1 || height < 1) throw new ArgumentException($"Size must be greater than 0");
            var xInterval = GridInterval.FromExclusiveMax(minX, minX + width);
            var yInterval = GridInterval.FromExclusiveMax(minY, minY + height);
            return new GridBoundingBox(xInterval, yInterval);
        }

        public GridBoundingBox(GridInterval xInterval, GridInterval yInterval)
        {
            XInterval = xInterval;
            YInterval = yInterval;
            TopLeft = new GridCoordinate(XInterval.Min, YInterval.Min);
            TopRight = new GridCoordinate(XInterval.MaxExcl - 1, YInterval.Min);
            BottomRight = new GridCoordinate(XInterval.MaxExcl - 1, YInterval.MaxExcl - 1);
            BottomLeft = new GridCoordinate(XInterval.Min, YInterval.MaxExcl - 1);
        }

        public int MinX => XInterval.Min;
        public int MinY => YInterval.Min;
        public int MaxX => XInterval.Max;
        public int MaxY => YInterval.Max;
        public int Width => XInterval.Length;
        public int Height => YInterval.Length;
        public int MaxXExcl => XInterval.MaxExcl;
        public int MaxYExcl => YInterval.MaxExcl;

        public bool Contains(int x, int y)
        {
            return XInterval.Contains(x) && YInterval.Contains(y);
        }

        public bool Contains(GridCoordinate coordinate)
        {
            return Contains(coordinate.X, coordinate.Y);
        }

        public bool Contains(GridBoundingBox box)
        {
            return XInterval.Contains(box.XInterval) && YInterval.Contains(box.YInterval);
        }

        public bool Overlaps(GridBoundingBox other)
        {
            return XInterval.Overlaps(other.XInterval) && YInterval.Overlaps(other.YInterval);
        }

        public bool Touches(GridBoundingBox other)
        {
            return (XInterval.Touches(other.XInterval) && YInterval.Overlaps(other.YInterval)) 
                   || (YInterval.Touches(other.YInterval) && XInterval.Overlaps(other.XInterval));
        }

        public GridBoundingBox Translate(int x, int y)
        {
            return new GridBoundingBox(XInterval.Translate(x), YInterval.Translate(y));
        }

        public GridBoundingBox SetPosition(int x, int y, IntervalAnchor xAnchor, IntervalAnchor yAnchor, int xOffset = 0,
            int yOffset = 0)
        {
            return new GridBoundingBox(XInterval.SetPosition(x, xAnchor, xOffset), YInterval.SetPosition(y, yAnchor, yOffset));
        }

        public GridBoundingBox SetMinX(int value)
        {
            return new GridBoundingBox(XInterval.SetMin(value), YInterval);
        }

        public GridBoundingBox SetMaxX(int value)
        {
            return new GridBoundingBox(XInterval.SetMax(value), YInterval);
        }

        public GridBoundingBox SetMinY(int value)
        {
            return new GridBoundingBox(XInterval, YInterval.SetMin(value));
        }

        public GridBoundingBox SetMaxY(int value)
        {
            return new GridBoundingBox(XInterval, YInterval.SetMax(value));
        }

        public GridBoundingBox Relate(GridBoundingBox other, Relation xRelation, Relation yRelation, int xOffset = 0,
            int yOffset = 0)
        {
            return new GridBoundingBox(XInterval.Relate(other.XInterval, xRelation, xOffset), YInterval.Relate(other.YInterval, yRelation, yOffset));
        }

        public GridBoundingBox PlaceBeside(GridBoundingBox other, Grid4Direction direction)
        {
            return direction switch
            {
                Grid4Direction.Top => Relate(other, Relation.CenterToCenter(), Relation.EndToStart(), 0, -1),
                Grid4Direction.Right => Relate(other, Relation.StartToEnd(), Relation.CenterToCenter(), 1, 0),
                Grid4Direction.Bottom => Relate(other, Relation.CenterToCenter(), Relation.StartToEnd(), 0, 1),
                Grid4Direction.Left => Relate(other, Relation.EndToStart(), Relation.CenterToCenter(), -1, 0),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
