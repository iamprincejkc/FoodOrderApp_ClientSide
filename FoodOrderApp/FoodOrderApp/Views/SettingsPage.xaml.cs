using FoodOrderApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void ButtonCategories_Clicked(object sender, EventArgs e)
        {
            var addcategory = new AddCategory();
            await addcategory.AddCateogriesAsync();
        }

        private async void ButtonProducts_Clicked(object sender, EventArgs e)
        {
            var addproducts = new AddFoodItemData();
            await addproducts.AddFoodItemAsync();
        }


        private void ButtonCart_Clicked(object sender, EventArgs e)
        {
            var cct = new CreateCartTable();
            if(cct.CreateTable())
            {
                DisplayAlert("Success","Table has been created","OK");
            }
            else
            {
                DisplayAlert("Error", "Error while creating table", "OK");
            }    
        }
    }
}