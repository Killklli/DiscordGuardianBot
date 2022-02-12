using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscordGuardianBot
{
    /// <summary>
    /// Methods commonly used for validation of data
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Validate the config files
        /// </summary>
        public static void ValidateConfigFiles(string curDir)
        {
            // Check if the settings file exists
            if (File.Exists(curDir + "/botsettings.json") == false)
            {
                // If it dosen't generate a temporary file
                Config tempcreds = new Config
                {
                    SheetName = "PageNameOnDoc",
                    SpreadSheetID = "SPREADSHEET-ID-URL",
                    BotToken = "DiscordBotToken",
                    TwitterOauthKey = "0000000000",
                    TwitterOauthSecret = "0000000",
                    TwitterAccessSecret = "000000000",
                    TwitterAccessToken = "00000000",
                    GroupMe = "00000000",
                    DocsID = "0000000000"
                };
                File.WriteAllText(curDir + "/botsettings.json", JsonConvert.SerializeObject(tempcreds));
                // Kick back an exit warning to update creds
                Console.WriteLine("No BotSettings detected. One has been generated. Press any button to quit");
                Console.ReadKey();
                Environment.Exit(1);
            }
            // Check if google sheets creds file exists
            if (File.Exists(curDir + "/credentials.json") == false)
            {
                Console.WriteLine("No Google Sheets Config file detected (Go download your creds from Google). Press any button to quit");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
        /// <summary>
        /// Check if message is a command
        /// </summary>
        public static bool Iscommand(SocketMessage message)
        {
            if (Istext(message))
            {
                if (message.Content[0].ToString() == "!")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool CheckCommand(SocketMessage message, string command)
        {
            // Split the message so we can parse it
            List<string> splitmessage = message.Content.Split().ToList();
            // Compare
            if (splitmessage[0].ToLower() == "!" + command.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool WordCountEqual(SocketMessage message, int number)
        {
            // Split the message so we can parse it
            List<string> splitmessage = message.Content.Split().ToList();
            if (splitmessage.Count == number)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool WordCountGreater(SocketMessage message, int number)
        {
            // Split the message so we can parse it
            List<string> splitmessage = message.Content.Split().ToList();
            if (splitmessage.Count >= number)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check if the message sent is text or an image
        /// </summary>
        public static bool Istext(SocketMessage message)
        {
            if (message.Content.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check if user has been authenticated yet
        /// </summary>
        public static bool IsUserAuthenticated(SocketMessage message, string role)
        {
            foreach (SocketRole rolesfound in ((SocketGuildUser)message.Author).Roles)
            {
                if(rolesfound.Name.ToLower() == role.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        public static bool BotMentioned(SocketMessage message, DiscordSocketClient discordclient)
        {
            // Check if there were any mentions
            if (message.MentionedUsers.Count > 0)
            {
                // Check if the user mentioned the bot
                foreach (var mention in message.MentionedUsers)
                {
                    if (mention.Id == discordclient.CurrentUser.Id)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check against the list of roles in the Server if the user is a HG or Admin
        /// </summary>
        public static bool IsHGorAdmin(SocketMessage User, DiscordSocketClient client)
        {
            try
            {
                // Get the users info
                var userinfo = client.GetGuild(405513567681642517).GetUser(User.Author.Id) as IGuildUser;
                // Check all the roles in the discord
                foreach (var role in client.GetGuild(405513567681642517).Roles)
                {
                    // If the role matches either option
                    if (role.Name.ToLower().Trim() == "head-guardian" || role.Name.ToLower().Trim() == "admin")
                    {
                        // Check the role against all of the ID's the user has
                        foreach (var userrole in userinfo.RoleIds)
                        {
                            // If it matches return true
                            if (role.Id == userrole)
                            {
                                return true;
                            }
                        }
                    }
                }
                // Else just return false
                return false;
            }
            catch { return false; }
        }
        /// <summary>
        /// Check against the list of roles in the Server if the user is a TL
        /// </summary>
        public static bool IsTL(SocketMessage message)
        {
            try
            {
                foreach (SocketRole rolesfound in ((SocketGuildUser)message.Author).Roles)
                {
                    if (rolesfound.Name.ToLower().Contains("team-lead-"))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }
    }
}
