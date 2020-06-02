using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
using FYP_Sharebits.Models.Functional;
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
    public partial class ConfirmRequestPage : ContentPage
    {
        private CoachRequest selectedRequest;
        public ConfirmRequestPage()
        {
            InitializeComponent();
        }

        public ConfirmRequestPage(CoachRequest aRequest)
        {
            InitializeComponent();
            selectedRequest = aRequest;

            NameLabel.Text = selectedRequest.User.UserName;

            if (selectedRequest.User.Gender.Equals("M"))
            {
                genderLabel.Text = ResxFile.lbl_male;
            } else
            {
                genderLabel.Text = ResxFile.lbl_female;
            }

            heightLabel.Text = selectedRequest.User.Height.ToString();

            weightLabel.Text = selectedRequest.User.Weight.ToString();


        }

        private async void tbi_response_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert(ResxFile.msg_confirm, ResxFile.msg_confirmCoachReq, ResxFile.btn_accept, ResxFile.btn_decline);
            if (result)
            {
                var coachID = await Constants.GetCoachID();
                var addCoach = await APIConnection.addCoach(selectedRequest.User.Id, coachID);
                if (addCoach.Errors != null)
                {
                    await DisplayAlert(ResxFile.str_error, addCoach.Errors[0].Message, ResxFile.err_confirm);
                    return;
                } else
                {
                    await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_addCoachSucc, ResxFile.btn_ok);

                    var result2 = await APIConnection.delCoachingReq(selectedRequest.Id);
                    if (result2.Errors != null)
                    {
                        await DisplayAlert(ResxFile.str_error, result2.Errors[0].Message, ResxFile.err_confirm);
                    }
                }
            } else
            {
                var result2 = await APIConnection.delCoachingReq(selectedRequest.Id);
                if (result2.Errors != null)
                {
                    await DisplayAlert(ResxFile.str_error, result2.Errors[0].Message, ResxFile.err_confirm);
                }
            }

            
            await Navigation.PopAsync();
        }
    }
}