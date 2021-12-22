using NBA.Models;
using NBA.Services.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NBA.Services.Controllers
{
    [EnableCors(origins: Constants.Origins, headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class NbaController : ApiController
    {
        // BASIC API CALLS 

        [Route("Players")]
        [HttpGet]
        public Players GetPlayers(int year)
        {
            try
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest(string.Format(Constants.ApiEndpoints.Players_Base, year.ToString()));
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult = client.Execute(request);

                Players players = Players.FromJson(queryResult.Content);

                return players;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Players/Profile")]
        [HttpGet]
        public NBA.Models.Alt.Profile GetPlayersProfile(int year, long playerId)
        {
            try
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest(string.Format(Constants.ApiEndpoints.Players_Profile, year.ToString(), playerId));
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult = client.Execute(request);

                //Profile profile = Profile.FromJson(queryResult.Content);
                //Profile profile = JsonConvert.DeserializeObject<Profile>(queryResult.Content);
                NBA.Models.Alt.Profile profile = JsonConvert.DeserializeObject<NBA.Models.Alt.Profile>(queryResult.Content);

                return profile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Teams")]
        [HttpGet]
        public Teams GetTeams(int year)
        {
            try
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest(string.Format(Constants.ApiEndpoints.Teams_Base, year.ToString()));
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult = client.Execute(request);

                Teams teams = Teams.FromJson(queryResult.Content);

                return teams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Teams/Config")]
        [HttpGet]
        public TeamsConfig GetTeamsConfig(int year)
        {
            try
            {
                RestClient client = new RestClient();
                RestRequest request = new RestRequest(string.Format(Constants.ApiEndpoints.Teams_Config, year.ToString()));
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult = client.Execute(request);

                TeamsConfig teamsConfig = TeamsConfig.FromJson(queryResult.Content);

                return teamsConfig;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // INTEGRATIONS OF DATA FROM DIFFERENT APIs

        [Route("Players/Profile/All")]
        [HttpGet]
        public List<Player> GetPlayersWithProfiles(int year)
        {
            try
            {
                List<Player> playerList = new List<Player>();

                Players players = GetPlayers(year);

                foreach (NBA.Models.Africa item in players.League.Africa)
                {
                    Player player = new Player();
                    player.PersonId = item.PersonId;
                    player.TeamId = item.TeamId;
                    player.FirstName = item.FirstName;
                    player.LastName = item.LastName;
                    player.TemporaryDisplayName = item.TemporaryDisplayName;
                    player.HeightMeters = item.HeightMeters.ToString();
                    player.TeamSitesOnly = new NBA.Services.Models.TeamSitesOnly();                    
                    if (item.TeamSitesOnly != null) {player.TeamSitesOnly.PosFull = item.TeamSitesOnly.PosFull.ToString();}
                    player.Africa = new Models.Africa();
                    player.Africa.Country = item.Country;
                    playerList.Add(player);
                }

                foreach (Player item in playerList)
                {
                    NBA.Models.Alt.Profile profile = GetPlayersProfile(year, item.PersonId);

                    item.CareerSummary = new Models.CareerSummary();
                    item.CareerSummary.Fgp = profile.League.Standard.Stats.CareerSummary.Fgp;
                    item.CareerSummary.Ppg = profile.League.Standard.Stats.CareerSummary.Ppg;
                    item.CareerSummary.Rpg = profile.League.Standard.Stats.CareerSummary.Rpg;
                    item.CareerSummary.Apg = profile.League.Standard.Stats.CareerSummary.Apg;
                    item.CareerSummary.Bpg = profile.League.Standard.Stats.CareerSummary.Bpg;                 
                }

                playerList = playerList
                    .OrderByDescending(x => x.CareerSummary.Fgp)
                    .ThenByDescending(x => x.CareerSummary.Ppg)
                    .ThenByDescending(x => x.CareerSummary.Rpg)
                    .ThenByDescending(x => x.CareerSummary.Apg)
                    .ThenByDescending(x => x.CareerSummary.Bpg)
                    .ToList();

                return playerList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Teams/Config/All")]
        [HttpGet]
        public List<Models.Team> GetTeamsWithConfig(int year)
        {
            try
            {
                List<Models.Team> teamList = new List<Models.Team>();

                Teams teams = GetTeams(year);

                TeamsConfig teamsConfig = GetTeamsConfig(year);
                teamsConfig.Teams.Config = teamsConfig.Teams.Config.OrderBy(x => x.TeamId).ToList();

                foreach (NBA.Models.Africa item in teams.League.Africa)
                {
                    Models.Team team = new Models.Team();
                    team.TeamId = item.TeamId;
                    team.Config = new Models.Config();

                    // There ARE teams without an entry in the config
                    if (teamsConfig.Teams.Config.Where(w => w.TeamId == item.TeamId).Count() > 0) 
                    {
                        team.Config.PrimaryColor = teamsConfig.Teams.Config
                                                    .Where(w => w.TeamId == item.TeamId)
                                                    .Select(s => s.PrimaryColor)
                                                    .ToString();
                    }
                    else
                    {
                        team.Config.PrimaryColor = "#000000";
                    }                    

                    teamList.Add(team);
                }

                return teamList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Players/Profiles/Teams/Config/All")]
        [HttpGet]
        public List<Player> GetPlayersProfileTeamsConfig(int year)
        {
            try
            {
                List<Player> playersWithProfiles = GetPlayersWithProfiles(year);
                List<Models.Team> teamsWithConfig = GetTeamsWithConfig(year);

                foreach (Player item in playersWithProfiles)
                {
                    item.Team = new Models.Team();
                    item.Team.TeamId = item.TeamId;
                    item.Team.Config = new Models.Config();

                    string PrimaryColor = "";

                    foreach (var team in from Models.Team team in teamsWithConfig
                                         where item.TeamId == team.TeamId
                                         select team)
                    {
                        PrimaryColor = team.Config.PrimaryColor;
                    }

                    item.Team.Config.PrimaryColor = PrimaryColor;
                }

                return playersWithProfiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
