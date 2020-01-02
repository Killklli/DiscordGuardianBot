using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Compat.Web;
using System.Linq;
using System.Text;
using Victoria;
using Victoria.Enums;
using Victoria.Responses.Rest;

namespace DiscordGuardianBot.Commands
{
    class Music
    {
        public static async System.Threading.Tasks.Task<bool> MusicCommands(SocketMessage message, CommandContext context, DiscordSocketClient client, LavaNode lava)
        {
            if (Validation.CheckCommand(message, "play"))
            {
                if ((message.Author as IVoiceState).VoiceChannel == null)
                {
                    DiscordFunctions.EmbedThis("Music", "You must first join a voice channel!", "red", context);
                    return true;
                }


                var temp = client.GetGuild(context.Guild.Id).CurrentUser.VoiceState;
                if (temp != null && client.GetGuild(context.Guild.Id).CurrentUser.VoiceChannel != (message.Author as IVoiceState).VoiceChannel)
                {
                    DiscordFunctions.EmbedThis("Music", "I can't join another voice channel until I'm disconnected from another channel.", "red", context);
                    return true;
                }

                SearchResponse search = new SearchResponse();
                var videoId = string.Empty;
                var timestamp = string.Empty;
                string query = message.Content.ToLower().Replace("!play ", "");
                if (query.ToLower().Contains("www.youtube.com/watch?v="))
                {

                    var uri = new Uri(@query);
                    var queryid = HttpUtility.ParseQueryString(uri.Query);

                    if (queryid.AllKeys.Contains("v"))
                    {
                        videoId = queryid["v"];
                    }
                    else
                    {
                        videoId = uri.Segments.Last();
                    }
                    if (queryid.AllKeys.Contains("t"))
                    {
                        timestamp = queryid["t"];
                    }
                    if (timestamp != string.Empty)
                    {
                        videoId = videoId.Replace("&t=" + timestamp, "");
                    }
                    search = await lava.SearchYouTubeAsync(query.Replace("&t=" + timestamp, ""));
                }
                else
                {
                    search = await lava.SearchYouTubeAsync(query);
                }
                LavaTrack track = new LavaTrack();
                if (query.ToLower().Contains("www.youtube.com/watch?v="))
                {

                    bool found = false;
                    foreach (var vid in search.Tracks)
                    {
                        if (vid.Id.ToLower() == videoId)
                        {
                            track = vid;
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                    {
                        track = search.Tracks.FirstOrDefault();
                    }
                }
                else
                {
                    track = search.Tracks.FirstOrDefault();
                }

                var player = lava.HasPlayer(context.Guild)
                    ? lava.GetPlayer(context.Guild)
                    : await lava.JoinAsync((context.User as IVoiceState).VoiceChannel, (ITextChannel)message.Channel);

                if (player.PlayerState == PlayerState.Playing)
                {
                    player.Queue.Enqueue(track);
                    DiscordFunctions.EmbedThis("Music", "Enqeued " + track.Title, "orange", context);
                }
                else
                {
                    await player.PlayAsync(track);
                    try
                    {
                        if (timestamp != string.Empty)
                        {
                            if (timestamp.ToLower().Contains("s"))
                            {
                                timestamp = timestamp.ToLower().Replace("s", "");
                            }
                            await player.SeekAsync(TimeSpan.FromSeconds(Convert.ToDouble(timestamp)));
                        }
                    }
                    catch { }
                    DiscordFunctions.EmbedThis("Music", "Playing " + track.Title, "green", context);
                }
                return true;
            }
            else if (Validation.CheckCommand(message, "skip"))
            {
                var _player = lava.GetPlayer(context.Guild);
                if (_player is null || _player.Queue.Count is 0)
                {
                    DiscordFunctions.EmbedThis("Music", "Nothing in the queue", "orange", context);
                    return true;
                }

                var oldTrack = _player.Track;
                await _player.SkipAsync();
                DiscordFunctions.EmbedThis("Music", "Skipped: " + oldTrack.Title + "\nNow Playing: " + _player.Track.Title, "orange", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "stop"))
            {
                var _player = lava.GetPlayer(context.Guild);
                if (_player == null)
                {
                    return true;
                }

                await _player.StopAsync();
                DiscordFunctions.EmbedThis("Music", "Stopped player", "orange", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "volume"))
            {
                LavaPlayer _player;
                try
                {
                    _player = lava.GetPlayer(context.Guild);
                }
                catch
                {
                    DiscordFunctions.EmbedThis("Music", "Nothing is playing", "orange", context);
                    return true;
                }
                if (string.IsNullOrWhiteSpace(message.Content.Replace("!volume", "")))
                {
                    DiscordFunctions.EmbedThis("Music", "Please use a number between 2- 150", "orange", context);
                    return true;
                }
                var vol = Convert.ToUInt16(message.Content.Replace("!volume", "").Trim());
                if (vol > 150 || vol <= 2)
                {
                    DiscordFunctions.EmbedThis("Music", "Please use a number between 2- 150", "orange", context);
                    return true;
                }

                await _player.UpdateVolumeAsync(vol);
                DiscordFunctions.EmbedThis("Music", "Volume set to: " + vol.ToString(), "green", context);
                return true;
            }
            else if (Validation.CheckCommand(message, "pause"))
            {
                LavaPlayer _player;
                try
                {
                    _player = lava.GetPlayer(context.Guild);
                }
                catch
                {
                    DiscordFunctions.EmbedThis("Music", "Nothing is playing", "orange", context);
                    return true;
                }
                if (_player.PlayerState != PlayerState.Paused)
                {
                    await _player.PauseAsync();
                    DiscordFunctions.EmbedThis("Music", "Player is Paused", "orange", context);
                    return true;
                }
                else
                {
                    await _player.ResumeAsync();
                    DiscordFunctions.EmbedThis("Music", "Playback Resumed", "green", context);
                    return true;
                }
            }
            else if (Validation.CheckCommand(message, "resume"))
            {
                LavaPlayer _player;
                try
                {
                    _player = lava.GetPlayer(context.Guild);
                }
                catch
                {
                    DiscordFunctions.EmbedThis("Music", "Nothing is playing", "orange", context);
                    return true;
                }
                if (_player.PlayerState != PlayerState.Paused)
                {
                    await _player.ResumeAsync();
                    DiscordFunctions.EmbedThis("Music", "Playback Resumed", "green", context);
                    return true;
                }
                else
                {
                    DiscordFunctions.EmbedThis("Music", "Playback is not paused", "orange", context);
                    return true;
                }
            }
            else if (Validation.CheckCommand(message, "join"))
            {
                var user = context.User as SocketGuildUser;
                if (user.VoiceChannel is null)
                {
                    DiscordFunctions.EmbedThis("Music", "You need to connect to a voice channel", "red", context);
                    return true;
                }
                else
                {
                    LavaPlayer _player;
                    try
                    {
                        _player = lava.GetPlayer(context.Guild);
                        DiscordFunctions.EmbedThis("Music", "Bot is already in a channel", "red", context);
                        return true;
                    }
                    catch
                    {
                        await lava.JoinAsync((context.User as IVoiceState).VoiceChannel, (ITextChannel)message.Channel);
                        return true;
                    }
                }
            }
            else if (Validation.CheckCommand(message, "leave"))
            {
                var user = context.User as SocketGuildUser;
                if (user.VoiceChannel is null)
                {
                    DiscordFunctions.EmbedThis("Music", "Please join the channel the bot is in to make it leave", "red", context);
                    return true;
                }
                else
                {
                    LavaPlayer _player;
                    try
                    {
                        _player = lava.GetPlayer(context.Guild);
                    }
                    catch
                    {
                        DiscordFunctions.EmbedThis("Music", "Please join the channel the bot is in to make it leave", "red", context);
                        return true;
                    }
                    if (_player.VoiceChannel == user.VoiceChannel)
                    {
                        await lava.LeaveAsync((context.User as IVoiceState).VoiceChannel);
                    }
                    else
                    {
                        DiscordFunctions.EmbedThis("Music", "Please join the channel the bot is in to make it leave", "red", context);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
