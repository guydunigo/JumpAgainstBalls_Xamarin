using System;
using System.Collections.Generic;

namespace JumpAgainstBalls_Xamarin
{
    class BallPlayer : Ball
    {
        private float[] cel;

        private float maxHeight;

        public override float Y
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

        public BallPlayer(float radius = 100): base(radius)
        {
            cel = new float[] { 0, 0 };
            maxHeight = 0;
        }

        public String ConvertMaxHeightToM(float height)
        {
            return Tools.ConvertHeightToM(height, maxHeight);
        }

        public String ConvertHeightToM(float height)
        {
            return Tools.ConvertHeightToM(height, Y);
        }

        public bool Step(float dt, float[] gravity, float width, float height, List<Ball> balls)
        {
            bool collided = false;

            if (width > 0 && height > 0)
            {
                float[] normal = { 0, 0 };
                float[] unorm = { 0, 0 };
                float norm = 0;

                float[] pos = { X, Y };
                float[] new_pos = { X, Y };
                float[] other_pos = { X, Y };
                float[] new_cel = { X, Y };
                float tmp_cel = 0;

                int i = 0;

                for (i = 0; i < 2; i++)
                {
                    new_cel[i] = cel[i] + gravity[i] * dt;
                    new_pos[i] = pos[i] + new_cel[i] * dt;
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

                // Bouncing against other balls
                foreach (Ball other in balls)
                {
                    other_pos = other.Pos;
                    normal = Tools.GetVector(other_pos, new_pos);
                    norm = Tools.GetNorm(normal);
                    float radSum = Radius + other.Radius;
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

                // Nullify the celerity when too low
                for (i = 0; i < 2; i++)
                {
                    if (Math.Abs(new_cel[i]) <= Tools.LIMIT_CELL)
                        new_cel[i] = 0;
                }

                // Actually update values
                cel = new_cel;
                pos = new_pos;
            }

            return collided;
        }
    }
}
