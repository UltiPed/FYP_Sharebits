using FYP_Sharebits.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views.Social
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSharedPlanPage : ContentPage
    {
        public ViewSharedPlanPage()
        {
            InitializeComponent();
            PlanList.ItemsSource = TempData.plans;
        }

        private async void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            HabitPlans plan = PlanList.SelectedItem as HabitPlans;
            await Navigation.PushAsync(new SharedPlanItemPage(plan));
        }
    }
}