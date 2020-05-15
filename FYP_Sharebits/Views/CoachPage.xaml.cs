using FYP_Sharebits.Views.Coach;
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
    public partial class CoachPage : ContentPage
    {
        public CoachPage()
        {
            InitializeComponent();
        }

        private async void studentListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentListPage());
        }

        private async void assignPlanButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlanCreatePage());
        }

        private async void checkRequestButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckRequestPage());
        }
    }
}