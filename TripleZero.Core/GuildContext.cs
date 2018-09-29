using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SWGoH.Model;
using TripleZero.Repository.SWGoHHelp;


namespace TripleZero.Core
{
    public class GuildContext : IGuildContext
    {
        IMemoryCache _memoryCache;
        private SettingsTripleZeroRepository _settings;
        private IMapper _mapper;
        public GuildContext(SettingsTripleZeroRepository settings, IMemoryCache memoryCache , IMapper mapper)
        {
            _settings = settings;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public Guild GetGuildData( int allyCode)
        {
            var guildRepo = new SWGoHHelpGuildRepository(_settings.SWGoHHelpSettings, _memoryCache,_mapper);
            var result = guildRepo.GetGuild(allyCode);

            return result;
        }

        public Guild GetGuildData(string alias)
        {
            var allyCode =  alias.Replace("-","" );

            if (  int.TryParse(allyCode, out int result))
            {
                return GetGuildData(result);
            }
            else
                return null;
        }
    }
}
