using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class PlanRecords
    {
        public int itemID { get; set; }

        public DateTime recordDate { get; set; }

        public Decimal progress { get; set; }

        public Boolean isDone { get; set; }
    }
}
