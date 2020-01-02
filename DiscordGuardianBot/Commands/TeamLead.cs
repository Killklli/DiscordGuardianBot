using Discord.Commands;
using Discord.WebSocket;

namespace DiscordGuardianBot.Commands
{
    public class TeamLead
    {
        public static async System.Threading.Tasks.Task<bool> TeamCommand(SocketMessage message, CommandContext context)
        {

            if (Validation.CheckCommand(message, "squadlead"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 2))
                {
                    if (DiscordFunctions.GetWord(message, 2).Contains("@"))
                    {
                        foreach (SocketRole rolesfound in ((SocketGuildUser)message.Author).Roles)
                        {
                            if (rolesfound.Name.ToLower().Contains("team-lead-"))
                            {
                                await DiscordFunctions.SquadLeadTask(context, rolesfound.Name, DiscordFunctions.GetWord(message, 2));
                                DiscordFunctions.EmbedThis("User is now a squad lead", message.Author.Username, "green", context);
                                break;
                            }
                        }
                    }
                    else
                    {
                        DiscordFunctions.EmbedThis("Incomplete Command", "!squadlead @user", "red", context);
                    }
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "squad"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 2))
                {
                    if (DiscordFunctions.GetWord(message, 2).Contains("@"))
                    {
                        foreach (SocketRole rolesfound in ((SocketGuildUser)message.Author).Roles)
                        {
                            if (rolesfound.Name.ToLower().Contains("team-lead-"))
                            {
                                await DiscordFunctions.SquadTask(context, DiscordFunctions.GetWord(message, 3), DiscordFunctions.GetWord(message, 2), rolesfound.Name.Replace("team-lead-", ""));
                                DiscordFunctions.EmbedThis("User added to squad", message.Author.Username + " -> " + DiscordFunctions.GetWord(message, 3), "green", context);
                                break;
                            }
                        }
                    }
                    else
                    {
                        DiscordFunctions.EmbedThis("Incomplete Command", "!squad @user squadname", "red", context);
                    }
                }
                return true;

            }
            else if (Validation.CheckCommand(message, "addchannel"))
            {
                // Make sure we have all parts
                if (Validation.WordCountGreater(message, 2))
                {
                    foreach (SocketRole rolesfound in ((SocketGuildUser)message.Author).Roles)
                    {
                        if (rolesfound.Name.ToLower().Contains("team-lead-"))
                        {
                            var teamname = rolesfound.Name.ToLower().Trim().Replace("team-lead-", "");
                            var description = message.Content.Replace("!addchannel " + DiscordFunctions.GetWord(message, 1), "").Trim();
                            if (description.Length == 0)
                            {
                                description = " ";
                            }
                            await DiscordFunctions.CreateChannel(context, DiscordFunctions.GetWord(message, 1) + "-" + teamname, null, description, teamname, 2);
                            DiscordFunctions.EmbedThis("Channel Added", "", "green", context);

                        }
                    }
                }
                else
                {
                    DiscordFunctions.EmbedThis("Incomplete Command", "!addchannel channelname description", "red", context);
                }
                return true;

            }
            else if (Validation.CheckCommand(message, "deletechannel"))
            {
                // Make sure we have all parts
                if (Validation.WordCountEqual(message, 2))
                {
                    foreach (SocketRole rolesfound in ((SocketGuildUser)message.Author).Roles)
                    {
                        if (rolesfound.Name.ToLower().Contains("team-lead-"))
                        {
                            var teamname = rolesfound.Name.ToLower().Trim().Replace("team-lead-", "");
                            bool found = false;
                            foreach (var role in context.Guild.Roles)
                            {
                                if (role.Name.ToLower() == teamname)
                                {
                                    foreach (var channel in await context.Guild.GetChannelsAsync())
                                    {
                                        if (channel.Name == DiscordFunctions.GetWord(message, 1))
                                        {
                                            foreach (var perm in channel.PermissionOverwrites)
                                            {
                                                if (perm.TargetId == role.Id)
                                                {
                                                    await channel.DeleteAsync();
                                                    found = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (found == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (found == true)
                                {
                                    break;
                                }
                            }

                        }
                    }
                }
                else
                {
                    DiscordFunctions.EmbedThis("Incomplete Command", "!deletechannel channelname", "red", context);
                }
                return true;
            }
            return false;
        }
    }
}
