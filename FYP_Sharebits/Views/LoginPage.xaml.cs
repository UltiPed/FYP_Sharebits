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

namespace FYP_Sharebits.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
                Constants.authData = result.Data.Login;
            }
        }
    }
}