using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using FYP_Sharebits.Models.DBModels;
using System.Threading.Tasks;
using System.Linq;

namespace FYP_Sharebits.ViewModels
{

    public class CalendarViewModel : INotifyPropertyChanged
    {
        private CalendarEventCollection appointments;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Color> colorCollection;
        private ObservableCollection<string> meetingSubjectCollection;

        private Color redColor = Color.FromHex("#FF0000");
        private Color limeColor = Color.FromHex("#00FF00");

        private ObservableCollection<PlanRecords> records;
        private ObservableCollection<PlanItems> items;
        private ObservableCollection<HabitPlans> plans;

        public ObservableCollection<HabitPlans> Plans
        {
            get
            {
                return plans;
            }
        }

        public ObservableCollection<PlanItems> Items
        {
            get
            {
                return items;
            }
        }

        public ObservableCollection<PlanRecords> Records
        {
            get
            {
                return records;
            }
        }


        public CalendarEventCollection Appointments
        {
            get
            {
                return this.appointments;
            }

            set
            {
                this.appointments = value;
                this.OnPropertyChanged("Appointments");
            }
        }

        public CalendarViewModel()
        {
            this.Appointments = new CalendarEventCollection();
            LoadRecords().Wait();
            AddRecordsToAppointment();
        }

        private async Task LoadRecords()
        {
            records = new ObservableCollection<PlanRecords>(await App.Database.GetRecordsAsync());

            String itemIDs = "(";
            Boolean isFirst = true;
            foreach (PlanRecords record in records)
            {
                if (isFirst)
                {
                    itemIDs = itemIDs + record.itemID;
                    isFirst = false;
                }
                else
                {
                    itemIDs = itemIDs + ", " + record.itemID;
                }
            }
            itemIDs = itemIDs + ")";

            String itemQuery = "SELECT * FROM [PlanItems] WHERE itemID IN " + itemIDs;
            items = new ObservableCollection<PlanItems>(await App.Database.QueryPlanItems(itemQuery));

            String habitIDs = "(";
            isFirst = true;
            foreach (PlanItems item in items)
            {
                if (isFirst)
                {
                    habitIDs = habitIDs + item.habitID;
                    isFirst = false;
                }
                else
                {
                    habitIDs = habitIDs + ", " + item.habitID;
                }
            }
            habitIDs = habitIDs + ")";

            String habitQuery = "SELECT * FROM HabitPlans WHERE habitID IN " + habitIDs;
            plans = new ObservableCollection<HabitPlans>(await App.Database.QueryHabitPlans(habitQuery));
        }

        private void AddAppointmentDetails()
        {
            meetingSubjectCollection = new ObservableCollection<string>();
            meetingSubjectCollection.Add("BigDoor Board Meeting   Paris, France");
            meetingSubjectCollection.Add("Development Meeting   New York, U.S.A");
            meetingSubjectCollection.Add("Project Plan Meeting   Kuala Lumpur, Malaysia");
            meetingSubjectCollection.Add("Support - Web Meeting   Dubai, UAE");
            meetingSubjectCollection.Add("Project Release Meeting   Istanbul, Turkey");
            meetingSubjectCollection.Add("Customer Meeting   Tokyo, Japan");
            meetingSubjectCollection.Add("Consulting with doctor   Amsterdam, Netherlands");

            colorCollection = new ObservableCollection<Color>();
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FFF09609"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }

        private void AddAppointments()
        {
            var today = DateTime.Now.Date;
            var random = new Random();
            for (int month = -1; month < 2; month++)
            {
                for (int day = -5; day < 5; day++)
                {
                    for (int count = 0; count < 2; count++)
                    {
                        var appointment = new CalendarInlineEvent();
                        appointment.Subject = meetingSubjectCollection[random.Next(meetingSubjectCollection.Count)];
                        appointment.Color = colorCollection[random.Next(10)];
                        appointment.StartTime = today.AddMonths(month).AddDays(random.Next(0, 28)).AddHours(random.Next(9, 18));
                        appointment.EndTime = appointment.StartTime.AddHours(2);
                        if (count % 2 == 0)
                        {
                            appointment.IsAllDay = true;
                        }

                        this.Appointments.Add(appointment);
                    }
                }
            }
        }

        private void AddRecordsToAppointment()
        {
            foreach (PlanRecords record in records)
            {
                var appointment = new CalendarInlineEvent();
                PlanItems item = items.Where(x => x.itemID == record.itemID).FirstOrDefault();
                HabitPlans plan = plans.Where(x => x.habitID == item.habitID).FirstOrDefault();
                if (Appointments.Where(x => x.Subject.Equals(plan.habitName) && x.StartTime.Equals(record.recordDate)).Count() == 0)
                {
                    appointment.Subject = plan.habitName;
                    if (record.isDone)
                    {
                        appointment.Color = limeColor;
                    }
                    else
                    {
                        appointment.Color = redColor;
                    }
                    appointment.StartTime = record.recordDate;
                    appointment.EndTime = appointment.StartTime.AddHours(2);
                    appointment.IsAllDay = true;
                    this.appointments.Add(appointment);
                }
                else
                {
                    int targetIndex = Appointments.IndexOf(Appointments.Where(x => x.Subject.Equals(plan.habitName) && x.StartTime.Equals(record.recordDate)).FirstOrDefault());
                    if (record.isDone)
                    {
                        Appointments[targetIndex].Color = limeColor;
                    }
                    else
                    {
                        Appointments[targetIndex].Color = limeColor;
                    }
                }

            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
