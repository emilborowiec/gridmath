#region

using System;

#endregion

namespace PonderingProgrammer.GridMath.Algorithms
{
    public static class Octants
    {
        public static Octant GetOctant(int dx, int dy)
        {
            if (dx == 0.0 && dy == 0.0)
            {
                throw new ArgumentException("Cannot compute the octant for point ( " + dx + ", " + dy + " )");
            }

            double adx = Math.Abs(dx);
            double ady = Math.Abs(dy);

            if (dx >= 0) // right
            {
                if (dy >= 0) // down-right
                {
                    return adx >= ady ? Octant.Zero : Octant.One;
                }

                return adx >= ady ? Octant.Seven : Octant.Six;
            }

            if (dy >= 0) // down-left
            {
                return adx >= ady ? Octant.Three : Octant.Two;
            }

            return adx >= ady ? Octant.Four : Octant.Five;
        }
    }
}