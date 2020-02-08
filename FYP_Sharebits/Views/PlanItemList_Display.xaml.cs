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
    }
}