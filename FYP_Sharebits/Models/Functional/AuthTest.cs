using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FYP_Sharebits.Models.Functional
{
    public partial class AuthTest
    {
        //[JsonProperty("data")]
        //public Data Data { get; set; }
        [JsonProperty("errors")]
        public Errors[] Errors { get; set; }
    }
    public partial class Data
    {
        [JsonProperty("test")]
        public String test { get; set; }
    }
}
