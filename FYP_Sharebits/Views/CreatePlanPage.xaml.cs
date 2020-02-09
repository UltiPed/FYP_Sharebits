using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FYP_Sharebits.Data;
using System.Collections.ObjectModel;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Resources;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlanPage : ContentPage
    {
        ObservableCollection<String> pickerContents;

        public CreatePlanPage()
        {
            InitializeComponent();

            pickerContents = new ObservableCollection<string>();

            pickerContents.Add(ResxFile.pker_Normal);
            pickerContents.Add(ResxFile.pker_Challenge);
            TypePicker.ItemsSource = pickerContents;
            TypePicker.SelectedIndex = 0;
        }

        private async void proceedButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NameEntry.Text))
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_NullPlanName, ResxFile.err_confirm);
                return;
            }

            HabitPlans plan = new HabitPlans();
            plan.habitName = NameEntry.Text;

            if (TypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Normal))
            {
                plan.habitType = "Normal";
            
            } else
            {
                plan.habitType = "Challenge";
                plan.endDate = EndDatePicker.Date;
            }

            plan.startDate = StartDatePicker.Date;

            await Navigation.PushAsync(new PlanItemList_Create(plan));

        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypePicker.SelectedItem == null) { return; }

            if (TypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Normal))
            {
                EndDatePicker.IsEnabled = false;
            } else
            {
                EndDatePicker.IsEnabled = true;
            }
        }
    }
}