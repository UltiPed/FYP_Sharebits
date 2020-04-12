using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FYP_Sharebits.Models.APIModels
{
    public partial class BaseModel
    {
        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
    public partial class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("path")]
        public string[] Path { get; set; }
    }
    public partial class Location
    {
        [JsonProperty("line")]
        public long Line { get; set; }

        [JsonProperty("column")]
        public long Column { get; set; }
    }
    public partial class Data
    {
        [JsonProperty("login")]
        public AuthData Login { get; set; }

        [JsonProperty("searchPlan")]
        public HabitPlan[] SearchPlan { get; set; }
    }

    public partial class BaseModel
    {
        public static BaseModel FromJson(string json) => JsonConvert.DeserializeObject<BaseModel>(json, FYP_Sharebits.Models.APIModels.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BaseModel self) => JsonConvert.SerializeObject(self, FYP_Sharebits.Models.APIModels.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
