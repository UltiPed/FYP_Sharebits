using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.Functional;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfile : ContentPage
    {
        public MyProfile()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (await Constants.IsAuth())
            {
                //locading
                authenticated_layout.IsVisible = true;
                unauthenticated_layout.IsVisible = false;
            }
            else
            {
                //loading
                authenticated_layout.IsVisible = false;
                unauthenticated_layout.IsVisible = true;
            }
        }

        private async void btn_logout_Clicked(object sender, EventArgs e)
        {
            BaseModel result = await APIConnection.Logout();
            if(result.Errors != null)
            {
                await DisplayAlert(ResxFile.btn_Logout, ResxFile.msg_Logout_Fail, ResxFile.btn_ok);
            }
            else if(result.Data.Logout.Message == "SUCC")
            {
                if(SecureStorage.Remove("UserId") && SecureStorage.Remove("Token"))
                {
                    await Navigation.PushAsync(new LoginPage());
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert(ResxFile.btn_Logout, ResxFile.msg_Logout_Fail, ResxFile.btn_ok);
                }
            }
            else
            {
                await DisplayAlert(ResxFile.btn_Logout, ResxFile.msg_Logout_Fail, ResxFile.btn_ok);
            }
        }

        private async void toLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}