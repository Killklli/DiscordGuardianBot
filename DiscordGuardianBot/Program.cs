using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using Newtonsoft.Json;
using AIMLbot;
using Victoria;
using Microsoft.Extensions.DependencyInjection;
using Victoria.EventArgs;
using DiscordGuardianBot.Commands;

namespace DiscordGuardianBot
{
    class Program
    {
        // Discord Client for actual connection
        private DiscordSocketClient discordclient;
        // Set the Datetime for tweets
        public static DateTime lastrun = DateTime.UtcNow;
        // Load Creds
        private static Config creds = new Config();
        // Set Global regex for emojis
        private static readonly Regex reg = new Regex(@"<:[^:\s]*(?:::[^:\s]*)*:[^:\s]*(?:[^:\s]*)>");

        private static readonly Bot AI = new Bot();
        private static readonly AIMLbot.User myUser = new AIMLbot.User("AngelaLansbury", AI);

        private LavaNode _lavaNode;
        private ServiceProvider _services;

        /// <summary>
        /// Entry way to start the net core application
        /// </summary>
        static void Main()
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }


        /// <summary>
        /// Used for starting the connection to discord and loading settings
        /// </summary>
        public async Task MainAsync()
        {
            // Get the current directory
            string curDir = Directory.GetCurrentDirectory();
            Validation.ValidateConfigFiles(curDir);
            // Assuming everything exists load in the json file
            string credsfile = File.ReadAllText(curDir + "/botsettings.json");
            creds = JsonConvert.DeserializeObject<Config>(credsfile);

            // Load the Database from google sheets
            List<UserData> users = GoogleData.ReadDB();
            using (var db = new LiteDatabase(@"Guardians.db"))
            {
                var Guardians = db.GetCollection<UserData>("Guardians");
                foreach (UserData user in users)
                {
                    UserData Guardian = null;
                    foreach (var Guard in Guardians.FindAll())
                    {
                        if (Guard.DiscordUsername.ToLower().Trim().Contains(user.DiscordUsername.ToLower().Trim()))
                        {
                            Guardian = Guard;
                            break;
                        }
                    }
                    if (Guardian != null)
                    {
                        Guardian.Team = user.Team;
                        Guardians.Update(Guardian);
                    }
                    else
                    {
                        if (user.DiscordUsername.Contains("#"))
                        {
                            try
                            {
                                using (var eventdb = new LiteDatabase(@"Events.db"))
                                {
                                    var Events = eventdb.GetCollection<Events>("Events");
                                    var Event = Events.FindAll();

                                    Guardian = new UserData
                                    {
                                        Event = Event.First().Event,
                                        DiscordUsername = user.DiscordUsername,
                                        Team = user.Team,
                                        Authenticated = false,
                                        Channels = null,
                                        GroupMeBot = null,
                                        GroupMeGroup = null,
                                        GroupMeTime = null
                                    };
                                    Guardians.Insert(Guardian);
                                }
                            }
                            catch { }
                        }
                    }

                }
                // Index document using a document property
                Guardians.EnsureIndex(x => x.DiscordUsername);
            }
            AI.UpdatedConfigDirectory = "./config";
            AI.UpdatedAimlDirectory = "./aiml";
            AI.loadSettings();
            AI.loadAIMLFromFiles();
            AI.isAcceptingUserInput = true;

            // Initalize the client
            _services = ConfigureServices();
            discordclient = _services.GetRequiredService<DiscordSocketClient>();
            discordclient.Log += LogAsync;
            discordclient.Ready += Ready;
            discordclient.MessageReceived += MessageReceivedAsync;

            // Tokens should be considered secret data, and never hard-coded.
            await discordclient.LoginAsync(TokenType.Bot, creds.BotToken);
            await discordclient.StartAsync();
            Twitter TweetGrabber = new Twitter
            {
                Creds = creds,
                Client = discordclient,
                Lastrun = lastrun
            };
            System.Timers.Timer TwitterTimer = new System.Timers.Timer();
            TwitterTimer.Elapsed += new ElapsedEventHandler(TweetGrabber.GetTweetAsync);
            TwitterTimer.Interval = 60000;
            TwitterTimer.Enabled = true;

            System.Timers.Timer SMSGroup = new System.Timers.Timer();
            SMSGroup.Elapsed += new ElapsedEventHandler(Timer);
            SMSGroup.Interval = 60000;
            SMSGroup.Enabled = true;

            System.Timers.Timer FileWatcher = new System.Timers.Timer();
            FileWatcher.Elapsed += new ElapsedEventHandler(Filewatcher);
            FileWatcher.Interval = 5000;
            FileWatcher.Enabled = true;

            System.Timers.Timer Activity = new System.Timers.Timer();
            Activity.Elapsed += new ElapsedEventHandler(ActivityEventAsync);
            Activity.Interval = TimeSpan.FromMinutes(48).TotalMilliseconds;
            Activity.Enabled = true;

            await discordclient.SetGameAsync("Murder She Wrote", null, ActivityType.Watching);
            activity++;

            // Block the program until it is closed.
            await Task.Delay(-1);
        }
        public int activity = 0;
        private async void ActivityEventAsync(object sender, ElapsedEventArgs e)
        {
            if (activity == 0)
            {
                await discordclient.SetGameAsync("Murder She Wrote", null, ActivityType.Watching);
                activity++;
            }
            else if (activity == 1)
            {
                await discordclient.SetGameAsync("Murder She Wrote", null, ActivityType.Playing);
                activity++;
            }
            else if (activity == 2)
            {
                await discordclient.SetGameAsync("Murder She Wrote", null, ActivityType.Streaming);
                activity++;
            }
            else
            {
                await discordclient.SetGameAsync("Murder She Wrote", null, ActivityType.Listening);
                activity = 0;
            }
        }

        private async Task OnTrackEndedAsync(TrackEndedEventArgs args)
        {
            var toEmebed = new EmbedBuilder();

            if (!args.Reason.ShouldPlayNext())
                return;

            if (!args.Player.Queue.TryDequeue(out var track))
            {
                await _lavaNode.LeaveAsync(args.Player.VoiceChannel);
                return;
            }
            var nexttrack = (LavaTrack)track;
            await args.Player.PlayAsync(nexttrack);
            toEmebed.WithTitle("Now playing");
            toEmebed.WithDescription(nexttrack.Title);
            toEmebed.WithColor(Color.Green);
            await args.Player.TextChannel.SendMessageAsync("", false, toEmebed.Build());

        }


        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<LavaConfig>()
                .AddSingleton<LavaNode>()
                .BuildServiceProvider();
        }
        public static void Timer(object source, ElapsedEventArgs e)
        {
            using (var db = new LiteDatabase(@"Guardians.db"))
            {
                var Guardians = db.GetCollection<UserData>("Guardians");
                var Guardian = Guardians.Find(x => x.GroupMeTime != null);
                foreach (var time in Guardian)
                {
                    if (time.GroupMeTime != null)
                    {
                        int count = GroupMe.CountGroup(time.GroupMeGroup, creds.GroupMe);
                        if (Convert.ToDateTime(time.GroupMeTime).AddMinutes(15) <= DateTime.Now || count > 2)
                        {
                            GroupMe.DeleteGroup(time.GroupMeGroup, creds.GroupMe);
                            time.GroupMeGroup = null;
                            time.GroupMeTime = null;
                            time.Channels = null;
                            time.GroupMeBot = null;
                            Guardians.Update(time);
                        }
                        else if (count == 2)
                        {
                            GroupMe.CloseGroup(time.GroupMeGroup, creds.GroupMe);
                            time.GroupMeTime = null;
                            Guardians.Update(time);
                        }
                    }
                }
            }
        }
        public void Filewatcher(object source, ElapsedEventArgs e)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(@"/var/www/output");
                //DirectoryInfo d = new DirectoryInfo(@"./output");
                FileInfo[] Files = d.GetFiles("*");
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");

                    foreach (FileInfo file in Files)
                    {
                        try
                        {
                            string contents = File.ReadAllText(file.FullName);
                            dynamic data = JsonConvert.DeserializeObject(contents);
                            string user = data.sender_type;
                            if (user != "bot" && user != "system")
                            {
                                string message = data.text;
                                var chan = message.Substring(0, message.IndexOf(':'));
                                var text = message.Substring(message.IndexOf(':') + 1);
                                if (text != null && chan != null)
                                {
                                    foreach (var channel in discordclient.GetGuild(405513567681642517).Channels)
                                    {
                                        if (channel.Name.ToLower() == chan.ToLower())
                                        {
                                            string groupid = data.group_id;
                                            var Guardian = Guardians.FindOne(x => x.GroupMeGroup == groupid);
                                            string username = Guardian.DiscordUsername;
                                            bool found = false;
                                            foreach (var person in discordclient.GetGuild(405513567681642517).Users)
                                            {
                                                if (person.Username.ToLower() + "#" + person.Discriminator.ToString() == Guardian.DiscordUsername.ToLower())
                                                {
                                                    foreach(var per in channel.Users)
                                                    {
                                                        if(per.Id == person.Id)
                                                        {
                                                            if(per.GetPermissions(channel).SendMessages == true)
                                                            {
                                                                found = true;
                                                            }
                                                        }
                                                    }
                                                    username = person.Nickname;
                                                    break;
                                                }
                                            }
                                            if (found == true)
                                            {
                                                discordclient.GetGuild(405513567681642517).GetTextChannel(channel.Id).SendMessageAsync(Guardian.DiscordUsername + ": " + text);
                                            }
                                            file.Delete();
                                            break;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                file.Delete();
                            }
                        }
                        catch
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Read every message that gets sent
        /// </summary>
        public async void ReadmessagesAsync(SocketMessage message)
        {
            // Set up the Socket User Message so we can use it for context for later
            var usermessage = message as SocketUserMessage;
            var chnl = message.Channel as SocketGuildChannel;
            bool GuildCheck = true;
            // Make sure we don't do anything in RTX-London
            try
            {
                if (chnl.Guild.Id == 358283239053590528)
                {
                    GuildCheck = false;
                }
            }
            catch { }
            if (GuildCheck == true)
            {
                try
                {
                    if (Validation.Istext(message))
                    {
                        // Check if the message is a command based off if a bang is the first letter
                        if (Validation.Iscommand(message))
                        {
                            // Set up Command Context
                            var context = new CommandContext(discordclient, usermessage);
                            // Check if the message sent matches an actual command in the approved or public commands 
                            bool found = false;
                            if (Validation.CheckCommand(message, "help") && found == false)
                            {
                                found = await Help.HelpCommands(message, context);
                            }
                            if (message.Channel.Name.ToLower() == "landing" && found == false)
                            {
                                found = await Landing.LandingCommands(message, context, discordclient);
                            }
                            else
                            {   
                                // Head Guardian/Admin Commands
                                if (Validation.IsHGorAdmin(message, discordclient) && found == false)
                                {
                                    found = await Admins.AdminCommands(message, context, discordclient);
                                }
                                // Team Lead Commands
                                if (Validation.IsTL(message) && found == false)
                                {
                                    found = await TeamLead.TeamCommand(message, context);
                                }
                                if (found == false)
                                {
                                    found = await Fun.FunCommands(message, context, discordclient);
                                }
                                if (found == false)
                                {
                                    found = await Music.MusicCommands(message, context, discordclient, _lavaNode);
                                }
                                if (found == false)
                                {
                                    found = await Phone.PhoneCommands(message, discordclient);
                                }
                            }
                        }
                        // Check if there were any mentions
                        else if (Validation.BotMentioned(message, discordclient))
                        {
                            // Set up Command Context
                            var context = new CommandContext(discordclient, usermessage);
                            if (message.Content.Replace("<@!503445217530085396>", "").Trim() == "")
                            {
                                // If so send a gif
                                await context.Channel.SendMessageAsync(Angela.RandomAngela());
                            }
                            else
                            {
                                try
                                {
                                    // Send request
                                    Request r = new Request(message.Content.Replace("<@!503445217530085396>", "").Trim(), myUser, AI);

                                    // Save answer
                                    Result res = AI.Chat(r);
                                    // Output answer
                                    await message.Channel.SendMessageAsync(res.Output);
                                }
                                catch { }

                            }
                        }
                        // Log all text
                        WriteLog(message.Channel + ": " + message.Author + " - " + message.Content);
                        if (!reg.IsMatch(message.Content) && message.Author.IsBot == false)
                        {
                            try
                            {
                                using (var db = new LiteDatabase(@"Guardians.db"))
                                {
                                    var Guardians = db.GetCollection<UserData>("Guardians");
                                    var Guardian = Guardians.Find(x => x.GroupMeGroup != null && x.Channels.Contains(message.Channel.Name.ToLower()) && x.GroupMeTime == null);
                                    foreach (var notify in Guardian)
                                    {
                                        if (notify.DiscordUsername.ToLower() != message.Author.Username.ToLower() + "#" + message.Author.Discriminator.ToString())
                                        {
                                            GroupMe.SendMessage(notify.GroupMeBot, message.Channel.Name, message.Author.Username, message.Content, creds.GroupMe);
                                        }
                                    }
                                }
                            }
                            catch { }
                        }

                    }

                }
                catch { Console.WriteLine("Parsing Commands timed out: " + message.Channel.Name + " " + message.Content); }
            }
        }
        /// <summary>
        /// Write log messages to the console
        /// </summary>
        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
        /// <summary>
        /// The Ready event indicates that the client has opened a connection and it is now safe to access the cache.
        /// </summary>
        private async Task<Task> Ready()
        {
            LavaConfig conf = new LavaConfig();
            _lavaNode = new LavaNode(discordclient, conf);
            _lavaNode.OnTrackEnded += OnTrackEndedAsync;
            await _lavaNode.ConnectAsync();

            Console.WriteLine(discordclient.CurrentUser + " is connected!");
            return Task.CompletedTask;
        }
        /// <summary>
        /// Thread entry where the messages from Discord are passed under
        /// </summary>
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            // The bot should never respond to itself.
            if (message.Author.Id == discordclient.CurrentUser.Id)
                return;
            await Task.Run(() => ReadmessagesAsync(message));

        }
        /// <summary>
        /// Write a string to the log file
        /// </summary>
        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            // Get the current directory
            string curDir = Directory.GetCurrentDirectory();
            string logFilePath = curDir + "/";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, System.IO.FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }
    }

}
