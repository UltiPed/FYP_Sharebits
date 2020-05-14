using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class User
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("height")]
        public float Height { get; set; }

        [JsonProperty("weight")]
        public float Weight { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("sessionToken")]
        public string SessionToken { get; set; }

        [JsonProperty("createdHabits")]
        public HabitPlan[] CreatedHabits { get; set; }

    }
}
