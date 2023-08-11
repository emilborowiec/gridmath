﻿#region

using GridMath.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public class GridFan : IGridFan
    {
        private static XYGridCoordinate FindIntersection(
            ICollection<XYGridCoordinate> circle,
            ICollection<XYGridCoordinate> arm)
        {
            var same = arm.Where(circle.Contains).ToArray();
            if (same.Length > 0) return same[0];
            var close = arm.First(c => circle.Any(cc => SquareGridUtils.ManhattanDistance(cc, c) <= 1));
            return close;
        }

        public GridFan(XYGridCoordinate origin, int radius, Grid8Direction direction)
        {
            if (radius < 1) throw new ArgumentOutOfRangeException(nameof(radius), radius, null);

            _origin = origin;
            _radius = radius;
            _direction = direction;
        }

        private Grid8Direction _direction;
        private XYGridCoordinate _origin;
        private int _radius;

        public IEnumerable<XYGridCoordinate> Interior
        {
            get
            {
                var edges = Edge.ToArray();
                var floodStart = new GridDirectionCoordinates(_direction, 1).ToGridCartesian();
                var fill = FloodFill.GetFloodFillCoordinates(
                    _origin.Translation(floodStart.X, floodStart.Y), edges, BoundingBox);
                var interior = new List<XYGridCoordinate>(edges);
                interior.AddRange(fill);
                return interior;
            }
        }

        public IEnumerable<XYGridCoordinate> Edge
        {
            get
            {
                var bb = BoundingBox;
                var circle = Bresenham.PlotCircle(_origin.X, _origin.Y, Radius);
                var (arm1End, arm2End) = GetArmEndpoints();
                var partCircle = circle.Where(c => bb.Contains(c)).ToList();
                var arm1 = new GridSegment(_origin, arm1End).Edge.ToList();
                var arm2 = new GridSegment(_origin, arm2End).Edge.ToList();
                arm2.Remove(_origin);
                var edge = new List<XYGridCoordinate>(partCircle);
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
                var circle = Bresenham.PlotCircle(_origin.X, _origin.Y, Radius).ToList();
                var (arm1End, arm2End) = GetArmEndpoints();
                var arm1 = new GridSegment(_origin, arm1End).Edge.OrderBy(c => SquareGridUtils.ChebyshevDistance(c, _origin)).ToList();
                var arm2 = new GridSegment(_origin, arm2End).Edge.OrderBy(c => SquareGridUtils.ChebyshevDistance(c, _origin)).ToList();
                switch (_direction)
                {
                    case Grid8Direction.TopLeft:
                        minX = _origin.X - Radius;
                        maxX = _origin.X;
                        minY = _origin.Y - Radius;
                        maxY = _origin.Y;
                        break;
                    case Grid8Direction.Top:
                        minX = FindIntersection(circle, arm2).X;
                        maxX = FindIntersection(circle, arm1).X;
                        minY = _origin.Y - Radius;
                        maxY = _origin.Y;
                        break;
                    case Grid8Direction.TopRight:
                        minX = _origin.X;
                        maxX = _origin.X + Radius;
                        minY = _origin.Y - Radius;
                        maxY = _origin.Y;
                        break;
                    case Grid8Direction.Right:
                        minX = _origin.X;
                        maxX = _origin.X + Radius;
                        minY = FindIntersection(circle, arm2).Y;
                        maxY = FindIntersection(circle, arm1).Y;
                        break;
                    case Grid8Direction.BottomRight:
                        minX = _origin.X;
                        maxX = _origin.X + Radius;
                        minY = _origin.Y;
                        maxY = _origin.Y + Radius;
                        break;
                    case Grid8Direction.Bottom:
                        minX = FindIntersection(circle, arm1).X;
                        maxX = FindIntersection(circle, arm2).X;
                        minY = _origin.Y;
                        maxY = _origin.Y + Radius;
                        break;
                    case Grid8Direction.BottomLeft:
                        minX = _origin.X - Radius;
                        maxX = _origin.X;
                        minY = _origin.Y;
                        maxY = _origin.Y + Radius;
                        break;
                    case Grid8Direction.Left:
                        minX = _origin.X - Radius;
                        maxX = _origin.X;
                        minY = FindIntersection(circle, arm1).Y;
                        maxY = FindIntersection(circle, arm2).Y;
                        break;
                }

                return GridBoundingBox.FromMinMax(minX, minY, maxX, maxY);
            }
        }

        public XYGridCoordinate Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public Grid8Direction Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public int Radius
        {
            get => _radius;
            set
            {
                if (value < 1) return;
                _radius = value;
            }
        }

        public bool Contains(XYGridCoordinate position)
        {
            return Contains(position.X, position.Y);
        }

        public bool Contains(int x, int y)
        {
            var squareRadius = Radius * Radius;
            return BoundingBox.Contains(x, y) && SquareGridUtils.Sed(_origin.X, _origin.Y, x, y) <= squareRadius;
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

        public void Rotate(GridRotation rotation)
        {
            _direction = Directions.Rotate(_direction, rotation);
        }

        public void Flip(OrthogonalGridAxis axis)
        {
            _direction = axis switch
            {
                OrthogonalGridAxis.Horizontal when _direction == Grid8Direction.TopLeft => Grid8Direction.BottomLeft,
                OrthogonalGridAxis.Horizontal when _direction == Grid8Direction.Top => Grid8Direction.Bottom,
                OrthogonalGridAxis.Horizontal when _direction == Grid8Direction.TopRight => Grid8Direction.BottomRight,
                OrthogonalGridAxis.Horizontal when _direction == Grid8Direction.BottomLeft => Grid8Direction.TopLeft,
                OrthogonalGridAxis.Horizontal when _direction == Grid8Direction.Bottom => Grid8Direction.Top,
                OrthogonalGridAxis.Horizontal when _direction == Grid8Direction.BottomRight => Grid8Direction.TopRight,
                OrthogonalGridAxis.Vertical when _direction == Grid8Direction.TopLeft => Grid8Direction.TopRight,
                OrthogonalGridAxis.Vertical when _direction == Grid8Direction.Left => Grid8Direction.Right,
                OrthogonalGridAxis.Vertical when _direction == Grid8Direction.BottomLeft => Grid8Direction.BottomRight,
                OrthogonalGridAxis.Vertical when _direction == Grid8Direction.TopRight => Grid8Direction.TopLeft,
                OrthogonalGridAxis.Vertical when _direction == Grid8Direction.Right => Grid8Direction.Left,
                OrthogonalGridAxis.Vertical when _direction == Grid8Direction.BottomRight => Grid8Direction.BottomRight,
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
                    start = new GridPolarCoordinates(Directions.Right, Radius);
                    end = new GridPolarCoordinates(Directions.Top, Radius);
                    break;
                case Grid8Direction.Top:
                    start = new GridPolarCoordinates(Directions.TopRight, Radius);
                    end = new GridPolarCoordinates(Directions.TopLeft, Radius);
                    break;
                case Grid8Direction.TopLeft:
                    start = new GridPolarCoordinates(Directions.Top, Radius);
                    end = new GridPolarCoordinates(Directions.Left, Radius);
                    break;
                case Grid8Direction.Left:
                    start = new GridPolarCoordinates(Directions.TopLeft, Radius);
                    end = new GridPolarCoordinates(Directions.BottomLeft, Radius);
                    break;
                case Grid8Direction.BottomLeft:
                    start = new GridPolarCoordinates(Directions.Left, Radius);
                    end = new GridPolarCoordinates(Directions.Bottom, Radius);
                    break;
                case Grid8Direction.Bottom:
                    start = new GridPolarCoordinates(Directions.BottomLeft, Radius);
                    end = new GridPolarCoordinates(Directions.BottomRight, Radius);
                    break;
                case Grid8Direction.BottomRight:
                    start = new GridPolarCoordinates(Directions.Bottom, Radius);
                    end = new GridPolarCoordinates(Directions.Right, Radius);
                    break;
                case Grid8Direction.Right:
                    start = new GridPolarCoordinates(Directions.BottomRight, Radius);
                    end = new GridPolarCoordinates(Directions.TopRight, Radius);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null);
            }

            return (start, end);
        }

        private (XYGridCoordinate arm1EndCart, XYGridCoordinate arm2EndCart) GetArmEndpoints()
        {
            var (arm1End, arm2End) = GetPolarInterval();
            var arm1EndCart = arm1End.ToGridCartesian().Translation(_origin.X, _origin.Y);
            var arm2EndCart = arm2End.ToGridCartesian().Translation(_origin.X, _origin.Y);
            return (arm1EndCart, arm2EndCart);
        }
    }
}