using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
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
    public partial class CoachPlanListPage : ContentPage
    {
        ObservableCollection<CoachPlan> coachPlans;
        String userID;

        public CoachPlanListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            userID = await Constants.GetUserId();

            var getAssigned = await APIConnection.GetAssigned(userID);
            if (getAssigned.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, getAssigned.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            }
            coachPlans = new ObservableCollection<CoachPlan>(getAssigned.Data.GetAssigned);

            PlanList.ItemsSource = coachPlans;
        }

        private async void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CoachPlan selectedPlan = PlanList.SelectedItem as CoachPlan;
            await Navigation.PushAsync(new CoachPlanItemsPage(selectedPlan));
        }
    }
}