using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.Resources;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlanItemsPage : ContentPage
    {
        List<String> Categories;
        List<String> Measurements;
        List<String> Patterns;

        public CreatePlanItemsPage()
        {
            InitializeComponent();
            Categories = new List<string>();
            Categories.Add(ResxFile.pker_Walk);
            Categories.Add(ResxFile.pker_Run);
            CategoryPicker.ItemsSource = Categories;

            AddButton.IsEnabled = false;

        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedItem.ToString().Equals(ResxFile.pker_Walk))
            {
                Measurements = new List<string>();
                Measurements.Add(ResxFile.pker_Dur);
                Measurements.Add(ResxFile.pker_Dist);
                Measurements.Add(ResxFile.pker_Steps);

            } else if (CategoryPicker.SelectedItem.ToString().Equals(ResxFile.pker_Run))
            {
                Measurements = new List<string>();
                Measurements.Add(ResxFile.pker_Dur);
                Measurements.Add(ResxFile.pker_Dist);
            }

            MeasureTypePicker.ItemsSource = Measurements;

            PatternPicker.ItemsSource = new List<String>();
            AddButton.IsEnabled = false;

        }

        private void MeasureTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedItem.ToString().Equals(ResxFile.pker_Walk))
            {
                if (MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Dur))
                {
                    Patterns = new List<string>();
                    Patterns.Add(ResxFile.pat_Walk_Dur_minutes);
                    Patterns.Add(ResxFile.pat_Walk_Dur_hours);

                } else if (MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Steps))
                {
                    Patterns = new List<string>();
                    Patterns.Add(ResxFile.pat_Walk_Steps);

                } else if (MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Dist))
                {
                    Patterns = new List<string>();
                    Patterns.Add(ResxFile.pat_Walk_Dist_meter);
                }

            } else if (CategoryPicker.SelectedItem.ToString().Equals(ResxFile.pker_Run))
            {
                if (MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Dur))
                {
                    Patterns = new List<string>();
                    Patterns.Add(ResxFile.pat_Run_Dur_minutes);
                    Patterns.Add(ResxFile.pat_Run_Dur_hours);

                } else if (MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Dist))
                {
                    Patterns = new List<string>();
                    Patterns.Add(ResxFile.pat_Run_Dist_meter);
                }
            }

            PatternPicker.ItemsSource = Patterns;
        }


        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AmountEntry.Text) && !MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_YesNo))
            {
                await DisplayAlert(ResxFile.str_error, ResxFile.err_InvalidAmt, ResxFile.err_confirm);
                return;
            }

            PlanItems item = new PlanItems();

            if (!MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_YesNo))
            {
                item.itemGoal = Decimal.Parse((AmountEntry.Text));

                if (MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Dist))
                {
                    item.itemType = "Distance";
                
                } else if (!MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Dur))
                {
                    item.itemType = "Duration";

                } else if (!MeasureTypePicker.SelectedItem.ToString().Equals(ResxFile.pker_Steps))
                {
                    item.itemType = "Steps";

                } else
                {
                    item.itemType = "Times";
                }


            } else
            {
                item.itemType = "YesNo";
            }

            PlanItemList_Create.ToAddItems.Add(item);

            await Navigation.PopAsync();

        }

        private void PatternPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PatternPicker.SelectedIndex >= 0)
            {
                AddButton.IsEnabled = true;
            } else
            {
                AddButton.IsEnabled = false;
            }
        }
    }
}