using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{
    public class CategoryDataService
    {

        FirebaseClient client;
        public CategoryDataService()
        {
            client = new FirebaseClient("https://foodorderingapp-7b2d1-default-rtdb.firebaseio.com/");
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = (await client.Child("Categories")
                .OnceAsync<Category>())
                .Select(c => new Category
                {
                    CategoryID=c.Object.CategoryID,
                    CategoryName=c.Object.CategoryName,
                    CategoryPoster=c.Object.CategoryPoster,
                    ImageUrl=c.Object.ImageUrl
                   
                }).ToList();

            return categories;
        }
    }
}
