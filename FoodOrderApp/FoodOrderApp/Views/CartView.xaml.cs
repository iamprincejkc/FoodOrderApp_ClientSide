using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartView : ContentPage
    {
        public CartView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
          //  LabelName.Text = "Welcome " + Preferences.Get("Username", "Guest");
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            base.OnBackButtonPressed();
            return true;
        }

    }
}