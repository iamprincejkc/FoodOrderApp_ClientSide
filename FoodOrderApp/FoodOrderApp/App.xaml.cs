using FoodOrderApp.Helpers;
using FoodOrderApp.Views;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp
{
    public partial class App : Application
    {
        public App()
        {
            //if (Debugger.IsAttached)
            //    Preferences.Clear();

            InitializeComponent();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            var cct = new CreateCartTable();
            cct.CreateTable();


            //MainPage = new MainPage();
            //MainPage = new LoginView();
            //MainPage = new ContactView();
            //MainPage = new NavigationPage(new SettingsPage());

            string uname = Preferences.Get("Username", string.Empty);
            if (string.IsNullOrEmpty(uname))
            {
                MainPage = new LoginView();
            }
            else
            {
                MainPage = new NavigationPage(new ProductsView());
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
