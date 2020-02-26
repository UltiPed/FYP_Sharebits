using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class CreatedRecords
    {
        public DateTime recordDate { get; set; }

        public Decimal progress { get; set; }

        public Boolean isDone { get; set; }
    }
}
