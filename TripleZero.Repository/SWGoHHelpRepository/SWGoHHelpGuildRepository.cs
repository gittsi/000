using AutoMapper;
using Newtonsoft.Json;
using SWGoH.Model;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TripleZero.Core.Caching;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.SWGoHHelper;
using TripleZero.Repository.SWGoHHelpRepository;
using TripleZero.Repository.SWGoHHelpRepository.Dto;

namespace TripleZero.Repository.SWGoHHelp
{
    public class SWGoHHelpGuildRepository : ISWGoHHelpGuildRepository
    {
        public string _url;

        SWGoHHelpSettings _settings;
        IMapper _mapper;
        private CacheClient _cacheClient;
        public SWGoHHelpGuildRepository(SWGoHHelpSettings settings, CacheClient cacheClient, IMapper mapper)
        {
            _settings = settings;
            _cacheClient = cacheClient;
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/guild/";
        }

        public async Task<Guild> GetGuild(int allyCode)
        {
            var guildJson = await FetchGuild(new int[] { Convert.ToInt32(allyCode) }, null, null, true, false, true, new { desc = 1, name = 1, members = 1, status = 1, required = 1, message = 1, gp = 1, raid = 1, roster = 1, updated = 1 });
            var guildrDto = JsonConvert.DeserializeObject<GuildSWGoHHelp>(guildJson);

            if (guildrDto == null)
                return null;

            var guild = _mapper.Map<Guild>(guildrDto);

            return guild;
        }

        public async Task<string> FetchGuild(int[] allycodes, string language = null, bool? enums = null, bool? roster = null, bool? units = null, bool? mods = null, object project = null)
        {
            var helper = new Authentication(_settings, _cacheClient);
            var token = await helper.GetToken();

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

            Console.WriteLine($"fetching data for {(allycodes.Any() ? allycodes.FirstOrDefault().ToString() : "")}");
            var response = new SWGoHHelpApiHelper().FetchApi(_url, obj, token);
            Console.WriteLine($"got data for {(allycodes.Any() ? allycodes.FirstOrDefault().ToString() : "")}");

            return response;
        }
    }
}
