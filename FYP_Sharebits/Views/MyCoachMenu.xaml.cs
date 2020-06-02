﻿using FYP_Sharebits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var loggedIn = await Constants.IsAuth();
            if (loggedIn)
            {
                var check = await Constants.CheckCoach();
                if (!check)
                {
                    ManageStudentLayout.IsVisible = false;
                } else
                {
                    ManageStudentLayout.IsVisible = true;
                }
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
    }
}