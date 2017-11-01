using Xamarin.Forms;

namespace JumpAgainstBalls_Xamarin
{
    class Ball
    {
        public float[] Pos { get; set; }

        public float X {
            get => Pos[0];
            set
            {
                Pos[0] = value;
            }
        }
        public virtual float Y
        {
            get => Pos[1];
            set
            {
                Pos[1] = value;
            }
        }

        private float radius;
        public float Radius {
            get => radius;
            set
            {
                if (value > 0)
                {
                    radius = value;
                }
            }
        }

        public Color Color { get; set; }

        public Ball(float radius = 100)
        {
            Radius = radius;
            Pos = new float[] { 0, 0 };
        }

        public bool IsVisible(float height, long offset)
        {
            return (Y - Radius + offset >= 0 && Y + Radius + offset <= height);
        }
    }
}
