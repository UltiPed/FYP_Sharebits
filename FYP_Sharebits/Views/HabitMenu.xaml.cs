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

namespace FYP_Sharebits.Views
{
    public partial class HabitMenu : ContentPage
    {
        ObservableCollection<HabitPlans> Plans;

        public HabitMenu()
        {
            InitializeComponent();
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
    }
}