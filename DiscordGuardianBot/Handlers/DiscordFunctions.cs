using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DiscordGuardianBot
{
    public class DiscordFunctions
    {
        /// <summary>
        /// Used for updating a users role for reg
        /// </summary>
        public static async Task RoleTask(CommandContext Context, string role, string userid)
        {
            // Search for the roles in the server
            var roles = Context.Guild.Roles;
            ulong roleId = 000000;
            bool found = false;
            // See if the role you are sending matches one on the server
            foreach (var singlerole in roles)
            {
                if (singlerole.Name.ToLower() == role.ToLower())
                {
                    roleId = singlerole.Id;
                    found = true;
                    break;
                }
            }
            // If we found the role update it for the user
            if (found == true)
            {
                IRole roleid = Context.Guild.GetRole(roleId);
                var user = await Context.Guild.GetUserAsync(Convert.ToUInt64(userid));
                await user.AddRoleAsync(roleid);
            }
        }
        /// <summary>
        /// Returns the word selected
        /// </summary>
        public static string GetWord(SocketMessage message, int place)
        {
            // Split the message so we can parse it
            List<string> splitmessage;
            try
            {
                splitmessage = message.Content.Split().ToList();

                if (splitmessage.Count >= place)
                {
                    return splitmessage[place].Trim();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch { return string.Empty; }
        }
        /// <summary>
        /// Used for updating a users role for Squad leads
        /// </summary>
        public static async Task SquadLeadTask(CommandContext Context, string role, string user)
        {
            // Search for the roles in the server
            var roles = Context.Guild.Roles;
            ulong roleId = 000000;
            bool found = false;
            // See if the role you are sending matches one on the server
            foreach (var singlerole in roles)
            {
                if (singlerole.Name.ToLower().Trim() == role.ToLower().Trim().Replace("team-lead-", "lead-"))
                {
                    roleId = singlerole.Id;
                    found = true;
                    break;
                }
            }
            // If we found the role update it for the user
            if (found == true)
            {
                IRole roleid = Context.Guild.GetRole(roleId);
                ulong userid = Convert.ToUInt64(user.Replace("<", "").Replace(">", "").Replace("@", ""));
                await (await Context.Guild.GetUserAsync(userid)).AddRoleAsync(roleid);
            }
        }
        /// <summary>
        /// Used for adding a user to a squad
        /// </summary>
        public static async Task SquadTask(CommandContext Context, string role, string user, string team)
        {
            // Search for the roles in the server
            var roles = Context.Guild.Roles;
            ulong roleId = 000000;
            bool found = false;
            // See if the role you are sending matches one on the server
            foreach (var singlerole in roles)
            {
                if (singlerole.Name.ToLower().Trim() == role.ToLower().Trim() + "-" + team.ToLower().Trim())
                {
                    roleId = singlerole.Id;
                    found = true;
                    break;
                }
            }
            // If we found the role update it for the user
            if (found == true)
            {
                IRole roleid = Context.Guild.GetRole(roleId);
                ulong userid = Convert.ToUInt64(user.Replace("<", "").Replace(">", "").Replace("@", ""));
                await (await Context.Guild.GetUserAsync(userid)).AddRoleAsync(roleid);
            }
        }
        /// <summary>
        /// Used for deleting the last sent message in a channel
        /// </summary>
        public static async Task DeleteLastMessage(CommandContext Context, string Channel)
        {
            // Get the list of channels
            IReadOnlyCollection<IGuildChannel> channels = await Context.Guild.GetChannelsAsync();
            // Check if the channel exists
            foreach (var channelname in channels)
            {
                if (channelname.Name.ToString().ToLower().Trim() == Channel.ToString().ToLower().Trim())
                {
                    try
                    {
                        // Convert the found channel to an IMessageChannel
                        var message = channelname as IMessageChannel;
                        // Pull the last message and change it to a usable format
                        var messages = await message.GetMessagesAsync(1).FlattenAsync();
                        // Delete the message based of its ID
                        await message.DeleteMessageAsync(messages.ToList()[0].Id);
                    }
                    catch { }
                }
            }
            return;
        }
        /// <summary>
        /// Used for deleting the a specific message
        /// </summary>
        public static async Task DeleteSpecificMessage(SocketMessage message)
        {
            try
            {
                // Delete the message based of its ID
                await message.Channel.DeleteMessageAsync(message.Id);
            }
            catch { }
            return;
        }
        /// <summary>
        /// Used for sending a message through a chat to a specific channel
        /// </summary>
        public static async Task SendMessage(CommandContext Context, string Message, string Channel)
        {
            // Get the list of channels
            IReadOnlyCollection<IGuildChannel> channels = await Context.Guild.GetChannelsAsync();
            // Check if the channel exists
            foreach (var channelname in channels)
            {
                // Make sure we are in the right channel
                if (channelname.Name.ToString().ToLower().Trim() == Channel.ToString().ToLower().Trim())
                {
                    var sendtext = channelname as IMessageChannel;
                    // Send the TOS
                    await sendtext.SendMessageAsync(Message);
                }
            }
        }
        /// <summary>
        /// Create a Category using the channel using roles provided
        /// </summary>
        public static async Task CreateCategory(CommandContext Context, string Category, [Optional]List<Permissions> Roles, [Optional]int? Position)
        {
            // Get the list of Categories
            IReadOnlyCollection<IGuildChannel> categories = await Context.Guild.GetCategoriesAsync();
            // Check if the Category exists
            bool exists = false;
            ICategoryChannel newcategory = null;
            foreach (var categoryname in categories)
            {
                if (categoryname.Name.ToString().ToLower().Trim() == Category.ToString().ToLower().Trim())
                {
                    // If the channel exists exit
                    newcategory = categoryname as ICategoryChannel;
                    exists = true;
                    break;
                }
            }
            // Create the Category
            if (exists == false)
            {
                newcategory = await Context.Guild.CreateCategoryAsync(Category);
            }

            // Wait for Category to Generate
            await Task.Delay(1000);
            if (newcategory != null)
            {
                // Check if we are passing roles
                if (Roles != null)
                {
                    // Parse in the roles to add them to the channel
                    foreach (Permissions role in Roles)
                    {
                        // Before we go any further let's see if the role already exists
                        // If the role exists exit the task
                        foreach (Discord.IRole existingrole in Context.Guild.Roles)
                        {
                            // Compare the list of roles in the discord with the Role
                            if (existingrole.Name.ToLower().Trim() == role.Role.ToLower().Trim())
                            {
                                // Add the selected roles to the channel using inhert as its base
                                await newcategory.AddPermissionOverwriteAsync(existingrole, role.ChannelPermType);
                                break;
                            }
                        }
                    }
                    // Remove the everyone permission if it's not in the list
                    bool permfound = false;
                    foreach (Permissions perm in Roles)
                    {
                        if (perm.Role.ToLower().Contains("everyone") == true)
                        {
                            permfound = true;
                            break;
                        }
                    }
                    if (permfound == false)
                    {
                        foreach (Discord.IRole existingrole in Context.Guild.Roles)
                        {
                            // Compare the list of roles in the discord with the Role
                            if (existingrole.Name.ToLower() == "@everyone")
                            {
                                OverwritePermissions denypermissions = new OverwritePermissions(createInstantInvite: PermValue.Deny, manageChannel: PermValue.Deny, addReactions: PermValue.Deny, viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, sendTTSMessages: PermValue.Deny, manageMessages: PermValue.Deny, embedLinks: PermValue.Deny, attachFiles: PermValue.Deny, readMessageHistory: PermValue.Deny, mentionEveryone: PermValue.Deny, useExternalEmojis: PermValue.Deny, connect: PermValue.Deny, speak: PermValue.Deny, muteMembers: PermValue.Deny, deafenMembers: PermValue.Deny, moveMembers: PermValue.Deny, useVoiceActivation: PermValue.Deny, manageRoles: PermValue.Deny, manageWebhooks: PermValue.Deny);
                                // Remove Everyones permissions
                                await newcategory.AddPermissionOverwriteAsync(existingrole, denypermissions);
                                break;
                            }
                        }
                    }
                }
                // Check if a position was provided
                if (Position != null)
                {
                    // Update its position
                    await newcategory.ModifyAsync(x =>
                    {
                        x.Position = Position.Value;
                    });
                }
            }
        }
        /// <summary>
        /// Create a Voice Channel using the name and roles provided
        /// </summary>
        public static async Task CreateVoiceChannel(CommandContext Context, string VoChannel, [Optional]List<Permissions> Roles, [Optional]string Category, [Optional]int? Position)
        {
            await Task.Delay(1000);
            // Get the list of channels
            IReadOnlyCollection<IGuildChannel> channels = await Context.Guild.GetVoiceChannelsAsync();
            // Check if the channel exists
            bool exists = false;
            Discord.IVoiceChannel newchannel = null;
            foreach (var channelname in channels)
            {
                if (channelname.Name.ToString().ToLower().Trim() == VoChannel.ToString().ToLower().Trim())
                {
                    // If the channel exists exit
                    newchannel = channelname as IVoiceChannel;
                    exists = true;
                    break;
                }
            }
            if (exists == false)
            {
                newchannel = await Context.Guild.CreateVoiceChannelAsync(VoChannel);
            }

            // Wait for VO to Generate
            await Task.Delay(1000);
            if (newchannel != null)
            {
                // Check if roles were passed
                if (Roles != null)
                {
                    // Parse in the roles to add them to the channel
                    foreach (Permissions role in Roles)
                    {
                        // Before we go any further let's see if the role already exists
                        // If the role exists exit the task
                        foreach (Discord.IRole existingrole in Context.Guild.Roles)
                        {
                            // Compare the list of roles in the discord with the Role
                            if (existingrole.Name.ToLower().Trim() == role.Role.ToLower().Trim())
                            {
                                // Add the selected roles to the channel using inhert as its base
                                await newchannel.AddPermissionOverwriteAsync(existingrole, role.ChannelPermType);
                                break;
                            }
                        }
                    }
                    // Remove the everyone permission if it's not in the list
                    bool permfound = false;
                    foreach (Permissions perm in Roles)
                    {
                        if (perm.Role.ToLower().Contains("everyone") == true)
                        {
                            permfound = true;
                            break;
                        }
                    }
                    if (permfound == false)
                    {
                        foreach (Discord.IRole existingrole in Context.Guild.Roles)
                        {
                            // Compare the list of roles in the discord with the Role
                            if (existingrole.Name.ToLower() == "@everyone")
                            {
                                OverwritePermissions denypermissions = new OverwritePermissions(createInstantInvite: PermValue.Deny, manageChannel: PermValue.Deny, addReactions: PermValue.Deny, viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, sendTTSMessages: PermValue.Deny, manageMessages: PermValue.Deny, embedLinks: PermValue.Deny, attachFiles: PermValue.Deny, readMessageHistory: PermValue.Deny, mentionEveryone: PermValue.Deny, useExternalEmojis: PermValue.Deny, connect: PermValue.Deny, speak: PermValue.Deny, muteMembers: PermValue.Deny, deafenMembers: PermValue.Deny, moveMembers: PermValue.Deny, useVoiceActivation: PermValue.Deny, manageRoles: PermValue.Deny, manageWebhooks: PermValue.Deny);
                                // Remove Everyones permissions
                                await newchannel.AddPermissionOverwriteAsync(existingrole, denypermissions);
                                break;
                            }
                        }
                    }
                }
                // Check if a category was passed
                if (Category != null)
                {
                    // Get the list of categories
                    IReadOnlyCollection<IGuildChannel> categories = await Context.Guild.GetCategoriesAsync();
                    ulong categoryId = 000000;
                    // Check if the category name matches the id if it does return the ID
                    foreach (var categoryname in categories)
                    {
                        if (categoryname.Name.ToLower().Trim() == Category.ToLower().Trim())
                        {
                            categoryId = categoryname.Id;
                            break;
                        }
                    }

                    // Add it to the category
                    await newchannel.ModifyAsync(x =>
                    {
                        x.CategoryId = categoryId;
                    });
                }
                // Check if a position was provided
                if (Position != null)
                {
                    // Update its position
                    await newchannel.ModifyAsync(x =>
                    {
                        x.Position = Position.Value;
                    });
                }
                if (Roles == null)
                {
                    await newchannel.SyncPermissionsAsync();
                }
            }
        }
        /// <summary>
        /// Create a Text Channel using the name and roles provided
        /// </summary>
        public static async Task CreateChannel(CommandContext Context, string Channel, [Optional]List<Permissions> Roles, [Optional]string Description, [Optional]string Category, [Optional]int? Position)
        {
            await Task.Delay(1000);
            // Get the list of channels
            IReadOnlyCollection<IGuildChannel> channels = await Context.Guild.GetChannelsAsync();
            // Check if the channel exists
            bool exists = false;
            Discord.ITextChannel newchannel = null;
            foreach (var channelname in channels)
            {
                try
                {
                    if (channelname.Name.ToString().ToLower().Trim() == Channel.ToString().ToLower().Trim())
                    {
                        // If the channel exists exit
                        exists = true;
                        newchannel = channelname as ITextChannel;
                        break;
                    }
                }
                catch { }
            }
            if (exists == false)
            {
                newchannel = await Context.Guild.CreateTextChannelAsync(Channel);
            }
            // Wait for Channel to Generate
            await Task.Delay(1000);
            if (newchannel != null)
            {
                // Check if roles were passed
                if (Roles != null)
                {
                    // Parse in the roles to add them to the channel
                    foreach (Permissions role in Roles)
                    {
                        // Before we go any further let's see if the role already exists
                        // If the role exists exit the task
                        foreach (Discord.IRole existingrole in Context.Guild.Roles)
                        {
                            // Compare the list of roles in the discord with the Role
                            if (existingrole.Name.ToLower().Trim() == role.Role.ToLower().Trim())
                            {
                                // Add the selected roles to the channel using inhert as its base
                                await newchannel.AddPermissionOverwriteAsync(existingrole, role.ChannelPermType);
                                break;
                            }
                        }
                    }
                    // Remove the everyone permission if it's not in the list
                    bool permfound = false;
                    foreach (Permissions perm in Roles)
                    {
                        if (perm.Role.ToLower().Contains("everyone") == true)
                        {
                            permfound = true;
                            break;
                        }
                    }
                    if (permfound == false)
                    {
                        foreach (Discord.IRole existingrole in Context.Guild.Roles)
                        {
                            // Compare the list of roles in the discord with the Role
                            if (existingrole.Name.ToLower() == "@everyone")
                            {
                                OverwritePermissions denypermissions = new OverwritePermissions(createInstantInvite: PermValue.Deny, manageChannel: PermValue.Deny, addReactions: PermValue.Deny, viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, sendTTSMessages: PermValue.Deny, manageMessages: PermValue.Deny, embedLinks: PermValue.Deny, attachFiles: PermValue.Deny, readMessageHistory: PermValue.Deny, mentionEveryone: PermValue.Deny, useExternalEmojis: PermValue.Deny, connect: PermValue.Deny, speak: PermValue.Deny, muteMembers: PermValue.Deny, deafenMembers: PermValue.Deny, moveMembers: PermValue.Deny, useVoiceActivation: PermValue.Deny, manageRoles: PermValue.Deny, manageWebhooks: PermValue.Deny);
                                // Remove Everyones permissions
                                await newchannel.AddPermissionOverwriteAsync(existingrole, denypermissions);
                                break;
                            }
                        }
                    }
                }
                // Check if a description was passed, if it was update the description
                if (Description != null)
                {
                    // Modify the new channel created description
                    await newchannel.ModifyAsync(x =>
                    {
                        x.Topic = Description;
                    });
                }
                // Check if a category was passed
                if (Category != null)
                {
                    // Get the list of categories
                    IReadOnlyCollection<IGuildChannel> categories = await Context.Guild.GetCategoriesAsync();
                    ulong categoryId = 000000;
                    // Check if the category name matches the id if it does return the ID
                    foreach (var categoryname in categories)
                    {
                        if (categoryname.Name.ToLower().Trim() == Category.ToLower().Trim())
                        {
                            categoryId = categoryname.Id;
                            break;
                        }
                    }

                    // Add it to the category
                    await newchannel.ModifyAsync(x =>
                    {
                        x.CategoryId = categoryId;
                    });
                }
                // Check if a position was provided
                if (Position != null)
                {
                    // Update its position
                    await newchannel.ModifyAsync(x =>
                    {
                        x.Position = Position.Value;
                    });
                }
                if (Roles == null)
                {
                    await newchannel.SyncPermissionsAsync();
                }
            }
        }
        /// <summary>
        /// Used for creating a discord role in the current guild
        /// Allowed Perms are Admin, Mod, Standard and Display
        /// </summary>
        public static async Task CreateRole(CommandContext Context, string Role, string Perms, Color RoleColor, bool DisplayedRole, [Optional]int? Position, [Optional]bool? Update)
        {
            // Before we go any further let's see if the role already exists
            // Grab Roles
            GuildPermissions roleperms = Permissions.GuildPermissions(Perms.ToLower().Trim());
            // If the role exists exit the task and update
            foreach (Discord.IRole existingrole in Context.Guild.Roles)
            {
                // Compare the list of roles in the discord with the Role
                if (existingrole.Name.ToLower().Trim() == Role.ToLower().Trim() && Update != null)
                {
                    await existingrole.ModifyAsync(x =>
                    {
                        x.Permissions = roleperms;
                        x.Hoist = DisplayedRole;
                        x.Color = RoleColor;
                        x.Mentionable = true;
                    });
                    return;
                }
            }

            // Actually create the role using the provided settings
            var role = await Context.Guild.CreateRoleAsync(Role, roleperms, RoleColor, DisplayedRole);

            // Pause after role creation
            await Task.Delay(3000);
            if (Position != null)
            {
                await role.ModifyAsync(x =>
                {
                    x.Permissions = roleperms;
                    x.Hoist = DisplayedRole;
                    x.Color = RoleColor;
                    x.Mentionable = true;
                    x.Position = Position.Value;
                });
            }
            else
            {
                await role.ModifyAsync(x =>
                {
                    x.Permissions = roleperms;
                    x.Hoist = DisplayedRole;
                    x.Color = RoleColor;
                    x.Mentionable = true;
                });
            }

        }
        /// <summary>
        /// Used for cleaning up old discord events
        /// </summary>
        public static async Task CleanupEvent(CommandContext context, SocketMessage message)
        {

            // Dump Channels NOT Guardian Bar
            foreach (Discord.IGuildChannel channel in await context.Guild.GetChannelsAsync())
            {
                bool contains = channel.Name.ToLower().Trim().Contains(DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower());
                if (contains)
                {
                    if (channel.Name.ToLower().Trim() != "guardian-bar-" + DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower())
                    {
                        try
                        {
                            await channel.DeleteAsync();
                        }
                        catch { }
                        await Task.Delay(50);
                    }
                    else
                    {
                        // Get the list of categories
                        IReadOnlyCollection<IGuildChannel> categories = await context.Guild.GetCategoriesAsync();
                        ulong categoryId = 000000;
                        // Check if the category name matches the id if it does return the ID
                        foreach (var categoryname in categories)
                        {
                            if (categoryname.Name.ToLower().Trim() == "archive")
                            {
                                categoryId = categoryname.Id;
                                break;
                            }
                        }

                        // Add it to the category
                        await channel.ModifyAsync(x =>
                        {
                            x.CategoryId = categoryId;
                        });
                    }
                }
            }

            // Dump Voice Channels
            foreach (Discord.IVoiceChannel channel in await context.Guild.GetVoiceChannelsAsync())
            {
                if (channel.Name.ToLower().Trim().Contains(DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower()))
                {
                    try
                    {
                        await channel.DeleteAsync();
                    }
                    catch { }
                    await Task.Delay(50);
                }
            }

            // Dump Categories
            foreach (Discord.ICategoryChannel channel in await context.Guild.GetCategoriesAsync())
            {
                if (channel.Name.ToLower().Trim().Contains(DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower()) || channel.Name.ToLower().Trim().Contains(DiscordFunctions.GetWord(message, 1).ToLower() + "-commons-" + DiscordFunctions.GetWord(message, 2).ToLower()))
                {
                    try
                    {
                        await channel.DeleteAsync();
                    }
                    catch { }
                    await Task.Delay(50);
                }
            }

            // Dump Roles
            foreach (Discord.IRole existingrole in context.Guild.Roles)
            {
                if (existingrole.Name.ToLower().Trim().Contains(DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower()))
                {
                    if (existingrole.Name.ToLower().Trim() != "guardian-" + DiscordFunctions.GetWord(message, 1).ToLower() + "-" + DiscordFunctions.GetWord(message, 2).ToLower())
                    {
                        try
                        {
                            await existingrole.DeleteAsync();
                        }
                        catch { }
                        await Task.Delay(50);
                    }
                }
            }

        }

        /// <summary>
        /// Creates an embeded message to display on Discord using inputs provided
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="image"></param>
        /// <param name="chooseColor"></param>
        /// <param name="Context"></param>
        public static void EmbedThisImage(string title = null, string description = null, string image = null, string chooseColor = "", CommandContext Context = null)
        {
            var toEmebed = new EmbedBuilder();
            toEmebed.WithTitle(title);
            toEmebed.WithDescription(description);
            toEmebed.WithImageUrl(image);
            chooseColor.ToLower();
            switch (chooseColor)
            {
                case "red":
                    toEmebed.WithColor(Color.DarkRed);
                    break;
                case "blue":
                    toEmebed.WithColor(Color.Blue);
                    break;
                case "gold":
                    toEmebed.WithColor(Color.Gold);
                    break;
                default:
                    toEmebed.WithColor(RandomColor());
                    break;
            }

            Context.Channel.SendMessageAsync("", false, toEmebed.Build());
        }

        /// <summary>
        /// Creates an embeded message to display on Discord using inputs provided
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="chooseColor"></param>
        public static void EmbedThis(string title = null, string description = null, string chooseColor = "", CommandContext Context = null)
        {
            var toEmebed = new EmbedBuilder();
            toEmebed.WithTitle(title);
            toEmebed.WithDescription(description);
            chooseColor.ToLower();
            switch (chooseColor)
            {
                case "red":
                    toEmebed.WithColor(Color.DarkRed);
                    break;
                case "blue":
                    toEmebed.WithColor(Color.Blue);
                    break;
                case "gold":
                    toEmebed.WithColor(Color.Gold);
                    break;
                case "magenta":
                    toEmebed.WithColor(Color.Magenta);
                    break;
                case "green":
                    toEmebed.WithColor(Color.Green);
                    break;
                case "orange":
                    toEmebed.WithColor(Color.Orange);
                    break;
                default:
                    toEmebed.WithColor(RandomColor());
                    break;
            }

            Context.Channel.SendMessageAsync("", false, toEmebed.Build());
        }
        public static Color RandomColor()
        {
            List<Color> colors = new List<Color>
            {
                Color.Blue,
                Color.DarkBlue,
                Color.DarkerGrey,
                Color.DarkGreen,
                Color.DarkGrey,
                Color.DarkMagenta,
                Color.DarkOrange,
                Color.DarkPurple,
                Color.DarkRed,
                Color.DarkTeal,
                Color.Gold,
                Color.Green,
                Color.LighterGrey,
                Color.LightGrey,
                Color.LightOrange,
                Color.Magenta,
                Color.Orange,
                Color.Purple,
                Color.Red,
                Color.Teal
            };
            var random = new Random();
            int index = random.Next(colors.Count);
            return colors[index];
        }
    }
}
