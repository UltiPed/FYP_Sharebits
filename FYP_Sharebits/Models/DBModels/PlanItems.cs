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

        public Decimal itemGoal { get; set; }

        public int habitID_DB { get; set; }

        public PlanItems() { }

        /*
        public PlanItems(int aHabitID, String anItemType, String anItemName, String anItemGoal)
        {
            this.habitID = aHabitID;
            this.itemType = anItemType;
            this.itemName = anItemName;
            this.itemGoal = anItemGoal;
        }
        */
    }
}
