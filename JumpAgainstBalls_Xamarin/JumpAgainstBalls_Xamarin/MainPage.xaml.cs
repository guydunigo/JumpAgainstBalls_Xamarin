using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JumpAgainstBalls_Xamarin
{
	public partial class MainPage : ContentPage
    {
        private GameThreadObj threadObj;
        private Thread thread;
        private BallPlayer player;
        private List<Ball> balls;

        public MainPage()
		{
			InitializeComponent();

            player = new BallPlayer(100)
            {
                X = 500,
                Y = 500,
                Color = Color.Cyan
            };
            balls = new List<Ball>();

            var view = this.FindByName<GameView>("View");
            if (view != null)
            {
                view.Player = player;
                view.Balls = balls;

                view.ThreadObj = threadObj = new GameThreadObj(view, player, balls, false)
                {
                    Accel = new double[] { 0, Tools.ACCEL_Y }
                };
                thread = threadObj.CreateThread();
            }
        }

        public void StartThread()
        {
            threadObj.StartThread(ref thread);
        }
        
        public void StopThread()
        {
            threadObj.StopThread(ref thread);
        }
    }
}
