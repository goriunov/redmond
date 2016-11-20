using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Redmond
{
	public class boxPage : ContentPage
	{
		List <FoodItem> orders ;
		ListView orderedItems;
		public boxPage()
		{
			Title = "Orders Box";
			Padding = new Thickness(0  , 10 , 0, 0);
			orders = new List<FoodItem>();

			if(Application.Current.Properties.ContainsKey("OrderArray")){
				orders = Application.Current.Properties["OrderArray"] as List<FoodItem>;
			}
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
						FontSize = 18
					};
					lb.SetBinding(Label.TextProperty , "Text");

					Label detail = new Label {
						FontSize = 11,
						TextColor = Color.Gray
					};
					detail.SetBinding(Label.TextProperty , "Detail");

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

			Content = new StackLayout
			{
				Children = {
					orderedItems
				}
			};
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

