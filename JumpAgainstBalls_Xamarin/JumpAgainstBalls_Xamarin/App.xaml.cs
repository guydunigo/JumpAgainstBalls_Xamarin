using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace JumpAgainstBalls_Xamarin
{
	public partial class App : Application
    {

        public App ()
		{
			InitializeComponent();

			MainPage = new JumpAgainstBalls_Xamarin.MainPage();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            if (MainPage is MainPage mp)
            {
                mp.StartThread();
            }
        }

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
            if (MainPage is MainPage mp)
            {
                mp.StopThread();
            }
        }

		protected override void OnResume ()
		{
            // Handle when your app resumes
            if (MainPage is MainPage mp)
            {
                mp.StartThread();
            }
        }
    }
}
