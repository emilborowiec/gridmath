using System;

namespace PonderingProgrammer.GridMath
{
    public static class Directions
    {
        private const double TwoPi = Math.PI * 2;
        public static readonly double Top = Math.PI / 2;
        public static readonly double Right = 0;
        public static readonly double Bottom = Math.PI * 1.5;
        public static readonly double Left = Math.PI;
        public static readonly double TopLeft = Math.PI * 0.75;
        public static readonly double TopRight = Math.PI * 0.25;
        public static readonly double BottomRight = Math.PI * 1.75;
        public static readonly double BottomLeft = Math.PI * 1.25;
        
        public static double DirectionToAngle(Grid4Direction direction)
        {
            return direction switch
            {
                Grid4Direction.Top => Top,
                Grid4Direction.Right => Right,
                Grid4Direction.Bottom => Bottom,
                Grid4Direction.Left => Left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        public static double DirectionToAngle(Grid8Direction direction)
        {
            return direction switch
            {
                Grid8Direction.Top => Top,
                Grid8Direction.Right => Right,
                Grid8Direction.Bottom => Bottom,
                Grid8Direction.Left => Left,
                Grid8Direction.TopLeft => TopLeft,
                Grid8Direction.TopRight => TopRight,
                Grid8Direction.BottomRight => BottomRight,
                Grid8Direction.BottomLeft => BottomLeft,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Grid4Direction AngleToDirection4(double angle)
        {
            if (angle < 0 || angle > TwoPi) throw new ArgumentOutOfRangeException();
            
            if (angle >= BottomRight || angle < TopRight) return Grid4Direction.Right;
            if (angle >= TopRight || angle < TopLeft) return Grid4Direction.Top;
            if (angle >= TopLeft || angle < BottomLeft) return Grid4Direction.Left;
            return Grid4Direction.Bottom;
        }

        public static Grid8Direction AngleToDirection8(double angle)
        {
            if (angle < 0 || angle > TwoPi) throw new ArgumentOutOfRangeException();

            if (angle >= BottomRight || angle < TopRight) return Grid8Direction.Right;
            if (angle >= Right || angle < Top) return Grid8Direction.TopRight;
            if (angle >= TopRight || angle < TopLeft) return Grid8Direction.Top;
            if (angle >= Top || angle < Left) return Grid8Direction.TopLeft;
            if (angle >= TopLeft || angle < BottomLeft) return Grid8Direction.Left;
            if (angle >= Left || angle < Bottom) return Grid8Direction.BottomLeft;
            if (angle >= Bottom || angle < Right) return Grid8Direction.BottomRight;
            return Grid8Direction.Bottom;
        }

        public static Grid4Direction Rotate(Grid4Direction direction, Grid4Rotation rotation)
        {
            var rotAngle = RotationToAngle(rotation);
            var newAngle = DirectionToAngle(direction) + rotAngle;
            if (newAngle < 0) newAngle += TwoPi;
            if (newAngle > TwoPi) newAngle -= TwoPi;
            return AngleToDirection4(newAngle);
        }

        public static Grid8Direction Rotate(Grid8Direction direction, Grid4Rotation rotation)
        {
            var rotAngle = RotationToAngle(rotation);
            var newAngle = DirectionToAngle(direction) + rotAngle;
            if (newAngle < 0) newAngle += TwoPi;
            if (newAngle > TwoPi) newAngle -= TwoPi;
            return AngleToDirection8(newAngle);
        }

        public static double RotationToAngle(Grid4Rotation rotation)
        {
            var rotAngle = rotation switch
            {
                Grid4Rotation.NinetyCcw => Top,
                Grid4Rotation.OneEighty => Left,
                Grid4Rotation.NinetyCw => Bottom,
                _ => 0
            };
            return rotAngle;
        }
    }
}