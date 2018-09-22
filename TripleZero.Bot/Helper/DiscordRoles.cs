﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;

namespace TripleZero.Bot.Helper
{
    public static class DiscordRoles
    {
        public static bool UserInRole(SocketCommandContext context, string roleTocheck)
        {
            var user = context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == roleTocheck);

            if (user.Roles.Contains(role)) return true; else return false;
        }
    }
}
