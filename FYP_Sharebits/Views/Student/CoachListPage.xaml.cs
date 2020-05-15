using FYP_Sharebits.Models;
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
    public partial class CoachListPage : ContentPage
    {
        ObservableCollection<Coachs> coachs;

        public CoachListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String userID = await Constants.GetUserId();

            String studentQuery = "SELECT * FROM [Students] WHERE studentID='" + userID + "'";

            var students = await App.Database.QueryStudents(studentQuery);

            coachs = new ObservableCollection<Coachs>();

            if (students.Count > 0)
            {
                String arguments = "";
                int count = 1;
                foreach (Students student in students){
                    if (count > 1)
                    {
                        arguments += ", " + student.coachID;
                    } else
                    {
                        arguments += student.coachID;
                    }
                    count++;
                }
                String coachQuery = "SELECT * FROM [Coachs] WHERE coachID IN (" + arguments + ")";
                coachs = new ObservableCollection<Coachs>(await App.Database.QueryCoachs(coachQuery));
            }
            

            CoachListView.ItemsSource = coachs;
        }
    }
}