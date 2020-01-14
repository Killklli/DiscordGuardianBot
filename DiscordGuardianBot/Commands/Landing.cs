using Discord.Commands;
using Discord.WebSocket;
using LiteDB;
using System;

namespace DiscordGuardianBot.Commands
{
    class Landing
    {
        public static async System.Threading.Tasks.Task<bool> LandingCommands(SocketMessage message, CommandContext context, DiscordSocketClient client)
        {
            if (Validation.CheckCommand(message, "rtxlondon"))
            {
                Authentication.RtxLondonAsync(message, context, client);
                return true;
            }
            else if (Validation.CheckCommand(message, "guardian"))
            {
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(@"Events.db"))
                {
                    var Events = db.GetCollection<Events>("Events");
                    using (var guardiandb = new LiteDatabase(@"Guardians.db"))
                    {
                        var Guardians = guardiandb.GetCollection<UserData>("Guardians");
                        UserData Guardian = null;
                        foreach (var Guard in Guardians.FindAll())
                        {
                            if (Guard.DiscordUsername.ToLower().Trim().Contains(message.Author.Username.ToLower() + "#" + message.Author.Discriminator))
                            {
                                Guardian = Guard;
                                break;
                            }
                        }
                        if (Guardian != null && Events != null)
                        {
                            if (Events.Count() != 0)
                            {
                                var Event = Events.FindOne(x => x.Event.StartsWith(Guardian.Event));
                                if (Event != null)
                                {
                                    Authentication.GuardianAuth(message, context, Event);
                                    Guardian.Authenticated = true;
                                    Guardians.Update(Guardian);
                                }
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
