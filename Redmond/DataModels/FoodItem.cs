using System;
using Newtonsoft.Json;


namespace Redmond
{
	public class FoodItem
	{
		[JsonProperty(PropertyName = "Id")]
		public string ID {get; set;}

		[JsonProperty(PropertyName = "ImageSource")]
		public string ImageSource {get; set;}

		[JsonProperty(PropertyName = "Description")]
		public string description {get; set;}

		[JsonProperty(PropertyName = "Detail")]
		public string Detail {get;  set;}

		[JsonProperty(PropertyName = "Text")]
		public string Text {get; set;}
	}
}
