﻿#region

using System;

#endregion

namespace GridMath.Samples
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
}