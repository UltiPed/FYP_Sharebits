using FYP_Sharebits.Models;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Resources;
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
        ObservableCollection<Coachs> coachs;

        public FindCoachPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            coachs = new ObservableCollection<Coachs>(await App.Database.GetCoachs());

            CoachListView.ItemsSource = coachs;
        }

        private async void CoachListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (CoachListView.SelectedItem == null) { return; }

            Coachs coach = CoachListView.SelectedItem as Coachs;

            CoachingRequest request = new CoachingRequest();
            request.coachID = coach.coachID;
            request.studentID = await Constants.GetUserId();


            //Checking the coach has coaching user already or not
            String studentQuery = "SELECT * FROM [Students] WHERE coachID=" + coach.coachID + " AND studentID='" + request.studentID + "'";

            var students = await App.Database.QueryStudents(studentQuery);

            if (students.Count > 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_coachAlready, ResxFile.err_confirm);
                CoachListView.SelectedItem = null;
                return;
            }


            //Checking the same request has been made already or not
            String requestQuery = "SELECT * FROM [CoachingRequest] WHERE coachID=" + coach.coachID + " AND studentID='" + request.studentID + "'";

            var checkRequests = await App.Database.QueryRequests(requestQuery);

            if (checkRequests.Count > 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_reqAlready, ResxFile.err_confirm);
                CoachListView.SelectedItem = null;
                return;
            }

            int result = await App.Database.InsertRow<CoachingRequest>(request);

            if (result > 0)
            {
                await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_sendCoachReq, ResxFile.btn_ok);
            } else
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_sendCoachReq, ResxFile.err_confirm);
            }

            CoachListView.SelectedItem = null;
        }
    }
}