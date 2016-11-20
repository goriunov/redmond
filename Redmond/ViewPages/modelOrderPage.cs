using System;

using Xamarin.Forms;

namespace Redmond
{
	public class modelOrderPage : ContentPage
	{
		public modelOrderPage()
		{
			Padding = new Thickness(5 ,20 ,5 ,5);

			Button buttonBack = new Button {
				Text = "Go back"
			};

			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" },
					buttonBack
				}
			};

			buttonBack.Clicked += ButtonBack_Clicked;
		}

		async void ButtonBack_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}

