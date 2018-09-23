using AutoMapper;
using SWGoH.Model;
using SWGoH.Model.Enums;
using System;
using System.Linq;
using TripleZero.Repository.SWGoHHelp.Dto;

namespace TripleZero.Repository.Mapping
{
    internal class MappingConfiguration : IMappingConfiguration
    {
        private IMapper _Mapper;
        public MappingConfiguration()
        {
            _Mapper = GetConfigureMapper();
        }
        public IMapper GetConfigureMapper()
        {
            if (_Mapper != null)
                return _Mapper;
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<Crew, string>()
                    //.ForMember(dest => dest, src => src.MapFrom(source => source.UnitId))
                    //;
                    cfg.CreateMap<Slot, ModSlot>().ConvertUsing(value =>
                    {
                        switch (value)
                        {
                            case Slot.Arrow:
                                return ModSlot.Receiver;
                            case Slot.Circle:
                                return ModSlot.DataBus;
                            case Slot.Cross:
                                return ModSlot.Multiplexer;
                            case Slot.Diamond:
                                return ModSlot.Processor;
                            case Slot.Square:
                                return ModSlot.Transmitter;
                            case Slot.Triangle:
                                return ModSlot.HoloArray;
                            default:
                                return ModSlot.None;
                        }
                    });


                    cfg.CreateMap<SWGoHHelp.Dto.Mod, SWGoH.Model.Mod>()
                    .ForMember(dest => dest.Level, src => src.MapFrom(source => source.Level))
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.Id))
                    .ForMember(dest => dest.PrimaryStat, src => src.Ignore())
                    .ForMember(dest => dest.SecondaryStat, src => src.Ignore())
                    .ForMember(dest => dest.Rarity, src => src.MapFrom(source => source.Pips))
                    .ForMember(dest => dest.Type, src => src.MapFrom(source => source.Slot))
                    ;

                    cfg.CreateMap<Roster, Character>()
                    .ForMember(dest => dest.Gear, src => src.MapFrom(source => source.Gear))
                    .ForMember(dest => dest.StatPower, src => src.Ignore())
                    .ForMember(dest => dest.Mods, src => src.MapFrom(source => source.Mods))
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Stars, src => src.MapFrom(source => (int)source.Rarity))
                    .ForMember(dest => dest.Level, src => src.MapFrom(source => (int)source.Level))
                    .ForMember(dest => dest.Abilities, src => src.MapFrom(source => source.Skills))
                    .ForMember(dest => dest.GeneralStats, src => src.Ignore())
                    .ForMember(dest => dest.OffenseStats, src => src.Ignore())
                    .ForMember(dest => dest.Survivability, src => src.Ignore())
                    .ForMember(dest => dest.Tags, src => src.Ignore())
                    .ForMember(dest => dest.SWGoHUrl, src => src.Ignore())
                    .ForMember(dest => dest.Power, src => src.Ignore())
                    ;

                    cfg.CreateMap<Roster, Ship>()
                    .ForMember(dest => dest.Crew, src => src.MapFrom(source => source.Crew))
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Stars, src => src.MapFrom(source => (int)source.Rarity))
                    .ForMember(dest => dest.Level, src => src.MapFrom(source => (int)source.Level))
                    .ForMember(dest => dest.Abilities, src => src.MapFrom(source => source.Skills))
                    .ForMember(dest => dest.GeneralStats, src => src.Ignore())
                    .ForMember(dest => dest.OffenseStats, src => src.Ignore())
                    .ForMember(dest => dest.Survivability, src => src.Ignore())
                    .ForMember(dest => dest.Tags, src => src.Ignore())
                    .ForMember(dest => dest.SWGoHUrl, src => src.Ignore())
                    .ForMember(dest => dest.Power, src => src.Ignore())
                    ;

                    cfg.CreateMap<PlayerSWGoHHelp, Player>()
                    .ForMember(dest => dest.Id, src => src.Ignore())
                    .ForMember(dest => dest.GuildName, src => src.MapFrom(source => source.GuildName))
                    .ForMember(dest => dest.PlayerNameInGame, src => src.MapFrom(source => source.Name))
                    .ForMember(dest => dest.PlayerName, src => src.Ignore())
                    .ForMember(dest => dest.RosterUpdateDate, src => src.MapFrom(source => ConvertFromUnixTimestamp(source.Updated)))
                    .ForMember(dest => dest.DBUpdateDate, src => src.MapFrom(source => source.Updated))
                    .ForMember(dest => dest.GalacticPowerCharacters, src => src.MapFrom(source => source.GpChar))
                    .ForMember(dest => dest.GalacticPowerShips, src => src.MapFrom(source => source.GpShip))
                    .ForMember(dest => dest.Characters, src => src.MapFrom(source => source.Roster.Where(p=>p.Type == RosterType.Char)))
                    .ForMember(dest => dest.Ships, src => src.MapFrom(source => source.Roster.Where(p => p.Type == RosterType.Ship)))
                    .ForMember(dest => dest.Arena, src => src.MapFrom(source => source.Arena))
                    .ForMember(dest => dest.LoadedFromCache, src => src.Ignore())
                    ;

                    cfg.AllowNullDestinationValues = true;
                    cfg.AllowNullCollections = true;
                });

                config.AssertConfigurationIsValid();
                return config.CreateMapper();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DateTime ConvertFromUnixTimestamp(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(timestamp);
        }
    }    
}
