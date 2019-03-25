using AutoMapper;
using Newtonsoft.Json;
using SWGoH.Model;
using SWGoH.Model.Model.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool _diagnosticModeOn;

        public SWGoHHelpGuildRepository(SWGoHHelpSettings settings, CacheClient cacheClient, IMapper mapper , bool diagnosticModeOn = false)
        {
            _diagnosticModeOn = diagnosticModeOn;
            _settings = settings;
            _cacheClient = cacheClient;
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/guilds/";
        }

        //public SWGoHHelpGuildRepository(SWGoHHelpSettings settings, IMapper mapper)
        //{
        //    _diagnosticModeOn = false;
        //    _settings = settings;
        //    _cacheClient = new CacheClient(new ApplicationSettings );
        //    _mapper = mapper;
        //    if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
        //    _url = settings.Protocol + "://" + settings.Host + settings.Port + "/swgoh/guilds/";
        //}

        public async Task<Guild> GetGuild(int allyCode)
        {
            Stopwatch sw = new Stopwatch();
            if (_diagnosticModeOn)            
                sw.Start();

            var guildJson = await FetchGuild(new int[] { Convert.ToInt32(allyCode) }, null, null, true, true, false, null);
            if (_diagnosticModeOn)
            {
                sw.Stop();
                Console.WriteLine($"Get guildJson from SWGoH.Help API : {sw.Elapsed}");
            }

            if (_diagnosticModeOn)
            {
                sw = new Stopwatch();
                sw.Start();
            }
                
            var guildDto = JsonConvert.DeserializeObject<List<GuildSWGoHHelp>>(guildJson).FirstOrDefault();
            if (_diagnosticModeOn)
            {
                sw.Stop();
                Console.WriteLine($"Deserialize object from SWGoH.Help to GuildDto : {sw.Elapsed}");
            }

            if (guildDto == null)
                return null;

            if (_diagnosticModeOn)
            {
                sw = new Stopwatch();
                sw.Start();
            }
            var guild = _mapper.Map<Guild>(guildDto);
            if (_diagnosticModeOn)
            {
                sw.Stop();
                Console.WriteLine($"Map GuildDto to Guild : {sw.Elapsed}");
            }

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
