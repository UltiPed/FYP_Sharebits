using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class HabitPlans
    {
        [PrimaryKey, AutoIncrement]
        public int planID { get; set; }

        public String habitName { get; set; }

        public String habitType { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public HabitPlans() { }
        /*
        public HabitPlans(String aHabitName, String aHabitType, DateTime aStartDate, DateTime anEndDate)
        {
            this.habitName = aHabitName;
            this.habitType = aHabitType;
            this.startDate = aStartDate;
            this.endDate = anEndDate;
        }
        */
    }
}
