using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.Functional;
using FYP_Sharebits.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachDetailPage : ContentPage
    {
        private Models.APIModels.Coach aCoach;
        public CoachDetailPage()
        {
            InitializeComponent();
        }

        public CoachDetailPage(Models.APIModels.Coach coach, Boolean couldReq)
        {
            InitializeComponent();
            aCoach = coach;
            User user = coach.User;

            NameLabel.Text = user.UserName;
            if (user.Gender.Equals("M"))
            {
                genderLabel.Text = ResxFile.lbl_gender_male;
            }
            else
            {
                genderLabel.Text = ResxFile.lbl_female;
            }
            heightLabel.Text = user.Height.ToString();
            weightLabel.Text = user.Weight.ToString();

            if (!couldReq)
            {
                this.ToolbarItems.Remove(tbi_request);
            }
        }

        private async void tbi_request_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert(ResxFile.msg_confirm, ResxFile.msg_confirmSendReq, ResxFile.btn_yes, ResxFile.btn_cancel);
            if (result)
            {
                String userID = await Constants.GetUserId();
                var sendReq = await APIConnection.createCoachingReq(userID, aCoach.Id);
                if (sendReq.Errors != null)
                {
                    await DisplayAlert(ResxFile.str_error, sendReq.Errors[0].Message, ResxFile.err_confirm);
                } else
                {
                    await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_sendCoachReq, ResxFile.btn_ok);
                    await Navigation.PopAsync();
                }
            }
        }
    }
}