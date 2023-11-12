using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ClassLibrary.Models
{
    public partial class Match
    {
        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("attendance")]
        public long Attendance { get; set; }

        [JsonProperty("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset Datetime { get; set; }

        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }

        [JsonProperty("home_team_events")]
        public List<TeamEvent> HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public List<TeamEvent> AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }

        public override string ToString() => $"{AwayTeamCountry}";
    }

    //Country results
    public partial class Team
    {
        [JsonProperty("goals")]
        public long Goals { get; set; }
    }

    //Ranked player list player
    public partial class TeamEvent
    {
        [JsonProperty("type_of_event")]
        public string TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        public int Goals { get; set; }

        public int YellowCards { get; set; }

        public Image RankedPicture { get; set; }
    }

    //Team
    public partial class TeamStatistics
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("starting_eleven")]
        public List<StartingEleven> StartingEleven { get; set; }

        [JsonProperty("substitutes")]
        public List<StartingEleven> Substitutes { get; set; }
    }

    //Player
    public partial class StartingEleven
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("shirt_number")]
        public long ShirtNumber { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        public bool Favourite { get; set; }
        public Image PlayerImage { get; set; }
        public string PlayerImageLoaction { get; set; }

        public override bool Equals(object obj) => obj is StartingEleven o && Name == o.Name;
        public override int GetHashCode() => HashCode.Combine(Name, ShirtNumber, Position);
        internal string FormatForFileLine() => $"{Name} {PlayerImageLoaction}";
    }

    public enum Position
    {
        Defender,
        Forward,
        Midfield,
        Goalie
    }
}
