﻿using Discord;

namespace DiscordBotGuardian
{
    public class RolePermissions
    {
        public string Role { get; set; }
        public OverwritePermissions ChannelPermType { get; set; }

        public static OverwritePermissions ChannelPermissions(string type)
        {
            if (type.ToLower() == "denyall")
            {
                return new OverwritePermissions(createInstantInvite: PermValue.Deny, manageChannel: PermValue.Deny, addReactions: PermValue.Deny, viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, sendTTSMessages: PermValue.Deny, manageMessages: PermValue.Deny, embedLinks: PermValue.Deny, attachFiles: PermValue.Deny, readMessageHistory: PermValue.Deny, mentionEveryone: PermValue.Deny, useExternalEmojis: PermValue.Deny, connect: PermValue.Deny, speak: PermValue.Deny, muteMembers: PermValue.Deny, deafenMembers: PermValue.Deny, moveMembers: PermValue.Deny, useVoiceActivation: PermValue.Deny, manageRoles: PermValue.Deny, manageWebhooks: PermValue.Deny);
            }
            else
            {
                // return inherit
                return new OverwritePermissions();
            }
        }

        public static GuildPermissions GuildPermissions(string type)
        {
            if (type.ToLower() == "admin")
            {
                return new GuildPermissions(createInstantInvite: true, kickMembers: true, banMembers: true, administrator: true, manageChannels: true, manageGuild: true, addReactions: true, viewAuditLog: true, sendMessages: true, sendTTSMessages: true, manageMessages: true, embedLinks: true, attachFiles: true, readMessageHistory: true, mentionEveryone: true, useExternalEmojis: true, connect: true, speak: true, muteMembers: true, deafenMembers: true, moveMembers: true, useVoiceActivation: true, changeNickname: true, manageNicknames: true, manageRoles: true, manageWebhooks: true, manageEmojis: true);
            }
            else if(type.ToLower() == "mod")
            {
                return new GuildPermissions(createInstantInvite: false, kickMembers: false, banMembers: false, administrator: false, manageChannels: false, manageGuild: false, addReactions: true, viewAuditLog: false, sendMessages: true, sendTTSMessages: false, manageMessages: true, embedLinks: true, attachFiles: true, readMessageHistory: true, mentionEveryone: false, useExternalEmojis: true, connect: true, speak: true, muteMembers: true, deafenMembers: true, moveMembers: false, useVoiceActivation: true, changeNickname: true, manageNicknames: true, manageRoles: false, manageWebhooks: false, manageEmojis: false);
            }
            else if(type.ToLower() == "standard")
            {
                return new GuildPermissions(createInstantInvite: false, kickMembers: false, banMembers: false, administrator: false, manageChannels: false, manageGuild: false, addReactions: true, viewAuditLog: false, sendMessages: true, sendTTSMessages: false, manageMessages: false, embedLinks: true, attachFiles: true, readMessageHistory: true, mentionEveryone: false, useExternalEmojis: true, connect: true, speak: true, muteMembers: false, deafenMembers: false, moveMembers: false, useVoiceActivation: true, changeNickname: true, manageNicknames: false, manageRoles: false, manageWebhooks: false, manageEmojis: false);
            }
            else
            {
                return new GuildPermissions(createInstantInvite: false, kickMembers: false, banMembers: false, administrator: false, manageChannels: false, manageGuild: false, addReactions: false, viewAuditLog: false, sendMessages: false, sendTTSMessages: false, manageMessages: false, embedLinks: false, attachFiles: false, readMessageHistory: false, mentionEveryone: false, useExternalEmojis: false, connect: false, speak: false, muteMembers: false, deafenMembers: false, moveMembers: false, useVoiceActivation: false, changeNickname: false, manageNicknames: false, manageRoles: false, manageWebhooks: false, manageEmojis: false);
            }
        }
    }
}
