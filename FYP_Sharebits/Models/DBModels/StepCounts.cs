using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FYP_Sharebits.Models.DBModels
{
    public class StepCounts
    {
        [PrimaryKey]
        public DateTime recordDate { get; set; }

        public int stepCount { get; set; }
    }
}
