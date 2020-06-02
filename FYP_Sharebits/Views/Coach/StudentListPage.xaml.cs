using FYP_Sharebits.Data;
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

namespace FYP_Sharebits.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentListPage : ContentPage
    {
        private ObservableCollection<Models.APIModels.Student> students;

        public StudentListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            String userID = await Constants.GetUserId();
            String coachID = await Constants.GetCoachID();

            var getStu = await APIConnection.getStudents(coachID);

            if (getStu.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, getStu.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            } else
            {
                students = new ObservableCollection<Models.APIModels.Student>(getStu.Data.GetStudents);
            }
            
            StudentsListView.ItemsSource = students;

        }

        private async void StudentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) { return; }

            var aUser = StudentsListView.SelectedItem as Models.APIModels.Student;

            await Navigation.PushAsync(new StudentDetailPage(aUser));

            StudentsListView.SelectedItem = null;
        }
    }
}