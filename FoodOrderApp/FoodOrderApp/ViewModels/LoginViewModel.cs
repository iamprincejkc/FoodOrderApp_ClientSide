using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private string _Username;
        public string Username 
        { 
            set { _Username = value; OnPropertyChanged(); } 
            get { return _Username; } 
        }
        
        private string _Password;
        public string Password
        {
            set { _Password = value; OnPropertyChanged(); }
            get { return _Password; }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            set { _IsBusy = value; OnPropertyChanged(); }
            get { return _IsBusy; }
        }
        private bool _IsValid;
        public bool IsValid
        {
            set { _IsValid = value; OnPropertyChanged(); }
            get { return _IsValid; }
        }


        private bool _Result;
        public bool Result
        {
            set { _Result = value; OnPropertyChanged(); }
            get { return _Result; }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;


                if (!IsValid)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Valid", "OK");
                    //var userservice = new UserService();
                    //Result = await userservice.RegisterUser(Username, Password);
                    //if (Result)
                    //    await Application.Current.MainPage.DisplayAlert("Success", "User Registered", "OK");
                    //else
                    //    await Application.Current.MainPage.DisplayAlert("Error", "User Already Exist", "OK");
                }   
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;


                if (!IsValid)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid", "OK");
                }
                else
                {
                    var userservice = new UserService();
                    Result = await userservice.LoginUser(Username, Password);
                    if (Result)
                    {
                        Preferences.Set("Username", Username);
                        Application.Current.MainPage = new NavigationPage(new ProductsView());
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Login Failed", "Wrong Username or Password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
