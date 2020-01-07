using Discord.WebSocket;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using TweetSharp;
using static DiscordGuardianBot.MediaModels;

namespace DiscordGuardianBot
{

    public class Twitter
    {
        // Get Auth creds 
        public string OAuthConsumerSecret { get; set; }
        public string OAuthConsumerKey { get; set; }
        public string AccessToken { get; set; }
        public string AccessKey { get; set; }
        public Config Creds { get; set; }
        private static Config staticcreds = new Config();
        public DiscordSocketClient Client { get; set; }
        public DateTime Lastrun { get; set; }

        // Discord to DM
        public static async void SendDMAsync(long user, string message)
        {
            string curDir = Directory.GetCurrentDirectory();
            string credsfile = File.ReadAllText(curDir + "/botsettings.json");
            staticcreds = JsonConvert.DeserializeObject<Config>(credsfile);

            // Pass your credentials to the service
            TwitterService service = new TwitterService(staticcreds.TwitterOauthKey, staticcreds.TwitterOauthSecret);

            // Step 1 - Retrieve an OAuth Request Token
            OAuthRequestToken requestToken = service.GetRequestToken();

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(staticcreds.TwitterAccessToken, staticcreds.TwitterAccessSecret);
            SendDirectMessageOptions dm;
            dm = new SendDirectMessageOptions() { Recipientid = user, Text = message };
            
            await service.SendDirectMessageAsync(dm);
        }



        // Tweets for announcments to discord
        public async void GetTweetAsync(object source, ElapsedEventArgs e)
        {
            try
            {
                var twitter = new Twitter
                {
                    OAuthConsumerKey = Creds.TwitterOauthKey,
                    OAuthConsumerSecret = Creds.TwitterOauthSecret,
                    AccessToken = Creds.TwitterAccessToken,
                    AccessKey = Creds.TwitterAccessSecret
                };
                ulong channelid = new ulong();
                string tweetchannel = "";
                bool everyonetag = false;
                using (var db = new LiteDatabase(@"Config.db"))
                {
                    var DatabaseConfig = db.GetCollection<DatabaseConfig>("DatabaseConfig");
                    var config = DatabaseConfig.FindOne(x => x.Id.Equals(1));
                    if (DatabaseConfig.Count() == 0)
                    {
                        config = new DatabaseConfig
                        {
                            Everyonetag = false,
                            TweetChannel = "announcements"
                        };
                        DatabaseConfig.Insert(config);
                    }
                    tweetchannel = config.TweetChannel;
                    everyonetag = config.Everyonetag;
                }
                // Guardian ID 405513567681642517
                // Bot Test 486327167035244554
                foreach (var channelparser in Client.GetGuild(405513567681642517).TextChannels)
                {
                    if (channelparser.Name.Trim().ToLower() == tweetchannel.ToLower().Trim())
                    {
                        channelid = channelparser.Id;
                        break;
                    }
                }
                List<Tweet> twitts = twitter.GetTwitts("GuardianWire", 15);

                bool tweeted = false;
                foreach (var t in twitts)
                {
                    if (Lastrun <= t.created_at)
                    {
                        if (t.text != String.Empty)
                        {
                            await Client.GetGuild(405513567681642517).GetTextChannel(channelid).SendMessageAsync(t.text);
                            tweeted = true;
                        }
                        if (t.media != null && t.video == null)
                        {
                            await Client.GetGuild(405513567681642517).GetTextChannel(channelid).SendMessageAsync(t.media);
                            tweeted = true;
                        }
                        if (t.url != null)
                        {
                            await Client.GetGuild(405513567681642517).GetTextChannel(channelid).SendMessageAsync(t.url);
                            tweeted = true;
                        }
                        if (t.video != null)
                        {
                            await Client.GetGuild(405513567681642517).GetTextChannel(channelid).SendMessageAsync(t.video);
                            tweeted = true;
                        }
                    }
                }
                if (tweeted == true)
                {
                    if (everyonetag == true)
                    {
                        await Client.GetGuild(405513567681642517).GetTextChannel(channelid).SendMessageAsync("@everyone");
                    }
                }
                Lastrun = DateTime.UtcNow;
            }
            catch { }
        }

        public List<Tweet> GetTwitts(string userName, int count, string accessToken = null)
        {
            // Pass your credentials to the service
            TwitterService service = new TwitterService(OAuthConsumerKey, OAuthConsumerSecret);

            // Step 1 - Retrieve an OAuth Request Token
            OAuthRequestToken requestToken = service.GetRequestToken();

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(AccessToken, AccessKey);
            ListTweetsOnUserTimelineOptions tweetOptions = new ListTweetsOnUserTimelineOptions
            {
                ScreenName = userName,
                Count = count,
                TweetMode = TweetMode.Extended
            };
            var tweet = service.ListTweetsOnUserTimeline(tweetOptions);

            // created_at, text, truncated
            List<Tweet> list = new List<Tweet>();

            foreach (var t in tweet)
            {
                Tweet combine = new Tweet
                {
                    created_at = new DateTime(long.Parse(t.CreatedDate.Ticks.ToString()))
                };
                if (t.RetweetedStatus == null)
                {
                    if (t.FullText.ToString().Contains("https://t.co/"))
                    {
                        combine.text = t.FullText.ToString().Remove(t.FullText.ToString().IndexOf("https://t.co/"));
                    }
                    else
                    {
                        combine.text = t.FullText.ToString();
                    }
                    try
                    {
                        foreach (var entity in t.Entities.Media)
                        {
                            combine.media = entity.MediaUrlHttps;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        foreach (var entity in t.Entities.Urls)
                        {
                            combine.url = entity.ExpandedValue;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        foreach (var entity in t.ExtendedEntities.Media)
                        {
                            try
                            {
                                foreach (var video in entity.VideoInfo.Variants)
                                {
                                    combine.video = video.Url.ToString();
                                }
                            }
                            catch { }
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    if (t.RetweetedStatus.FullText.ToString().Contains("https://t.co/"))
                    {
                        combine.text = t.RetweetedStatus.FullText.ToString().Remove(t.RetweetedStatus.FullText.ToString().IndexOf("https://t.co/"));
                    }
                    else
                    {
                        combine.text = t.RetweetedStatus.FullText.ToString();
                    }
                    try
                    {
                        foreach (var entity in t.RetweetedStatus.Entities.Media)
                        {
                            combine.media = entity.MediaUrlHttps;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        foreach (var entity in t.RetweetedStatus.Entities.Urls)
                        {
                            combine.url = entity.ExpandedValue;
                        }
                    }
                    catch
                    {
                    }
                }
                list.Add(combine);
            }
            list.Reverse();
            return list;
        }

    }
}