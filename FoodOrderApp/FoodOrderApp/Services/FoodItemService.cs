using Firebase.Database;
using FoodOrderApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{
    public class FoodItemService
    {
        FirebaseClient client;
        public FoodItemService()
        {
            client = new FirebaseClient("https://foodorderingapp-7b2d1-default-rtdb.firebaseio.com/");
        }
        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            var fooditems = (await client.Child("FoodItems")
                .OnceAsync<FoodItem>())
                .Select(c => new FoodItem
                {
                    CategoryID = c.Object.CategoryID,
                    Description = c.Object.Description,
                    HomeSelected = c.Object.HomeSelected,
                    ImageUrl = c.Object.ImageUrl,
                    Name = c.Object.Name,
                    Price = c.Object.Price,
                    ProductID = c.Object.ProductID,
                    Rating = c.Object.Rating,
                    RatingDetail = c.Object.RatingDetail,

                }).ToList();

            return fooditems;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryAsync (int categoryid)
        {
            var fooditemsbyid = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(c => c.CategoryID == categoryid).ToList();
            foreach (var item in items)
            {
                fooditemsbyid.Add(item);
            }
            

            return fooditemsbyid;
        }

        public async Task<ObservableCollection<FoodItem>> GetLatestFoodItemsAsync()
        {
            var fooditemsbyid = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).OrderByDescending(c => c.ProductID).Take(3);
            foreach (var item in items)
            {
                fooditemsbyid.Add(item);
            }


            return fooditemsbyid;

        }
    }
}
