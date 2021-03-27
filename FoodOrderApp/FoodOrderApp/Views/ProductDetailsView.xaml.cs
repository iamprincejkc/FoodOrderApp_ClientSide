using FoodOrderApp.Model;
using FoodOrderApp.ViewModels;
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
    public partial class ProductDetailsView : ContentPage
    {
        ProductDetailsViewModel pvm;
        public ProductDetailsView(FoodItem fooditem)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            pvm = new ProductDetailsViewModel(fooditem);
            this.BindingContext = pvm;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}