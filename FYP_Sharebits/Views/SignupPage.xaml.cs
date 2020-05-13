using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            gender.Items.Add(ResxFile.lbl_gender_male);
            gender.Items.Add(ResxFile.lbl_gender_female);
            var a = gender.SelectedIndex;
        }

        private async void btn_signup_Clicked(object sender, EventArgs e)
        {
            var a = gender.SelectedIndex;
            await DisplayAlert("Error", "Not finished yet", "OK");
        }
    }
}