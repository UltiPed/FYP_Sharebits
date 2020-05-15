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

namespace FYP_Sharebits.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckRequestPage : ContentPage
    {
        private ObservableCollection<CoachingRequest> requests;
        private int coachID;

        public CheckRequestPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String userID = await Constants.GetUserId();

            String coachQuery = "SELECT * FROM [Coashs] WHERE userID='" + userID + "'";

            var coachCheck = await App.Database.QueryCoachs(coachQuery);

            if (coachCheck.Count > 0)
            {
                coachID = coachCheck[0].coachID;

                String studentQuery = "SELECT * FROM [CoachingRequest] WHERE coachID=" + coachID;

                requests = new ObservableCollection<CoachingRequest>(await App.Database.QueryRequests(studentQuery));

                RequestListView.ItemsSource = requests;
            }
        }

        private async void RequestListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var answer = await DisplayAlert(ResxFile.err_confirm, ResxFile.msg_acceptStudent, ResxFile.btn_yes, ResxFile.btn_no);

            Students newStu;

            newStu = new Students();

            newStu.coachID = coachID;

            newStu.studentID = (e.SelectedItem as CoachingRequest).studentID;

            if (answer)
            {
                int result = await App.Database.InsertRow<Students>(newStu);

                if (result > 0)
                {
                    String deleteQuery = "DELETE FROM [CoachingRequest] WHERE coachID=" + coachID + " AND userID='" + newStu.studentID + "'";

                    int result2 = await App.Database.ExecuteQuery(deleteQuery);

                    if (result2 > 0)
                    {
                        await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_acceptRequest, ResxFile.btn_ok);
                    } else
                    {
                        await DisplayAlert(ResxFile.str_error, ResxFile.err_deleteRequest, ResxFile.btn_ok);
                    }
                } else
                {
                    await DisplayAlert(ResxFile.str_error, ResxFile.err_insertStudent, ResxFile.btn_ok);
                }
            } else
            {
                String deleteQuery = "DELETE FROM [CoachingRequest] WHERE coachID=" + coachID + " AND userID='" + newStu.studentID + "'";

                int result2 = await App.Database.ExecuteQuery(deleteQuery);

                if (result2 > 0)
                {
                    await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_declineRequest, ResxFile.btn_ok);
                }
                else
                {
                    await DisplayAlert(ResxFile.str_error, ResxFile.err_deleteRequest, ResxFile.btn_ok);
                }
            }

            OnAppearing();
        }
    }
}