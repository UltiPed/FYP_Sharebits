using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class HabitPlan
    {
        public int _id { get; set; }

        public String habitName { get; set; }

        public String habitType { get; set; }

        public DateTime startDate { get; set; }

        public DateTime? endDate { get; set; }

        public Creator creator { get; set; }

        public CreatedItems[] createdItems { get; set; }
    }
}
