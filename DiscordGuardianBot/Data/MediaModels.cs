﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DiscordGuardianBot
{
    public class MediaModels
    {
        public class Tweet
        {
            public string text = "";
            public DateTime created_at;
            public string media = null;
            public string url = null;
            public string video = null;
        }
        public class GiphyModels
        {
            public class GiphySearchResult
            {
                [JsonProperty("data")]
                public List<GiphyData> Data { get; set; }
            }

            public class GiphySingleResult
            {
                [JsonProperty("data")]
                public GiphyData Data { get; set; }
            }

            public class GiphyData
            {
                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("id")]
                public string Id { get; set; }

                [JsonProperty("url")]
                public string Url { get; set; }

                [JsonProperty("rating")]
                public string Rating { get; set; }

                [JsonProperty("bitly_url")]
                public string BitlyUrl { get; set; }

                [JsonProperty("embed_url")]
                public string EmbedUrl { get; set; }
            }
        }

        public class XKCD
        {
            [JsonProperty("num")]
            public int Num { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("img")]
            public string Url { get; set; }
        }


    }
}
