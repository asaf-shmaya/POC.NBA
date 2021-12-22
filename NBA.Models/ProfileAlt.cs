using System.Collections.Generic;

namespace NBA.Models.Alt
{

    public partial class Profile
    {
        public Internal Internal { get; set; }
        public League League { get; set; }
    }

    public partial class Internal
    {
        public string PubDateTime { get; set; }
        public string IgorPath { get; set; }
        public string Xslt { get; set; }
        public bool? XsltForceRecompile { get; set; }
        public bool? XsltInCache { get; set; }
        public long? XsltCompileTimeMillis { get; set; }
        public long? XsltTransformTimeMillis { get; set; }
        public string ConsolidatedDomKey { get; set; }
        public long? EndToEndTimeMillis { get; set; }
    }

    public partial class League
    {
        public Standard Standard { get; set; }
    }

    public partial class Standard
    {
        public long? TeamId { get; set; }
        public Stats Stats { get; set; }
    }

    public partial class Stats
    {
        public CareerSummary Latest { get; set; }
        public CareerSummary CareerSummary { get; set; }
        public RegularSeason RegularSeason { get; set; }
    }

    public partial class CareerSummary
    {
        public string Tpp { get; set; }
        public string Ftp { get; set; }
        public string Fgp { get; set; }
        public string Ppg { get; set; }
        public string Rpg { get; set; }
        public string Apg { get; set; }
        public string Bpg { get; set; }
        public string Mpg { get; set; }
        public string Spg { get; set; }
        public long? Assists { get; set; }
        public long? Blocks { get; set; }
        public long? Steals { get; set; }
        public long? Turnovers { get; set; }
        public long? OffReb { get; set; }
        public long? DefReb { get; set; }
        public long? TotReb { get; set; }
        public long? Fgm { get; set; }
        public long? Fga { get; set; }
        public long? Tpm { get; set; }
        public long? Tpa { get; set; }
        public long? Ftm { get; set; }
        public long? Fta { get; set; }
        public long? PFouls { get; set; }
        public long? Points { get; set; }
        public long? GamesPlayed { get; set; }
        public long? GamesStarted { get; set; }
        public long? PlusMinus { get; set; }
        public long? Min { get; set; }
        public long? Dd2 { get; set; }
        public long? Td3 { get; set; }
        public long? SeasonYear { get; set; }
        public long? SeasonStageId { get; set; }
        public string Topg { get; set; }
        public long? TeamId { get; set; }
    }

    public partial class RegularSeason
    {
        public List<Season> Season { get; set; }
    }

    public partial class Season
    {
        public long? SeasonYear { get; set; }
        public List<CareerSummary> Teams { get; set; }
        public CareerSummary Total { get; set; }
    }
}
