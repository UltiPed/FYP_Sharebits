using FYP_Sharebits.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCoachMenu : ContentPage
    {
        public MyCoachMenu()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                no_wifi.IsVisible = true;
                authenticated_layout.IsVisible = false;
                unauthenticated_layout.IsVisible = false;
                return;
            }
            else
            {
                no_wifi.IsVisible = false;
            }
            var loggedIn = await Constants.IsAuth();
            if (loggedIn)
            {
                authenticated_layout.IsVisible = true;
                unauthenticated_layout.IsVisible = false;
                var check = await Constants.CheckCoach();
                if (!check)
                {
                    ManageStudentLayout.IsVisible = false;
                } else
                {
                    ManageStudentLayout.IsVisible = true;
                }
            }
            else
            {
                authenticated_layout.IsVisible = false;
                unauthenticated_layout.IsVisible = true;
            }
        }

        private async void coachListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoachListPage());
        }

        private async void findCoachButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FindCoachPage());
        }

        private async void checkRecommendButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoachPlanListPage());
        }

        private async void manageStudentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoachPage());
        }

        private async void toLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}