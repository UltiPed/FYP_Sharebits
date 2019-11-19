using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.DBModels
{
    public class PlanItems
    {
        [PrimaryKey, AutoIncrement]
        public int itemID { get; set; }

        public int habitID { get; set; }

        public String itemType { get; set; }

        public String itemName { get; set; }

        public String itemGoal { get; set; }
    }
}
