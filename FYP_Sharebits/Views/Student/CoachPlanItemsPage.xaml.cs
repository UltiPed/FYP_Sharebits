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

namespace FYP_Sharebits.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachPlanItemsPage : ContentPage
    {
        ObservableCollection<CoachPlanItems> ToAddItems;

        private CoachPlans ToAddPlan;

        public CoachPlanItemsPage()
        {
            InitializeComponent();
        }

        public CoachPlanItemsPage(CoachPlans plan)
        {
            InitializeComponent();
            ToAddPlan = plan;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int planID = ToAddPlan.planID;
            String itemQuery = "SELECT * FROM [CoachPlanItems] WHERE planID=" + planID;
            ToAddItems = new ObservableCollection<CoachPlanItems>(await App.Database.QueryCoachPlanItems(itemQuery));

            ItemListView.ItemsSource = ToAddItems;
        }

        private async void ProceedButton_Clicked(object sender, EventArgs e)
        {
            HabitPlans newPlan = new HabitPlans();
            newPlan.habitName = ToAddPlan.habitName;
            newPlan.habitType = ToAddPlan.habitType;
            newPlan.startDate = ToAddPlan.startDate;
            newPlan.endDate = ToAddPlan.endDate;

            int result = await App.Database.InsertRow<HabitPlans>(newPlan);
            if (result == 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                return;
            }

            var temp = await App.Database.GetPlansAsync();
            int newID = temp.Count;
            foreach (CoachPlanItems item in ToAddItems)
            {
                PlanItems newItem = new PlanItems();
                newItem.habitID = newID;
                newItem.itemGoal = item.itemGoal;
                newItem.itemName = item.itemName;
                newItem.itemType = item.itemType;

                int result2 = await App.Database.InsertRow<PlanItems>(newItem);
                if (result2 == 9)
                {
                    await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                    return;
                }
            }
            await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_SuccNewPlan, ResxFile.btn_ok);
            await Navigation.PopToRootAsync();
        }
    }
}