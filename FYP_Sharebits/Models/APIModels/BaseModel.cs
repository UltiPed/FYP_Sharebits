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

        [JsonProperty("logout")]
        public Msg Logout { get; set; }

        [JsonProperty("createUser")]
        public User CreateUser { get; set; }

        [JsonProperty("createHabitPlan")]
        public HabitPlan CreateHabitPlan { get; set; }

        [JsonProperty("createItem")]
        public PlanItem CreateItem { get; set; }

        [JsonProperty("createRecord")]
        public PlanRecord CreateRecord { get; set; }

        [JsonProperty("searchPlan")]
        public HabitPlan[] SearchPlan { get; set; }

        [JsonProperty("setPublish")]
        public Msg SetPublish { get; set; }

        [JsonProperty("pushPlans")]
        public HabitPlan[] PushPlans { get; set; }

        [JsonProperty("pullPlans")]
        public HabitPlan[] PullPlans { get; set; }

        [JsonProperty("pushItems")]
        public PlanItem[] PushItems { get; set; }

        [JsonProperty("pushRecords")]
        public PlanRecord[] PushRecords { get; set; }

        [JsonProperty("beCoach")]
        public Msg BeCoach { get; set; }

        [JsonProperty("addCoach")]
        public Msg AddCoach { get; set; }

        [JsonProperty("createCoachingReq")]
        public Msg CreateCoachingReq { get; set; }

        [JsonProperty("delCoachingReq")]
        public Msg DelCoachingReq { get; set; }

        [JsonProperty("commentPlan")]
        public Msg CommentPlan { get; set; }

        [JsonProperty("findCoaches")]
        public Coach[] FindCoaches { get; set; }

        [JsonProperty("getCoaches")]
        public Coach[] GetCoaches { get; set; }

        [JsonProperty("getStudents")]
        public Student[] GetStudents { get; set; }

        [JsonProperty("getCoachingReq")]
        public CoachRequest[] GetCoachingReq { get; set; }

        [JsonProperty("getComment")]
        public Comment[] GetComment { get; set; }

        [JsonProperty("assignPlan")]
        public Msg AssignPlan { get; set; }

        [JsonProperty("getAssigned")]
        public CoachPlan[] GetAssigned { get; set; }
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
