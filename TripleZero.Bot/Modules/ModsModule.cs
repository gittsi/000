using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
//using TripleZero.Infrastructure.DI;
using SWGoH.Model.Extensions;
using SWGoH.Model.Enums;
using SWGoH.Model;
using TripleZero.Bot.Settings;
using AutoMapper;
using TripleZero.Core;
using Microsoft.Extensions.Caching.Memory;
using TripleZero.Infrastructure.DI;
using TripleZero.Core.Caching;
using Discord;
//using TripleZero.Helper;
//using TripleZero.Core.Caching;

namespace TripleZero.Modules
{
    [Name("Mods")]
    [Summary("Mods Commands")]
    public class ModsModule : ModuleBase<SocketCommandContext>
    {
        //ApplicationSettings _applicationSettings;
        //public ModsModule(ApplicationSettings applicationSettings)
        //{
        //    _applicationSettings = applicationSettings;
        //}

        //public ModsModule()
        //{

        //}

        private CacheClient _cacheClient = IResolver.Current.CacheClient;

        #region "Secondary stats"
        private async void SendSecondaryModReply(Player player, ModStatType modStatType, ModValueType secondaryStatValueType, List<Tuple<string, Mod>> result)
        {
            if (player.LoadedFromCache) await ReplyAsync($"{_cacheClient.GetCachedDataRepositoryMessage()}");

            string retStr = "";
            if (result != null)
            {
                retStr += $"```css\n{modStatType.GetDescription()} secondary mods for player {player.PlayerName} - {player.PlayerNameInGame} \n```";
                retStr += string.Format("```Last update : {0}(UTC)```\n", player.RosterUpdateDate.ToString("yyyy-MM-dd HH:mm:ss"));

                retStr += string.Format("Returned mods : {0}", result.Count());
                foreach (var row in result)
                {
                    var modStats = row.Item2.SecondaryStat.Where(p => p.StatType == modStatType && p.ValueType == secondaryStatValueType).FirstOrDefault();
                    var newString = string.Format("{3}: **{2}{4}** {1} {0}", row.Item1.PadRight(25), EnumExtensions.GetDescription(row.Item2.Type).ToString().PadRight(10), modStats.Value.ToString().PadRight(2), modStatType.ToString(), secondaryStatValueType == ModValueType.Percentage ? "%" : "");

                    retStr += "\n";
                    retStr += newString;

                    if (retStr.Length > 1800)
                    {
                        await ReplyAsync($"{retStr}");
                        retStr = "";
                    }
                }
                if (retStr.Length > 0)
                    await ReplyAsync($"{retStr}");
                else
                    await ReplyAsync($"No mods found!");
            }
            else
            {
                retStr = $"I didn't find any mods for username {player.PlayerName}`";
                await ReplyAsync($"{retStr}");
            }
        }
        private async Task<List<Tuple<string, Mod>>> GetSpecificSecondaryMods(Player player, ModStatType modStatType, ModValueType modValueType, int rows = 20)
        {
            await Task.FromResult(1);

            var sortedMods = (from Character in player.Characters.Where(p => p.Mods != null)
                              from Mod in Character.Mods.Where(p => p.SecondaryStat != null)
                              from Stats in Mod.SecondaryStat.Where(p => p.StatType == modStatType && p.ValueType == modValueType)
                              select new
                              {
                                  Character.Name,                                  
                                  Mod
                              }
                        ).OrderByDescending(t => t.Mod.SecondaryStat.Where(p => p.StatType == modStatType && p.ValueType == modValueType).FirstOrDefault().Value).Take(rows).ToList();

            return sortedMods.Select(x => new Tuple<string, Mod>(x.Name, x.Mod)).ToList();
        }
        [Command("mods-s", RunMode = RunMode.Async)]
        [Summary("Get mods sorted by a **secondary** stat of a given player")]
        [Remarks("*mods-s {playerUserName} {modType(add **%** if you want percentage)} { {rows(optional)}\n\n examples \n1) $mods-s playerName defense \n2) $mods-s playerName defense% 5)*")]
        [Alias("ms")]
        public async Task GetSecondaryStatMods(string playerUserName, string modType, string resultsRows = "20")
        {
            //await ReplyAsync($"Sorry, API changed and cannot retrieve mods currently...");
            //return;

            bool rowsIsNumber = int.TryParse(resultsRows, out int rows);
            if (!rowsIsNumber) { await ReplyAsync($"If you want to specify how many results want, you have to put a number as third parameter! '{rows}' is not a number!");  return; }

            playerUserName = playerUserName.ToLower().Trim();
            modType = modType.ToLower().Trim();

            string loadingStr = string.Format("```{1} mods of player {0} are loading...```", playerUserName, modType);
            var messageLoading = await ReplyAsync($"{loadingStr}");

            ModValueType secondaryStatValueType = ModValueType.None;
            if (modType.Substring(modType.Length - 1, 1) == "%")
            {
                secondaryStatValueType = ModValueType.Percentage;
                modType = modType.Replace("%", "");
            }
            else
            {
                secondaryStatValueType = ModValueType.Flat;
            }

            ModStatType secondaryStatType = (ModStatType)EnumExtensions.GetEnumFromDescription(modType, typeof(ModStatType));

            if (secondaryStatType == ModStatType.None)
            {
                await ReplyAsync($"Something is wrong with your command...");
                await messageLoading.DeleteAsync();
                return;
            }

            //get player
            //var player = IResolver.Current.MongoDBRepository.GetPlayer(playerUserName).Result;
            var applicationSettings = new ApplicationSettings(new SettingsConfiguration());
            var repoSettings = applicationSettings.GetTripleZeroRepositorySettings();
            IMapper mapper = null;
            //var context = new PlayerContext(repoSettings, new MemoryCache(new MemoryCacheOptions()), mapper, new Core.Caching.CacheClient(applicationSettings.GetTripleZeroRepositorySettings(),applicationSettings.GetTripleZeroBotSettings()));
            var context = new PlayerContext(repoSettings,_cacheClient ,mapper);
            try
            {
                var player = await context.GetPlayerData(playerUserName);

                if (player == null)
                {
                    await ReplyAsync($"I couldn't find player : {playerUserName}...");
                    await messageLoading.DeleteAsync();
                    return;
                }

                var result = await GetSpecificSecondaryMods(player, secondaryStatType, secondaryStatValueType, rows);
                SendSecondaryModReply(player, secondaryStatType, secondaryStatValueType, result);
            }
            catch (Exception ex)
            {
                await this.Context.Client.GetUser("TSiTaS", "1984").SendMessageAsync($"{this.Context.User.Username} : '{this.Context.Message}' resulted to : {ex.Message}");
            }
            finally
            {
                await messageLoading.DeleteAsync();
            }            
        }
        #endregion

        #region "Primary stats"
        private async void SendPrimaryModReply(Player player, ModStatType modStatType, List<Tuple<string, Mod>> result)
        {
            if (player.LoadedFromCache) await ReplyAsync($"{_cacheClient.GetCachedDataRepositoryMessage()}");

            string retStr = "";
            if (result != null)
            {
                retStr += $"```css\n{modStatType.GetDescription()} primary mods for player {player.PlayerName} - {player.PlayerNameInGame} \n```";
                retStr += string.Format("```Last update : {0}(UTC)```\n", player.RosterUpdateDate.ToString("yyyy-MM-dd HH:mm:ss"));

                retStr += string.Format("Returned mods : {0}", result.Count());
                foreach (var row in result)
                {
                    var modStats = row.Item2.PrimaryStat;
                    var newString = string.Format("{3}: **{2}{4}** {1} {0}", row.Item1.PadRight(25), row.Item2.Type.ToString().PadRight(10), modStats.Value.ToString().PadRight(2), modStatType.ToString(), modStats.ValueType == ModValueType.Percentage ? "%" : "");
                    retStr += "\n";
                    retStr += newString;

                    if (retStr.Length > 1800)
                    {
                        await ReplyAsync($"{retStr}");
                        retStr = "";
                    }
                }

                if (retStr.Length > 0)
                    await ReplyAsync($"{retStr}");
                else
                    await ReplyAsync($"No mods found!");
            }
            else
            {
                retStr = $"I didn't find any mods for username {player.PlayerName}`";
                await ReplyAsync($"{retStr}");
            }
        }
        private async Task<List<Tuple<string, Mod>>> GetSpecificPrimaryMods(Player player, ModStatType modStatType, int rows = 20)
        {
            await Task.FromResult(1);
            var sortedMods = (from Character in player.Characters.Where(p => p.Mods != null)
                              from Mod in Character.Mods.Where(p => p.PrimaryStat != null && p.PrimaryStat.StatType == modStatType)
                              select new
                              {
                                  Character.Name,
                                  Mod
                              }
                        ).OrderByDescending(t => t.Mod.PrimaryStat.Value).Take(rows).ToList();

            return sortedMods.Select(x => new Tuple<string, Mod>(x.Name, x.Mod)).ToList();
        }

        [Command("mods-p", RunMode = RunMode.Async)]
        [Summary("Get mods sorted by a **primary** stat of a given player")]
        [Remarks("*mods-p {playerUserName} {modType(add **%** if you want percentage)} { {rows(optional)}\n\n example \n$mods-p playerName speed 5)*")]
        [Alias("mp")]
        public async Task GetPrimaryStatMods(string playerUserName, string modType, string resultsRows = "20")
        {
            //await ReplyAsync($"Sorry, API changed and cannot retrieve mods currently...");
            //return;

            bool rowsIsNumber = int.TryParse(resultsRows, out int rows);
            if (!rowsIsNumber) { await ReplyAsync($"If you want to specify how many results want, you have to put a number as third parameter! '{rows}' is not a number!"); return; }

            playerUserName = playerUserName.Trim().ToLower();
            modType = modType.Trim().ToLower();

            string loadingStr = string.Format("```{1} mods of player {0} are loading...```", playerUserName, modType);
            var messageLoading = await ReplyAsync($"{loadingStr}");

            ModStatType primaryStatType = (ModStatType)EnumExtensions.GetEnumFromDescription(modType, typeof(ModStatType));

            if (primaryStatType == ModStatType.None)
            {
                await ReplyAsync($"Something is wrong with your command...");
                await messageLoading.DeleteAsync();
                return;
            }

            //get player
            var applicationSettings = new ApplicationSettings(new SettingsConfiguration());
            var repoSettings = applicationSettings.GetTripleZeroRepositorySettings();
            IMapper mapper = null;
            var context = new PlayerContext(repoSettings, _cacheClient, mapper);
            try
            {
                var player = await context.GetPlayerData(playerUserName);

                if (player == null)
                {
                    await ReplyAsync($"I couldn't find player : {playerUserName}...");
                    await messageLoading.DeleteAsync();
                    return;
                }

                var result = await GetSpecificPrimaryMods(player, primaryStatType, rows);
                SendPrimaryModReply(player, primaryStatType, result);
            }
            catch (Exception ex)
            {
                await this.Context.Client.GetUser("TSiTaS", "1984").SendMessageAsync($"{this.Context.User.Username} : '{this.Context.Message}' resulted to : {ex.Message}");

            }
            finally
            {
                await messageLoading.DeleteAsync();
            }            
        }
        #endregion

        private async void SendSecondaryModReplyGuild(Guild guild, ModStatType modStatType, ModValueType secondaryStatValueType, List<Tuple<string,string, Mod>> result)
        {
            if (guild.LoadedFromCache) await ReplyAsync($"{_cacheClient.GetCachedDataRepositoryMessage()}");

            string retStr = "";
            if (result != null)
            {
                retStr += $"```css\n{modStatType.GetDescription()} secondary mods for guild {guild.Name} \n```";
                retStr += string.Format("```Last update : {0}(UTC)```\n", guild.SWGoHUpdateDate.ToString("yyyy-MM-dd HH:mm:ss"));

                retStr += string.Format("Returned mods : {0}", result.Count());
                foreach (var row in result)
                {
                    var modStats = row.Item3.SecondaryStat.Where(p => p.StatType == modStatType && p.ValueType == secondaryStatValueType).FirstOrDefault();
                    var newString = string.Format("{3}: **{2}{4}** {1} {0} {5}", row.Item1.PadRight(25), EnumExtensions.GetDescription(row.Item3.Type).ToString().PadRight(10), modStats.Value.ToString().PadRight(2), modStatType.ToString(), secondaryStatValueType == ModValueType.Percentage ? "%" : "", row.Item2.PadRight(25));

                    retStr += "\n";
                    retStr += newString;

                    if (retStr.Length > 1800)
                    {
                        await ReplyAsync($"{retStr}");
                        retStr = "";
                    }
                }
                if (retStr.Length > 0)
                    await ReplyAsync($"{retStr}");
                else
                    await ReplyAsync($"No mods found!");
            }
            else
            {
                retStr = $"I didn't find any mods for guild {guild.Name}`";
                await ReplyAsync($"{retStr}");
            }
        }
        private async Task<List<Tuple<string,string, Mod>>> GetSpecificSecondaryModsGuild(Guild guild, ModStatType modStatType, ModValueType modValueType, int rows = 20)
        {
            await Task.FromResult(1);

            var sortedMods = (
                              from Player in guild.Players
                              from Character in Player.Characters.Where(p => p.Mods != null)
                              from Mod in Character.Mods.Where(p => p.SecondaryStat != null)
                              from Stats in Mod.SecondaryStat.Where(p => p.StatType == modStatType && p.ValueType == modValueType)
                              select new
                              {
                                  PlayerName = Player.PlayerNameInGame,
                                  Character.Name,
                                  Mod
                              }
                        ).OrderByDescending(t => t.Mod.SecondaryStat.Where(p => p.StatType == modStatType && p.ValueType == modValueType).FirstOrDefault().Value).Take(rows).ToList();

            return sortedMods.Select(x => new Tuple<string,string, Mod>(x.PlayerName, x.Name, x.Mod)).ToList();
        }
        [Command("mods-s-g", RunMode = RunMode.Async)]
        [Summary("Get mods sorted by a **secondary** stat of a given player's guild")]
        [Remarks("*mods-s-g {playerUserName} {modType(add **%** if you want percentage)} { {rows(optional)}\n\n examples \n1) $mods-s-g playerName defense \n2) $mods-s-g playerName defense% 5)*")]
        [Alias("msg")]
        public async Task GetSecondaryStatModsGuild(string playerUserName, string modType, string resultsRows = "20")
        {
            //await ReplyAsync($"Sorry, API changed and cannot retrieve mods currently...");
            //return;

            bool rowsIsNumber = int.TryParse(resultsRows, out int rows);
            if (!rowsIsNumber) { await ReplyAsync($"If you want to specify how many results want, you have to put a number as third parameter! '{rows}' is not a number!"); return; }

            playerUserName = playerUserName.ToLower().Trim();
            modType = modType.ToLower().Trim();

            string loadingStr = string.Format("```{1} mods are loading...```", playerUserName, modType);
            var messageLoading = await ReplyAsync($"{loadingStr}");

            ModValueType secondaryStatValueType = ModValueType.None;
            if (modType.Substring(modType.Length - 1, 1) == "%")
            {
                secondaryStatValueType = ModValueType.Percentage;
                modType = modType.Replace("%", "");
            }
            else
            {
                secondaryStatValueType = ModValueType.Flat;
            }

            ModStatType secondaryStatType = (ModStatType)EnumExtensions.GetEnumFromDescription(modType, typeof(ModStatType));

            if (secondaryStatType == ModStatType.None)
            {
                await ReplyAsync($"Something is wrong with your command...");
                await messageLoading.DeleteAsync();
                return;
            }

            //get player
            //var player = IResolver.Current.MongoDBRepository.GetPlayer(playerUserName).Result;
            var applicationSettings = new ApplicationSettings(new SettingsConfiguration());
            var repoSettings = applicationSettings.GetTripleZeroRepositorySettings();
            IMapper mapper = null;
            var context = new GuildContext(repoSettings, _cacheClient, mapper);

            try
            {
                var guild = await context.GetGuildData(playerUserName);

                if (guild == null)
                {
                    await ReplyAsync($"I couldn't find player : {playerUserName}...");
                    await messageLoading.DeleteAsync();
                    return;
                }

                var result = await GetSpecificSecondaryModsGuild(guild, secondaryStatType, secondaryStatValueType, rows);
                SendSecondaryModReplyGuild(guild, secondaryStatType, secondaryStatValueType, result);
            }
            catch (Exception ex)
            {
                await this.Context.Client.GetUser("TSiTaS", "1984").SendMessageAsync($"{this.Context.User.Username} : '{this.Context.Message}' resulted to : {ex.Message}");
            }
            finally
            {
                await messageLoading.DeleteAsync();
            }          
            
        }

    }
}
