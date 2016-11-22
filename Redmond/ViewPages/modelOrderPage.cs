using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		Entry phoneNumber;
		Entry firstName;
		Entry familyName;
		Entry email;
		DatePicker date;
		TimePicker time;
		int allPrice = 0;
		string allItemsNames = "";

		public modelOrderPage()
		{
			
			Padding = new Thickness(5 ,20 ,5 ,5);

			if(Application.Current.Properties.ContainsKey("OrderArray")){
				orders = Application.Current.Properties["OrderArray"] as List<FoodItem>;
			};


			for(int i=0 ; i < orders.Count ; i ++){
				allPrice += Convert.ToInt32(orders[i].Price);
				allItemsNames = allItemsNames + orders[i].Text + ","; 
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
			slider.Value = 12;
			slider.ValueChanged += (sender, e) => {
				var zoomLevel = e.NewValue; 
				var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
				map.MoveToRegion(new MapSpan (map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
			};

			map.Pins.Add(pin);

			countedDistance = new Label{
				HorizontalTextAlignment = TextAlignment.Center
			};

			Button buttonBuy = new Button {
				Text = "Make order for $" + allPrice 
			};
			phoneNumber = new Entry {
				WidthRequest = 180,
				Placeholder ="88 888 888 888",
				Keyboard = Keyboard.Numeric
			};
			firstName = new Entry {
				WidthRequest = 180,
				Placeholder ="Tim"
			};
			familyName = new Entry {
				WidthRequest = 180,
				Placeholder ="Loren"
			};
			email = new Entry {
				Placeholder = "supermail@mail.com",
				Keyboard = Keyboard.Email
			};
			date = new DatePicker{
				Format = "MMM dd"
			};
			time = new TimePicker{
				Format = "HH:mm"
			};

			StackLayout mainDisplay = new StackLayout{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					new Label { 
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize = 20,
						Text = "Order Details" 
					},
					new Label{
						Text = "First Name:"
					},
					firstName,
					new Label{
						Text = "Family Name:"
					},
					familyName,
					new Label{
						Text = "Email:"
					},
					email,
					new Label{
						Text = "Phone number:"
					},
					phoneNumber,
					new StackLayout {
						HeightRequest = 250,
						Children = {
							countedDistance ,map, slider
						}
					},
					new Label {
						HorizontalTextAlignment = TextAlignment.Center,
						Margin = new Thickness(0,10,0,0),
						FontSize = 20,
						Text = "Order on"
					},
					new StackLayout{
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						Children = {
							date,
							time
						}
					},
				}
			};

			Content = new ScrollView {
				Content =  new StackLayout{
					Children = {
						mainDisplay, buttonBuy
					}
				}	
			};
			buttonBuy.Clicked += ButtonBuy_Clicked;
		}



		async void ButtonBuy_Clicked(object sender, EventArgs e){


			orderDetails details = new orderDetails{
				firstName = firstName.Text,
				familyName = familyName.Text,
				email = email.Text,
				phoneNumber = phoneNumber.Text,
				date = date.Date.Day.ToString() + "/"+  date.Date.Month.ToString(),
				time = time.Time.Hours.ToString() + ":" + time.Time.Minutes.ToString(),
				order = allItemsNames,
				price = allPrice.ToString()
			};
			await AzureManager.AzureManagerInstance.makeOrder(details);
			await DisplayAlert("Success" , "Your order send to the shop" , "Ok");
			Application.Current.Properties.Remove("OrderArray");
			await Navigation.PopModalAsync();
		}

		async void GetLocation() {
			var position = await CrossGeolocator.Current.GetPositionAsync(timeoutMilliseconds: 5000);
			Device.StartTimer(TimeSpan.FromSeconds(3), ()=>{
				GetLocation();
				return false;
			});
			countedDistance.Text = "Aproximatly distance"+ " " + distance(-36.857461 , 174.766412 ,position.Latitude ,position.Longitude , 'K').ToString().Substring(0,3) + " km";
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

