﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using NBA.Models;
//
//    var teams = Teams.FromJson(jsonString);

namespace NBA.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Teams
    {
        [JsonProperty("_internal")]
        public Internal Internal { get; set; }

        [JsonProperty("league")]
        public League League { get; set; }
    }

    public partial class Africa
    {
        [JsonProperty("isNBAFranchise")]
        public bool IsNbaFranchise { get; set; }

        [JsonProperty("isAllStar")]
        public bool IsAllStar { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("altCityName")]
        public string AltCityName { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("tricode")]
        public string Tricode { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("urlName")]
        public string UrlName { get; set; }

        [JsonProperty("teamShortName")]
        public string TeamShortName { get; set; }

        [JsonProperty("confName")]
        public ConfName ConfName { get; set; }

        [JsonProperty("divName")]
        public DivName DivName { get; set; }
    }

    public enum ConfName { East, Empty, Intl, Sacramento, Summer, Utah, West };

    public enum DivName { Atlantic, Central, East, Empty, Northwest, Pacific, Southeast, Southwest, West };

    public partial class Teams
    {
        public static Teams FromJson(string json) => JsonConvert.DeserializeObject<Teams>(json, NBA.Models.Converter.Settings);
    }


}
