using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Redmond
{
	public class OrdersBoxPage : ContentPage
	{
		List <FoodItem> orders = null;
		ListView orderedItems;
		public OrdersBoxPage()
		{
			Title = "Orders Box";
			Padding = new Thickness(5  , 5 , 5, 5);
			orders = new List<FoodItem>();

			if(Application.Current.Properties.ContainsKey("OrderArray")){
				orders = Application.Current.Properties["OrderArray"] as List<FoodItem>;
			};

			orderedItems = new ListView {
				SeparatorVisibility = SeparatorVisibility.None,
				ItemsSource = orders,
				ItemTemplate = new DataTemplate(() =>{

					Image image = new Image{
						HeightRequest = 41,
						Aspect = Aspect.AspectFill
					};
					image.SetBinding(Image.SourceProperty , "ImageSource");

					Label lb = new Label {
						FontSize = 15
					};
					lb.SetBinding(Label.TextProperty , "Text");

					Label detail = new Label {
						FontSize = 8,
						TextColor = Color.Gray
					};
					detail.SetBinding(Label.TextProperty , "Detail");

					Label price = new Label {
						FontSize = 15,
						TextColor = Color.Gray
					};
					price.SetBinding(Label.TextProperty , "Price");

					ViewCell viewCell = new ViewCell{
						View = new StackLayout {
							Spacing = 15,
							Orientation = StackOrientation.Horizontal,
							Children = {
								new StackLayout {
									WidthRequest = 80,
									Children ={
										image
									}
								},
								new StackLayout {
									HorizontalOptions = LayoutOptions.FillAndExpand,
									Children ={
										lb  , detail
									}
								},
								new StackLayout {
									Orientation = StackOrientation.Horizontal,
									VerticalOptions = LayoutOptions.Center,
									Children ={
										new Label{
											HorizontalTextAlignment = TextAlignment.End,
											Text = "$",
											TextColor = Color.Gray,
											FontSize = 15
										},
										price
									}
								}
							}
						}
					};


					var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; 
					deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
					deleteAction.Clicked += DeleteAction_Clicked;
					viewCell.ContextActions.Add(deleteAction);
					return viewCell;
					})
				};
			orderedItems.ItemSelected += (sender, e) => {
				((ListView)sender).SelectedItem = null;
			};


			Button makeOrder = new Button {
				Text="Order now",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex("#1259CD")
			};
			

			Content = new StackLayout
			{
				
				Children = {
					new StackLayout {
						VerticalOptions = LayoutOptions.FillAndExpand,
						Children = {
							orderedItems
						}
					},
					new StackLayout {
						Children ={
							makeOrder
						}
					}

				}
			};
				makeOrder.Clicked += MakeOrder_Clicked;
			
		}

		async void MakeOrder_Clicked(object sender, EventArgs e)
		{
			if(orders.ToArray().Length > 0){
				await Navigation.PushModalAsync(new OrderDetailsPage());
				await Navigation.PopAsync();
			}
		}

		void DeleteAction_Clicked(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			var contact = mi.CommandParameter as FoodItem;
			orders.Remove(contact);
			orderedItems.ItemsSource = null;
			orderedItems.ItemsSource = orders;


		}
	}
}

