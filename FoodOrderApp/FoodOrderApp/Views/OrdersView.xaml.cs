using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersView : ContentPage
    {
        public OrdersView(string id)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            LabelName.Text = "Welcome " + Preferences.Get("Username", "Guest");
            LabelOrderID.Text = id;
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}