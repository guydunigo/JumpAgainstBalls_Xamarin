using System;

namespace JumpAgainstBalls_Xamarin
{
    static class Tools
    {
        public const int STEPTIME = 20;
        public const long MAX_TIME = 30000;
        public const int ACCEL_Y = 10;
        public const int ACCEL_X_COEF = -3;

        public const double TIMEFACTOR = 1/50.0;
        public const int LIMIT_CELL = 1;
        public const double BALLS_BOUNCE_COEF = 1.5f;

        public static double GetNorm(double[] vect)
        {
            return Math.Sqrt(vect[0] * vect[0] + vect[1] * vect[1]);
        }
        public static double[] GetVector(double[] pt0, double[] pt1)
        {
            return new double[] { pt1[0] - pt0[0], pt1[1] - pt0[1] };
        }

        public static double[] GetUnitVector(double[] pt0, double[] pt1)
        {
            double[] normal = GetVector(pt0, pt1);
            double norm = GetNorm(normal);
            return new double[] { normal[0] / norm, normal[1] / norm };
        }

        public static double[] GetUnitVector(double[] normal, double norm)
        {
            return new double[] { normal[0] / norm, normal[1] / norm };
        }

        public static long ConvertHeight(double height, double y)
        {
            return (long)(-(y - height) / 10);
        }

        public static String ConvertHeightToM(double height, double y)
        {
            return ConvertHeight(height, y).ToString() + "m";
        }
    }
}
