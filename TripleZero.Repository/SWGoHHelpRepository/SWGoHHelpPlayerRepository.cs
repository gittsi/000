using AutoMapper;
using Newtonsoft.Json;
using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.SWGoHHelpRepository.Dto;
using TripleZero.Repository.SWGoHHelper;
using TripleZero.Core.Caching;
using System.Threading.Tasks;
using SWGoH.Model.Model.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace TripleZero.Repository.SWGoHHelpRepository
{
    public class SWGoHHelpPlayerRepository : ISWGoHHelpPlayerRepository
    {
        public string _url;

        SWGoHHelpSettings _settings;
        IMapper _mapper;
        private CacheClient _cacheClient;
        private MemoryCache _myCache;

        public SWGoHHelpPlayerRepository(SWGoHHelpSettings settings, CacheClient cacheClient, IMapper mapper)
        {
            _settings = settings;
            _cacheClient = cacheClient;
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/player/";
        }

        public SWGoHHelpPlayerRepository(SWGoHHelpSettings settings, MemoryCache myCache, IMapper mapper)
        {
            _settings = settings;
            _myCache = myCache;
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/player/";
        }

        public async Task<Player> GetPlayer(int allyCode)
        {
            var playerJson = await FetchPlayer(new int[] { Convert.ToInt32(allyCode) }, null, null, new { allyCode = 1, name = 1, level = 1, guildName = 1, stats = 1, roster = 1, arena = 1, updated = 1 });
            var playerDto = JsonConvert.DeserializeObject<PlayerSWGoHHelp[]>(playerJson);

            if (playerDto == null)
                return null;

            var player = _mapper.Map<List<Player>>(playerDto);

            return player.FirstOrDefault();
        }

        private async Task<string> FetchPlayer(int[] allycodes, string language = null, bool? enums = null, object project = null)
        {
            Authentication helper = null;

            if(_cacheClient!= null) helper = new Authentication(_settings, _cacheClient);
            if (_myCache != null) helper = new Authentication(_settings, _myCache);
            var token = await helper.GetToken();

            dynamic obj = new ExpandoObject();
            obj.allycodes = allycodes;
            if (language != null)
                obj.language = language;
            if (enums.HasValue)
                obj.enums = enums;
            if (project != null)
                obj.project = project;

            Console.WriteLine($"fetching data for {(allycodes.Any() ? allycodes.FirstOrDefault().ToString() : "") }");
            var response = new SWGoHHelpApiHelper().FetchApi(_url, obj, token);
            Console.WriteLine($"got data for {(allycodes.Any() ? allycodes.FirstOrDefault().ToString() : "")}");

            return response;
        }
    }
}
