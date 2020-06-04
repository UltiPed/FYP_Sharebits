using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Models.Functional;
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
    public partial class ViewSharedPlanPage : ContentPage
    {
        private ObservableCollection<HabitPlan> plans;
        
        public ViewSharedPlanPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(!(await Constants.IsAuth())){
                authenticated_layout.IsVisible = false;
                unauthenticated_layout.IsVisible = true;
                return;
            }
            authenticated_layout.IsVisible = true;
            unauthenticated_layout.IsVisible = false;

            var findPlan = await APIConnection.searchPlan("");
            if (findPlan.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, findPlan.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            } else
            {
                plans = new ObservableCollection<HabitPlan>(findPlan.Data.SearchPlan);
            }

            PlanList.ItemsSource = plans;
        }

        private async void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            HabitPlan plan = PlanList.SelectedItem as HabitPlan;
            await Navigation.PushAsync(new SharedPlanItemPage(plan));
        }

        private async void toLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}