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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfile : ContentPage
    {
        public MyProfile()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (await Constants.IsAuth())
            {
                //locading
                authenticated_layout.IsVisible = true;
                unauthenticated_layout.IsVisible = false;
            }
            else
            {
                //loading
                authenticated_layout.IsVisible = false;
                unauthenticated_layout.IsVisible = true;
            }
        }

        private async void btn_logout_Clicked(object sender, EventArgs e)
        {
            BaseModel result = await APIConnection.Logout();
            if(result.Errors != null)
            {
                await DisplayAlert(ResxFile.btn_Logout, ResxFile.msg_Logout_Fail, ResxFile.btn_ok);
            }
            else if(result.Data.Logout.Message == "SUCC")
            {
                if(SecureStorage.Remove("UserId") && SecureStorage.Remove("Token"))
                {
                    await Navigation.PushAsync(new LoginPage());
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    await DisplayAlert(ResxFile.btn_Logout, ResxFile.msg_Logout_Fail, ResxFile.btn_ok);
                }
            }
            else
            {
                await DisplayAlert(ResxFile.btn_Logout, ResxFile.msg_Logout_Fail, ResxFile.btn_ok);
            }
        }

        private async void toLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void btn_beCoach_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert(ResxFile.msg_rusure, ResxFile.msg_coachResponsibility, ResxFile.btn_yes, ResxFile.btn_cancel);
            if (result)
            {
                String userID = await Constants.GetUserId();
                BaseModel result2 = await APIConnection.beCoach(userID);
                if (result2.Errors != null)
                {
                    await DisplayAlert(ResxFile.str_error, result2.Errors[0].Message, ResxFile.err_confirm);
                }
                else if (result2.Data.BeCoach.Message == "SUCC")
                {
                    await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_beCoachSucc, ResxFile.btn_ok);
                    
                }
            }
        }

        private async void btn_pushPlans_Clicked(object sender, EventArgs e)
        {
            var result1 = await DisplayAlert(ResxFile.msg_rusure, ResxFile.msg_coachResponsibility, ResxFile.btn_yes, ResxFile.btn_cancel);
            if (result1)
            {
                String queryString = "SELECT * FROM [HabitPlans] WHERE habitID_DB IS NULL";
                ObservableCollection<HabitPlans> plans = new ObservableCollection<HabitPlans>(await App.Database.QueryHabitPlans(queryString));

                String planIDs = "(";
                Boolean isFirst = true;
                foreach (HabitPlans plan in plans)
                {
                    if (isFirst)
                    {
                        planIDs = planIDs + plan.habitID;
                        isFirst = false;
                    }
                    else
                    {
                        planIDs = planIDs + ", " + plan.habitID;
                    }
                }
                planIDs = planIDs + ")";

                String queryString2 = "SELECT * FROM [PlanItems] WHERE habitID IN " + planIDs;
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ObservableCollection<PlanItems> items = new ObservableCollection<PlanItems>(await App.Database.QueryPlanItems(queryString2));

                String userID = await Constants.GetUserId();

                BaseModel result2 = await APIConnection.PushPlans(plans, items, userID);
                if (result2.Errors != null)
                {
                    await DisplayAlert(ResxFile.btn_Logout, result2.Errors[0].Message, ResxFile.btn_ok);
                }
                else
                {
                    foreach (Models.APIModels.HabitPlan plan in result2.Data.PushPlans)
                    {
                        String queryString3 = "Update [HabitPlans] SET habitID_DB='" + plan.Id + "' WHERE habitID=" + plan.LocalId;
                        var result3 = await App.Database.ExecuteQuery(queryString3);
                        if (result3 == 0)
                        {
                            await DisplayAlert(ResxFile.str_error, "Update row(habitID = " + plan.LocalId + ") Failed.", ResxFile.err_confirm);
                            break;
                        }
                    }

                    await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_pushPlanSucc, ResxFile.btn_ok);
                }
            }
        }
    }
}