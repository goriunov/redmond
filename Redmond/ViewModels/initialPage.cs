using System;

using Xamarin.Forms;

namespace Redmond
{
	public class initialPage : ContentPage
	{

		Button navigationButton;
		public void ComonStyles (){
			BackgroundColor = Color.FromHex("#1259CD");
			NavigationPage.SetHasNavigationBar(this, false);
		}
		public initialPage()
		{
			Device.OnPlatform(
				iOS:()=>{
				ComonStyles();
				Padding = new Thickness(10,20,10,10);
				},
				Android: () =>{
				ComonStyles();
				Padding = new Thickness(10, 0 , 10 ,10);
				},
				Default: () =>{
				ComonStyles();
			 	}
			);

			navigationButton = new Button{
				Text = "Start ordering",

				FontSize = 20,
				TextColor = Color.White,
				BackgroundColor = Color.FromHex("#3E7EE7")
			};

			StackLayout mainContent = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness(0 , 60 ,0 ,0),
				Children = {
					navigationButton
				}

			};

			Content = new StackLayout
			{
				Padding = new Thickness(0 , 100 ,0 ,0),
				Spacing = 40,
				Children = {
					new Label{
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize = 35,
						Text = "Redmond",
						TextColor = Color.White,
						FontAttributes = FontAttributes.Bold
					},
					new Label{
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize= 15,
						Text = "Order your faivorite food from your mobile",
						TextColor = Color.White,
						FontAttributes = FontAttributes.Italic
					},
					mainContent
				}
			};
		}
	}
}

