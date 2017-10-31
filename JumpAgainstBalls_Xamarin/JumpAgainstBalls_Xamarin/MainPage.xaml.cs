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

            var view = this.FindByName<GameView>("View");
            if (view != null)
            {
                threadObj = new GameThreadObj(view, player, balls, false);
            }
        }

        public void StartThread()
        {
            threadObj.StartThread(thread);
        }
        
        public void StopThread()
        {
            threadObj.StopThread(thread);
            thread = null;
        }
    }
}
