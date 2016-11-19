using System;

using Xamarin.Forms;

namespace Redmond
{
	public class navigationControll : TabbedPage
	{
		public navigationControll()
		{
			var navigationPage = new NavigationPage( new ordersList());
			navigationPage.Title = "Menu";

			Children.Add(navigationPage);

			//var secondPage = new NavigationPage ( new initialPage());
			//secondPage.Title = "Welcome Page";
			//Children.Add (secondPage);
		}
	}
}

