#region

using System;
using System.Collections.Generic;
using System.Linq;
using PonderingProgrammer.GridMath.Algorithms;

#endregion

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridFan : IGridFan
    {
        public GridFan(GridCoordinatePair origin, int radius, Grid8Direction direction)
        {
            _origin = origin;
            _radius = radius;
            _direction = direction;
        }

        private Grid8Direction _direction;
        private GridCoordinatePair _origin;
        private int _radius;

        public IEnumerable<GridCoordinatePair> Interior
        {
            get
            {
                var edges = Edge.ToArray();
                var fill = FloodFill.GetFloodFillCoordinates(BoundingBox.Center, edges, BoundingBox);
                var interior = new List<GridCoordinatePair>(edges);
                interior.AddRange(fill);
                return interior;
            }
        }

        public IEnumerable<GridCoordinatePair> Edge
        {
            get
            {
                var circle = Bresenham.PlotCircle(_origin.X, _origin.Y, _radius);
                var partCircle = circle.Where(c => BoundingBox.Contains(c));
                var (arm1End, arm2End) = GetPolarInterval();
                var arm1EndCart = arm1End.ToGridCartesian().Translation(_origin.X, _origin.Y);
                var arm2EndCart = arm2End.ToGridCartesian().Translation(_origin.X, _origin.Y);
                var arm1 = new GridSegment(_origin, arm1EndCart).Edge.ToList();
                var arm2 = new GridSegment(_origin, arm2EndCart).Edge.ToList();
                arm1.Remove(arm1EndCart);
                arm2.Remove(arm2EndCart);
                arm2.Remove(_origin);
                var edge = new List<GridCoordinatePair>(partCircle);
                edge.AddRange(arm1);
                edge.AddRange(arm2);
                
                return edge;
            }
        }

        public GridBoundingBox BoundingBox
        {
            get
            {
                var minX = 0;
                var minY = 0;
                var maxX = 0;
                var maxY = 0;
                switch (_direction)
                {
                    case Grid8Direction.TopLeft:
                        minX = _origin.X - _radius;
                        maxX = _origin.X;
                        minY = _origin.Y - _radius;
                        maxY = _origin.Y;
                        break;
                    case Grid8Direction.Top:
                        minX = _origin.X - (_radius/2);
                        maxX = _origin.X + (_radius/2);
                        minY = _origin.Y - _radius;
                        maxY = _origin.Y;
                        break;
                    case Grid8Direction.TopRight:
                        minX = _origin.X;
                        maxX = _origin.X + _radius;
                        minY = _origin.Y - _radius;
                        maxY = _origin.Y;
                        break;
                    case Grid8Direction.Right:
                        minX = _origin.X;
                        maxX = _origin.X + _radius;
                        minY = _origin.Y - (_radius/2);
                        maxY = _origin.Y + (_radius/2);
                        break;
                    case Grid8Direction.BottomRight:
                        minX = _origin.X;
                        maxX = _origin.X + _radius;
                        minY = _origin.Y;
                        maxY = _origin.Y + _radius;
                        break;
                    case Grid8Direction.Bottom:
                        minX = _origin.X - (_radius/2);
                        maxX = _origin.X + (_radius/2);
                        minY = _origin.Y;
                        maxY = _origin.Y + _radius;
                        break;
                    case Grid8Direction.BottomLeft:
                        minX = _origin.X - _radius;
                        maxX = _origin.X;
                        minY = _origin.Y;
                        maxY = _origin.Y + _radius;
                        break;
                    case Grid8Direction.Left:
                        minX = _origin.X - _radius;
                        maxX = _origin.X;
                        minY = _origin.Y - (_radius/2);
                        maxY = _origin.Y + (_radius/2);
                        break;
                }
                return GridBoundingBox.FromMinMax(minX, minY, maxX, maxY);
            }
        }

        public GridCoordinatePair Origin
        {
            get => _origin;
            set
            {
                _origin = value;
            }
        }

        public Grid8Direction Direction { get; set; }

        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
            }
        }

        public bool Contains(GridCoordinatePair position)
        {
            return Contains(position.X, position.Y);
        }

        public bool Contains(int x, int y)
        {
            var squareRadius = _radius * _radius;
            return BoundingBox.Contains(x, y) && _origin.Sed(x, y) <= squareRadius;
        }

        public bool Overlaps(GridBoundingBox boundingBox)
        {
            // todo: this is potentially expensive operation - write better algorithm
            return Interior.Any(boundingBox.Contains);
        }

        public void Translate(int x, int y)
        {
            _origin = _origin.Translation(x, y);
        }

        public void Rotate(Grid4Rotation rotation)
        {
            _direction = Directions.Rotate(_direction, rotation);
        }

        public void Flip(GridAxis axis)
        {
            _direction = axis switch
            {
                GridAxis.Horizontal when _direction == Grid8Direction.TopLeft => Grid8Direction.BottomLeft,
                GridAxis.Horizontal when _direction == Grid8Direction.Top => Grid8Direction.Bottom,
                GridAxis.Horizontal when _direction == Grid8Direction.TopRight => Grid8Direction.BottomRight,
                GridAxis.Horizontal when _direction == Grid8Direction.BottomLeft => Grid8Direction.TopLeft,
                GridAxis.Horizontal when _direction == Grid8Direction.Bottom => Grid8Direction.Top,
                GridAxis.Horizontal when _direction == Grid8Direction.BottomRight => Grid8Direction.TopRight,
                GridAxis.Vertical when _direction == Grid8Direction.TopLeft => Grid8Direction.TopRight,
                GridAxis.Vertical when _direction == Grid8Direction.Left => Grid8Direction.Right,
                GridAxis.Vertical when _direction == Grid8Direction.BottomLeft => Grid8Direction.BottomRight,
                GridAxis.Vertical when _direction == Grid8Direction.TopRight => Grid8Direction.TopLeft,
                GridAxis.Vertical when _direction == Grid8Direction.Right => Grid8Direction.Left,
                GridAxis.Vertical when _direction == Grid8Direction.BottomRight => Grid8Direction.BottomRight,
                _ => _direction,
            };
        }

        public (GridPolarCoordinates start, GridPolarCoordinates end) GetPolarInterval()
        {
            GridPolarCoordinates start;
            GridPolarCoordinates end;
            switch (_direction)
            {
                case Grid8Direction.TopRight:
                    start = new GridPolarCoordinates(0, _radius);
                    end = new GridPolarCoordinates(Math.PI * 0.5, _radius);
                    break;
                case Grid8Direction.Top:
                    start = new GridPolarCoordinates(Math.PI * 0.25, _radius);
                    end = new GridPolarCoordinates(Math.PI * 0.75, _radius);
                    break;
                case Grid8Direction.TopLeft:
                    start = new GridPolarCoordinates(Math.PI * 0.5, _radius);
                    end = new GridPolarCoordinates(Math.PI, _radius);
                    break;
                case Grid8Direction.Left:
                    start = new GridPolarCoordinates(Math.PI * 0.75, _radius);
                    end = new GridPolarCoordinates(Math.PI * 1.25, _radius);
                    break;
                case Grid8Direction.BottomLeft:
                    start = new GridPolarCoordinates(Math.PI, _radius);
                    end = new GridPolarCoordinates(Math.PI * 1.5, _radius);
                    break;
                case Grid8Direction.Bottom:
                    start = new GridPolarCoordinates(Math.PI * 1.25, _radius);
                    end = new GridPolarCoordinates(Math.PI * 1.75, _radius);
                    break;
                case Grid8Direction.BottomRight:
                    start = new GridPolarCoordinates(Math.PI * 1.5, _radius);
                    end = new GridPolarCoordinates(Math.PI * 2, _radius);
                    break;
                case Grid8Direction.Right:
                    start = new GridPolarCoordinates(Math.PI * 1.75, _radius);
                    end = new GridPolarCoordinates(Math.PI * 0.25, _radius);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (start, end);
        }
    }
}