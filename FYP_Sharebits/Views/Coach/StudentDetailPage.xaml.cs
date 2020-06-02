using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views.Coach
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentDetailPage : ContentPage
    {
        public StudentDetailPage()
        {
            InitializeComponent();
        }

        public StudentDetailPage(Models.APIModels.Student student)
        {
            InitializeComponent();
            User user = student.User;
            NameLabel.Text = user.UserName;
            if (user.Gender.Equals("M"))
            {
                genderLabel.Text = ResxFile.lbl_gender_male;
            } else
            {
                genderLabel.Text = ResxFile.lbl_female;
            }
            heightLabel.Text = user.Height.ToString();
            weightLabel.Text = user.Weight.ToString();
        }
    }
}