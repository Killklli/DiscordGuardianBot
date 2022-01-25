using Discord;
using System.Collections.Generic;

namespace DiscordGuardianBot
{
    public class AudioOptions
    {
        public bool Shuffle { get; set; }
        public bool RepeatTrack { get; set; }
        public IUser Summoner { get; set; }
    }
    public class Events
    {
        /// <summary>
        /// ID used as primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Used for holding events
        /// </summary>
        public string Event { get; set; }
    }
    public class DatabaseConfig
    {
        /// <summary>
        /// ID used as primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Used for holding everyone config
        /// </summary>
        public bool Everyonetag { get; set; }
        public string TweetChannel { get; set; }
    }
    public class CatDog
    {
        public string File { get; set; }
    }
    public class CatFact
    {
        public string Fact { get; set; }
        public int Length { get; set; }
    }
    public class UserData
    {
        /// <summary>
        /// ID used as primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Used for assigning their discord username
        /// </summary>
        public string DiscordUsername { get; set; }
        /// <summary>
        /// Used for loading the users team
        /// </summary>
        public string Team { get; set; }
        public string Event { get; set; }
        public bool Authenticated { get; set; }
        public string GroupMeGroup { get; set; }
        public string GroupMeTime { get; set; }
        public string GroupMeBot { get; set; }
        public List<string> Channels { get; set; }
    }
    public class Points
    {
        public int Id { get; set; }
        public string PointTitle { get; set; }
        public double PointsNumber { get; set; }
    }
}
