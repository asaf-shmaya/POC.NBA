using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBA.Services
{
    public static class Constants
    {
        public static class ApiEndpoints
        {
            public const string Players_Base = "https://data.nba.net/data/10s/prod/v1/{0}/players.json";
            public const string Players_Profile = "https://data.nba.net/data/10s/prod/v1/{0}/players/{1}_profile.json";
            public const string Teams_Base = "https://data.nba.net/data/10s/prod/v1/{0}/teams.json";
            public const string Teams_Config = "https://data.nba.net/data/1h/prod/{0}/teams_config.json";
        }

        public const string Origins = "https://localhost:44301/";
    }
        
}