using System.Collections.Generic;
using Xamarin.Forms;

namespace JumpAgainstBalls_Xamarin
{
    public class GameView : ContentView
	{
        public Label Txt { get; }
        internal BallPlayer Player { get; set; }
        internal List<Ball> Balls { get; set; }
        public bool IsDemo { get; set; }

        public float WidthF { get => (float)Width; }
        public float HeightF { get => (float)Height; }

        public GameView ()
        {
            Txt = new Label { Text = "Welcome to Xamarin.Forms!" };
            Content = Txt;
            IsDemo = false;
		}

        public void ReDraw(long offsetY)
        {
            if (Player != null)
            {
                Txt.Text = Player.Y.ToString();
            }
        }
    }
}