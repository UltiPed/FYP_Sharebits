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
        public HabitMenu()
        {
            InitializeComponent();
        }

        private async void test_Clicked(object sender, EventArgs e)
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

            int insertResult = await App.Database.InsertUser(newUser);

            ObservableCollection<Users> NewUsers = new ObservableCollection<Users>(await App.Database.GetUsersAsync());
            userList.ItemsSource = NewUsers;
            */
        }
    }
}