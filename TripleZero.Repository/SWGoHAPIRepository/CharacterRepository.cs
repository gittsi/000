using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWGoH.Model;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.SWGoHAPIRepository.Dto;

namespace TripleZero.Repository.SWGoHAPIRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        public string Url => "https://swgoh.gg/api/characters/";
        

       
        IMapper _mapper;

        public CharacterRepository( IMapper mapper)
        {
            
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();

        }

        public async Task<List<CharacterConfig>> GetCharacters()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Url);
                HttpContent content = response.Content;
                string reqResult = await content.ReadAsStringAsync();

                var characterDto = JsonConvert.DeserializeObject<CharacterDto[]>(reqResult);

                return _mapper.Map<List<CharacterConfig>>(characterDto.ToList());
                
                //JObject json = new JObject();
                //try
                //{
                //    json = JObject.Parse(reqResult);
                //}
                //catch (Exception ex)
                //{
                //    //swallow the error                    
                //    return null;
                //}               
            }

            return null;
        }

       
    }
}
