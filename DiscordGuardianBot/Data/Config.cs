namespace DiscordGuardianBot
{
    /// <summary>
    ///  Used for loading in creds from the credentials file
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The spreadsheet ID (In the URL)
        /// </summary>
        public string SpreadSheetID { get; set; }

        /// <summary>
        /// ID for Doc for rules
        /// </summary>
        public string DocsID { get; set; }

        /// <summary>
        /// The spreadsheet page name (The Tab)
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// The Discord bot token you get https://discordapp.com/developers/applications/
        /// </summary>
        public string BotToken { get; set; }

        /// <summary>
        /// Stores the OauthTwitter Key
        /// </summary>
        public string TwitterOauthKey { get; set; }

        /// <summary>
        /// Stores the OauthTwitter Secret
        /// </summary>
        public string TwitterOauthSecret { get; set; }

        /// <summary>
        /// Twitter API access token
        /// </summary>
        public string TwitterAccessToken { get; set; }

        /// <summary>
        /// Twitter API access secret
        /// </summary>
        public string TwitterAccessSecret { get; set; }

        public string GroupMe { get; set; }
        public string Giphy { get; set; }

    }

}
