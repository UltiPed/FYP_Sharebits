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
    public partial class RecordListPage : ContentPage
    {
        int habitID;
        DateTime selectedDT;
        ObservableCollection<PlanRecords> records;


        public RecordListPage()
        {
            InitializeComponent();
        }

        public RecordListPage(int selectedID, DateTime requestedTime)
        {
            InitializeComponent();
            habitID = selectedID;
            selectedDT = requestedTime;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String itemQuery = "SELECT * FROM [PlanItems] WHERE habitID=" + habitID;

            ObservableCollection<PlanItems> items = new ObservableCollection<PlanItems>(await App.Database.QueryPlanItems(itemQuery));

            String itemIDs = "(";
            Boolean isFirst = true;
            foreach (PlanItems item in items)
            {
                if (isFirst)
                {
                    itemIDs = itemIDs + item.itemID;
                    isFirst = false;
                } else
                {
                    itemIDs = itemIDs + ", " + item.itemID;
                }
            }
            itemIDs = itemIDs + ")";

            String QueryString = "SELECT * FROM PlanRecords WHERE [recordDate]=? AND [itemID] IN " + itemIDs;

            records = new ObservableCollection<PlanRecords>(await App.Database.QueryPlanRecords(QueryString, DateTime.Now.Date));

            RecordListView.ItemsSource = records;
        }

        private void RecordListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (RecordListView.SelectedItem == null) { return; }

            RecordListView.SelectedItem = null;
        }
    }
}