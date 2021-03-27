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
    public class AddCategory
    {
        public List<Category> Categories { get; set; }
        FirebaseClient client;
        public AddCategory()
        {
            client = new FirebaseClient("https://foodorderingapp-7b2d1-default-rtdb.firebaseio.com/");
            Categories = new List<Category>()
            {
                new Category(){CategoryID=1,CategoryName="Pizza",ImageUrl="Pizza.png" },
                new Category(){CategoryID=2,CategoryName="Burger",ImageUrl="Burger.png" },
                new Category(){CategoryID=2,CategoryName="Dessert",ImageUrl="Dessert.png" }
            };
        }
        public async Task AddCateogriesAsync()
        {
            try
            {
                foreach (var category in Categories)
                {
                    await client.Child("Categories").PostAsync(new Category()
                    {
                        CategoryID=category.CategoryID,
                        CategoryName=category.CategoryName,
                        CategoryPoster=category.CategoryPoster,
                        ImageUrl=category.ImageUrl
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
