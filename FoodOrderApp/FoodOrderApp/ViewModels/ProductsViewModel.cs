using FoodOrderApp.Model;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set  {  _Username = value; OnPropertyChanged(); }
            get {  return _Username;  }
        }

        private int _UserCartItemsCount;

        public int UserCartItemsCount
        {
            get { return _UserCartItemsCount; }
            set { _UserCartItemsCount = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<FoodItem> LatestItems { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }

        public ProductsViewModel()
        {
            var uname = Preferences.Get("Username", String.Empty);

            if(String.IsNullOrEmpty(uname))
                Username = "Guest";
            else
                Username = uname;

            UserCartItemsCount = new CartItemService().GetUserCartCount();
            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<FoodItem>();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());

            GetCategories();
            GetLatestItems();
        }
        private async Task LogoutAsync()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new LogoutView(), false);

            var result =  await Application.Current.MainPage.DisplayAlert("Log Out ?","Are you sure you want to log out ?","Log Out","Cancel");
            if (result)
            {
                Preferences.Remove("Username");
                Application.Current.MainPage = new LoginView();
            }
        }
        private async Task ViewCartAsync()
        {
            if(UserCartItemsCount<1)
            {
                await Application.Current.MainPage.DisplayAlert("Oops!","Please Add Cart Items","OK");
                return;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new CartView(), false);
        }
        public async void GetLatestItems()
        {
            var data = await new FoodItemService().GetLatestFoodItemsAsync();
            LatestItems.Clear();
            foreach (var item in data)
            {
                LatestItems.Add(item);
            }
        }
        public async void GetCategories()
        {
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var item in data)
            {
                Categories.Add(item);   
            }
        }
    }
}
