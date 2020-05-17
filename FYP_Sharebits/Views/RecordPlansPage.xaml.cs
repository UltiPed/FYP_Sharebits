using FYP_Sharebits.Models.DBModels;
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
    public partial class RecordPlansPage : ContentPage
    {
        ObservableCollection<HabitPlans> plans;
        DateTime requestedTime;

        public RecordPlansPage()
        {
            InitializeComponent();
        }

        public RecordPlansPage(DateTime targetTime)
        {
            InitializeComponent();
            requestedTime = targetTime;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String recordQuery = "SELECT * FROM [PlanRecords] WHERE recordDate=?";

            ObservableCollection<PlanRecords> records = new ObservableCollection<PlanRecords>(await App.Database.QueryPlanRecords(recordQuery, requestedTime));

            String itemIDs = "(";
            Boolean isFirst = true;
            foreach(PlanRecords record in records)
            {
                if (isFirst)
                {
                    itemIDs = itemIDs + record.itemID;
                    isFirst = false;
                } else
                {
                    itemIDs = itemIDs + ", " + record.itemID;
                }
            }
            itemIDs = itemIDs + ")";

            String itemQuery = "SELECT DISTINCT habitID FROM [PlanItems] WHERE itemID IN " + itemIDs;
            ObservableCollection<PlanItems> items = new ObservableCollection<PlanItems>(await App.Database.QueryPlanItems(itemQuery));

            String habitIDs = "(";
            isFirst = true;
            foreach (PlanItems item in items)
            {
                if (isFirst)
                {
                    habitIDs = habitIDs + item.habitID;
                    isFirst = false;
                }
                else
                {
                    habitIDs = habitIDs + ", " + item.habitID;
                }
            }
            habitIDs = habitIDs + ")";

            String habitQuery = "SELECT * FROM HabitPlans WHERE habitID IN " + habitIDs;
            plans = new ObservableCollection<HabitPlans>(await App.Database.QueryHabitPlans(habitQuery));

            PlanList.ItemsSource = plans;
        }

        private async void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PlanList.SelectedItem == null) { return; }

            int selectedID = (PlanList.SelectedItem as HabitPlans).habitID;

            await Navigation.PushAsync(new RecordListPage(selectedID, requestedTime));

            PlanList.SelectedItem = null;
        }
    }
}