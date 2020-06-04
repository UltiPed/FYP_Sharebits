using FYP_Sharebits.Models;
using FYP_Sharebits.Models.APIModels;
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

namespace FYP_Sharebits.Views.Social
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentListPage : ContentPage
    {
        private HabitPlan plan;
        private ObservableCollection<Comment> comments;

        public CommentListPage()
        {
            InitializeComponent();
        }

        public CommentListPage(HabitPlan aPlan)
        {
            InitializeComponent();
            plan = aPlan;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadComment();
        }

        public async Task LoadComment()
        {
            comments = new ObservableCollection<Comment>();
            var getComments = await APIConnection.getComment(plan.Id);
            if (getComments.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, getComments.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            }
            else
            {
                comments = new ObservableCollection<Comment>(getComments.Data.GetComment);
            }
            CommentListView.ItemsSource = comments;
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
                        contentEditor.Text = String.Empty;
                        contentEditor.Unfocus();
                    }
                    await LoadComment();
                }
            }
        }

        private async void commentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateCommentPage(plan));
        }
    }
}