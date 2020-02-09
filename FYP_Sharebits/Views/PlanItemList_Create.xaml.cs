using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Data;
using System.Collections.ObjectModel;
using FYP_Sharebits.Resources;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanItemList_Create : ContentPage
    {
        public static ObservableCollection<PlanItems> ToAddItems;

        private HabitPlans ToAddPlan;

        public PlanItemList_Create()
        {
            InitializeComponent();
        }

        public PlanItemList_Create(HabitPlans plan)
        {
            InitializeComponent();
            ToAddItems = new ObservableCollection<PlanItems>();
            ToAddPlan = plan;
        }

        private async void newItemButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePlanItemsPage());
        }

        private async void ProceedButton_Clicked(object sender, EventArgs e)
        {
            if (ToAddItems.Count == 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_NoPlanItem, ResxFile.err_confirm);
                return;
            }

            var result = await App.Database.InsertRow<HabitPlans>(ToAddPlan);
            if (result == 0)
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                return;
            }

            var CurrentPlans = await App.Database.GetPlansAsync();
            int planCount = CurrentPlans.Count;
            foreach (PlanItems anItem in ToAddItems)
            {
                anItem.habitID = planCount;
                var result2 = await App.Database.InsertRow<PlanItems>(anItem);
                if (result2 == 0)
                {
                    await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                    return;
                }

                if (ToAddPlan.startDate.Date.Equals(DateTime.Now.Date))
                {
                    PlanRecords ToAddRecord = new PlanRecords();
                    var CurrentItems = await App.Database.GetItemsAsync();
                    int itemCount = CurrentItems.Count;

                    ToAddRecord.itemID = itemCount;
                    ToAddRecord.recordDate = DateTime.Now.Date;
                    ToAddRecord.progress = 0;
                    ToAddRecord.isDone = false;
                    ToAddRecord.recordID = ToAddRecord.itemID + "-" + ToAddRecord.recordDate.ToString();

                    var result3 = await App.Database.InsertRow(ToAddRecord);
                    if (result3 == 0)
                    {
                        await DisplayAlert(ResxFile.str_error, ResxFile.str_error, ResxFile.err_confirm);
                        return;
                    }
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