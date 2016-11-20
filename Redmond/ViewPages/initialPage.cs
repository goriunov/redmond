using System;

using Xamarin.Forms;

namespace Redmond
{
	public class initialPage : ContentPage
	{

		Button navigationButton;
		public initialPage()
		{
			BackgroundColor = Color.FromHex("#1259CD");
			this.Title = "Welcome";
			Device.OnPlatform(
				iOS:()=>{
				Padding = new Thickness(10,20,10,10);
				},
				Android: () =>{
				Padding = new Thickness(10, 0 , 10 ,10);
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
				Padding = new Thickness(0 , 70 ,0 ,0),
				Spacing = 30,
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
						Text = "Order your favourite food from your mobile at any time",
						TextColor = Color.White,
						FontAttributes = FontAttributes.Italic
					},
					mainContent
				}
			};

			navigationButton.Clicked += NavigationButton_Clicked;
		}

		async void  NavigationButton_Clicked(object sender, EventArgs e)
		{
			//FoodItem oneFood = new FoodItem {
			//	Text = "New Added stuff",
			//	Detail = "Some detail",
			//	ImageSource = "http://s3.amazonaws.com/etntmedia/media/images/ext/446341906/fast-food-burger-fries.jpg",
			//	description = "Super Testy food with cool stuff"
			//};
			//await AzureManager.AzureManagerInstance.AddFood(oneFood);
			await Navigation.PushAsync(new ordersListPage());
		}
	}
}

