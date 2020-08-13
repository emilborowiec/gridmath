using System;

namespace PonderingProgrammer.GridMath
{
    public readonly struct GridPolarCoordinates
    {
        private const double TwoPi = Math.PI * 2;

        public static GridPolarCoordinates FromGridCartesian(GridCoordinatePair coords)
        {
            return FromGridCartesian(coords.X, coords.Y);
        }
        
        public static GridPolarCoordinates FromGridCartesian(int x, int y)
        {
            var r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var t = Math.Atan2(-y, x);
            if (t < 0) t += TwoPi;
            return new GridPolarCoordinates(t, r);
        }
        
        public readonly double Theta;
        public readonly double Radius;

        public GridPolarCoordinates(double theta, double radius)
        {
            Theta = theta;
            Radius = radius;
        }

        public GridPolarCoordinates Rotation(double rotAngle)
        {
            var newAngle = Theta + rotAngle;
            if (newAngle < 0) newAngle += TwoPi;
            if (newAngle > TwoPi) newAngle -= TwoPi;
            return new GridPolarCoordinates(newAngle, Radius);
        }

        public GridCoordinatePair ToGridCartesian()
        {
            // Need to flip y because grid coord system goes downwards.
            // Flip must be done after mapping from real to grid, because of flooring
            var x = RealToGrid.ToGrid(Radius * Math.Cos(Theta));
            var y = -RealToGrid.ToGrid(Radius * Math.Sin(Theta));
            return new GridCoordinatePair(x, y);
        }
    }
}