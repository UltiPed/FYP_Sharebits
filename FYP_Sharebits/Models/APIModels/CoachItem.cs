using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class CoachItem
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("coachPlan")]
        public CoachPlan CoachPlan { get; set; }

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        [JsonProperty("itemGoal")]
        public Decimal? ItemGoal { get; set; }
    }
}
