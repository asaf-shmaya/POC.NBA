// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using NBA.Models;
//
//    var players = Players.FromJson(jsonString);

namespace NBA.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Players
    {
        [JsonProperty("_internal")]
        public Internal Internal { get; set; }

        [JsonProperty("league")]
        public League League { get; set; }
    }

    public partial class League
    {
        [JsonProperty("standard")]
        public List<Africa> Standard { get; set; }

        [JsonProperty("africa")]
        public List<Africa> Africa { get; set; }

        [JsonProperty("sacramento")]
        public List<Africa> Sacramento { get; set; }

        [JsonProperty("vegas")]
        public List<Africa> Vegas { get; set; }

        [JsonProperty("utah")]
        public List<Africa> Utah { get; set; }
    }

    public partial class Africa
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("temporaryDisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string TemporaryDisplayName { get; set; }

        [JsonProperty("personId")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long PersonId { get; set; }

        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("jersey")]
        public JerseyUnion Jersey { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("pos")]
        public Pos Pos { get; set; }

        [JsonProperty("heightFeet")]
        public HeightFeetUnion HeightFeet { get; set; }

        [JsonProperty("heightInches")]
        public HeightFeetUnion HeightInches { get; set; }

        [JsonProperty("heightMeters")]
        public string HeightMeters { get; set; }

        [JsonProperty("weightPounds")]
        public string WeightPounds { get; set; }

        [JsonProperty("weightKilograms")]
        public string WeightKilograms { get; set; }

        [JsonProperty("dateOfBirthUTC")]
        public string DateOfBirthUtc { get; set; }

        [JsonProperty("teamSitesOnly", NullValueHandling = NullValueHandling.Ignore)]
        public TeamSitesOnly TeamSitesOnly { get; set; }

        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }

        [JsonProperty("draft")]
        public Draft Draft { get; set; }

        [JsonProperty("nbaDebutYear")]
        public string NbaDebutYear { get; set; }

        [JsonProperty("yearsPro")]
        public string YearsPro { get; set; }

        [JsonProperty("collegeName")]
        public string CollegeName { get; set; }

        [JsonProperty("lastAffiliation")]
        public string LastAffiliation { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("isallStar", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsallStar { get; set; }
    }

    public partial class Draft
    {
        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("pickNum")]
        public string PickNum { get; set; }

        [JsonProperty("roundNum")]
        public string RoundNum { get; set; }

        [JsonProperty("seasonYear")]
        public string SeasonYear { get; set; }
    }

    public partial class TeamSitesOnly
    {
        [JsonProperty("playerCode")]
        public string PlayerCode { get; set; }

        [JsonProperty("posFull")]
        public PosFull PosFull { get; set; }

        [JsonProperty("displayAffiliation")]
        public string DisplayAffiliation { get; set; }

        [JsonProperty("freeAgentCode")]
        public string FreeAgentCode { get; set; }
    }

    public partial class Team
    {
        [JsonProperty("teamId")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long TeamId { get; set; }

        [JsonProperty("seasonStart")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long SeasonStart { get; set; }

        [JsonProperty("seasonEnd")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long SeasonEnd { get; set; }
    }

    public enum HeightFeetEnum { Empty };

    public enum HeightMeters { Empty, The175, The178, The18, The183, The185, The188, The19, The193, The196, The198, The201, The203, The206, The208, The211, The213, The216, The218, The221 };

    public enum JerseyEnum { Empty, The00 };

    public enum Pos { C, CF, Empty, F, FC, FG, G, GF };

    public enum PosFull { Center, CenterForward, Empty, Forward, ForwardCenter, ForwardGuard, Guard, GuardForward };

    public partial struct HeightFeetUnion
    {
        public HeightFeetEnum? Enum;
        public long? Integer;

        public static implicit operator HeightFeetUnion(HeightFeetEnum Enum) => new HeightFeetUnion { Enum = Enum };
        public static implicit operator HeightFeetUnion(long Integer) => new HeightFeetUnion { Integer = Integer };
    }

    public partial struct JerseyUnion
    {
        public JerseyEnum? Enum;
        public long? Integer;

        public static implicit operator JerseyUnion(JerseyEnum Enum) => new JerseyUnion { Enum = Enum };
        public static implicit operator JerseyUnion(long Integer) => new JerseyUnion { Integer = Integer };
    }

    public partial class Players
    {
        public static Players FromJson(string json) => JsonConvert.DeserializeObject<Players>(json, NBA.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Players self) => JsonConvert.SerializeObject(self, NBA.Models.Converter.Settings);
    }


}
