using FYP_Sharebits.Models;
using FYP_Sharebits.Models.DBModels;
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
        ObservableCollection<CoachPlans> coachPlans;
        String userID;

        public CoachPlanListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            userID = await Constants.GetUserId();

            String planQuery = "SELECT * FROM [CoachPlans] WHERE studentID='" + userID + "'";
            coachPlans = new ObservableCollection<CoachPlans>(await App.Database.QueryCoachPlans(planQuery));

            PlanList.ItemsSource = coachPlans;
        }

        private async void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CoachPlans selectedPlan = PlanList.SelectedItem as CoachPlans;
            await Navigation.PushAsync(new CoachPlanItemsPage(selectedPlan));
        }
    }
}