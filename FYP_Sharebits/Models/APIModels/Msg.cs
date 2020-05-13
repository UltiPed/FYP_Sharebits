using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public partial class Msg
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
