using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Models;

namespace FYP_Sharebits.Views
{
    public partial class SocialMenu : ContentPage
    {
        public SocialMenu()
        {
            InitializeComponent();
        }

        private async void toLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            SearchBarFrame.BackgroundColor = Color.White;
        }

        private void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            SearchBarFrame.BackgroundColor = Color.FromHex("#2196F3");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (await Constants.IsAuth())
            {
                //locading
                authenticated_layout.IsVisible = true;
                unauthenticated_layout.IsVisible = false;               
                social_search_bar.IsEnabled = true;
            }
            else
            {
                //loading
                authenticated_layout.IsVisible = false;
                unauthenticated_layout.IsVisible = true;                 
                social_search_bar.IsEnabled = false;
            }
        }

        private async void viewShareButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Social.ViewSharedPlanPage());
        }
    }
}