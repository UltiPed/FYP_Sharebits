using FYP_Sharebits.Models;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.Functional;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            gender.Items.Add(ResxFile.lbl_gender_male);     //0
            gender.Items.Add(ResxFile.lbl_gender_female);   //1
            var a = gender.SelectedIndex;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (await Constants.IsAuth())
            {
                await Navigation.PopAsync();
            }
        }
        private async void btn_signup_Clicked(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(email.Text) || 
                String.IsNullOrEmpty(userName.Text) || 
                String.IsNullOrEmpty(height.Text) || 
                String.IsNullOrEmpty(weight.Text) || 
                String.IsNullOrEmpty(pwd.Text) ||
                String.IsNullOrEmpty(con_pwd.Text) ||
                gender.SelectedIndex == -1)
            {
                await DisplayAlert(ResxFile.btn_Signup, ResxFile.msg_info_empty, ResxFile.btn_ok);
                return;
            }

            if(String.Compare(pwd.Text, con_pwd.Text) != 0)
            {
                await DisplayAlert(ResxFile.btn_Signup, ResxFile.msg_pwd_diff, ResxFile.btn_ok);
                return;
            }

            User user = new User()
            {
                Email = email.Text,
                UserName = userName.Text,
                Height = float.Parse(height.Text),
                Weight = float.Parse(weight.Text),
                Gender = (gender.SelectedIndex == 0) ? "M" : "F",
                Password = pwd.Text
            };
            BaseModel result = await APIConnection.Signup(user);
            if(result.Errors != null)
            {
                if(result.Errors[0].Message == "User exists already")
                {
                    await DisplayAlert(ResxFile.btn_Signup, ResxFile.msg_signup_repeat_user, ResxFile.btn_ok);
                    return;
                }
                await DisplayAlert(ResxFile.btn_Signup, ResxFile.msg_operation_fail, ResxFile.btn_ok);
                return;
            }
            await DisplayAlert(ResxFile.btn_Signup, ResxFile.msg_signup_succ, ResxFile.btn_ok);
            await Navigation.PopAsync();
            
        }
    }
}