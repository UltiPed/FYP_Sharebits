using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Models.Functional;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Resources;
using FYP_Sharebits.Models;
using Xamarin.Essentials;

namespace FYP_Sharebits.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(await Constants.IsAuth())
            {
                await Navigation.PopAsync();
            }
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text))
            {
                await DisplayAlert(ResxFile.btnLogin, ResxFile.msg_LoginEmpty, ResxFile.btn_ok);
            }
            else
            {
                BaseModel result = await APIConnection.Login(email.Text, password.Text);
                if(result.Errors != null)
                {
                    await DisplayAlert(ResxFile.btnLogin, ResxFile.msg_Login_Fail, ResxFile.btn_ok);
                    return;
                }
                try
                {
                    await SecureStorage.SetAsync("UserId", result.Data.Login.UserId);
                    await SecureStorage.SetAsync("Token", result.Data.Login.Token);
                    await SecureStorage.SetAsync("UserName", result.Data.Login.UserName);
                } catch (Exception)
                {
                    await DisplayAlert(ResxFile.btnLogin, ResxFile.msg_Login_Fail, ResxFile.btn_ok);
                    return;
                }
                //AuthData authData = result.Data.Login;
                await Navigation.PopAsync();
            }
        }

        private async void Signup_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}