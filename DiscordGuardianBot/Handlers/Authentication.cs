using Discord.Commands;
using Discord.WebSocket;
using LiteDB;

namespace DiscordGuardianBot
{
    class Authentication
    {
        public static async void RtxLondonAsync(SocketMessage message, CommandContext context, DiscordSocketClient client)
        {
            bool found = false;
            await DiscordFunctions.DeleteSpecificMessage(message);
            if (Validation.IsUserAuthenticated(message, "guardian-london-18") == false)
            {
                foreach(var user in client.GetGuild(358283239053590528).Users)
                {
                    if(message.Author.Id == user.Id)
                    {
                        await DiscordFunctions.RoleTask(context, "guardian-london-18", user.Id.ToString());
                        found = true;
                    }
                }
            }
            if (found == true)
            {
                var ThumbsUp = new Discord.Emoji("👍");
                await context.Message.AddReactionAsync(ThumbsUp);
            }
            else
            {
                await message.Channel.SendMessageAsync(message.Author.Mention + " You are not a London Guardian if you think this is a mistake please contact an @Admin");
            }
        }
        public static async void GuardianAuth(SocketMessage message, CommandContext context, Events Event)
        {
            bool found = false;
            if (Validation.IsUserAuthenticated(message, Event.Event) == false)
            {
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(@"Guardians.db"))
                {
                    var Guardians = db.GetCollection<UserData>("Guardians");
                    var Guardian = Guardians.FindOne(x => x.DiscordUsername.ToLower().StartsWith(message.Author.Username.ToLower() + "#" + message.Author.DiscriminatorValue.ToString()));
                    if (Guardian != null)
                    {
                        if (Guardian.DiscordUsername.ToLower() == message.Author.Username.ToLower() + "#" + message.Author.DiscriminatorValue.ToString())
                        {
                            Guardians.Update(Guardian);
                            await DiscordFunctions.RoleTask(context, "Guardian-" + Guardian.Event, message.Author.Id.ToString());
                            found = true;
                        }
                    }
                }
            }
            if (found == true)
            {
                var ThumbsUp = new Discord.Emoji("👍");
                await context.Message.AddReactionAsync(ThumbsUp);
            }
            else
            {
                await message.Channel.SendMessageAsync(message.Author.Mention + " You are not registered as a Guardian if you think this is a mistake please contact an @Admin");
            }
        }
    }
}
