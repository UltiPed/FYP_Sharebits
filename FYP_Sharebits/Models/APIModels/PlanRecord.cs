using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class PlanRecord
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("planItem")]
        public PlanItem PlanItem { get; set; }

        [JsonProperty("recordDate")]
        public DateTimeOffset RecordDate { get; set; }

        [JsonProperty("progress")]
        public Decimal? Progress { get; set; }

        [JsonProperty("isDone")]
        public bool IsDone { get; set; }

        [JsonProperty("localID")]
        public int? LocalId { get; set; }
    }
}
