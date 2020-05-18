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
        HabitPlans selectedPlan;
        ObservableCollection<PlanItems> items;
        public SharedPlanItemPage()
        {
            InitializeComponent();
        }

        public SharedPlanItemPage(HabitPlans plan)
        {
            InitializeComponent();
            selectedPlan = plan;
            items = TempData.GetPlanItems(plan.habitID);
            ItemListView.ItemsSource = items;
        }

        private async void userButton_Clicked(object sender, EventArgs e)
        {
            Users user = TempData.GetUser(selectedPlan.habitID);
            await Navigation.PushAsync(new SharedPlanUserPage(user));
        }

        private async void cloneButton_Clicked(object sender, EventArgs e)
        {
            HabitPlans toAddPlan = new HabitPlans();
            toAddPlan.habitName = selectedPlan.habitName;
            toAddPlan.habitType = selectedPlan.habitType;
            toAddPlan.startDate = DateTime.Now.Date;

            int result = await App.Database.InsertRow<HabitPlans>(toAddPlan);

            if (result != 0)
            {
                var currentPlans = await App.Database.GetPlansAsync();
                int newID = currentPlans.Count;

                foreach (PlanItems item in items)
                {
                    item.habitID = newID;
                    int result2 = await App.Database.InsertRow<PlanItems>(item);
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
    }
}