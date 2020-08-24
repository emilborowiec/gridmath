#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    public static class Directions
    {
        public const double TwoPi = Math.PI * 2;
        public const double Degree0 = 0;
        public const double Degree45 = Math.PI * 0.25;
        public const double Degree90 = Math.PI * 0.5;
        public const double Degree135 = Math.PI * 0.75;
        public const double Degree180 = Math.PI;
        public const double Degree225 = Math.PI * 1.25;
        public const double Degree270 = Math.PI * 1.5;
        public const double Degree315 = Math.PI * 1.75;

        public const double Right = Degree0;
        public const double BottomRight = Degree45;
        public const double Bottom = Degree90;
        public const double BottomLeft = Degree135;
        public const double Left = Degree180;
        public const double TopLeft = Degree225;
        public const double Top = Degree270;
        public const double TopRight = Degree315;

        public static double DirectionToAngle(Grid4Direction direction)
        {
            return direction switch
            {
                Grid4Direction.Top => Top,
                Grid4Direction.Right => Right,
                Grid4Direction.Bottom => Bottom,
                Grid4Direction.Left => Left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
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
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
            };
        }

        public static Grid4Direction AngleToDirection4(double angle)
        {
            angle = WrapAngle(angle);

            if (angle >= BottomRight || angle < TopRight) return Grid4Direction.Right;
            if (angle >= TopRight || angle < TopLeft) return Grid4Direction.Top;
            if (angle >= TopLeft || angle < BottomLeft) return Grid4Direction.Left;
            return Grid4Direction.Bottom;
        }

        public static Grid8Direction AngleToDirection8(double angle)
        {
            angle = WrapAngle(angle);

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
            var rotAngle = Grid4RotationToAngle(rotation);
            var newAngle = DirectionToAngle(direction) + rotAngle;
            return AngleToDirection4(newAngle);
        }

        public static Grid8Direction Rotate(Grid8Direction direction, Grid4Rotation rotation)
        {
            var rotAngle = Grid4RotationToAngle(rotation);
            var newAngle = DirectionToAngle(direction) + rotAngle;
            return AngleToDirection8(newAngle);
        }

        public static double Grid4RotationToAngle(Grid4Rotation rotation)
        {
            var rotAngle = rotation switch
            {
                Grid4Rotation.Ccw90 => Top,
                Grid4Rotation.Cw90 => Bottom,
                _ => 0,
            };
            return rotAngle;
        }

        public static double WrapAngle(double angle)
        {
            return angle - (TwoPi * Math.Floor(angle / TwoPi));
        }
    }
}