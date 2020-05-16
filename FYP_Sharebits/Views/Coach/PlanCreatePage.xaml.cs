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
    public partial class PlanCreatePage : ContentPage
    {
        ObservableCollection<String> pickerContents;

        ObservableCollection<String> studentIDs = new ObservableCollection<string>();

        String selectedID;

        int coachId;

        public PlanCreatePage()
        {
            InitializeComponent();

            pickerContents = new ObservableCollection<string>();

            pickerContents.Add(ResxFile.pker_Normal);
            pickerContents.Add(ResxFile.pker_Challenge);
            TypePicker.ItemsSource = pickerContents;
            TypePicker.SelectedIndex = 0;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ObservableCollection<Students> students;

            String userID = await Constants.GetUserId();

            String coachQuery = "SELECT * FROM [Coachs] WHERE userID='" + userID + "'";

            var coachCheck = await App.Database.QueryCoachs(coachQuery);

            if (coachCheck.Count > 0)
            {
                coachId = coachCheck[0].coachID;

                String studentQuery = "SELECT * FROM [Students] WHERE coachID=" + coachId;

                students = new ObservableCollection<Students>(await App.Database.QueryStudents(studentQuery));

                foreach(Students stu in students)
                {
                    studentIDs.Add(stu.studentID);
                }

                StudentPicker.ItemsSource = studentIDs;
            }
        }

        private void StudentPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedID = StudentPicker.SelectedItem.ToString();
        }

        private async void proceedButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NameEntry.Text))
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_NullPlanName, ResxFile.err_confirm);
                return;
            }

            CoachPlans plan = new CoachPlans();
            plan.habitName = NameEntry.Text;

            if (TypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Normal))
            {
                plan.habitType = "Normal";

            }
            else
            {
                plan.habitType = "Challenge";
                plan.endDate = EndDatePicker.Date;
            }
            plan.coachID = coachId;
            plan.studentID = selectedID;

            plan.startDate = StartDatePicker.Date;

            await Navigation.PushAsync(new PlanItemsPage(plan));

        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypePicker.SelectedItem == null) { return; }

            if (TypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Normal))
            {
                EndDatePicker.IsEnabled = false;
            }
            else
            {
                EndDatePicker.IsEnabled = true;
            }
        }
    }
}