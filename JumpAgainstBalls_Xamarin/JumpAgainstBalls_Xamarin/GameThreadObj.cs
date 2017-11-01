using System;
using System.Collections.Generic;
using Xamarin.Forms;
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
        private bool allSet;
        public long Time { get; set; }
        public bool ResizeRequested { get; set; }
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
            allSet = false;
            Time = 0;
            ResizeRequested = false;
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

                if (View.Width > 0 && View.Height > 0)
                {
                    if (!allSet)
                    {
                        LeftBalls = new Ball[]{
                            new Ball(100) { X = 30, Y = View.HeightF / 3, Color = Color.Green },
                            new Ball(100) { X = 30, Y = (View.HeightF * 4) / 3, Color = Color.Blue }
                        };
                        RightBalls = new Ball[]{
                            new Ball(100) { X = View.WidthF - 30, Y = (View.HeightF * 2) / 3, Color = Color.Yellow },
                            new Ball(100) { X = View.WidthF - 30, Y = (View.HeightF * 4) / 3, Color = Color.Magenta }
                        };

                        Balls.Clear();
                        Balls.Add(new Ball(3 * View.WidthF)
                        {
                            X = View.WidthF / 2,
                            Y = (View.HeightF + 2.9f * View.WidthF),
                            Color = Color.Red
                        });

                        Balls.AddRange(LeftBalls);
                        Balls.AddRange(RightBalls);

                        allSet = true;
                    }
                    else if (ResizeRequested)
                    {
                        foreach (Ball b in RightBalls)
                        {
                            b.X = View.WidthF - 30;
                        }
                        ResizeRequested = false;
                    }
                    
                    lock(Player)
                    {
                        Player.Step(Tools.STEPTIME * Tools.TIMEFACTOR, Accel, View.WidthF, View.HeightF, Balls);
                    }

                    // update offset :
                    if (Player.Y + OffsetY < View.Height / 3)
                    {
                        OffsetY = (long)(View.Height / 3 - Player.Y);
                    }
                    else if (Player.Y + Player.Radius + OffsetY > 2 / 3.0 * View.Height)
                    {
                        OffsetY = (long)(2 / 3.0 * View.Height - Player.Y - Player.Radius);
                    }

                    // Modifying UI needs to be done on main thread :
                    Device.BeginInvokeOnMainThread(() => View.ReDraw());
                }

                Thread.Sleep(Tools.STEPTIME);
            }
        }

        public void StopThread(ref Thread thread)
        {
            if (thread != null)
            {
                StopRequested = true;
                thread.Join();
            }
            thread = null;
        }

        public Thread StartThread(ref Thread thread)
        {
            StopRequested = false;
            if (thread == null)
            {
                thread = new Thread(GameLoop);
            }
            thread.Start();
            return thread;
        }

        public Thread CreateThread()
        {
            return new Thread(GameLoop);
        }
    }
}
