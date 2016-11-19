using System;

using Xamarin.Forms;

namespace Redmond
{
	public class App : Application
	{
		public App()
		{

			MainPage = new NavigationPage(new initialPage()){BarBackgroundColor = Color.FromHex("#1259CD") , BarTextColor=Color.White };
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
