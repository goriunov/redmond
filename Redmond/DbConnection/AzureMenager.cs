using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Redmond
{
	public class AzureManager
	{
		private MobileServiceClient client;
		private static AzureManager instance;

		private IMobileServiceTable<FoodItem> foodItem;
		private IMobileServiceTable<orderDetails> orderdetails;

		private AzureManager()
		{
			this.client = new MobileServiceClient("http://msa-food.azurewebsites.net/");
			this.foodItem = this.client.GetTable<FoodItem>();
			this.orderdetails = this.client.GetTable<orderDetails>();
		}

		public MobileServiceClient AzureClient
		{
			get { return client; }
		}

		public static AzureManager AzureManagerInstance
		{
			get
			{
				if (instance == null) {
					instance = new AzureManager();
				}

				return instance;
			}
		}


		public async Task<List<FoodItem>> GetFoodItems(){
			return await this.foodItem.ToListAsync();
		}

		public async Task AddFood( FoodItem food) {
			await this.foodItem.InsertAsync(food);
		}
		public async Task makeOrder(orderDetails details){
			await this.orderdetails.InsertAsync(details);
		}
	}
}
