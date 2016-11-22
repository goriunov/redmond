using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Redmond
{
	public class orderDetails
	{
	
		[JsonProperty(PropertyName= "Id")]
		public string ID {get;set;}

		[JsonProperty(PropertyName = "firstName")]
		public string firstName{get;set;}

		[JsonProperty(PropertyName = "familyName")]
		public string familyName{get; set;}

		[JsonProperty(PropertyName = "email")]
		public string email {get; set;}

		[JsonProperty(PropertyName = "pnoneNumber")]
		public string phoneNumber {get; set;}

		[JsonProperty (PropertyName = "date")]
		public string date {get;set;}

		[JsonProperty (PropertyName = "time")]
		public string time {get;set;}

		[JsonProperty (PropertyName = "order")]
		public string order {get;set;}

		[JsonProperty (PropertyName = "price")]
		public string price {get;set;}
		
	}
}
