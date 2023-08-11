#region

using System;
using System.Collections.Generic;

#endregion

namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public class GridRectangle : IGridRectangle
    {
        public GridRectangle(int minX, int minY, int width, int height)
        {
            _boundingBox = GridBoundingBox.FromSize(minX, minY, width, height);
        }

        public GridRectangle(GridBoundingBox boundingBox)
        {
            _boundingBox = boundingBox;
        }

        private GridBoundingBox _boundingBox;

        public GridBoundingBox BoundingBox => _boundingBox;

        public IEnumerable<XYGridCoordinate> Interior
        {
            get
            {
                for (var y = _boundingBox.MinY; y < _boundingBox.MaxYExcl; y++)
                for (var x = _boundingBox.MinX; x < _boundingBox.MaxXExcl; x++)
                {
                    yield return new XYGridCoordinate(x, y);
                }
            }
        }

        public IEnumerable<XYGridCoordinate> Edge
        {
            get
            {
                for (var x = _boundingBox.MinX; x < _boundingBox.MaxXExcl; x++)
                {
                    yield return new XYGridCoordinate(x, MinY);
                }

                for (var y = _boundingBox.MinY + 1; y < _boundingBox.MaxYExcl; y++)
                {
                    yield return new XYGridCoordinate(MaxX, y);
                }

                if (Height > 1)
                {
                    for (var x = _boundingBox.MaxX - 1; x >= _boundingBox.MinX; x--)
                    {
                        yield return new XYGridCoordinate(x, MaxY);
                    }
                }

                if (Width > 1)
                {
                    for (var y = _boundingBox.MaxY - 1; y > _boundingBox.MinY; y--)
                    {
                        yield return new XYGridCoordinate(MinX, y);
                    }
                }
            }
        }

        public int MinX
        {
            get => _boundingBox.MinX;
            set => _boundingBox = _boundingBox.SetMinX(value);
        }

        public int MinY
        {
            get => _boundingBox.MinY;
            set => _boundingBox = _boundingBox.SetMinY(value);
        }

        public int MaxX
        {
            get => _boundingBox.MaxX;
            set => _boundingBox = _boundingBox.SetMaxX(value);
        }

        public int MaxY
        {
            get => _boundingBox.MaxY;
            set => _boundingBox = _boundingBox.SetMaxY(value);
        }

        public int Width
        {
            get => _boundingBox.Width;
            set => _boundingBox = _boundingBox.SetWidth(value);
        }

        public int Height
        {
            get => _boundingBox.Height;
            set => _boundingBox = _boundingBox.SetHeight(value);
        }

        public XYGridCoordinate TopLeft
        {
            get => _boundingBox.TopLeft;
            set => throw new NotImplementedException();
        }

        public bool Contains(XYGridCoordinate position)
        {
            return _boundingBox.Contains(position);
        }

        public bool Contains(int x, int y)
        {
            return _boundingBox.Contains(x, y);
        }

        public bool Overlaps(GridBoundingBox boundingBox)
        {
            return _boundingBox.Overlaps(boundingBox);
        }

        public void Translate(int x, int y)
        {
            _boundingBox = _boundingBox.Translation(x, y);
        }

        public void Rotate(GridRotation rotation)
        {
            var c = _boundingBox.Center;
            var centeredBox = _boundingBox.Translation(-c.X, -c.Y);

            var polarTopLeft = GridPolarCoordinates.FromGridCartesian(centeredBox.TopLeft);
            var polarBotRight = GridPolarCoordinates.FromGridCartesian(centeredBox.BottomRight);
            
            var c1 = polarTopLeft
                     .Rotation(rotation.ToRadians(4))
                     .ToGridCartesian()
                     .Translation(c.X, c.Y);
            var c2 = polarBotRight
                     .Rotation(rotation.ToRadians(4))
                     .ToGridCartesian()
                     .Translation(c.X, c.Y);
            
            _boundingBox = GridBoundingBox.FromMinMax(Math.Min(c1.X, c2.X), Math.Min(c1.Y, c2.Y), Math.Max(c1.X, c2.X), Math.Max(c1.Y, c2.Y));
        }

        public void Flip(OrthogonalGridAxis axis)
        {
            // Done
        }
    }
}