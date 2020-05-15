using FYP_Sharebits.Data;
using FYP_Sharebits.Models;
using FYP_Sharebits.Models.DBModels;
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
    public partial class StudentListPage : ContentPage
    {
        private ObservableCollection<Students> students;

        public StudentListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String userID = await Constants.GetUserId();

            String coachQuery = "SELECT * FROM [Coashs] WHERE userID='" + userID + "'";

            var coachCheck = await App.Database.QueryCoachs(coachQuery);

            if (coachCheck.Count > 0)
            {
                int coachID = coachCheck[0].coachID;

                String studentQuery = "SELECT * FROM [Students] WHERE coachID=" + coachID;

                students = new ObservableCollection<Students>(await App.Database.QueryStudents(studentQuery));

                StudentsListView.ItemsSource = students;
            }

        }
    }
}