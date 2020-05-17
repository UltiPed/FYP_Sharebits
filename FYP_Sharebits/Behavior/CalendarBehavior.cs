using System;
using System.Collections.ObjectModel;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace FYP_Sharebits.Behavior
{
    internal class CalendarBehavior : Behavior<ContentPage>
    {
        private Syncfusion.SfCalendar.XForms.SfCalendar calendar;
        private Grid grid;
        private double calendarWidth = 500;
        private int padding = 50;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable.Content.FindByName<Syncfusion.SfCalendar.XForms.SfCalendar>("calendar");
            this.grid = bindable.Content.FindByName<Grid>("grid");

            this.calendar.InlineViewMode = InlineViewMode.Agenda;
            this.calendar.ShowLeadingAndTrailingDays = false;
            this.calendar.BackgroundColor = Color.White;
            this.calendar.MonthViewSettings.DayHeight = 50;
            this.calendar.MonthViewSettings.WeekEndTextColor = Color.FromHex("#009688");
            this.calendar.MonthViewSettings.HeaderBackgroundColor = Color.White;
            this.calendar.MonthViewSettings.InlineBackgroundColor = Color.White;
            this.calendar.MonthViewSettings.DateSelectionColor = Color.FromHex("#EEEEEE");
            this.calendar.MonthViewSettings.TodayTextColor = Color.Red;
            this.calendar.MonthViewSettings.TodayBorderColor = Color.Red;
            this.calendar.MonthViewSettings.SelectedDayTextColor = Color.Black;
        }

        private void Bindable_SizeChanged(object sender, EventArgs e)
        {
            var sampleView = sender as ContentPage;
            if (sampleView.Width < this.calendarWidth + padding)
            {
                this.grid.WidthRequest = sampleView.Width - padding;
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            this.grid = null;
            this.calendar = null;
        }

    }
}
