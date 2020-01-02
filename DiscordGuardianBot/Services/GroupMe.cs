using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace DiscordGuardianBot
{
    class GroupMe
    {
        public static Tuple<string, string, string> CreateGroup(string ChannelName, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/groups?token=" + Token);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"name\":\"" + ChannelName + "\"," +
                              "\"share\": true," +
                              "\"type\":\"closed\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
                string word = data.response.share_url;
                string[] words = word.Split("/");
                string group = data.response.group_id;
                return new Tuple<string, string, string>(words[4], words[5], group);
            }

        }
        public static void SendMessage(string BotID, string ChannelName, string Author, string Text, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/bots/post?token=" + Token);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"bot_id\":\"" + BotID + "\"," +
                              "\"text\":\"" + ChannelName + ":" + Author + ":" + Text + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
            }
        }
        public static string CreateBot(string GroupID, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/bots?token=" + Token);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"bot\": {\"name\": \"D\", \"group_id\": \"" + GroupID + "\", \"callback_url\": \"http://groupme.guardianservers.net/oauth/index.php\"}}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
                string word = data.response.bot.bot_id;
                return word;
            }
        }
        public static void DeleteBot(string BotID, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/bots/destroy?token=" + Token);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"bot_id\": \"" + BotID + "\"}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
            }
        }
        public static void CloseGroup(string GroupID, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/groups/" + GroupID + "/update?token=" + Token);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"share\": false}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
            }

        }
        public static void DeleteGroup(string GroupID, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/groups/" + GroupID + "/destroy?token=" + Token);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
            }

        }
        public static int CountGroup(string GroupId, string Token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.groupme.com/v3/groups/" + GroupId + "?token=" + Token);
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JsonConvert.DeserializeObject(result);
                var response = data.response.members;
                int count = 0;
                foreach(var person in response)
                {
                    count++;
                }
                return count;
            }
        }
        
        
    }
}
