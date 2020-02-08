using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class PlanRecords
    {
        [PrimaryKey]
        public String recordID { get; set; }

        public int itemID { get; set; }

        public DateTime recordDate { get; set; }

        public Decimal progress { get; set; }

        public Boolean isDone { get; set; }

        public PlanRecords() { }

        /*
        public PlanRecords(int anItemID, DateTime aRecordDate, Decimal aProgress, Boolean anIsDone)
        {
            this.itemID = anItemID;
            this.recordDate = aRecordDate;
            this.progress = aProgress;
            this.isDone = anIsDone;

            this.recordID = itemID.ToString() + "-" + recordDate.ToString();
        }
        */
    }

    

}
