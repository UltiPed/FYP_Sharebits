﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public class Comment
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("recordDate")]
        public DateTimeOffset RecordDate { get; set; }
    }
}
