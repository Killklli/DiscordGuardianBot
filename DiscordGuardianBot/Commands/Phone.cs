using Discord;
using Discord.WebSocket;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiscordGuardianBot.Commands
{
    class Phone
    {
        public static async System.Threading.Tasks.Task<bool> PhoneCommands(SocketMessage message, DiscordSocketClient client)
        {
            if (Validation.CheckCommand(message, "sms"))
            {
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    string curDir = Directory.GetCurrentDirectory();
                    // Assuming everything exists load in the json file
                    string credsfile = File.ReadAllText(curDir + "/botsettings.json");
                    Config creds = JsonConvert.DeserializeObject<Config>(credsfile);
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    UserData Guardian = Guardians.FindOne(x => x.DiscordUsername.ToLower().StartsWith(message.Author.Username.ToLower() + "#" + message.Author.DiscriminatorValue.ToString()));
                    if (Guardian != null)
                    {
                        if (Guardian.GroupMeGroup == null)
                        {
                            var result = GroupMe.CreateGroup(message.Author.Username + "#" + message.Author.DiscriminatorValue.ToString(), creds.GroupMe);
                            Guardian.GroupMeGroup = result.Item3;
                            Guardian.GroupMeTime = DateTime.Now.ToString();
                            Guardian.Channels = new List<string>() { "announcements", "announcements-" + Guardian.Event };
                            Guardian.GroupMeBot = GroupMe.CreateBot(result.Item3, creds.GroupMe);
                            Guardians.Update(Guardian);
                            await message.Author.SendMessageAsync("Enable SMS: Link will only work once \nhttps://app.groupme.com/join_group/" + result.Item1 + "/" + result.Item2 + "\nPlease remember to enable SMS notifications in GroupMe via https://web.groupme.com/settings\nYou can also reply to SMS messages using this format `channelname:message`");
                        }
                        else
                        {
                            GroupMe.DeleteGroup(Guardian.GroupMeGroup, creds.GroupMe);
                            Guardian.GroupMeGroup = null;
                            Guardian.GroupMeTime = null;
                            Guardian.Channels = null;
                            Guardian.GroupMeBot = null;
                            Guardians.Update(Guardian);
                            await message.Author.SendMessageAsync("SMS Link has been deleted");
                        }
                    }
                    else
                    {
                        await message.Channel.SendMessageAsync("User is not a Guardian, unable to set up SMS notifications");
                    }
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "notify"))
            {
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    UserData Guardian = Guardians.FindOne(x => x.DiscordUsername.ToLower().StartsWith(message.Author.Username.ToLower() + "#" + message.Author.DiscriminatorValue.ToString()));
                    if (Guardian != null)
                    {
                        if (Guardian.GroupMeGroup != null)
                        {
                            string chan = "";
                            try
                            {
                                string temp = DiscordFunctions.GetWord(message, 1).ToLower();
                                foreach (var channel in client.GetGuild(405513567681642517).Channels)
                                {
                                    bool found = false;
                                    foreach (var person in channel.Users)
                                    {
                                        if (person.Id == message.Author.Id)
                                        {
                                            found = true;
                                        }
                                    }
                                    if (temp == channel.Name.ToLower() && found == true)
                                    {
                                        chan = channel.Name.ToLower();
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                chan = message.Channel.Name.ToLower();
                            }
                            if (chan != "")
                            {
                                if (Guardian.Channels == null)
                                {
                                    Guardian.Channels = new List<string>() { chan };
                                    await message.Author.SendMessageAsync("Channel: " + chan + " was added to your notifications");
                                }
                                else
                                {
                                    if (Guardian.Channels.Contains(chan))
                                    {
                                        List<string> channels = Guardian.Channels;
                                        channels.Remove(chan);
                                        await message.Author.SendMessageAsync("Channel: " + chan + " was removed");
                                    }
                                    else
                                    {
                                        List<string> channels = Guardian.Channels;
                                        channels.Add(chan);
                                        Guardian.Channels = channels;
                                        await message.Author.SendMessageAsync("Channel: " + chan + " was added to your notifications");
                                    }
                                }
                                Guardians.Update(Guardian);

                            }
                            else
                            {
                                await message.Author.SendMessageAsync("Channel either does not exist or you do not have permissions to view it");
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
