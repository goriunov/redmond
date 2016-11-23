using System;
using Xamarin.Forms;

namespace Redmond
{
	public class GreetPage : ContentPage
	{

		Button navigationButton;
		public GreetPage()
		{
			BackgroundColor = Color.FromHex("#1259CD");
			Title = "Welcome";
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
			StackLayout lablesContent = new StackLayout{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 30,
				Children ={
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
					}
				} 
			};

			Content = new StackLayout
			{
				Children = {
					lablesContent,
					mainContent
				}
			};

			navigationButton.Clicked += NavigationButton_Clicked;
		}

		async void  NavigationButton_Clicked(object sender, EventArgs e)
		{
			//FoodItem oneFood = new FoodItem {
			//	Text = "Ramen",
			//	Detail = "Chinese-style wheat noodles in a meat",
			//	ImageSource = "http://www.japan-talk.com/images/jt/header/tonkotsu-ramen-78.jpg",
			//	description = "Hot Chinese-style wheat noodles in a meat, fish, miso or soy sauce broth. It's an inexpensive, filling, easy to find snack. Despite the fact that ramen is cheap, there's a big difference in quality from one shop to the next. A shop that earns a reputation amongst ramen aficionados will regularly have long lines while a shop just next door may be empty.",
			//	Type = "Japanese Food",
			//	Price = "35"
			//};
			//await AzureManager.AzureManagerInstance.AddFood(oneFood);
			await Navigation.PushAsync(new MenuListPage());
		}
	}
}

