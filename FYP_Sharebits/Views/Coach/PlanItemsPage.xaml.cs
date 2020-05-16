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
    public partial class PlanItemsPage : ContentPage
    {
        public static ObservableCollection<CoachPlanItems> ToAddItems;

        private CoachPlans ToAddPlan;

        public PlanItemsPage()
        {
            InitializeComponent();
        }

        public PlanItemsPage(CoachPlans plan)
        {
            InitializeComponent();
            ToAddItems = new ObservableCollection<CoachPlanItems>();
            ToAddPlan = plan;
        }

        private async void newItemButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlanItemCreatePage());
        }

        private async void ProceedButton_Clicked(object sender, EventArgs e)
        {
            if (ToAddItems.Count == 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_NoPlanItem, ResxFile.err_confirm);
                return;
            }

            var result = await App.Database.InsertRow<CoachPlans>(ToAddPlan);
            if (result == 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                return;
            }

            var CurrentPlans = await App.Database.GetCoachPlansAsync();
            int planCount = CurrentPlans.Count;
            foreach (CoachPlanItems anItem in ToAddItems)
            {
                anItem.planID = planCount;
                var result2 = await App.Database.InsertRow<CoachPlanItems>(anItem);
                if (result2 == 0)
                {
                    await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                    return;
                }

            }

            await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_SuccNewPlan, ResxFile.btn_ok);
            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ItemListView.ItemsSource = ToAddItems;
        }

    }
}