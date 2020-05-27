using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FYP_Sharebits.Data;
using System.Collections.ObjectModel;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Models.Functional;
using Syncfusion.SfCalendar.XForms;

namespace FYP_Sharebits.Views
{
    public partial class HabitMenu : ContentPage
    {
        ObservableCollection<HabitPlans> Plans;
        public HabitMenu()
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQyMDg1QDMxMzgyZTMxMmUzME0yU1pCV2xwWGl0bGVZQkJlTE13djRZajh2LzBYejlsdk5XRUNydlRncEk9");
            InitializeComponent();
            //calendar.InlineItemTapped += Calendar_InlineItemTapped;
        }

        private async void add_Clicked(object sender, EventArgs e)
        {
            //Just some sample code for testing local db(users)
            /*
            ObservableCollection<Users> OriginalUsers = new ObservableCollection<Users>(await App.Database.GetUsersAsync());

            Users newUser = new Users();
            newUser.userID = "ID" + OriginalUsers.Count.ToString();
            newUser.userName = "Name" + OriginalUsers.Count.ToString();
            newUser.height = (Decimal)1.90;
            newUser.weight = (Decimal)65.5;
            newUser.birthday = DateTime.Today.Date;
            newUser.gender = "Male";

            int insertResult = await App.Database.InsertRow<Users>(newUser);

            ObservableCollection<Users> NewUsers = new ObservableCollection<Users>(await App.Database.GetUsersAsync());
            userList.ItemsSource = NewUsers;
            */
            await Navigation.PushAsync(new CreatePlanPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Plans = new ObservableCollection<HabitPlans>(await App.Database.GetPlansAsync());
           PlanList.ItemsSource = Plans;
        }

        private async void PlanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PlanList.SelectedItem == null) { return; }

            HabitPlans planSelected = e.SelectedItem as HabitPlans;
            await Navigation.PushAsync(new PlanItemList_Display(planSelected));
            PlanList.SelectedItem = null;


        }

        private async void testing_Clicked(object sender, EventArgs e)
        {
            //String queryString = "query{ HabitPlan { _id, habitName habitType, startDate, endDate, createdItems{ _id, itemType, itemGoal, createdRecords{ recordDate, progress, isDone } }, creator{ userName, password, email, height, weight }        }    }";
            //await APIConnection.GetAllPlans();
            //await APIConnection.AuthTest();
            //await APIConnection.GetAllPlans_GraphQL(queryString);

            await Navigation.PushAsync(new RecordDatesPage2());
        }

        private async void testStep_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestStepCounter());
        }
        private void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            var appointment = e.InlineEvent;
            
            DisplayAlert(appointment.Subject, appointment.StartTime.ToString(), "ok");
        }
    }
}