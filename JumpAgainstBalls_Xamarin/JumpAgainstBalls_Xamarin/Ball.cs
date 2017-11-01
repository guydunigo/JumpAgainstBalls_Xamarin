using Xamarin.Forms;

namespace JumpAgainstBalls_Xamarin
{
    class Ball
    {
        public double[] Pos { get; set; }

        public double X {
            get => Pos[0];
            set
            {
                Pos[0] = value;
            }
        }
        public virtual double Y
        {
            get => Pos[1];
            set
            {
                Pos[1] = value;
            }
        }

        private double radius;
        public double Radius {
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

        public Ball(double radius = 100)
        {
            Radius = radius;
            Pos = new double[] { 0, 0 };
        }

        public bool IsVisible(double height, long offset)
        {
            return (Y + Radius + offset >= 0 && Y - Radius + offset <= height);
        }
    }
}
