using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Models.Functional;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckRequestPage : ContentPage
    {
        private ObservableCollection<CoachRequest> requests;
        private String coachID;
        private String coachName;

        public CheckRequestPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String userID = await Constants.GetUserId();

            coachName = await Constants.GetUserName();
            coachID = await Constants.GetCoachID();

            var getReq = await APIConnection.getCoachingReq(coachID);

            requests = new ObservableCollection<CoachRequest>();
            if (getReq.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, getReq.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
            } else
            {
                requests = new ObservableCollection<CoachRequest>(getReq.Data.GetCoachingReq);
            }

            RequestListView.ItemsSource = requests;
        }

        private async void RequestListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            CoachRequest selectedReq = RequestListView.SelectedItem as CoachRequest;

            await Navigation.PushAsync(new ConfirmRequestPage(selectedReq));

            RequestListView.SelectedItem = null;
        }
    }
}