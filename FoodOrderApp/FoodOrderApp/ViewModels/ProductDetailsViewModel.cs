using FoodOrderApp.Model;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class ProductDetailsViewModel:BaseViewModel
    {
        private FoodItem _SelectedFoodItem;
        public FoodItem SelectedFoodItem
        {
            set
            {
                _SelectedFoodItem = value;
                OnPropertyChanged();
            }
            get
            {
                return _SelectedFoodItem;
            }
        }
        private int _TotalQuantity;
        public int TotalQuantity
        {
            set
            {
                _TotalQuantity = value;
                if (_TotalQuantity < 0)
                    _TotalQuantity = 0;
                
                OnPropertyChanged();
            }
            get
            {
                return _TotalQuantity;
            }
        }
        public Command IncrementOrderCommand { get; set; }
        public Command DecrementOrderCommand { get; set; }
        public Command AddToCartCommand { get; set; }
        public Command ViewCartCommand { get; set; }
        public Command HomeCommand { get; set; }

        public ProductDetailsViewModel(FoodItem fooditem)
        {
            SelectedFoodItem = fooditem;
            TotalQuantity = 0;

            IncrementOrderCommand = new Command(() => IncrementOrder());
            DecrementOrderCommand = new Command(() => DecrementOrder());
            AddToCartCommand = new Command(() => AddToCart());
            ViewCartCommand = new Command(() => ViewCart());
            HomeCommand = new Command(() => Home());
        }
        private void DecrementOrder()
        {
            TotalQuantity--;
        }
        private void AddToCart()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            try
            {
                var item = cn.Table<CartItem>().ToList().FirstOrDefault(e => e.ProductId == SelectedFoodItem.ProductID);
                if(item!=null)
                {
                    item.Quantity += TotalQuantity;
                    cn.Update(item);
                }
                else
                {
                    CartItem cart = new CartItem()
                    {
                        ProductId = SelectedFoodItem.ProductID,
                        ProductName = SelectedFoodItem.Name,
                        Price = SelectedFoodItem.Price,
                        Quantity = TotalQuantity,
                        ImageUrl= SelectedFoodItem.ImageUrl
                    };
                    cn.Insert(cart);
                }
                cn.Commit();
                cn.Close();
                Application.Current.MainPage.DisplayAlert("Cart","Selected Item Added To Cart","OK");
            }

            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private async void ViewCart()
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault().GetType() != typeof(CartView))
                await Application.Current.MainPage.Navigation.PushAsync(new CartView(), false);
        }
        private async void Home()
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault().GetType() != typeof(ProductsView))
                await Application.Current.MainPage.Navigation.PushAsync(new MainTabbedPage(), false);
        }
        private void IncrementOrder()
        {
            TotalQuantity++;
        }
    }
}
