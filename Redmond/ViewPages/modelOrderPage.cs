using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace Redmond
{
	public class modelOrderPage : ContentPage
	{
		Map map ;
		Label countedDistance;
		List<FoodItem> orders = new List<FoodItem>();

		public modelOrderPage()
		{
			
			Padding = new Thickness(5 ,20 ,5 ,5);

			if(Application.Current.Properties.ContainsKey("OrderArray")){
				orders = Application.Current.Properties["OrderArray"] as List<FoodItem>;
			};

			int allPrice = 0;
			for(int i=0 ; i < orders.Count ; i ++){
				allPrice += Convert.ToInt32(orders[i].Price);
			};

			map = new Map(MapSpan.FromCenterAndRadius (new Position (-36.857461, 174.766412){}, Distance.FromMiles (1.2))){
				IsShowingUser = true,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			GetLocation();

			Pin pin = new Pin{
				Type = PinType.Place,
				Position = new Position(-36.857461, 174.766412),
				Label = "Redmond"
			};

			var slider = new Slider (10, 18, 1);
			slider.Value = 14;
			slider.ValueChanged += (sender, e) => {
				var zoomLevel = e.NewValue; 
				var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
				map.MoveToRegion(new MapSpan (map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
			};

			map.Pins.Add(pin);

			countedDistance = new Label{
				Text = "Aproximatly distance:"
			};

			Button buttonBuy = new Button {
				Text = "Make order for $" + allPrice 
			};

			Entry hours = new Entry {
				Placeholder = "Hours",
				Keyboard = Keyboard.Numeric
			};
			Entry minuts = new Entry{
				Placeholder = "Minuts",
				Keyboard = Keyboard.Numeric
			};
			Entry date = new Entry{
				Placeholder = "Date",
				Keyboard = Keyboard.Numeric
			};

			StackLayout mainDisplay = new StackLayout{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					new Label { 
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize = 20,
						Text = "Order Imformation" 
					},
					new Label{
						Text = "First Name:"
					},
					new Entry {
						WidthRequest = 180,
						Placeholder ="Write your first name"
					},
				
					new Label{
						Text = "Family Name:"
					},
					new Entry {
						WidthRequest = 180,
						Placeholder ="Write your family name"
					},
					new Label{
						Text = "Email:"
					},
					new Entry {
						WidthRequest = 180,
						Placeholder ="Write your email"
					},
					new StackLayout {
						HeightRequest = 200,
						Children = {
							map, slider, countedDistance
						}
					},
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label {
								Margin = new Thickness(0,5,0,0),
								FontSize = 18,
								Text = "Order on:"
							},
							new StackLayout{
								HorizontalOptions = LayoutOptions.EndAndExpand,
								VerticalOptions = LayoutOptions.Center,
								Orientation = StackOrientation.Horizontal,
								Children = {
									date,
									new Label {
										Text=":"
									},
									hours,
									new Label {
										Text=":"
									},
									minuts
								}
							},
						}
					},
				}
			};

			Content = new StackLayout {
				Children = {
					mainDisplay, buttonBuy
				}	
			};

			minuts.TextChanged +=(sender, e) => {
				Entry entry = ((Entry)sender);
				var val = entry.Text;
				if(val.Length > 2){
					val = val.Remove(val.Length -1);
					entry.Text = val;
				}
			};
			date.TextChanged += (sender, e) => {
				Entry entry = ((Entry)sender);
				var val = entry.Text;
				if(val.Length > 2){
					val = val.Remove(val.Length -1);
					entry.Text = val;
				}
			};
			hours.TextChanged += (sender, e) => {
				Entry entry = ((Entry)sender);
				var val = entry.Text;
				if(val.Length > 2){
					val = val.Remove(val.Length -1);
					entry.Text = val;
				}
			};


			buttonBuy.Clicked += ButtonBuy_Clicked;
		}



		async void ButtonBuy_Clicked(object sender, EventArgs e){
			await Navigation.PopModalAsync();
		}

		async void GetLocation() {
			var position = await CrossGeolocator.Current.GetPositionAsync(timeoutMilliseconds:10000);
			countedDistance.Text = countedDistance.Text+ " " + distance(-36.857461 , 174.766412 ,position.Latitude ,position.Longitude , 'K').ToString().Substring(0,3) + " km";
		}


		private double distance(double lat1, double lon1, double lat2, double lon2, char unit) {
			double theta = lon1 - lon2;
			double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
			dist = Math.Acos(dist);
			dist = rad2deg(dist);
			dist = dist * 60 * 1.1515;
			if (unit == 'K') {
				dist = dist * 1.609344;
			} else if (unit == 'N') {
				dist = dist * 0.8684;
			}
			return (dist + 0.3);
		}

		private double deg2rad(double deg) {
			return (deg * Math.PI / 180.0);
		}

		private double rad2deg(double rad) {
			return (rad / Math.PI * 180.0);
		}

	}
}

