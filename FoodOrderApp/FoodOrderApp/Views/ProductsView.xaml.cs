using FoodOrderApp.Model;
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
    public partial class ProductsView : ContentPage
    {
        public ProductsView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = e.CurrentSelection.FirstOrDefault() as Category;
            if (category == null)
                return;


            if (Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault().GetType() != typeof(CategoryView))
                await Navigation.PushAsync(new CategoryView(category), false);
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}