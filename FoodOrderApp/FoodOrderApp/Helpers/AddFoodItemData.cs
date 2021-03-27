using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    public class AddFoodItemData
    {
        public List<FoodItem> FoodItems { get; set; }
        FirebaseClient client;
        public AddFoodItemData()
        {
            client = new FirebaseClient("https://foodorderingapp-7b2d1-default-rtdb.firebaseio.com/");
            FoodItems = new List<FoodItem>()
            {
                new FoodItem(){ProductID=1, CategoryID=3,ImageUrl="Dessert.png",
                Name="Ice Cream", Description="Ice Cream - Breakfast", HomeSelected="CompleteHeart.png",
                    Price=45, Rating=" 5.0", RatingDetail="(879 ratings)"},


                new FoodItem(){ProductID=1, CategoryID=1,ImageUrl="Burger.png",
                Name="Burger Giant", Description="Burger Giant - Breakfast", HomeSelected="CompleteHeart.png",
                    Price=45, Rating=" 4.4", RatingDetail="(500 ratings)"},


                new FoodItem(){ProductID=1, CategoryID=3,ImageUrl="Burger.png",
                Name="Burger w/ Fries", Description="Burger w/ Fries - Breakfast", HomeSelected="EmptyHeart.png",
                    Price=45, Rating=" 3.8", RatingDetail="(376 ratings)"},


                new FoodItem(){ProductID=1, CategoryID=3,ImageUrl="Pizza.png",
                Name="Pizza Hawaiian", Description="Pizza Hawaiian - Breakfast", HomeSelected="CompleteHeart.png",
                    Price=45, Rating=" 4.6", RatingDetail="(788 ratings)"},
            };
        }
        public async Task AddFoodItemAsync()
        {
            try
            {
                foreach (var fooditem in FoodItems)
                {
                    await client.Child("FoodItems").PostAsync(new FoodItem()
                    {
                        ProductID = fooditem.ProductID,
                        CategoryID = fooditem.CategoryID,
                        Description = fooditem.Description,
                        HomeSelected = fooditem.HomeSelected,
                        RatingDetail = fooditem.RatingDetail,
                        Rating = fooditem.Rating,
                        Price = fooditem.Price,
                        Name = fooditem.Name,
                        ImageUrl = fooditem.ImageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
