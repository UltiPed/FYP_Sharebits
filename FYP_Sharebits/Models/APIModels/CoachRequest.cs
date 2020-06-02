using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class CoachRequest
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("coach")]
        public Coach Coach { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
