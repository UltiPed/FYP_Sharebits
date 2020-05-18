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
    public partial class SharedPlanUserPage : ContentPage
    {
        public SharedPlanUserPage()
        {
            InitializeComponent();
        }

        public SharedPlanUserPage(Users user)
        {
            InitializeComponent();
            NameLabel.Text = user.userName;
            genderLabel.Text = user.gender;
            heightLabel.Text = user.height.ToString();
            weightLabel.Text = user.weight.ToString();
        }
    }
}