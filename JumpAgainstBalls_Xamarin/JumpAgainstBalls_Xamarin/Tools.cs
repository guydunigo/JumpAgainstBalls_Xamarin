using System;

namespace JumpAgainstBalls_Xamarin
{
    static class Tools
    {
        public const int STEPTIME = 20;
        public const long MAX_TIME = 30000;
        public const int ACCEL_Y = 10;
        public const int ACCEL_X_COEF = -3;

        public const float TIMEFACTOR = 1 / 50.0f;
        public const int LIMIT_CELL = 1;
        public const float BALLS_BOUNCE_COEF = 1.5f;

        public static float GetNorm(float[] vect)
        {
            return (float)Math.Sqrt(vect[0] * vect[0] + vect[1] * vect[1]);
        }
        public static float[] GetVector(float[] pt0, float[] pt1)
        {
            return new float[] { pt1[0] - pt0[0], pt1[1] - pt0[1] };
        }

        public static float[] GetUnitVector(float[] pt0, float[] pt1)
        {
            float[] normal = GetVector(pt0, pt1);
            float norm = GetNorm(normal);
            return new float[] { normal[0] / norm, normal[1] / norm };
        }

        public static float[] GetUnitVector(float[] normal, float norm)
        {
            return new float[] { normal[0] / norm, normal[1] / norm };
        }

        public static long ConvertHeight(float height, float y)
        {
            return (long)(-(y - height) / 10);
        }

        public static String ConvertHeightToM(float height, float y)
        {
            return ConvertHeight(height, y).ToString() + "m";
        }
    }
}
