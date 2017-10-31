using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JumpAgainstBalls_Xamarin
{
    class GameThreadObj
    {
        public BallPlayer Player { get; set; }
        public List<Ball> Balls { get; set; }
        public Ball[] LeftBalls { get; set; }
        public Ball[] RightBalls { get; set; }
        public float[] Accel { get; set; }
        public long OffsetY { get; set; }
        public bool IsDemo { get; set; }
        public bool AllSet { get; set; }
        public long Time { get; set; }
        public bool StopRequested { get; set; }
        public GameView View { get; set; }
        
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

                Thread.Sleep(Tools.STEPTIME);
            }
        }

        public void StopThread(Thread thread)
        {
            if (thread != null)
            {
                StopRequested = true;
                thread.Join();
            }
        }

        public Thread StartThread(Thread thread = null)
        {
            StopRequested = false;
            if (thread == null)
            {
                thread = new Thread(this.GameLoop);
            }
            thread.Start();
            return thread;
        }
    }
}
