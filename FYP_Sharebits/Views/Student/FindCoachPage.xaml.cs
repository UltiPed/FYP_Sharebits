using FYP_Sharebits.Models;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Models.Functional;
using FYP_Sharebits.Resources;
using FYP_Sharebits.Views.Student;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindCoachPage : ContentPage
    {
        ObservableCollection<Models.APIModels.Coach> coachs;

        public FindCoachPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            coachs = new ObservableCollection<Models.APIModels.Coach>();

            var findCoach = await APIConnection.findCoaches();
            if (findCoach.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, findCoach.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            } else
            {
                coachs = new ObservableCollection<Models.APIModels.Coach>(findCoach.Data.FindCoaches);
            }

            CoachListView.ItemsSource = coachs;
        }

        private async void CoachListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (CoachListView.SelectedItem == null) { return; }

            var selectedCoach = CoachListView.SelectedItem as Models.APIModels.Coach;

            await Navigation.PushAsync(new CoachDetailPage(selectedCoach, true));

            CoachListView.SelectedItem = null;
        }
    }
}