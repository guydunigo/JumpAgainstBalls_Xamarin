using System;
using System.Collections.Generic;
using System.Text;

namespace JumpAgainstBalls_Xamarin
{
    class GameThreadObj
    {
        public volatile BallPlayer Player { get; set; }
        public volatile List<Ball> Balls { get; set; }
        public volatile Ball[] LeftBalls { get; set; }
        public volatile Ball[] RightBalls { get; set; }
        public volatile float[] Accel { get; set; }
        public volatile long OffsetY { get; set; }
        public volatile bool IsDemo { get; set; }
        public volatile bool AllSet { get; set; }
        public volatile long Time { get; set; }
        public volatile bool StopRequested { get; set; }
        public volatile GameView View { get; set; }
        
        public GameThreadObj(
            GameView v,
            BallPlayer p,
            List<Ball> bs,
            bool demo = false)
        {
            View = v;
            Player = p;
            Balls = bs;
            IsDemo = demo;

            Accel = new float[] { 0, 0 };
            OffsetY = 0;
            AllSet = false;
            Time = 0;
            StopRequested = false;
        }

        public void GameLoop(Object obj)
        {
            while (StopRequested == false)
            {
                if (!IsDemo && Time > Tools.MAX_TIME)
                {
                    break;
                }

                if (View.Width != 0 && View.Height != 0)
                {

                }
            }
        }
    }
}
