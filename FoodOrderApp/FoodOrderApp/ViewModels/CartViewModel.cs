using FoodOrderApp.Model;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    class CartViewModel:BaseViewModel
    {
        public ObservableCollection<UserCartItem> CartItems { get; set; }
        private decimal _TotalCost;

        public decimal TotalCost
        {
            get { return _TotalCost; }
            set { _TotalCost = value; OnPropertyChanged(); }

        }
        private DateTime _Datenow;
        public DateTime Datenow
        {
            get { return _Datenow; }
            set { _Datenow = DateTime.Now; OnPropertyChanged(); }
        }
        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { _IsRefreshing=value; OnPropertyChanged(); }
        }
        public Command PlaceOrdersCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public CartViewModel()
        {
            CartItems = new ObservableCollection<UserCartItem>();
            LoadItems();
            PlaceOrdersCommand = new Command(async () => await PlaceOrdersAsync());
            RefreshCommand = new Command(async () => await RefreshAsync());
        }
        private async Task RefreshAsync()
        {
             LoadItems();
             IsRefreshing = false;
        }
        private async Task PlaceOrdersAsync()
        {
            var id = await new OrderService().PlaceOrderAsync() as string;
            RemoveItemsFromCart();
            if(Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault().GetType() != typeof(OrdersView))
                await Application.Current.MainPage.Navigation.PushAsync(new OrdersView(id), false);
        }
        private void RemoveItemsFromCart()
        {
            var cis = new CartItemService();
            cis.RemoveItemsFromCart();
        }
        private  void LoadItems()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var items = cn.Table<CartItem>().ToList();
            CartItems.Clear();
            TotalCost = 0;
            foreach (var item in items)
            {
                CartItems.Add(new UserCartItem()
                {
                    CartItemId=item.CartItemId,
                    Price=item.Price*item.Quantity,
                    ProductId=item.ProductId,
                    ProductName=item.ProductName+" x"+item.Quantity,
                    Quantity=item.Quantity,
                    Cost=item.Price*item.Quantity,
                    ImageUrl=item.ImageUrl
                });
                TotalCost += (item.Price     * item.Quantity);
            }
        }
    }
}
