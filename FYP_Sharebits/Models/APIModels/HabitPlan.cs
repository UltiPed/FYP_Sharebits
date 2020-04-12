using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class HabitPlan
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("habitName")]
        public string HabitName { get; set; }

        [JsonProperty("habitType")]
        public string HabitType { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [JsonProperty("creator")]
        public User Creator { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdItems")]
        public PlanItem[] CreatedItems { get; set; }

        [JsonProperty("newItems")]
        public PlanItem[] NewItems { get; set; }

        [JsonProperty("localID")]
        public int? LocalId { get; set; }
    }
}
