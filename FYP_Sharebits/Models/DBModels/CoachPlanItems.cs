using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class CoachPlanItems
    {
        [PrimaryKey, AutoIncrement]
        public int itemID { get; set; }

        public int planID { get; set; }

        public String itemType { get; set; }

        public String itemName { get; set; }

        public Decimal itemGoal { get; set; }

        public int habitID_DB { get; set; }

        public CoachPlanItems() { }
    }
}
