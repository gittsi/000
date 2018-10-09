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
    public class ShipRepository : IShipRepository
    {
        public string Url => "https://swgoh.gg/api/ships/";
        

       
        IMapper _mapper;

        public ShipRepository( IMapper mapper)
        {
            
            _mapper = mapper;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();

        }

        public async Task<List<ShipConfig>> GetShips()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Url);
                HttpContent content = response.Content;
                string reqResult = await content.ReadAsStringAsync();

                var shipDto = JsonConvert.DeserializeObject<ShipDto[]>(reqResult);

                return _mapper.Map<List<ShipConfig>>(shipDto.ToList());
                
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
