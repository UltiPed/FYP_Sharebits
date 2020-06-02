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

namespace FYP_Sharebits.Views.Social
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharedPlanItemPage : ContentPage
    {
        private HabitPlan selectedPlan;
        private ObservableCollection<PlanItem> items;
        public SharedPlanItemPage()
        {
            InitializeComponent();
        }

        public SharedPlanItemPage(HabitPlan plan)
        {
            InitializeComponent();
            selectedPlan = plan;

            items = new ObservableCollection<PlanItem>(plan.CreatedItems);

            ItemListView.ItemsSource = items;
        }

        private async void userButton_Clicked(object sender, EventArgs e)
        {
            User aUser = selectedPlan.Creator;
            await Navigation.PushAsync(new SharedPlanUserPage(aUser));
        }

        private async void cloneButton_Clicked(object sender, EventArgs e)
        {
            HabitPlans toAddPlan = new HabitPlans();
            toAddPlan.habitName = selectedPlan.HabitName;
            toAddPlan.habitType = selectedPlan.HabitType;
            toAddPlan.startDate = DateTime.Now.Date;
            if (selectedPlan.HabitType.Equals("Challenge"))
            {
                DateTime sdt = selectedPlan.StartDate.UtcDateTime;
                DateTime edt = selectedPlan.EndDate.Value.UtcDateTime;
                toAddPlan.endDate = toAddPlan.startDate.AddDays((edt - sdt).TotalDays).Date;
            }

            int result = await App.Database.InsertRow<HabitPlans>(toAddPlan);

            if (result != 0)
            {
                var currentPlans = await App.Database.GetPlansAsync();
                int newID = currentPlans.Count;

                foreach (PlanItem item in items)
                {
                    PlanItems toAddItem = new PlanItems();
                    toAddItem.habitID = newID;
                    toAddItem.itemGoal = item.ItemGoal.Value;
                    toAddItem.itemType = item.ItemType;
                    toAddItem.itemName = item.ItemName;
                    int result2 = await App.Database.InsertRow<PlanItems>(toAddItem);
                    if (result2 == 0)
                    {
                        await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                        return;
                    }
                }

                await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_SuccNewPlan, ResxFile.btn_ok);
                await Navigation.PopToRootAsync();
            } else
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                return;
            }
            
        }

        private async void commentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentListPage(selectedPlan));
        }
    }
}