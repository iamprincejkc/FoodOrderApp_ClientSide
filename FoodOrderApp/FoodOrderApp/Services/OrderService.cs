using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database.Query;
using Firebase.Database;
using System.Threading.Tasks;
using Xamarin.Forms;
using FoodOrderApp.Model;
using Xamarin.Essentials;

namespace FoodOrderApp.Services
{
    class OrderService
    {
        FirebaseClient client;
        public OrderService()
        {
            client = new FirebaseClient("https://foodorderingapp-7b2d1-default-rtdb.firebaseio.com/");
        }

        public async Task<string> PlaceOrderAsync()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var data = cn.Table<CartItem>().ToList();
            var orderId = Guid.NewGuid().ToString();
            var uname = Preferences.Get("Username", "Guest");
            decimal totalcost = 0;

            foreach (var item in data)
            {
                OrderDetails order = new OrderDetails()
                {
                    OrderId=orderId,
                    OrderDetailId= Guid.NewGuid().ToString(),
                    ProductID=item.ProductId,
                    ProductName=item.ProductName,
                    Price=item.Price,
                    Quantity=item.Quantity
            };
                totalcost += (item.Price + item.Quantity);
                await client.Child("OrderDetails").PostAsync(order);
            }

            await client.Child("Orders").PostAsync(
              new Order()
              {
                  OrderId=orderId,
                  Username=uname,
                  TotalCost=totalcost
              });
            return orderId;
        }
    }
}
