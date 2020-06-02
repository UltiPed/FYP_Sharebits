using FYP_Sharebits.Models;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Models.Functional;
using FYP_Sharebits.Views.Student;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachListPage : ContentPage
    {
        ObservableCollection<Models.APIModels.Coach> coachs;

        public CoachListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String userID = await Constants.GetUserId();
            coachs = new ObservableCollection<Models.APIModels.Coach>();

            var getCoachs = await APIConnection.getCoaches(userID);
            if (getCoachs.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, getCoachs.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            } else
            {
                coachs = new ObservableCollection<Models.APIModels.Coach>(getCoachs.Data.GetCoaches);
            }


            CoachListView.ItemsSource = coachs;
        }

        private async void CoachListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) { return; }

            var selectedCoach = CoachListView.SelectedItem as Models.APIModels.Coach;
            await Navigation.PushAsync(new CoachDetailPage(selectedCoach, false));

            CoachListView.SelectedItem = null;
        }
    }
}