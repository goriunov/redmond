using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace Redmond
{
	public class ordersListPage : ContentPage
	{
		public class Groups : ObservableCollection<FoodItem> {
			public string longName{get; set;}
			public string shortMame{get; set;}
		}
		private ObservableCollection<Groups> grouped { get; set; }

		List<FoodItem> listCollection = new List<FoodItem>();
		ListView foodList;
		ActivityIndicator whileLoading;
		StackLayout  mainStack;
		public ordersListPage()
		{
			Title = "Menu";
			Padding = new Thickness(5 ,5 , 0, 0);
			ToolbarItems.Add( new ToolbarItem("Box" ,"box.png", async () => {
				await Navigation.PushAsync(new boxPage());
			}));

			getItems();

			foodList = new ListView {
				SeparatorVisibility = SeparatorVisibility.None,
				IsGroupingEnabled = true,
				ItemTemplate = new DataTemplate(()=>{
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

					return  new ViewCell{
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
				Margin = new Thickness(0, 220 , 0 , 0 ),
				Color = Color.FromHex("#1259CD")
			};
			whileLoading.SetBinding(ActivityIndicator.IsRunningProperty , "isRunning");
			whileLoading.SetBinding(ActivityIndicator.IsVisibleProperty , "isRunning");
			whileLoading.BindingContext = new {isRunning = true};

			mainStack = new StackLayout
			{
				Children = {
					whileLoading, foodList
				}
			};
			Content = mainStack;
			foodList.ItemTapped += FoodList_ItemTapped;
			foodList.ItemSelected += FoodList_ItemSelected;
		}

		private async void getItems(){
			listCollection = await AzureManager.AzureManagerInstance.GetFoodItems();
			grouped = new ObservableCollection<Groups>();

			var americanFood = new Groups(){
				longName = "American Food",
				shortMame = "Am"
			};
			var asianFood = new Groups(){
				longName = "Asian Food",
				shortMame = "As"
			};

			var drink = new Groups(){
				longName = "Drinks",
				shortMame = "D"
			};

			var japanFood = new Groups(){
				longName = "Japanese Food",
				shortMame = "J"
			};


			for(int i=0; i<listCollection.Count; i++){
				if(listCollection[i].Type == "Drinks" ){
					drink.Add(listCollection[i]);
				};
				if(listCollection[i].Type == "American Food"){
					americanFood.Add(listCollection[i]);
				}
				if(listCollection[i].Type == "Asian Food"){
					asianFood.Add(listCollection[i]);
				}
				if(listCollection[i].Type == "Japanese Food"){
					japanFood.Add(listCollection[i]);
				}

			}
			grouped.Add(americanFood);
			grouped.Add(asianFood);
			grouped.Add(japanFood);
			grouped.Add(drink);

			foodList.ItemsSource = grouped;
			foodList.GroupDisplayBinding = new Binding ("longName");
			foodList.GroupShortNameBinding = new Binding ("shortName");

			whileLoading.BindingContext = new {isRunning = false};
		}

		void FoodList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
		}

		async void FoodList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			FoodItem item = (FoodItem)e.Item;
			await Navigation.PushAsync(new detailPage(item));
		}
	}
}

