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
    }
}
