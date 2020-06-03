using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class CoachPlan
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

        [JsonProperty("coach")]
        public Coach Coach { get; set; }

        [JsonProperty("student")]
        public Student Student { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdItems")]
        public CoachItem[] CreatedItems { get; set; }
    }
}
