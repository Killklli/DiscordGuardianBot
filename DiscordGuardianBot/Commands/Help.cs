using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordGuardianBot.Commands
{
    class Help
    {
        public static async System.Threading.Tasks.Task<bool> HelpCommands(SocketMessage message, CommandContext context)
        {
            var generalHelp = new EmbedBuilder();
            generalHelp.WithTitle("Angela Commands");
            generalHelp.WithDescription("Text:");
            generalHelp.AddField("Help", "Shows this command");
            generalHelp.AddField("Excuse", "Shows an excuse");
            generalHelp.AddField("Pun", "Shows a Pun");
            generalHelp.AddField("Roll <x>", "Allows you to roll a number");
            generalHelp.AddField("Flip", "Flips a coin");
            generalHelp.AddField("Wednesday", "It's Wednesday my dudes");
            generalHelp.AddField("Cat", "Shows a random picture of a cat");
            generalHelp.AddField("Dog", "Shows a random picture of a dog");
            generalHelp.AddField("Ping", "Pong");
            generalHelp.AddField("Info", "Tells you stats about the bot");
            generalHelp.AddField("Rate <info>", "Asks Angela to rate something on a scale from 1-10");
            generalHelp.AddField("Nuggets <x>", "Allows you to convert $ to chicken nuggets");
            generalHelp.AddField("Points", "Shows the current point standings");
            generalHelp.AddField("Ficus", "Praise be the ficus");
            generalHelp.AddField("Gif <string>", "Returns you a random gif from giphy");
            generalHelp.AddField("XKCD", "Returns you a random XKCD comic");
            generalHelp.AddField("SMS", "Enables or Disables SMS notifications");
            generalHelp.AddField("Notify <channel>", "Enables notifications for a specific channel (No entry will set it to the channel you are in)");
            generalHelp.WithFooter("Page 1/5");

            generalHelp.WithThumbnailUrl("https://i.imgur.com/FmAdIKq.png");
            generalHelp.WithColor(Color.Blue);

            var musicHelp = new EmbedBuilder();
            musicHelp.WithTitle("Angela Commands");
            musicHelp.WithDescription("Music:");
            musicHelp.AddField("Play <song>", "Plays a song from youtube in voice chat");
            musicHelp.AddField("Skip", "Skips the current playing song");
            musicHelp.AddField("Stop", "Stops playing music");
            musicHelp.AddField("Volume <2-150>", "Sets the music volume");
            musicHelp.AddField("Pause", "Pauses the current song");
            musicHelp.AddField("Resume", "Resumes the current song");
            musicHelp.AddField("Join", "Joins Angela to your voice channel");
            musicHelp.AddField("Leave", "Leaves the current voice channel");
            musicHelp.WithFooter("Page 2/5");

            musicHelp.WithThumbnailUrl("https://i.imgur.com/FmAdIKq.png");
            musicHelp.WithColor(Color.Blue);


            var landingHelp = new EmbedBuilder();
            landingHelp.WithTitle("Angela Commands");
            landingHelp.WithDescription("Landing:");
            landingHelp.AddField("Guardian", "Attempts to authenticate you as a Guardian");
            landingHelp.AddField("RtxLondon", "Attempts to authenticate you as an RTX London Guardian");
            landingHelp.WithFooter("Page 3/5");

            landingHelp.WithThumbnailUrl("https://i.imgur.com/FmAdIKq.png");
            landingHelp.WithColor(Color.Teal);

            var teamleadHelp = new EmbedBuilder();
            teamleadHelp.WithTitle("Angela Commands");
            teamleadHelp.WithDescription("Team Lead:");
            teamleadHelp.AddField("Squadlead <@user>", "Makes a user a squad lead (Must be run in a channel you can @ them)");
            teamleadHelp.AddField("Squad <@user> <squad>", "Adds a user to a squad (Must be run in a channel you can @ them)");
            teamleadHelp.AddField("Addchannel <channelname> <description>", "Creates a channel with a description under your team");
            teamleadHelp.AddField("Deletechannel <channelname>", "Deletes a channel under your team");
            teamleadHelp.WithFooter("Page 4/5");

            teamleadHelp.WithThumbnailUrl("https://i.imgur.com/FmAdIKq.png");
            teamleadHelp.WithColor(Color.Purple);


            var adminHelp = new EmbedBuilder();
            adminHelp.WithTitle("Angela Commands");
            adminHelp.WithDescription("Admin:");
            adminHelp.AddField("Sweep <x>", "Clears x messages");
            adminHelp.AddField("Impersonate <message>", "Allows you to impersonate Angela");
            adminHelp.AddField("Updaterules", "Forces the rules page to update");
            adminHelp.AddField("Giant", "Fee-Fi-Fo");
            adminHelp.AddField("Everyone <true/false>", "Updates if the tweet bot has the everyone tag");
            adminHelp.AddField("Tweetchannel <#channel>", "Specifies the channel to send tweets to");
            adminHelp.AddField("Debuglist", "Prints information about Events/Broken info to Console");
            adminHelp.AddField("Reload", "Reloads user database information");
            adminHelp.AddField("Withdraw <username#discriminator>", "Withdraws a user as a guardian from that event");
            adminHelp.AddField("Newevent <event> <year>", "Creates a new event with the year and info");
            adminHelp.AddField("Deleteevent <event> <year>", "Deletes a previous years event specific info and saves the bar");
            adminHelp.AddField("Points <add/delete/modify> <pointset> <number>", "Allows you to manage points (Add creates a new set, Delete removes a set, modify changes points), pointset and number are only used for modify");
            adminHelp.AddField("Unauthenticated", "Prints out the list of users that have not authenticated for the event");

            adminHelp.WithFooter("Page 5/5");

            adminHelp.WithThumbnailUrl("https://i.imgur.com/FmAdIKq.png");
            adminHelp.WithColor(Color.Red);

            var casevar = "";
            try
            {
                casevar = DiscordFunctions.GetWord(message, 1);
            }
            catch { }
            switch (casevar)
            {
                case "1":
                    await context.Channel.SendMessageAsync("", false, generalHelp.Build());
                    break;
                case "2":
                    await context.Channel.SendMessageAsync("", false, musicHelp.Build());
                    break;
                case "3":
                    await context.Channel.SendMessageAsync("", false, landingHelp.Build());
                    break;
                case "4":
                    await context.Channel.SendMessageAsync("", false, teamleadHelp.Build());
                    break;
                case "5":
                    await context.Channel.SendMessageAsync("", false, adminHelp.Build());
                    break;
                case "all":
                    await context.Channel.SendMessageAsync("", false, generalHelp.Build());
                    await context.Channel.SendMessageAsync("", false, musicHelp.Build());
                    await context.Channel.SendMessageAsync("", false, landingHelp.Build());
                    await context.Channel.SendMessageAsync("", false, teamleadHelp.Build());
                    await context.Channel.SendMessageAsync("", false, adminHelp.Build());
                    break;
                default:
                    if (message.Channel.Name.ToLower() == "landing")
                    {
                        await context.Channel.SendMessageAsync("", false, landingHelp.Build());
                    }
                    else
                    {
                        await context.Channel.SendMessageAsync("", false, generalHelp.Build());

                    }
                    break;

            }
            return true;
        }
    }
}
