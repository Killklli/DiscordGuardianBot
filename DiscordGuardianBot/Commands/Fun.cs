using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static DiscordGuardianBot.MediaModels;
using static DiscordGuardianBot.MediaModels.GiphyModels;

namespace DiscordGuardianBot.Commands
{
    class Fun
    {
        public static async Task<bool> FunCommands(SocketMessage message, CommandContext context, DiscordSocketClient client)
        {
            if (Validation.CheckCommand(message, "wednesday"))
            {
                string source = "https://i.imgur.com/2SRddtz.jpg";
                string desc = "***It is Wednesday,***\n" + "***my dudes***\n";
                DiscordFunctions.EmbedThisImage("Wednesday", desc, source, "magenta", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "ficus"))
            {
                string source = "https://pbs.twimg.com/profile_images/884098118907699200/i8L4V-es_400x400.jpg";
                string desc = "";
                DiscordFunctions.EmbedThisImage("Praise be", desc, source, "magenta", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "8ball"))
            {
                string user = message.Author.Username;

                if (string.IsNullOrWhiteSpace(message.Content.Replace("!8ball", "")))
                {
                    throw new ArgumentException(user + " 🎱 Please enter a question to ask 8ball" + "\n **Command Usage: **.8ball <question> ");
                }

                Random rand = new Random();
                int value = rand.Next(0, 7);

                string response = "";
                switch (value)
                {
                    case 0:
                        response = "Yeah";
                        break;
                    case 1:
                        response = "100% dude";
                        break;
                    case 2:
                        response = "Probably lol";
                        break;
                    case 3:
                        response = "lmao, why??";
                        break;
                    case 4:
                        response = "Nope";
                        break;
                    case 5:
                        response = "Nah, I doubt it tbh";
                        break;
                    case 6:
                        response = "Absolutely not";
                        break;
                }

                string title = "🎱 Magic 8 Ball 🎱";
                string description = $"**Question:** {message.Content.Replace("!8ball", "")}\n**Asked by: **{user}\n**Answer:** {response}";

                DiscordFunctions.EmbedThis(title, description, "", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "flip") || Validation.CheckCommand(message, "coin"))
            {
                await context.Channel.SendMessageAsync("*Flipping a coin...* ⚖️");

                Random rand = new Random();
                int result = rand.Next(0, 2);
                string user = context.User.Mention;

                await Task.Delay(1000);
                if (result == 0)
                {
                    await context.Channel.SendMessageAsync(user + "*, it's tails!*");
                }
                else
                {
                    await context.Channel.SendMessageAsync(user + "*, it's heads!*");
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "cat"))
            {
                using (var webclient = new HttpClient())
                {
                    webclient.Timeout = TimeSpan.FromSeconds(2);
                    var s = await webclient.GetStringAsync("http://aws.random.cat/meow");
                    var json = JsonConvert.DeserializeObject<CatDog>(s);
                    await context.Channel.SendMessageAsync(json.File);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "dog"))
            {
                using (var webclient = new HttpClient())
                {
                    webclient.Timeout = TimeSpan.FromSeconds(2);
                    var dog = "http://random.dog/" + await webclient.GetStringAsync("http://random.dog/woof");
                    await context.Channel.SendMessageAsync(dog);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "info"))
            {
                var application = await context.Client.GetApplicationInfoAsync();
                EmbedBuilder eb = new EmbedBuilder();
                IGuildUser bot = await context.Guild.GetCurrentUserAsync();
                eb.Author = new EmbedAuthorBuilder().WithName(bot.Nickname ?? bot.Username).WithIconUrl(context.Client.CurrentUser.GetAvatarUrl());
                eb.ThumbnailUrl = context.Client.CurrentUser.GetAvatarUrl();
                eb.Color = Color.Green;
                eb.Description = $"{Format.Bold("Info")}\n" +
                                    $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                                    $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                                    $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                                    $"- Uptime: {(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss")}\n\n" +

                                    $"{Format.Bold("Stats")}\n" +
                                    $"- Heap Size: {Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString()} MB\n" +
                                    $"- Guilds: {(context.Client as DiscordSocketClient).Guilds.Count}\n" +
                                    $"- Channels: {(context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
                                    $"- Users: {(context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}";
                await context.Channel.SendMessageAsync("", false, eb.Build());
                return true;
            }
            else if (Validation.CheckCommand(message, "ping"))
            {
                DiscordFunctions.EmbedThis("Pong", "Status: " + client.Status + "\nResponse Time: " + client.Latency + "ms", "", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "whoami"))
            {
                await context.Channel.SendMessageAsync(context.User.Mention + $" You are: " + message.Author.Username + "#" + message.Author.Discriminator.ToString());
                return true;
            }
            else if (Validation.CheckCommand(message, "excuse"))
            {
                DiscordFunctions.EmbedThis("", Angela.RandomExcuse(), "", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "pun"))
            {
                DiscordFunctions.EmbedThis("", Angela.RandomPun(), "", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "roll"))
            {
                if (Validation.WordCountEqual(message, 2))
                {
                    var isNumeric = int.TryParse(DiscordFunctions.GetWord(message, 1), out int n);
                    if (isNumeric == true)
                    {
                        DiscordFunctions.EmbedThis("Rolled", Angela.Roll(Int32.Parse(DiscordFunctions.GetWord(message, 1))).ToString(), "", context);
                    }
                    else
                    {
                        DiscordFunctions.EmbedThis("", "That is not a number", "", context);
                    }
                }
                else
                {
                    // Warn the user it was a bad command
                    DiscordFunctions.EmbedThis("Incomplete Command", "!roll number", "", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "nuggets"))
            {
                if (Validation.WordCountEqual(message, 2))
                {
                    var isNumeric = int.TryParse(DiscordFunctions.GetWord(message, 1), out int n);
                    if (isNumeric == true)
                    {
                        DiscordFunctions.EmbedThis("Nugget Conversion", message.Author.Mention + " $" + DiscordFunctions.GetWord(message, 1) + " is equal to " + Angela.Nuggets(Int32.Parse(DiscordFunctions.GetWord(message, 1))) + " chicken nuggets", "", context);
                    }
                    else
                    {
                        DiscordFunctions.EmbedThis("", "That is not a number", "", context);

                    }
                }
                else
                {
                    // Warn the user it was a bad command
                    DiscordFunctions.EmbedThis("Incomplete Command", "!nuggets number", "", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "rate"))
            {
                int random = new Random().Next(12);
                if (random == 11)
                {
                    random = new Random().Next(12);
                }
                await context.Channel.SendMessageAsync(context.User.Mention + $" 🤔 I would rate that a " + random.ToString() + "/10");
                return true;
            }
            else if (Validation.CheckCommand(message, "gif"))
            {
                string giphyUrl = $"http://api.giphy.com/v1/gifs/";
                string curDir = Directory.GetCurrentDirectory();
                // Assuming everything exists load in the json file
                string credsfile = File.ReadAllText(curDir + "/botsettings.json");
                Config creds = JsonConvert.DeserializeObject<Config>(credsfile);
                string key = $"api_key=" + creds.Giphy;
                try
                {
                    if (Validation.WordCountGreater(message, 1))
                    {
                        Uri uri = new Uri($"{giphyUrl}search?q={message.Content.Replace(DiscordFunctions.GetWord(message, 1), "").Trim().Replace(" ", "+")}&rating=pg-13&{key}");
                        var response = await new ApiHandler<GiphySearchResult>().GetJSONAsync(uri);
                        if (response != null)
                        {
                            await context.Channel.SendMessageAsync(response.Data.Any() ? response.Data.RandomItemLowerBias().Url : "Sorry, I couldn't find a gif for that.");
                        }
                    }
                }
                catch
                {
                    Uri uri = new Uri($"{giphyUrl}random?rating=pg-13&{key}");

                    var response = await new ApiHandler<GiphySingleResult>().GetJSONAsync(uri);
                    if (response?.Data != null)
                    {
                        await context.Channel.SendMessageAsync(response.Data.Url);
                    }
                }
                return true;

            }
            else if (Validation.CheckCommand(message, "xkcd"))
            {
                string xkcdUrl = $"http://xkcd.com/";
                Random rand = new Random();
                Uri uri = new Uri($"{xkcdUrl}{rand.Next(1, 2246)}/info.0.json");
                var response = await new ApiHandler<XKCD>().GetJSONAsync(uri);
                if (response?.Url != null)
                {
                    await context.Channel.SendMessageAsync(response.Title + ": " + response.Url);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "points"))
            {
                using (var db = new LiteDatabase(@"Points.db"))
                {
                    try
                    {
                        var Points = db.GetCollection<Points>("Points");
                        if (Validation.WordCountEqual(message, 3))
                        {
                            if (Validation.IsHGorAdmin(message, client))
                            {
                                if (DiscordFunctions.GetWord(message, 1).ToLower() == "add")
                                {
                                    Points point = new Points
                                    {
                                        PointsNumber = 0,
                                        PointTitle = DiscordFunctions.GetWord(message, 2)
                                    };
                                    Points.Insert(point);
                                    DiscordFunctions.EmbedThis(point.PointTitle, "Was added to the points database", "", context);
                                }
                                else if (DiscordFunctions.GetWord(message, 1).ToLower() == "delete")
                                {
                                    var result = Points.FindOne(x => x.PointTitle.StartsWith(DiscordFunctions.GetWord(message, 2)));
                                    Points.Delete(result.Id);
                                    DiscordFunctions.EmbedThis(DiscordFunctions.GetWord(message, 2), "Was removed from the points database", "", context);
                                }
                            }
                        }
                        else if (Validation.WordCountEqual(message, 4))
                        {
                            if (DiscordFunctions.GetWord(message, 1).ToLower() == "modify")
                            {
                                if (Validation.IsHGorAdmin(message, client))
                                {
                                    var result = Points.FindOne(x => x.PointTitle.StartsWith(DiscordFunctions.GetWord(message, 2)));
                                    result.PointsNumber = result.PointsNumber + Convert.ToDouble(DiscordFunctions.GetWord(message, 3));
                                    Points.Update(result);
                                    DiscordFunctions.EmbedThis(result.PointTitle, "Current points are now: " + result.PointsNumber, "", context);
                                }
                            }
                        }
                        else
                        {
                            var results = Points.FindAll();
                            var pointlist = "";
                            foreach (Points point in results)
                            {
                                pointlist = pointlist + '\t' + '\t' + '\t' + point.PointTitle + ": " + point.PointsNumber + '\n';

                            }
                            DiscordFunctions.EmbedThis("-------------------Points-------------------", pointlist, "", context);
                        }
                    }
                    catch { }
                }
                return true;
            }
            return false;
        }
    }
}