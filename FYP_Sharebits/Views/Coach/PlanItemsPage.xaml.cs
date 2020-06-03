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

namespace FYP_Sharebits.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanItemsPage : ContentPage
    {
        public static ObservableCollection<PlanItems> ToAddItems;

        private HabitPlans ToAddPlan;

        private String coachID;

        private Models.APIModels.Student selectedStudent;

        public PlanItemsPage()
        {
            InitializeComponent();
        }

        public PlanItemsPage(HabitPlans plan, String coachID, Models.APIModels.Student selectedStudent)
        {
            InitializeComponent();
            ToAddItems = new ObservableCollection<PlanItems>();
            ToAddPlan = plan;
            this.coachID = coachID;
            this.selectedStudent = selectedStudent;

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

            var assignPlan = await APIConnection.AssignPlan(ToAddPlan, ToAddItems, coachID, selectedStudent.User.Id);
            if (assignPlan.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, assignPlan.Errors[0].Message, ResxFile.err_confirm);
                return;
            }

            await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_SuccNewPlan, ResxFile.btn_ok);
            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ItemListView.ItemsSource = new ObservableCollection<PlanItems>();
            ItemListView.ItemsSource = ToAddItems;
        }

    }
}