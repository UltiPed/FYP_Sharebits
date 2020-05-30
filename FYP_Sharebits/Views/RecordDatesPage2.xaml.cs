using FYP_Sharebits.Models.DBModels;
using FYP_Sharebits.ViewModels;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_Sharebits.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordDatesPage2 : ContentPage
    {
        private Boolean isFirst;
        public RecordDatesPage2()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQyMDg1QDMxMzgyZTMxMmUzME0yU1pCV2xwWGl0bGVZQkJlTE13djRZajh2LzBYejlsdk5XRUNydlRncEk9");
            InitializeComponent();
            calendar.InlineItemTapped += Calendar_InlineItemTapped;
            isFirst = true;
        }

        private async void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            var appointment = e.InlineEvent;
            DateTime selectedDate = e.SelectedDate.Date;
            var cvm = this.BindingContext as CalendarViewModel;
            var habitID = cvm.Plans.Where(x => x.habitName == appointment.Subject).FirstOrDefault().habitID;

            await Navigation.PushAsync(new RecordListPage(habitID, selectedDate));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (isFirst)
            {
                var cvm = this.BindingContext as CalendarViewModel;
                var result = await cvm.LoadRecords();
                if (result)
                {
                    cvm.AddRecordsToAppointment();
                }
                else
                {
                    await DisplayAlert("Error", "Error", "OK");
                }
                isFirst = false;
            }
        }
    }
}