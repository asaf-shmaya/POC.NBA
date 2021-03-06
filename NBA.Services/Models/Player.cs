using System;
using System.Collections.Generic;

using System.Globalization;
using NBA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NBA.Services.Models
{
    public class Player
    {
        [JsonProperty("personId")]
        public long PersonId { get; set; }
        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("temporaryDisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string TemporaryDisplayName { get; set; }

        [JsonProperty("heightMeters")]
        public string HeightMeters { get; set; }

        [JsonProperty("dateOfBirthUTC")]
        public string DateOfBirthUtc { get; set; }

        [JsonProperty("careerSummary")] // From profile
        public CareerSummary CareerSummary { get; set; }

        [JsonProperty("teamSitesOnly", NullValueHandling = NullValueHandling.Ignore)] // From teams
        public TeamSitesOnly TeamSitesOnly { get; set; }
        
        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("africa")]
        public Africa Africa { get; set; }
    }

    public class CareerSummary
    {
        [JsonProperty("fgp")]
        public string Fgp { get; set; }

        [JsonProperty("ppg")]
        public string Ppg { get; set; }

        [JsonProperty("rpg")]
        public string Rpg { get; set; }

        [JsonProperty("apg")]
        public string Apg { get; set; }

        [JsonProperty("bpg")]
        public string Bpg { get; set; }
    }

    public class TeamSitesOnly
    {
        [JsonProperty("posFull")]
        public string PosFull { get; set; }
    }


}