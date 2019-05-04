using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordBotGuardian
{
    public class Twitter
    {
        public string OAuthConsumerSecret { get; set; }
        public string OAuthConsumerKey { get; set; }

        public async Task<List<Tweet>> GetTwitts(string userName, int count, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=0&tweet_mode=extended", count, userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            dynamic json = JsonConvert.DeserializeObject(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerableTwitts = (json as IEnumerable<dynamic>);

            if (enumerableTwitts == null)
            {
                return null;
            }
            // created_at, text, truncated
            List<Tweet> list = new List<Tweet>();

            foreach (var t in enumerableTwitts)
            {
                Tweet combine = new Tweet
                {
                    created_at = DateTime.ParseExact(t["created_at"].ToString(), "ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)
                };
                if (t["full_text"].ToString().Contains("https://t.co/"))
                {
                    combine.text = t["full_text"].ToString().Remove(t["full_text"].ToString().IndexOf("https://t.co/"));
                }
                else
                {
                    combine.text = t["full_text"].ToString();
                }
                try
                {
                    foreach (var entity in t["entities"])
                    {
                        if (entity.Name == "media")
                        {
                            foreach (var mediatype in entity.Value)
                            {
                                foreach (var entityvalue in mediatype)
                                {
                                    if (entityvalue.Name == "media_url_https")
                                    {
                                        combine.media = entityvalue.Value;
                                    }
                                }
                            }
                        }
                    }
                }
                catch { combine.media = null; }
                list.Add(combine);
            }
            list.Reverse();
            return list;
        }

        public async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            dynamic item = JsonConvert.DeserializeObject(json);
            return item["access_token"];
        }
    }
}