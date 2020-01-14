using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordGuardianBot.Commands
{
    public class Admins
    {
        public static async Task<bool> AdminCommands(SocketMessage message, CommandContext context, DiscordSocketClient client)
        {

            if (Validation.CheckCommand(message, "sweep"))
            {
                if (Validation.WordCountEqual(message, 2))
                {
                    try
                    {
                        int loops = 1;
                        try
                        {
                            loops = System.Convert.ToInt32(DiscordFunctions.GetWord(message, 1));
                        }
                        catch (FormatException)
                        {
                        }
                        await DiscordFunctions.DeleteLastMessage(context, message.Channel.ToString());
                        for (int i = 0; i < loops; i++)
                        {
                            await DiscordFunctions.DeleteLastMessage(context, message.Channel.ToString());
                            await Task.Delay(1000);
                        }
                        DiscordFunctions.EmbedThisImage("Sweep Sweep", "", "https://i.imgur.com/UH1MPDz.gif", "green", context);
                    }
                    catch { }
                }
                else
                {
                    // Warn the user it was a bad command
                    DiscordFunctions.EmbedThis("Incomplete Command", "!sweep #", "red", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "impersonate"))
            {
                // Make sure we have all parts
                if (Validation.WordCountGreater(message, 3))
                {
                    foreach (IGuildChannel channel in client.GetGuild(405513567681642517).TextChannels)
                    {
                        if (channel.Name == DiscordFunctions.GetWord(message, 1).Replace("#", ""))
                        {
                            string rebuiltstring = message.Content.Replace(DiscordFunctions.GetWord(message, 0) + " ", "").Replace(DiscordFunctions.GetWord(message, 1) + " ", "");
                            await client.GetGuild(405513567681642517).GetTextChannel(channel.Id).SendMessageAsync(rebuiltstring);
                            break;
                        }
                    }
                }
                else
                {
                    // Warn the user it was a bad command
                    DiscordFunctions.EmbedThis("Incomplete Command", "!impersonate #channel message", "red", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "deleteevent"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 3))
                {
                    using (var db = new LiteDatabase(@"Events.db"))
                    {
                        try
                        {
                            var Events = db.GetCollection<Events>("Events");
                            var Event = Events.FindOne(x => x.Event.StartsWith(DiscordFunctions.GetWord(message, 1) + "-" + DiscordFunctions.GetWord(message, 2)));
                            Events.Delete(Event.Id);
                        }
                        catch { }
                    }
                    // Send a message saying we sre starting
                    DiscordFunctions.EmbedThis("Event being deleted", message.Author.Mention + " deleting event " + DiscordFunctions.GetWord(message, 1) + "-" + DiscordFunctions.GetWord(message, 2), "orange", context);
                    await DiscordFunctions.CleanupEvent(context, message);
                    DiscordFunctions.EmbedThis("Deletion Complete", "Event Deleted " + DiscordFunctions.GetWord(message, 1) + "-" + DiscordFunctions.GetWord(message, 2), "green", context);
                }
                else
                {
                    // Warn the user it was a bad command
                    DiscordFunctions.EmbedThis("Incomplete Command", "!deleteevent EVENT YEAR", "red", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "updaterules"))
            {
                foreach (IGuildChannel channel in client.GetGuild(486327167035244554).TextChannels)
                {
                    if (channel.Name == "rules")
                    {
                        bool loop = true;
                        while (loop)
                        {
                            try
                            {
                                IEnumerable<IMessage> messages = await client.GetGuild(486327167035244554).GetTextChannel(channel.Id).GetMessagesAsync().FlattenAsync();
                                foreach (var messagefound in messages)
                                {
                                    await messagefound.DeleteAsync();
                                    await Task.Delay(1000);
                                }
                                if (messages.Count() == 0)
                                {
                                    loop = false;
                                }
                            }
                            catch { loop = false; }
                        }
                        foreach (var stringmes in GoogleData.ReadRules())
                        {
                            if (stringmes == "\n")
                            {
                                // None
                            }
                            else
                            {
                                await client.GetGuild(486327167035244554).GetTextChannel(channel.Id).SendMessageAsync(stringmes);
                            }
                            await Task.Delay(500);
                        }
                        break;
                    }
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "giant"))
            {
                DiscordFunctions.EmbedThisImage("Better watch out!", "", "https://pbs.twimg.com/media/EHnQ_CcWoAAMhqG?format=png&name=360x360", "red", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "everyone"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 2))
                {
                    using (var db = new LiteDatabase(@"Config.db"))
                    {
                        var DatabaseConfig = db.GetCollection<DatabaseConfig>("DatabaseConfig");
                        var Config = DatabaseConfig.FindOne(x => x.Id.Equals(1));
                        if (DatabaseConfig.Count() == 0)
                        {
                            Config.Everyonetag = false;
                            Config.TweetChannel = "announcements";
                            DatabaseConfig.Insert(Config);
                        }
                        if (DiscordFunctions.GetWord(message, 1).ToLower() == "true")
                        {
                            Config.Everyonetag = true;
                            DatabaseConfig.Update(Config);
                            DiscordFunctions.EmbedThis("Value Updated", message.Author.Mention + " Value is now true", "green", context);
                        }
                        else if (DiscordFunctions.GetWord(message, 1).ToLower() == "false")
                        {
                            Config.Everyonetag = false;
                            DatabaseConfig.Update(Config);
                            DiscordFunctions.EmbedThis("Value Updated", message.Author.Mention + " Value is now false", "green", context);

                        }
                        else
                        {
                            DiscordFunctions.EmbedThis("Error", message.Author.Mention + " Value must be true or false", "red", context);
                        }
                    }
                }
                else
                {
                    DiscordFunctions.EmbedThis("Incomplete Command", "!everyone true", "", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "tweetchannel"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 2))
                {
                    using (var db = new LiteDatabase(@"Config.db"))
                    {
                        var DatabaseConfig = db.GetCollection<DatabaseConfig>("DatabaseConfig");
                        var Config = DatabaseConfig.FindOne(x => x.Id.Equals(1));
                        if (DatabaseConfig.Count() == 0)
                        {
                            Config.Everyonetag = false;
                            Config.TweetChannel = "announcements";
                            DatabaseConfig.Insert(Config);
                        }
                        Config.TweetChannel = DiscordFunctions.GetWord(message, 1).ToLower().Replace("#", "");
                        DatabaseConfig.Update(Config);
                        DiscordFunctions.EmbedThis("Tweet Channel Update", "New tweet channel is" + DiscordFunctions.GetWord(message, 1), "green", context);
                    }
                }
                else
                {
                    DiscordFunctions.EmbedThis("Incomplete Command", "!tweetchannel #channel", "red", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "debuglist"))
            {
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    var results = Guardians.FindAll();
                    Console.WriteLine("-----------------Guardians-------------------");
                    foreach (UserData user in results)
                    {
                        Console.WriteLine(user.Id + " " + user.DiscordUsername + " " + user.Team + " " + user.Event + " " + user.GroupMeGroup + " " + user.GroupMeTime);
                    }
                }
                using (var db = new LiteDatabase(@"Events.db"))
                {
                    var Events = db.GetCollection<Events>("Events");
                    var results = Events.FindAll();
                    Console.WriteLine("-----------------Events-------------------");
                    foreach (Events user in results)
                    {
                        Console.WriteLine(user.Id + " " + user.Event);
                    }
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "reload"))
            {
                // Load the Database from google sheets
                List<UserData> users = GoogleData.ReadDB();
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    List<string> ErrorList = new List<string>();
                    foreach (UserData user in users)
                    {
                        UserData Guardian = null;
                        foreach (var Guard in Guardians.FindAll())
                        {
                            if(Guard.DiscordUsername.ToLower().Trim().Contains(user.DiscordUsername.ToLower().Trim()))
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
                                            Authenticated = false
                                        };
                                        Guardians.Insert(Guardian);
                                    }
                                }
                                catch {
                                    ErrorList.Add(user.DiscordUsername);
                                }
                            }
                            else
                            {
                                ErrorList.Add(user.DiscordUsername);
                            }
                        }

                    }
                    if (ErrorList.Count != 0)
                    {
                        string csv = String.Join(",\n", ErrorList.Select(x => x.ToString()).ToArray());
                        System.IO.File.WriteAllText("/opt/files/ErrorUsers.csv", csv);
                        await message.Channel.SendFileAsync("/opt/files/ErrorUsers.csv", "Users with Errors");
                        System.IO.File.Delete("/opt/files/ErrorUsers.csv");
                    }
                    // Index document using a document property
                    Guardians.EnsureIndex(x => x.DiscordUsername);
                }
                // Notify the user the DB reload has completed
                DiscordFunctions.EmbedThis("The DB has been reloaded", "", "green", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "withdraw"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 2))
                {
                    if (DiscordFunctions.GetWord(message, 1).Contains("#"))
                    {
                        using (var db = new LiteDatabase(@"Guardians.db"))
                        {
                            var Guardians = db.GetCollection<UserData>("Guardians");
                            UserData Guardian = Guardians.FindOne(x => x.DiscordUsername.ToLower().StartsWith(DiscordFunctions.GetWord(message, 1).ToLower()));
                            if (Guardian != null)
                            {
                                bool found = false;
                                Console.WriteLine(Guardian.DiscordUsername);
                                foreach (var user in await context.Guild.GetUsersAsync())
                                {
                                    if (user.Username.ToLower() + "#" + user.Discriminator.ToString() == Guardian.DiscordUsername.ToLower())
                                    {
                                        if (user.RoleIds.Count == 2)
                                        {
                                            foreach (var role in user.Guild.Roles)
                                            {
                                                if (user.Guild.EveryoneRole != role && user.RoleIds.Contains(role.Id))
                                                {
                                                    await user.RemoveRoleAsync(role);
                                                }
                                            }
                                            foreach (var role in user.Guild.Roles)
                                            {
                                                if (role.Name.ToLower() == "global entry")
                                                {
                                                    await user.AddRoleAsync(role);
                                                }
                                            }

                                        }
                                        DiscordFunctions.EmbedThis("Users Removed", "Please remember to remove the user from the sheet", "orange", context);
                                        found = true;
                                        break;
                                    }
                                }
                                Guardians.Delete(Guardian.Id);
                                if (found == false)
                                {
                                    DiscordFunctions.EmbedThis("Unable to locate user in Discord", "User Removed from Database", "green", context);
                                }
                            }
                            else
                            {
                                DiscordFunctions.EmbedThis("Error", "User is not a guardian", "red", context);
                            }
                        }
                    }
                    else
                    {
                        DiscordFunctions.EmbedThis("Incomplete Command", "!withdraw user", "", context);
                    }
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "newevent"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 3))
                {
                    using (var db = new LiteDatabase(@"Events.db"))
                    {
                        var Events = db.GetCollection<Events>("Events");

                        Events Eventexists = Events.FindOne(x => x.Event.ToLower().StartsWith(DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower()));
                        if (Eventexists == null)
                        {
                            Events NewEvent = new Events
                            {
                                Event = DiscordFunctions.GetWord(message, 1) + "-" + DiscordFunctions.GetWord(message, 2)
                            };
                            Events.Insert(NewEvent);
                        }
                    }
                    // Send a message saying we sre starting
                    DiscordFunctions.EmbedThisImage("New Event Generating", "Please give it a few minutes (5+) to complete", "https://i.imgur.com/Gyn3f3T.gifv", "orange", context);

                    await NewEventBuilder.GenerateRolesAsync(context, DiscordFunctions.GetWord(message, 1), Convert.ToInt32(DiscordFunctions.GetWord(message, 2)));
                    await NewEventBuilder.GenerateCategoriesAsync(context, DiscordFunctions.GetWord(message, 1), Convert.ToInt32(DiscordFunctions.GetWord(message, 2)));
                    await NewEventBuilder.GenerateChannelsAsync(context, DiscordFunctions.GetWord(message, 1), Convert.ToInt32(DiscordFunctions.GetWord(message, 2)));
                    DiscordFunctions.EmbedThisImage("New Event Generating Complete", "", "", "green", context);
                }
                else
                {
                    // Warn the user it was a bad command
                    DiscordFunctions.EmbedThis("Incomplete Command", "!newevent EVENT YEAR", "", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "cleardb"))
            {
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    foreach (var Guardian in Guardians.FindAll())
                    {
                        if (Guardian.Authenticated == false)
                        {
                            Guardians.Delete(Guardian.Id);
                        }
                    }
                }
                DiscordFunctions.EmbedThis("Users Removed", "", "orange", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "unauthenticated"))
            {
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    List<string> UnauthenticatedList = new List<string>();
                    var Guardianlist = Guardians.FindAll();
                    foreach (var Guardian in Guardianlist)
                    {
                        if (Guardian.Authenticated == false)
                        {
                            UnauthenticatedList.Add(Guardian.DiscordUsername);
                        }
                    }
                    string csv = String.Join(",\n", UnauthenticatedList.Select(x => x.ToString()).ToArray());
                    System.IO.File.WriteAllText("/opt/files/unauth.csv", csv);
                    await message.Channel.SendFileAsync("/opt/files/unauth.csv", "Unauthenticated users");
                    System.IO.File.Delete("/opt/files/unauth.csv");
                }
                return true;
            }
            return false;
        }
    }
}
