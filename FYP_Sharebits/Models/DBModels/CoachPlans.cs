using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.DBModels
{
    public class CoachPlans
    {
        [PrimaryKey, AutoIncrement]
        public int planID { get; set; }

        public int coachID { get; set; }

        public String coachName { get; set; }

        public String studentID { get; set; }

        public String habitName { get; set; }

        public String habitType { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public String status { get; set; }

        public CoachPlans() { }
    }
}
