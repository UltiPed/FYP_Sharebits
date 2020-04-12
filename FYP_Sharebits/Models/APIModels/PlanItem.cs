using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class PlanItem
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("habitPlan")]
        public HabitPlan HabitPlan { get; set; }

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        [JsonProperty("itemGoal")]
        public Decimal? ItemGoal { get; set; }

        [JsonProperty("createdRecords")]
        public PlanRecord[] CreatedRecords { get; set; }

        [JsonProperty("newRecords")]
        public PlanRecord[] NewRecords { get; set; }

        [JsonProperty("localID")]
        public int? LocalId { get; set; }
    }
}
