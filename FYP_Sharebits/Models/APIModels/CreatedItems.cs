using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class CreatedItems
    {
        public String itemType { get; set; }

        public String itemName { get; set; }

        public Decimal itemGoal { get; set; }

        public CreatedRecords[] createdRecords { get; set; }
    }
}
