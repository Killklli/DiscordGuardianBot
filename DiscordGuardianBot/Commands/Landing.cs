using Discord.Commands;
using Discord.WebSocket;
using LiteDB;

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
                bool throwerror = false;
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(@"Events.db"))
                {
                    var Events = db.GetCollection<Events>("Events");
                    using (var guardiandb = new LiteDatabase(@"Guardians.db"))
                    {
                        var Guardians = guardiandb.GetCollection<UserData>("Guardians");
                        var Guardian = Guardians.FindOne(x => x.DiscordUsername.ToLower().StartsWith(message.Author.Username.ToLower() + "#" + message.Author.DiscriminatorValue));
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
                                else
                                {
                                    throwerror = true;
                                }
                            }
                            else
                            {
                                throwerror = true;
                            }
                        }
                        else
                        {
                            throwerror = true;
                        }
                    }
                }
                if (throwerror == true)
                {
                    await DiscordFunctions.DeleteSpecificMessage(message);
                }
                return true;
            }
            return false;
        }
    }
}
