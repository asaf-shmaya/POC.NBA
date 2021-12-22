using NBA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBA.Services.Models
{
    public class Team
    {
        [JsonProperty("teamId")]
        public string TeamId { get; set; }
        [JsonProperty("africa")]
        public Africa Africa { get; set; }
        [JsonProperty("config")]
        public Config Config { get; set; }
    }

    public partial class Africa
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("confName")]
        //public ConfName ConfName { get; set; }
        public string ConfName { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class Config
    {
        [JsonProperty("primaryColor")]
        public string PrimaryColor { get; set; }
    }
}