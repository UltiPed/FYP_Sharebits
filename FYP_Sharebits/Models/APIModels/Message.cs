using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_Sharebits.Models.APIModels
{
    public partial class Message
    {
        [JsonProperty("message")]
        public string Msg { get; set; }
    }
}
