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
                if (plans.Count == 0)
                {
                    await DisplayAlert(ResxFile.str_error, ResxFile.err_noPlanPush, ResxFile.err_confirm);
                    return;
                }

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

        private async void btn_pullPlans_Clicked(object sender, EventArgs e)
        {
            var result1 = await DisplayAlert(ResxFile.msg_rusure, ResxFile.msg_coachResponsibility, ResxFile.btn_yes, ResxFile.btn_cancel);
            if (result1)
            {
                String userID = await Constants.GetUserId();
                var pullPlan = await APIConnection.pullPlans(userID);
                if (pullPlan.Errors != null)
                {
                    await DisplayAlert(ResxFile.str_error, pullPlan.Errors[0].Message, ResxFile.err_confirm);
                    return;
                } else
                {
                    ObservableCollection<HabitPlan> plans = new ObservableCollection<HabitPlan>(pullPlan.Data.PullPlans);
                    if (plans.Count == 0)
                    {
                        await DisplayAlert(ResxFile.str_error, ResxFile.err_noPlanPull, ResxFile.err_confirm);
                        return;
                    }
                    foreach (HabitPlan plan in plans)
                    {
                        String query1 = "SELECT * FROM [HabitPlans] WHERE habitID_DB='" + plan.Id + "'";
                        ObservableCollection<HabitPlans> checkPlan = new ObservableCollection<HabitPlans>(await App.Database.QueryHabitPlans(query1));
                        if (checkPlan.Count == 0)
                        {
                            HabitPlans newPlan = new HabitPlans();
                            newPlan.habitID_DB = plan.Id;
                            newPlan.habitName = plan.HabitName;
                            newPlan.habitType = plan.HabitType;
                            newPlan.startDate = DateTime.Now.Date;
                            if (newPlan.habitType.Equals("Challenge"))
                            {
                                DateTime sdt = plan.StartDate.UtcDateTime.Date;
                                DateTime edt = plan.EndDate.Value.UtcDateTime.Date;
                                newPlan.startDate = sdt;
                                newPlan.endDate = edt;
                            }

                            var result2 = await App.Database.InsertRow<HabitPlans>(newPlan);
                            if (result2 != 0)
                            {
                                checkPlan = new ObservableCollection<HabitPlans>(await App.Database.QueryHabitPlans(query1));
                                var localID = checkPlan[0].habitID;
                                foreach (PlanItem item in plan.CreatedItems)
                                {
                                    PlanItems newItem = new PlanItems();
                                    newItem.habitID = localID;
                                    newItem.itemGoal = item.ItemGoal.Value;
                                    newItem.itemName = item.ItemName;
                                    newItem.itemType = item.ItemType;

                                    var result3 = await App.Database.InsertRow<PlanItems>(newItem);
                                    if (result3 == 0)
                                    {
                                        await DisplayAlert(ResxFile.str_error, "result3", ResxFile.err_confirm);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    await DisplayAlert(ResxFile.msg_Success, ResxFile.msg_pullPlanSucc, ResxFile.btn_ok);
                }
            }
        }
    }
}