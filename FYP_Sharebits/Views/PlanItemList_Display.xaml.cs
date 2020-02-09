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

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanItemList_Display : ContentPage
    {
        public ObservableCollection<PlanItems> currentItems;
        public HabitPlans selectedPlan;

        public PlanItemList_Display()
        {
            InitializeComponent();
        }

        public PlanItemList_Display(HabitPlans plan)
        {
            InitializeComponent();
            selectedPlan = plan;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            String queryString = "SELECT * FROM PlanItems WHERE [habitID]='" + selectedPlan.planID.ToString() + "'";
            currentItems = new ObservableCollection<PlanItems>(await App.Database.QueryPlanItems(queryString));
            ItemListView.ItemsSource = currentItems;
        }

        private async void ItemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) { return; }

            if (DateTime.Compare(selectedPlan.startDate, DateTime.Now.Date) > 0)
            {
                await DisplayAlert(ResxFile.msg_InvalidAction, ResxFile.msg_NotStarted, ResxFile.btn_ok);
                await Navigation.PopAsync();
                return;
            }

            PlanItems selectedItem = e.SelectedItem as PlanItems;

            await Navigation.PushAsync(new EnterProgressPage(selectedItem));

            ItemListView.SelectedItem = null;
        }
    }
}