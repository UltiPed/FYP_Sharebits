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
            plan = aPlan;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            comments = new ObservableCollection<Comment>();
            var getComments = await APIConnection.getComment(plan.Id);
            if (getComments.Errors != null)
            {
                await DisplayAlert(ResxFile.str_error, getComments.Errors[0].Message, ResxFile.err_confirm);
                await Navigation.PopAsync();
                return;
            } else
            {
                comments = new ObservableCollection<Comment>(getComments.Data.GetComment);
            }

            CommentListView.ItemsSource = comments;
        }

        private async void commentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateCommentPage(plan));
        }
    }
}