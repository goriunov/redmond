using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Redmond
{
	public class SingleFoodItemPage : ContentPage
	{
		FoodItem toBoxAdd;
		public SingleFoodItemPage(FoodItem singleFood)
		{
			Title = singleFood.Text;
			NavigationPage.SetHasBackButton(this, false);
			toBoxAdd = singleFood;

			Button buttonBack = new Button {
				Text= "Back",
				Margin = new Thickness(5 ,0 , 5 , 5),
				BackgroundColor = Color.FromHex("#1259CD"),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			Button buttonOrder = new Button {
				Text= "Add",
				BackgroundColor = Color.FromHex("#1259CD"),
				Margin = new Thickness(5, 0, 5 , 5),
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
					new StackLayout{
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children= {	
							new Image {
								HeightRequest = 200,
								Source = ImageSource.FromUri(new Uri(singleFood.ImageSource)),
								Aspect = Aspect.AspectFill
							}
						}
					},
					new StackLayout {
						Padding = new Thickness( 10, 0, 10 ,0),
						Children = {
							new Label {
								FontSize= 20,
								FontAttributes = FontAttributes.Bold,
								Text = singleFood.Text
							},
							new Label {
								TextColor= Color.Gray,
								FontSize= 12,
								Text = "Price: $" + singleFood.Price
							}

						}
					},
					new Label {
						Margin = new Thickness(10 ,0 ,10 ,0),
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
			buttonOrder.Clicked += ButtonOrder_Clicked;
		}

		async void ButtonBack_Clicked(object sender, EventArgs e){
			await Navigation.PopAsync();
		}

		async void ButtonOrder_Clicked(object sender, EventArgs e){
			List<FoodItem> itemsArray = new List<FoodItem>();
			if(Application.Current.Properties.ContainsKey("OrderArray")){
				itemsArray = Application.Current.Properties ["OrderArray"] as List<FoodItem>;
			}
			itemsArray.Add(toBoxAdd);

			Application.Current.Properties ["OrderArray"] = itemsArray;
			await DisplayAlert("Success" , toBoxAdd.Text + " added to orders box successfuly" , "Ok");
		}
	}
}

