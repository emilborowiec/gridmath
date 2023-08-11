#region

using GridMath.Grids;
using GridMath.Grids.LineGrids;
using System;

#endregion

namespace GridMath
{
    /// <summary>
    ///     A GridBoundingBox is an Axis-Aligned Bounding Box on a space of integer numbers.
    /// </summary>
    /// <remarks>
    ///     It is modelled by a pair of <c>GridInteravl</c>s, one for X and one for Y axis.
    /// </remarks>
    public readonly struct GridBoundingBox : IEquatable<GridBoundingBox>
    {
        public static GridBoundingBox FromMinMax(XYGridCoordinate topLeft, XYGridCoordinate bottomRight)
        {
            return FromMinMax(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);
        }

        public static GridBoundingBox FromMinMax(int minX, int minY, int maxX, int maxY)
        {
            if (minX > maxX || minY > maxY) throw new ArgumentException("min cannot be greater than max");
            var xInterval = new IntegerInterval(minX, maxX);
            var yInterval = new IntegerInterval(minY, maxY);
            return new GridBoundingBox(xInterval, yInterval);
        }

        public static GridBoundingBox FromMinMaxExcl(int minX, int minY, int maxXExcl, int maxYExcl)
        {
            if (maxXExcl <= minX || maxYExcl <= minY)
            {
                throw new ArgumentException("Max is exclusive and must be greater than Min");
            }

            var xInterval = IntegerIntervalUtils.FromExclusiveMax(minX, maxXExcl);
            var yInterval = IntegerIntervalUtils.FromExclusiveMax(minY, maxYExcl);
            return new GridBoundingBox(xInterval, yInterval);
        }

        public static GridBoundingBox FromSize(int minX, int minY, int width, int height)
        {
            if (width < 1 || height < 1) throw new ArgumentException("Size must be greater than 0");
            var xInterval = IntegerIntervalUtils.FromExclusiveMax(minX, minX + width);
            var yInterval = IntegerIntervalUtils.FromExclusiveMax(minY, minY + height);
            return new GridBoundingBox(xInterval, yInterval);
        }

        public static bool operator ==(GridBoundingBox left, GridBoundingBox right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridBoundingBox left, GridBoundingBox right)
        {
            return !(left == right);
        }

        public GridBoundingBox(IntegerInterval xInterval, IntegerInterval yInterval)
        {
            XInterval = xInterval;
            YInterval = yInterval;
            TopLeft = new XYGridCoordinate(XInterval.Min, YInterval.Min);
            TopRight = new XYGridCoordinate(XInterval.Max, YInterval.Min);
            BottomRight = new XYGridCoordinate(XInterval.Max, YInterval.Max);
            BottomLeft = new XYGridCoordinate(XInterval.Min, YInterval.Max);
        }

        public IntegerInterval XInterval { get; }
        public IntegerInterval YInterval { get; }
        public XYGridCoordinate TopLeft { get; }
        public XYGridCoordinate TopRight { get; }
        public XYGridCoordinate BottomRight { get; }
        public XYGridCoordinate BottomLeft { get; }

        public int MinX => XInterval.Min;
        public int MinY => YInterval.Min;
        public int MaxX => XInterval.Max;
        public int MaxY => YInterval.Max;
        public int Width => XInterval.Length;
        public int Height => YInterval.Length;
        public int MaxXExcl => XInterval.MaxExcl;
        public int MaxYExcl => YInterval.MaxExcl;
        public XYGridCoordinate Center => new XYGridCoordinate(XInterval.Center, YInterval.Center);

        public bool Contains(int x, int y)
        {
            return XInterval.Contains(x) && YInterval.Contains(y);
        }

        public bool Contains(XYGridCoordinate coordinate)
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
            return (XInterval.Touches(other.XInterval) &&
                    (YInterval.Overlaps(other.YInterval) || YInterval.Touches(other.YInterval)))
                   || (YInterval.Touches(other.YInterval) &&
                       (XInterval.Overlaps(other.XInterval) || XInterval.Touches(other.XInterval)));
        }

        public XYGridCoordinate NearestPoint(int x, int y)
        {
            var xDistance = XInterval.Distance(x);
            var yDistance = YInterval.Distance(y);

            if (xDistance == 0 && yDistance == 0) return new XYGridCoordinate(x, y);

            if (xDistance == 0)
            {
                return new XYGridCoordinate(x, yDistance < 0 ? MinY : MaxY);
            }

            if (yDistance == 0)
            {
                return new XYGridCoordinate(xDistance < 0 ? MinX : MaxX, y);
            }

            return new XYGridCoordinate(xDistance < 0 ? MinX : MaxX, yDistance < 0 ? MinY : MaxY);
        }

        public GridBoundingBox Translation(int x, int y)
        {
            return new GridBoundingBox(XInterval.Translation(x), YInterval.Translation(y));
        }

        public GridBoundingBox SetPosition(
            int x,
            int y,
            IntervalAnchor xAnchor,
            IntervalAnchor yAnchor,
            int xOffset = 0,
            int yOffset = 0)
        {
            return new GridBoundingBox(
                XInterval.SetPosition(x, xAnchor, xOffset),
                YInterval.SetPosition(y, yAnchor, yOffset));
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

        public GridBoundingBox SetWidth(int value)
        {
            return FromSize(MinX, MinY, value, Height);
        }

        public GridBoundingBox SetHeight(int value)
        {
            return FromSize(MinX, MinY, Width, value);
        }

        public GridBoundingBox Relate(
            GridBoundingBox other,
            SpatialRelation xRelation,
            SpatialRelation yRelation,
            int xOffset = 0,
            int yOffset = 0)
        {
            return new GridBoundingBox(
                XInterval.Relate(other.XInterval, xRelation, xOffset),
                YInterval.Relate(other.YInterval, yRelation, yOffset));
        }

        public GridBoundingBox PlaceBeside(GridBoundingBox other, Grid4Direction direction)
        {
            return direction switch
            {
                Grid4Direction.Top => Relate(other, SpatialRelation.CenterToCenter(), SpatialRelation.EndToStart(), 0, -1),
                Grid4Direction.Right => Relate(other, SpatialRelation.StartToEnd(), SpatialRelation.CenterToCenter(), 1),
                Grid4Direction.Bottom => Relate(other, SpatialRelation.CenterToCenter(), SpatialRelation.StartToEnd(), 0, 1),
                Grid4Direction.Left => Relate(other, SpatialRelation.EndToStart(), SpatialRelation.CenterToCenter(), -1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
            };
        }

        public bool Equals(GridBoundingBox other)
        {
            return XInterval.Equals(other.XInterval) && YInterval.Equals(other.YInterval);
        }

        public override bool Equals(object obj)
        {
            return obj is GridBoundingBox other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(XInterval, YInterval);
        }
    }
}