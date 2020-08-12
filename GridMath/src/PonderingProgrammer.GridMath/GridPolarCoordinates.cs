using System;

namespace PonderingProgrammer.GridMath
{
    public readonly struct GridPolarCoordinates
    {
        public static GridPolarCoordinates FromGridCartesian(int x, int y)
        {
            var r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var t = Math.Atan2(y, x);
            return new GridPolarCoordinates(t, r);
        }
        
        public readonly double Theta;
        public readonly double Radius;

        public GridPolarCoordinates(double theta, double radius)
        {
            Theta = theta;
            Radius = radius;
        }

        public GridCoordinatePair ToGridCartesian()
        {
            return GridCoordinatePair.FromReal(Radius * Math.Cos(Theta), Radius * Math.Sin(Theta));
        }
    }
}