using System;

namespace PonderingProgrammer.GridMath
{
    public static class RandomExtensions
    {
        public static int RandRange(this Random rand, int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentException("max must be greater than min");
            }
            return min + rand.Next(max - min);
        }

        public static float RandRange(this Random rand, float min, float max)
        {
            if (min >= max)
            {
                throw new ArgumentException("max must be greater than min");
            }
            return (float)(min + rand.NextDouble() * (max - min));
        }

        public static double RandRange(this Random rand, double min, double max)
        {
            if (min >= max)
            {
                throw new ArgumentException("max must be greater than min");
            }
            return min + rand.NextDouble() * (max - min);
        }

        public static bool Chance(this Random rand, double chance)
        {
            return rand.NextDouble() < chance;
        }
    }
}
