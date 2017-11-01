using System;
using System.Collections.Generic;

namespace JumpAgainstBalls_Xamarin
{
    class BallPlayer : Ball
    {
        private double[] cel;

        private double maxHeight;

        public override double Y
        {
            get => base.Y;
            set
            {
                base.Y = value;
                if (value < maxHeight)
                {
                    maxHeight = value;
                }
            }
        }

        public BallPlayer(double radius = 100): base(radius)
        {
            cel = new double[] { 0, 0 };
            maxHeight = 0;
        }

        public String ConvertMaxHeightToM(double height)
        {
            return Tools.ConvertHeightToM(height, maxHeight);
        }

        public String ConvertHeightToM(double height)
        {
            return Tools.ConvertHeightToM(height, Y);
        }

        public bool Step(double dt, double[] gravity, double width, double height, List<Ball> balls)
        {
            bool collided = false;

            if (width > 0 && height > 0)
            {
                double[] normal = { 0, 0 };
                double[] unorm = { 0, 0 };
                double norm = 0;
                
                double[] new_pos = { X, Y };
                double[] other_pos = { X, Y };
                double[] new_cel = { X, Y };
                double tmp_cel = 0;

                int i = 0;

                for (i = 0; i < 2; i++)
                {
                    new_cel[i] = cel[i] + gravity[i] * dt;
                    new_pos[i] = Pos[i] + new_cel[i] * dt;
                }

                // Bouncing against other balls
                foreach (Ball other in balls)
                {
                    other_pos = other.Pos;
                    normal = Tools.GetVector(other_pos, new_pos);
                    norm = Tools.GetNorm(normal);
                    double radSum = Radius + other.Radius;
                    if (norm < radSum)
                    {
                        unorm = Tools.GetUnitVector(normal, norm);

                        tmp_cel = (-1) * Tools.BALLS_BOUNCE_COEF * (new_cel[0] * unorm[0] + new_cel[1] * unorm[1]);
                        for (i = 0; i < 2; i++)
                        {
                            new_cel[i] = tmp_cel * unorm[i];
                            new_pos[i] += unorm[i] * 2 * (radSum - norm);
                        }

                        collided = true;
                    }
                }

                // Colliding against walls
                if (new_pos[0] - Radius < 0 || new_pos[0] + Radius > width || new_pos[1] + Radius > height)
                {
                    if (new_pos[0] - Radius < 0)
                    { // Left
                        new_pos[0] = Radius;
                        new_cel[0] = 0;
                    }
                    else if (new_pos[0] + Radius > width)
                    { // Right
                        new_pos[0] = width - Radius;
                        new_cel[0] = 0;
                    }

                    if (new_pos[1] + Radius > height)
                    { // Bottom
                        new_pos[1] = height - Radius;
                        new_cel[1] = - new_cel[1];
                    }
                }

                // Nullify the celerity when too low
                for (i = 0; i < 2; i++)
                {
                    if (Math.Abs(new_cel[i]) <= Tools.LIMIT_CELL)
                        new_cel[i] = 0;
                }

                // Actually update values
                cel = new_cel;
                Pos = new_pos;
            }

            return collided;
        }
    }
}
