using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SWGoH.Model;
using TripleZero.Repository.SWGoHHelp;


namespace TripleZero.Core
{
    public class PlayerContext : IPlayerContext
    {
        IMemoryCache _memoryCache;
        private SettingsTripleZeroRepository _settings;
        private IMapper _mapper;
        public PlayerContext(SettingsTripleZeroRepository settings, IMemoryCache memoryCache , IMapper mapper)
        {
            _settings = settings;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public Player GetPlayerData( int allyCode)
        {
            var playerRepo = new SWGoHHelpPlayerRepository(_settings.SWGoHHelpSettings, _memoryCache,_mapper);
            var result = playerRepo.GetPlayer(allyCode);

            return result;
        }

        public Player GetPlayerData(string alias)
        {
            throw new NotImplementedException();
        }
    }
}
