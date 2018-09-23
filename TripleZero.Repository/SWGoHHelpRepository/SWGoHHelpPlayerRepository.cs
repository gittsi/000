using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.SWGoHHelp.Dto;
using TripleZero.Repository.SWGoHHelper;
using TripleZero.Repository.SWGoHHelpRepository.Authentication;

namespace TripleZero.Repository.SWGoHHelp
{
    public class SWGoHHelpPlayerRepository : ISWGoHHelpPlayerRepository
    {
        public string _url;

        IMemoryCache _memoryCache;
        SWGoHHelpSettings _settings;
        IMapper _mapper;
        public SWGoHHelpPlayerRepository(SWGoHHelpSettings settings, IMemoryCache memoryCache, IMapper mapper)
        {
            _settings = settings;
            _memoryCache = memoryCache;
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/player/";
        }

        public Player GetPlayer(int allyCode)
        {     
            var playerJson = FetchPlayer(new int[] { Convert.ToInt32(allyCode) } , null, null, new { allyCode =1, name =1 , level =1, guildName = 1, stats = 1, roster =1 , arena =1 ,updated = 1 } );
            var playerDto = JsonConvert.DeserializeObject<PlayerSWGoHHelp[]>(playerJson);

            if (playerDto == null)
                return null;

            var player = _mapper.Map<List<Player>>(playerDto);

            return player.FirstOrDefault();
        }

        private string FetchPlayer(int[] allycodes, string language = null, bool? enums = null, object project = null)
        {
            var helper = new Authentication(_settings, _memoryCache);
            var token = helper.GetToken();

            dynamic obj = new ExpandoObject();
            obj.allycodes = allycodes;
            if (language != null)
                obj.language = language;
            if (enums.HasValue)
                obj.enums = enums;
            if (project != null)
                obj.project = project;

            var response = new SWGoHHelpApiHelper().FetchApi(_url, obj, token);

            return response;
        }        
    }
}
