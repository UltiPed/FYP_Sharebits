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

namespace FYP_Sharebits.Views.Social
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCommentPage : ContentPage
    {
        private HabitPlan plan;
        public CreateCommentPage()
        {
            InitializeComponent();
        }

        public CreateCommentPage(HabitPlan aPlan)
        {
            InitializeComponent();
            plan = aPlan;
        }

        private async void postButton_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert(ResxFile.msg_confirm, ResxFile.msg_confirmComment, ResxFile.btn_yes, ResxFile.btn_cancel);
            if (result)
            {
                if (!String.IsNullOrEmpty(contentEditor.Text))
                {
                    String commentContent = contentEditor.Text;
                    String userID = await Constants.GetUserId();
                    String planID = plan.Id;

                    DateTime now = DateTime.Now;
                    now = DateTime.SpecifyKind(now, DateTimeKind.Utc);
                    DateTimeOffset nowOffset = now;

                    var postComment = await APIConnection.commentPlan(userID, planID, nowOffset.ToString(), commentContent);
                    if (postComment.Errors != null)
                    {
                        await DisplayAlert(ResxFile.str_error, postComment.Errors[0].Message, ResxFile.err_confirm);
                        return;
                    }
                    else
                    {
                        await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_postCommentSucc, ResxFile.btn_ok);
                        await Navigation.PopAsync();
                    }
                }
            }
            
        }
    }
}