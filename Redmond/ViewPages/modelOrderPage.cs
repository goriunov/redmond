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
				var zoomLevel = e.NewValue; // between 1 and 18
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

			Content = new StackLayout
			{
				Spacing = 10,
				Children = {
					new Label { 
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize = 20,
						Text = "Order Imformation" 
					},
					new StackLayout {
						Children = {
							new Label{
								Text = "First Name:"
							},
							new Entry {
								WidthRequest = 180,
								Placeholder ="Write your first name"
							}
						}
					},
					new StackLayout {
						Children = {
							new Label{
								Text = "Family Name:"
							},
							new Entry {
								WidthRequest = 180,
								Placeholder ="Write your family name"
							}
						}
					},
					new StackLayout {
						Children = {
							new Label{
								Text = "Email:"
							},
							new Entry {
								WidthRequest = 180,
								Placeholder ="Write your email"
							}
						}
					},
					new StackLayout {
						HeightRequest = 200,
						Children = {
							map, slider, countedDistance
						}
					},

					buttonBuy
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
			return (dist);
		}

		private double deg2rad(double deg) {
			return (deg * Math.PI / 180.0);
		}

		private double rad2deg(double rad) {
			return (rad / Math.PI * 180.0);
		}

	}
}

