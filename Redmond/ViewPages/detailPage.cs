using System;

using Xamarin.Forms;

namespace Redmond
{
	public class detailPage : ContentPage
	{
		
		public detailPage(FoodItem singleFood)
		{
			Padding = new Thickness(7 ,7 ,7 ,7);
			Title = singleFood.Text;
			NavigationPage.SetHasBackButton(this, false);

			Button buttonBack = new Button {
				Text= "Back",
				Margin = new Thickness(0 ,0 , 5 , 0),
				BackgroundColor = Color.FromHex("#1259CD"),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			Button buttonOrder = new Button {
				Text= "Order",
				BackgroundColor = Color.FromHex("#1259CD"),
				Margin = new Thickness(5, 0, 0 , 0),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			StackLayout actions = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					buttonBack,
					buttonOrder
				}
			};

			StackLayout information = new StackLayout{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 20,
				Children = {
					new Image {
						Margin = new Thickness(0, 5 ,0 ,0),
						Source = ImageSource.FromUri(new Uri(singleFood.ImageSource)),
						HeightRequest = 200,
						WidthRequest = 200
					},
					new Label {
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize= 20,
						FontAttributes = FontAttributes.Bold,
						Text = singleFood.Text
					},
					new Label {
						FontSize= 15,
						Text = singleFood.description
					}
				}

			};

			Content = new StackLayout{
				Children = {
					information,actions
				}
			};

			buttonBack.Clicked += ButtonBack_Clicked;
		}

		async void ButtonBack_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}

