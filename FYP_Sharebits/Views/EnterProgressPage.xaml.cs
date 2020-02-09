using FYP_Sharebits.Models.DBModels;
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
    public partial class EnterProgressPage : ContentPage
    {
        private PlanItems selectedItem;
        private PlanRecords recordToUpdate;
        public EnterProgressPage()
        {
            InitializeComponent();
        }

        public EnterProgressPage(PlanItems item)
        {
            InitializeComponent();
            selectedItem = item;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            PINameLabel.Text = selectedItem.itemName;
            TypeLabel.Text = selectedItem.itemType;

            String QueryString = "SELECT * FROM PlanRecords WHERE [itemID]=? AND [recordDate]=?";
            var QueryResult = await App.Database.QueryPlanRecords(QueryString, selectedItem.itemID, DateTime.Now.Date);
            if (QueryResult.Count > 0)
            {
                recordToUpdate = QueryResult[0];
                DoneLabel.Text = recordToUpdate.progress.ToString();
                if (recordToUpdate.isDone)
                {
                    EnterButton.IsEnabled = false;
                    AmountEntry.IsEnabled = false;
                }
            }

        }

        private async void EnterButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AmountEntry.Text))
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_InvalidAmt, ResxFile.err_confirm);
                return;
            }

            recordToUpdate.progress += Decimal.Parse(AmountEntry.Text);

            if (recordToUpdate.progress >= selectedItem.itemGoal)
            {
                recordToUpdate.isDone = true;
            }

            var UpdateResult = await App.Database.UpdateRow(recordToUpdate);
            if (UpdateResult > 0)
            {
                if (recordToUpdate.isDone)
                {
                    await DisplayAlert(ResxFile.msg_ItemFin_title, ResxFile.msg_itemFin, ResxFile.btn_ok);
                    await Navigation.PopAsync();

                } else
                {
                    await DisplayAlert(ResxFile.msg_UpdateRecSucc_title, ResxFile.msg_UpdateRecSucc, ResxFile.btn_ok);
                    await Navigation.PopAsync();
                }
                
            }
        }
    }
}