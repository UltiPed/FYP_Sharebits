﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var resModel = ResModel.FromJson(jsonString);
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FYP_Sharebits.Models.APIModels
{
    public partial class AuthData
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
