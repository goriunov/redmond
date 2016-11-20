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


		List<FoodItem> listCollection;
		ListView foodList;
		ActivityIndicator whileLoading;
		StackLayout  mainStack;
		public ordersListPage()
		{
			Padding = new Thickness(5 ,5 , 0, 0);
			this.Title = "Menu";
			listCollection = new List<FoodItem>(){
			};
			getItems();

			foodList= new ListView {
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

		//async void  repplacement (){
		//	for(int i=0; i<listCollection.Count; i++){
		//		if(i < 3){
		//			FoodItem oneFood = new FoodItem {
		//				Text = listCollection[i].Text,
		//				Detail = listCollection[i].Detail,
		//				ImageSource = listCollection[i].ImageSource,
		//				description = listCollection[i].description,
		//				Type = "American Food",
		//				Price = listCollection[i].Price
		//			};
		//			await AzureManager.AzureManagerInstance.AddFood(oneFood);
		//		}
		//		if(i < 6 && i > 2){
		//			FoodItem oneFood = new FoodItem {
		//				Text = listCollection[i].Text,
		//				Detail = listCollection[i].Detail,
		//				ImageSource = listCollection[i].ImageSource,
		//				description = listCollection[i].description,
		//				Type = "Asian Food",
		//				Price = listCollection[i].Price
		//			};
		//			await AzureManager.AzureManagerInstance.AddFood(oneFood);
		//		}
		//		if(i < 9 && i > 5){
		//			FoodItem oneFood = new FoodItem {
		//				Text = listCollection[i].Text,
		//				Detail = listCollection[i].Detail,
		//				ImageSource = listCollection[i].ImageSource,
		//				description = listCollection[i].description,
		//				Type = "Japanese Food",
		//				Price = listCollection[i].Price
		//			};
		//			await AzureManager.AzureManagerInstance.AddFood(oneFood);
		//		}
		//		if(i < 12 && i > 8){
		//			FoodItem oneFood = new FoodItem {
		//				Text = listCollection[i].Text,
		//				Detail = listCollection[i].Detail,
		//				ImageSource = listCollection[i].ImageSource,
		//				description = listCollection[i].description,
		//				Type = "Drinks",
		//				Price = listCollection[i].Price
		//			};
		//			await AzureManager.AzureManagerInstance.AddFood(oneFood);
		//		}
		//	}
		//}

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

