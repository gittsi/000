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
using TripleZero.Repository.SWGoHHelpRepository;
using TripleZero.Repository.SWGoHHelpRepository.Dto;

namespace TripleZero.Repository.SWGoHHelp
{
    public class SWGoHHelpGuildRepository : ISWGoHHelpGuildRepository
    {
        public string _url;

        IMemoryCache _memoryCache;
        SWGoHHelpSettings _settings;
        IMapper _mapper;
        public SWGoHHelpGuildRepository(SWGoHHelpSettings settings, IMemoryCache memoryCache, IMapper mapper)
        {
            _settings = settings;
            _memoryCache = memoryCache;
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/guild/";
        }

        public Guild GetGuild(int allyCode)
        {     
            var guildJson = FetchGuild(new int[] { Convert.ToInt32(allyCode) } , null, null, true,false,true,  new { desc =1, name =1 , members =1, status = 1, required = 1, message = 1 , gp =1 ,raid = 1 ,roster=1,updated=1} );
            var guildrDto = JsonConvert.DeserializeObject<GuildSWGoHHelp>(guildJson);

            if (guildrDto == null)
                return null;

            var guild = _mapper.Map<Guild>(guildrDto);

            return guild;
        }

        public string FetchGuild(int[] allycodes, string language = null, bool? enums = null, bool? roster = null, bool? units = null, bool? mods = null, object project = null)
        {
            var helper = new Authentication(_settings, _memoryCache);
            var token = helper.GetToken();

            dynamic obj = new ExpandoObject();
            obj.allycodes = allycodes;
            if (language != null)
                obj.language = language;
            if (enums.HasValue)
                obj.enums = enums;
            if (roster.HasValue)
                obj.roster = roster;
            if (units.HasValue)
                obj.units = units;
            if (mods.HasValue)
                obj.mods = mods;
            if (project != null)
                obj.project = project;

            var response = new SWGoHHelpApiHelper().FetchApi (_url, obj,token);

            return response;
        }
    }
}
