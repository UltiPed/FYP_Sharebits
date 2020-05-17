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
    public partial class RecordDatesPage : ContentPage
    {
        ObservableCollection<PlanRecords> records;

        public RecordDatesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            String queryString = "SELECT DISTINCT recordDate FROM [PlanRecords]";
            records = new ObservableCollection<PlanRecords>(await App.Database.QueryPlanRecords(queryString));

            DateList.ItemsSource = records;
        }

        private async void DateList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (DateList.SelectedItem == null) { return; }

            DateTime selectedTime = (DateList.SelectedItem as PlanRecords).recordDate;
            await Navigation.PushAsync(new RecordPlansPage(selectedTime));

            DateList.SelectedItem = null;
        }
    }
}