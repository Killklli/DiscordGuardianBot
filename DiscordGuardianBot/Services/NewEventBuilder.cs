using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordGuardianBot
{
    class NewEventBuilder
    {

        /// <summary>
        /// Used for generating a new set of roles
        /// </summary>
        public async static Task GenerateRolesAsync(CommandContext Context, string Event, int Year)
        {
            /// Create Roles
            ///
            // Actual Event Role
            await DiscordFunctions.CreateRole(Context, "Guardian-" + Event + "-" + Year, "Standard", Color.Green, true, 1, true);


            // Team Related Roles
            await DiscordFunctions.CreateRole(Context, "Crisis-Management-" + Event + "-" + Year, "Standard", Color.Default, false, 2, true);
            await DiscordFunctions.CreateRole(Context, "Center-Stage-" + Event + "-" + Year, "Standard", Color.Default, false, 3, true);
            await DiscordFunctions.CreateRole(Context, "Lead-Center-Stage-" + Event + "-" + Year, "Standard", Color.Default, false, 4, true);
            await DiscordFunctions.CreateRole(Context, "Expo-" + Event + "-" + Year, "Standard", Color.Default, false, 6, true);
            await DiscordFunctions.CreateRole(Context, "Freelancer-" + Event + "-" + Year, "Standard", Color.Default, false, 7, true);
            await DiscordFunctions.CreateRole(Context, "PA-" + Event + "-" + Year, "Standard", Color.Default, false, 8, true);
            await DiscordFunctions.CreateRole(Context, "PAL-" + Event + "-" + Year, "Standard", Color.Default, false, 9, true);
            await DiscordFunctions.CreateRole(Context, "Panels-" + Event + "-" + Year, "Standard", Color.Default, false, 10, true);
            await DiscordFunctions.CreateRole(Context, "Registration-" + Event + "-" + Year, "Standard", Color.Default, false, 11, true);
            await DiscordFunctions.CreateRole(Context, "Lead-Registration-" + Event + "-" + Year, "Standard", Color.Default, false, 12, true);
            await DiscordFunctions.CreateRole(Context, "Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 13, true);
            await DiscordFunctions.CreateRole(Context, "Lead-Response-" + Event + "-" + Year, "Standard", Color.Default, false, 14, true);
            await DiscordFunctions.CreateRole(Context, "Alpha-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 15, true);
            await DiscordFunctions.CreateRole(Context, "Bravo-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 16, true);
            await DiscordFunctions.CreateRole(Context, "Charlie-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 17, true);
            await DiscordFunctions.CreateRole(Context, "Delta-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 18, true);
            await DiscordFunctions.CreateRole(Context, "Echo-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 19, true);
            await DiscordFunctions.CreateRole(Context, "Foxtrot-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 20, true);
            await DiscordFunctions.CreateRole(Context, "Golf-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 21, true);
            await DiscordFunctions.CreateRole(Context, "Hotel-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 22, true);
            await DiscordFunctions.CreateRole(Context, "India-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 23, true);
            await DiscordFunctions.CreateRole(Context, "Juliet-Reponse-" + Event + "-" + Year, "Standard", Color.Default, false, 24, true);
            
            await DiscordFunctions.CreateRole(Context, "Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 25, true);
            await DiscordFunctions.CreateRole(Context, "Lead-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 26, true);
            await DiscordFunctions.CreateRole(Context, "Alpha-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 27, true);
            await DiscordFunctions.CreateRole(Context, "Bravo-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 28, true);
            await DiscordFunctions.CreateRole(Context, "Charlie-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 29, true);
            await DiscordFunctions.CreateRole(Context, "Delta-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 30, true);
            await DiscordFunctions.CreateRole(Context, "Echo-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 31, true);
            await DiscordFunctions.CreateRole(Context, "Foxtrot-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 32, true);
            await DiscordFunctions.CreateRole(Context, "Golf-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 33, true);
            await DiscordFunctions.CreateRole(Context, "Hotel-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 34, true);
            await DiscordFunctions.CreateRole(Context, "India-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 35, true);
            await DiscordFunctions.CreateRole(Context, "Juliet-Signatures-" + Event + "-" + Year, "Standard", Color.Default, false, 36, true);

            await Task.Delay(10000);

            await DiscordFunctions.CreateRole(Context, "Special-Rooms-" + Event + "-" + Year, "Standard", Color.Default, false, 37, true);
            await DiscordFunctions.CreateRole(Context, "Lead-Special-Rooms-" + Event + "-" + Year, "Standard", Color.Default, false, 38, true);
            await DiscordFunctions.CreateRole(Context, "Store-" + Event + "-" + Year, "Standard", Color.Default, false, 39, true);
            await DiscordFunctions.CreateRole(Context, "Tech-" + Event + "-" + Year, "Standard", Color.Default, false, 40, true);
            await DiscordFunctions.CreateRole(Context, "Happy-Hour-" + Event + "-" + Year, "Standard", Color.Default, false, 41, true);
            await DiscordFunctions.CreateRole(Context, "Lead-Happy-Hour-" + Event + "-" + Year, "Standard", Color.Default, false, 42, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Center-Stage-" + Event + "-" + Year, "Mod", Color.Default, false, 43, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Expo-" + Event + "-" + Year, "Mod", Color.Default, false, 45, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Freelancer-" + Event + "-" + Year, "Mod", Color.Default, false, 46, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-PA-" + Event + "-" + Year, "Mod", Color.Default, false, 47, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Panels-" + Event + "-" + Year, "Mod", Color.Default, false, 48, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Registration-" + Event + "-" + Year, "Mod", Color.Default, false, 49, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Response-" + Event + "-" + Year, "Mod", Color.Default, false, 50, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Signatures-" + Event + "-" + Year, "Mod", Color.Default, false, 51, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Special-Rooms-" + Event + "-" + Year, "Mod", Color.Default, false, 52, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Store-" + Event + "-" + Year, "Mod", Color.Default, false, 53, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Tech-" + Event + "-" + Year, "Mod", Color.Default, false, 54, true);
            await DiscordFunctions.CreateRole(Context, "Team-Lead-Happy-Hour-" + Event + "-" + Year, "Mod", Color.Default, false, 55, true);

            // Global Roles
            if (Context.Guild.Roles.Count < 50)
            {
                await DiscordFunctions.CreateRole(Context, "Mod", "Mod", Color.LightOrange, true, 56, true);
                await DiscordFunctions.CreateRole(Context, "Team-Lead", "Display", Color.Blue, true, 57, true);
                await DiscordFunctions.CreateRole(Context, "Staff", "Mod", Color.Magenta, true, 58, true);
                await DiscordFunctions.CreateRole(Context, "Head-Guardian-" + Event + "-" + Year, "Mod", Color.Red, false, 59, true);
                await DiscordFunctions.CreateRole(Context, "Head-Guardian", "Mod", Color.Red, true, 60, true);
                await DiscordFunctions.CreateRole(Context, "Admin", "Admin", Color.Purple, true, 61, true);
            }
            else
            {
                await DiscordFunctions.CreateRole(Context, "Mod", "Mod", Color.LightOrange, true, Context.Guild.Roles.Count - 6, true);
                await DiscordFunctions.CreateRole(Context, "Team-Lead", "Display", Color.Blue, true, Context.Guild.Roles.Count - 5, true);
                await DiscordFunctions.CreateRole(Context, "Staff", "Mod", Color.Magenta, true, Context.Guild.Roles.Count - 4, true);
                await DiscordFunctions.CreateRole(Context, "Head-Guardian-" + Event + "-" + Year, "Mod", Color.Red, false, Context.Guild.Roles.Count - 3, true);
                await DiscordFunctions.CreateRole(Context, "Head-Guardian", "Mod", Color.Red, true, Context.Guild.Roles.Count - 2, true);
                await DiscordFunctions.CreateRole(Context, "Admin", "Admin", Color.Purple, true, Context.Guild.Roles.Count - 1, true);
            }

        }

        /// <summary>
        /// Used for generating a new set of channels for a new year
        /// </summary>
        public async static Task GenerateChannelsAsync(CommandContext Context, string Event, int Year)
        {
            /// Create Global Perm set for all Guardians so we dont need to manually tack it on each time
            ///
            // Generate list
            List<Permissions> GuardianEventsStandard = new List<Permissions>();
            List<ReorderRoleProperties> reorder = new List<ReorderRoleProperties>();
            foreach (Discord.IRole existingrole in Context.Guild.Roles)
            {
                // Compare the list of roles in the discord with the Role
                if (existingrole.Name.Contains("Guardian-") && existingrole.Name.Contains("Head-Guardian-") == false)
                {
                    GuardianEventsStandard.Add(new Permissions { Role = existingrole.Name, ChannelPermType = Permissions.ChannelPermissions("standard") });
                    reorder.Add(new ReorderRoleProperties(id: existingrole.Id, pos: 0));
                }
            }
            await Context.Guild.ReorderRolesAsync(reorder);

            // Add the event we are adding
            GuardianEventsStandard.Add(new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") });
            // Add default roles
            GuardianEventsStandard.Add(new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") });
            GuardianEventsStandard.Add(new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") });
            GuardianEventsStandard.Add(new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") });




            List<Permissions> GuardianEventsReadOnly = new List<Permissions>();
            foreach (Discord.IRole existingrole in Context.Guild.Roles)
            {
                // Compare the list of roles in the discord with the Role
                if (existingrole.Name.Contains("Guardian-"))
                {
                    GuardianEventsReadOnly.Add(new Permissions { Role = existingrole.Name, ChannelPermType = Permissions.ChannelPermissions("readonly") });
                }
            }
            // Add the event we are adding
            GuardianEventsReadOnly.Add(new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") });
            // Add default roles
            GuardianEventsReadOnly.Add(new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") });
            GuardianEventsReadOnly.Add(new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") });
            GuardianEventsReadOnly.Add(new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") });


            /// Create Channels
            ///

            // Event Category Channels
            // Command Center
            await DiscordFunctions.CreateChannel(Context, "leaders-lodge-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "HGs and TLs", "Command-Center-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "announcements-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Announcements for RTX", "Command-Center-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "links-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Important links you should probably bookmark.", "Command-Center-" + Event + "-" + Year, 4);


            // Commons
            await DiscordFunctions.CreateChannel(Context, "guardian-bar-" + Event + "-" + Year, null, "There will always be a Guardian Bar", Event + "-Commons-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "ride-room-share-" + Event + "-" + Year, null, "Because no one uses the forum anymore.", Event + "-Commons-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "meetups-events-" + Event + "-" + Year, null, "It's like matchmaking, but in real life.", Event + "-Commons-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "town-hall-" + Event + "-" + Year, null, "Ask the HGs anything appropriate.", Event + "-Commons-" + Year, 4);
            await DiscordFunctions.CreateVoiceChannel(Context, Event + "-" + Year + "-vc-1", null, Event + "-Commons-" + Year, 5);
            await DiscordFunctions.CreateVoiceChannel(Context, Event + "-" + Year + "-vc-2", null, Event + "-Commons-" + Year, 6);
            await DiscordFunctions.CreateVoiceChannel(Context, "town-hall-vc-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, Event + "-Commons-" + Year, 7);

            // Team Chats
            // Center-Stage
            await DiscordFunctions.CreateChannel(Context, "leads-cs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Leads chat", "Center-Stage-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-cs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Center-Stage-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "general-cs-" + Event + "-" + Year, null, "Team Chat", "Center-Stage-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "ops-cs-" + Event + "-" + Year, null, "Team Operations", "Center-Stage-" + Event + "-" + Year, 4);
            await DiscordFunctions.CreateVoiceChannel(Context, "leads-vc-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Center-Stage-" + Event + "-" + Year, 5);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-cs-" + Event + "-" + Year, null, "Center-Stage-" + Event + "-" + Year, 6);

            // Expo
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-ex-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Expo-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Expo-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Expo-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-ex-" + Event + "-" + Year, null, "Team Chat", "Expo-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-ex-" + Event + "-" + Year, null, "Squad Chat", "Expo-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-ex-" + Event + "-" + Year, null, "Expo-" + Event + "-" + Year, 4);

            // Freelancer
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-fl-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Freelancer-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "FreeLancer-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Freelancer-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-fl-" + Event + "-" + Year, null, "Team Chat", "Freelancer-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-fl-" + Event + "-" + Year, null, "Team Operations", "Freelancer-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-fl-" + Event + "-" + Year, null, "Freelancer-" + Event + "-" + Year, 4);

            // PA
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-pa-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-PA-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "PA-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "PA-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-pa-" + Event + "-" + Year, null, "Team Chat", "PA-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-pa-" + Event + "-" + Year, null, "Team Operations", "PA-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "response-pa-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-PA-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "PAL-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Direct line to Response Leads", "PA-" + Event + "-" + Year, 4);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-pa-" + Event + "-" + Year, null, "PA-" + Event + "-" + Year, 5);

            // Panels
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-pl-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Panels-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Panels-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Panels-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-pl-" + Event + "-" + Year, null, "Team Chat", "Panels-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-pl-" + Event + "-" + Year, null, "Squad Chat", "Panels-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-pl-" + Event + "-" + Year, null, "Panels-" + Event + "-" + Year, 4);

            // Registration
            await DiscordFunctions.CreateChannel(Context, "leads-rg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Leads chat", "Registration-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-rg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Registration-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "general-rg-" + Event + "-" + Year, null, "Team Chat", "Registration-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "ops-rg-" + Event + "-" + Year, null, "Team Operations", "Registration-" + Event + "-" + Year, 4);
            await DiscordFunctions.CreateVoiceChannel(Context, "leads-rg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Registration-" + Event + "-" + Year, 5);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-rg-" + Event + "-" + Year, null, "Registration-" + Event + "-" + Year, 6);

            // Response
            await DiscordFunctions.CreateChannel(Context, "leads-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Leads chat", "Response-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-rs-" + Event + "-" + Year, null, "Team Chat", "Response-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Reponse-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "File share for Response team.", "Response-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "alpha-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Alpha-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 4);
            await DiscordFunctions.CreateChannel(Context, "bravo-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Bravo-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 5);
            await DiscordFunctions.CreateChannel(Context, "charlie-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Charlie-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 6);
            await DiscordFunctions.CreateChannel(Context, "delta-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Delta-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 7);
            await DiscordFunctions.CreateChannel(Context, "echo-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Echo-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 8);
            await DiscordFunctions.CreateChannel(Context, "foxtrot-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Foxtrot-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 9);
            await DiscordFunctions.CreateChannel(Context, "golf-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Golf-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 10);
            await DiscordFunctions.CreateChannel(Context, "hotel-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Hotel-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 11);
            await DiscordFunctions.CreateChannel(Context, "india-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "India-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 12);
            await DiscordFunctions.CreateChannel(Context, "juliet-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Juliet-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Response-" + Event + "-" + Year, 13);
            await DiscordFunctions.CreateVoiceChannel(Context, "leads-rs-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Response-" + Event + "-" + Year, 16);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-rs-" + Event + "-" + Year, null, "Response-" + Event + "-" + Year, 17);

            //Signatures
            await DiscordFunctions.CreateChannel(Context, "leads-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Leads chat", "Signatures-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Signatures-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "general-sg-" + Event + "-" + Year, null, "Team Chat", "Signatures-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "alpha-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Alpha-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 4);
            await DiscordFunctions.CreateChannel(Context, "bravo-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Bravo-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 5);
            await DiscordFunctions.CreateChannel(Context, "charlie-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Charlie-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 6);
            await DiscordFunctions.CreateChannel(Context, "delta-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Delta-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 7);
            await DiscordFunctions.CreateChannel(Context, "echo-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Echo-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 8);
            await DiscordFunctions.CreateChannel(Context, "foxtrot-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Foxtrot-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 9);
            await DiscordFunctions.CreateChannel(Context, "golf-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Golf-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 10);
            await DiscordFunctions.CreateChannel(Context, "hotel-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Hotel-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Squad Operations", "Signatures-" + Event + "-" + Year, 11);
            await DiscordFunctions.CreateVoiceChannel(Context, "leads-sg-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Signatures-" + Event + "-" + Year, 12);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-sg-" + Event + "-" + Year, null, "Signatures-" + Event + "-" + Year, 13);

            // Special Rooms
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-sr-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Special-Rooms-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Special-Rooms-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Special-Rooms-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-sr-" + Event + "-" + Year, null, "Team Chat", "Special-Rooms-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-sr-" + Event + "-" + Year, null, "Squad Chat", "Special-Rooms-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-sr-" + Event + "-" + Year, null, "Special-Rooms-" + Event + "-" + Year, 4);

            // Store
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-st-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Store-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Store-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Store-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-st-" + Event + "-" + Year, null, "Team Chat", "Store-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-st-" + Event + "-" + Year, null, "Squad Chat", "Store-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-st-" + Event + "-" + Year, null, "Store-" + Event + "-" + Year, 4);

            // Tech
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-th-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Tech-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Tech-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Tech-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "general-th-" + Event + "-" + Year, null, "Team Chat", "Tech-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "ops-th-" + Event + "-" + Year, null, "Squad Chat", "Tech-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-th-" + Event + "-" + Year, null, "Tech-" + Event + "-" + Year, 4);

            // Happy Hour
            await DiscordFunctions.CreateChannel(Context, "leads-hh-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Leads chat", "Happy-Hour-" + Event + "-" + Year, 1);
            await DiscordFunctions.CreateChannel(Context, "bulletin-board-hh-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            }, "Board.", "Happy-Hour-" + Event + "-" + Year, 2);
            await DiscordFunctions.CreateChannel(Context, "general-hh-" + Event + "-" + Year, null, "Team Chat", "Happy-Hour-" + Event + "-" + Year, 3);
            await DiscordFunctions.CreateChannel(Context, "ops-hh-" + Event + "-" + Year, null, "Team Operations", "Happy-Hour-" + Event + "-" + Year, 4);
            await DiscordFunctions.CreateVoiceChannel(Context, "leads-hh-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") },
                new Permissions { Role = "Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") }
            }, "Happy-Hour-" + Event + "-" + Year, 5);
            await DiscordFunctions.CreateVoiceChannel(Context, "general-hh-" + Event + "-" + Year, null, "Happy-Hour-" + Event + "-" + Year, 6);

        }


        /// <summary>
        /// Used for generating a new set of Categories
        /// </summary>
        public async static Task GenerateCategoriesAsync(CommandContext Context, string Event, int Year)
        {
            /// Create Global Perm set for all Guardians so we dont need to manually tack it on each time
            ///
            // Generate list
            List<Permissions> GuardianEventsStandard = new List<Permissions>();
            foreach (Discord.IRole existingrole in Context.Guild.Roles)
            {
                // Compare the list of roles in the discord with the Role
                if (existingrole.Name.Contains("Guardian-"))
                {
                    if (existingrole.Name.Contains("Head-Guardian-") == false)
                    {
                        GuardianEventsStandard.Add(new Permissions { Role = existingrole.Name, ChannelPermType = Permissions.ChannelPermissions("standard") });
                    }

                }
            }
            // Add default roles
            GuardianEventsStandard.Add(new Permissions { Role = "Head-Guardian", ChannelPermType = Permissions.ChannelPermissions("admin") });
            GuardianEventsStandard.Add(new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") });
            GuardianEventsStandard.Add(new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") });




            List<Permissions> GuardianEventsReadOnly = new List<Permissions>();
            foreach (Discord.IRole existingrole in Context.Guild.Roles)
            {
                // Compare the list of roles in the discord with the Role
                if (existingrole.Name.Contains("Guardian-") && existingrole.Name.Contains("Head-Guardian-") == false)
                {
                    GuardianEventsReadOnly.Add(new Permissions { Role = existingrole.Name, ChannelPermType = Permissions.ChannelPermissions("readonly") });
                }
            }
            // Add default roles
            GuardianEventsReadOnly.Add(new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") });
            GuardianEventsReadOnly.Add(new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") });
            GuardianEventsReadOnly.Add(new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") });

            /// Create Categories before we make channels
            ///
            // Global Categories
            await DiscordFunctions.CreateCategory(Context, "Administration", null, 1);
            await DiscordFunctions.CreateCategory(Context, "Global", GuardianEventsStandard, 2);
            GuardianEventsStandard.Remove(new Permissions { Role = "Head-Guardian", ChannelPermType = Permissions.ChannelPermissions("admin") });
            GuardianEventsStandard.Add(new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") });

            // Event Based Categories
            await DiscordFunctions.CreateCategory(Context, Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") }
            });
            await DiscordFunctions.CreateCategory(Context, "Command-Center-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Admin", ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Head-Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("admin") },
                new Permissions { Role = "Team-Lead-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("readonly") }
            });
            await DiscordFunctions.CreateCategory(Context, Event + "-Commons-" + Year, new List<Permissions> {
                new Permissions { Role = "Guardian-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Staff", ChannelPermType = Permissions.ChannelPermissions("standard") }
            });
            await DiscordFunctions.CreateCategory(Context, "Center-Stage-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Center-Stage-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Expo-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Expo-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Expo-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Freelancer-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Freelancer-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Freelancer-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "PA-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "PA-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-PA-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Panels-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Panels-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Panels-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Registration-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Registration-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Response-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Response-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Signatures-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Signatures-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Special-Rooms-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Special-Rooms-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Special-Rooms-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Store-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Store-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Store-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Tech-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Tech-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Tech-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });
            await DiscordFunctions.CreateCategory(Context, "Happy-Hour-" + Event + "-" + Year, new List<Permissions> {
                new Permissions { Role = "Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("standard") },
                new Permissions { Role = "Team-Lead-Happy-Hour-" + Event + "-" + Year, ChannelPermType = Permissions.ChannelPermissions("team-lead") }
            });

            // Archive
            IReadOnlyCollection<IGuildChannel> cats = await Context.Guild.GetCategoriesAsync();
            await DiscordFunctions.CreateCategory(Context, "Archive", GuardianEventsStandard, cats.Count);

        }
    }

}