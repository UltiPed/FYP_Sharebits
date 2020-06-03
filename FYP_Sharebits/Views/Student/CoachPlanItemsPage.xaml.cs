using FYP_Sharebits.Models.APIModels;
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
        ObservableCollection<CoachItem> ToAddItems;

        private CoachPlan ToAddPlan;

        public CoachPlanItemsPage()
        {
            InitializeComponent();
        }

        public CoachPlanItemsPage(CoachPlan plan)
        {
            InitializeComponent();
            ToAddPlan = plan;
            ToAddItems = new ObservableCollection<CoachItem>(plan.CreatedItems);
            ItemListView.ItemsSource = new ObservableCollection<CoachItem>();
            ItemListView.ItemsSource = ToAddItems;
        }


        private async void ProceedButton_Clicked(object sender, EventArgs e)
        {
            HabitPlans newPlan = new HabitPlans();
            newPlan.habitName = ToAddPlan.HabitName;
            newPlan.habitType = ToAddPlan.HabitType;
            newPlan.startDate = ToAddPlan.StartDate.UtcDateTime.Date;
            if (ToAddPlan.HabitType.Equals("Challenge"))
            {
                newPlan.endDate = ToAddPlan.EndDate.Value.UtcDateTime.Date;
            }

            int result = await App.Database.InsertRow<HabitPlans>(newPlan);
            if (result == 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                return;
            }

            var temp = await App.Database.GetPlansAsync();
            int newID = temp.Count;
            foreach (CoachItem item in ToAddItems)
            {
                PlanItems newItem = new PlanItems();
                newItem.habitID = newID;
                newItem.itemGoal = item.ItemGoal.Value;
                newItem.itemName = item.ItemName;
                newItem.itemType = item.ItemType;

                int result2 = await App.Database.InsertRow<PlanItems>(newItem);
                if (result2 == 0)
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