using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace Redmond
{
	public class ordersListPage : ContentPage
	{

		List<FoodItem> listCollection;
		ListView foodList;
		ActivityIndicator whileLoading;
		StackLayout  mainStack;
		public ordersListPage()
		{
			this.Title = "Menu";
			listCollection = new List<FoodItem>(){
			};
			getItems();


			foodList= new ListView {
				Margin = new Thickness(5 , 5 , 5 ,5),
				SeparatorVisibility = SeparatorVisibility.None,
				ItemsSource = listCollection,
				ItemTemplate = new DataTemplate(()=>{
					//ImageCell imagecell = new ImageCell();
					//imagecell.SetBinding(ImageCell.ImageSourceProperty , "ImageSource");
					//imagecell.SetBinding(ImageCell.DetailProperty , "Detail");
					//imagecell.SetBinding(ImageCell.TextProperty , "Text");


					//return imagecell;
					Image image = new Image{
						Aspect = Aspect.AspectFill
					};
					image.SetBinding(Image.SourceProperty , "ImageSource");

					Label lb = new Label {
						FontSize = 18
					};
					lb.SetBinding(Label.TextProperty , "Text");

					Label detail = new Label {
						FontSize = 12,
						TextColor = Color.Gray
					};
					detail.SetBinding(Label.TextProperty , "Detail");

					return  new ViewCell{
						View = new StackLayout {
							Padding = new Thickness(0 , 5 , 5 , 5),
							Orientation = StackOrientation.Horizontal,
								Children = {
								new StackLayout {
									HeightRequest= 60,
									WidthRequest = 80,
									Children ={
										image
									}
								},
								new StackLayout {
									Spacing = 5,
									Children ={
										lb  , detail
									}
								}
							}
						}
					};
				})
			};


			whileLoading = new ActivityIndicator { 
				Margin = new Thickness(0 , 10  ,0 , 0 )
			};
			whileLoading.SetBinding(ActivityIndicator.IsRunningProperty , "isRunning");
			whileLoading.BindingContext = new {isRunning = true};

		
			mainStack = new StackLayout
			{
				Children = {
					whileLoading,
					foodList
				}
			};

			Content = mainStack;

			foodList.ItemTapped += FoodList_ItemTapped;
			foodList.ItemSelected += FoodList_ItemSelected;
		}

		private async void getItems(){
			listCollection = await AzureManager.AzureManagerInstance.GetFoodItems();
			whileLoading.BindingContext = new {isRunning = false};
			foodList.ItemsSource = listCollection;

		}

		void FoodList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
		}

		async void FoodList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			FoodItem item = (FoodItem)e.Item;
			Debug.WriteLine("Work");
			await Navigation.PushAsync(new detailPage(item));
		}
	}
}

