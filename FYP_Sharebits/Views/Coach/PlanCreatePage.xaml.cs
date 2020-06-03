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
        private Models.APIModels.Student selectedStudent;

        ObservableCollection<String> pickerContents;

        private String coachID;
        private String userID;

        public PlanCreatePage(Models.APIModels.Student aStudent)
        {
            InitializeComponent();

            pickerContents = new ObservableCollection<string>();

            selectedStudent = aStudent;

            pickerContents.Add(ResxFile.pker_Normal);
            pickerContents.Add(ResxFile.pker_Challenge);
            TypePicker.ItemsSource = pickerContents;
            TypePicker.SelectedIndex = 0;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            userID = await Constants.GetUserId();
            coachID = await Constants.GetCoachID();

            
        }


        private async void proceedButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NameEntry.Text))
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_NullPlanName, ResxFile.err_confirm);
                return;
            }

            HabitPlans plan = new HabitPlans();
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