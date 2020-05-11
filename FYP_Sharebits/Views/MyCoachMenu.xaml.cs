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
    }
}