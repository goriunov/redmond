using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Redmond
{
	public class ordersList : ContentPage
	{

		public class Stuff {
			public Xamarin.Forms.ImageSource ImageSource {get; set;}
			public string Detail {get; set;}
			public string Text {get; set;}
		}


		public ordersList()
		{
			this.Title = "Menu";
			List<Stuff> listCollection = new List<Stuff>(){
				new Stuff {
					ImageSource  = ImageSource.FromUri(new Uri("http://ichef-1.bbci.co.uk/news/660/cpsprodpb/1325A/production/_88762487_junk_food.jpg")),
					Detail ="Detail",
					Text = "Main text"},
				new Stuff {
					ImageSource  = ImageSource.FromUri(new Uri("http://ichef-1.bbci.co.uk/news/660/cpsprodpb/1325A/production/_88762487_junk_food.jpg")),
					Detail ="Detail",
					Text = "Main text"},
				new Stuff {
					ImageSource  = ImageSource.FromUri(new Uri("http://ichef-1.bbci.co.uk/news/660/cpsprodpb/1325A/production/_88762487_junk_food.jpg")),
					Detail ="Detail",
					Text = "Main text"}
			};

			ListView foodList = new ListView {
				Margin = new Thickness(5 , 5 , 5 ,5),
				SeparatorVisibility = SeparatorVisibility.None,
				ItemsSource = listCollection,
				ItemTemplate = new DataTemplate(()=>{
					var imagecell = new ImageCell();
					imagecell.SetBinding(ImageCell.ImageSourceProperty , "ImageSource");
					imagecell.SetBinding(ImageCell.DetailProperty , "Detail");
					imagecell.SetBinding(ImageCell.TextProperty , "Text");
					return imagecell;
				})
			};
		

			Content = new StackLayout
			{
				Children = {
					foodList
				}
			};
		}
	}
}

