using AutoMapper;
using SWGoH.Model;
using SWGoH.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using TripleZero.Repository.Dto;
using TripleZero.Repository.SWGoHHelpRepository.Dto;

namespace TripleZero.Repository.Mapping
{
    public class MappingConfiguration : IMappingConfiguration
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

                    cfg.CreateMap<SWGoHHelpRepository.Dto.Mod, ModStat>().ConvertUsing<PrimaryModConverter>();
                    cfg.CreateMap<SWGoHHelpRepository.Dto.Mod, List<ModStat>>().ConvertUsing<SecondaryModConverter>();

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


                    cfg.CreateMap<SWGoHHelpRepository.Dto.Mod, SWGoH.Model.Mod>()
                    .ForMember(dest => dest.Level, src => src.MapFrom(source => source.Level))
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.Id))
                    .ForMember(dest => dest.PrimaryStat, src => src.MapFrom(source => source))
                    .ForMember(dest => dest.SecondaryStat, src => src.MapFrom(source => source))
                    .ForMember(dest => dest.Rarity, src => src.MapFrom(source => source.Pips))
                    .ForMember(dest => dest.Type, src => src.MapFrom(source => source.Slot))
                    ;

                    cfg.CreateMap<Roster, Character>()
                    .ForMember(dest => dest.Gear, src => src.MapFrom(source => source.Gear))
                    .ForMember(dest => dest.StatPower, src => src.Ignore())
                    .ForMember(dest => dest.Name, src => src.Ignore())
                    .ForMember(dest => dest.Mods, src => src.MapFrom(source => source.Mods))
                    .ForMember(dest => dest.Id, src => src.MapFrom(source => source.Name))
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
                    .ForMember(dest => dest.Name, src => src.Ignore())
                    .ForMember(dest => dest.Id, src => src.MapFrom(source => source.Name))
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

                    cfg.CreateMap<SWGoHHelpRepository.Dto.Skill, Ability>()
                   .ForMember(dest => dest.MaxLevel, src =>  src.MapFrom(source => 8))
                    .ForMember(dest => dest.Level, src => src.MapFrom(source => source.Tier))
                    .ForMember(dest => dest.Name, src => src.Ignore());

                    cfg.CreateMap<PlayerSWGoHHelp, Player>()
                    .ForMember(dest => dest.Id, src => src.Ignore())
                    .ForMember(dest => dest.GuildName, src => src.MapFrom(source => source.GuildName))
                    .ForMember(dest => dest.PlayerNameInGame, src => src.MapFrom(source => source.Name))
                    .ForMember(dest => dest.PlayerName, src => src.Ignore())
                    .ForMember(dest => dest.RosterUpdateDate, src => src.MapFrom(source => ConvertFromUnixTimestamp(source.Updated)))
                    .ForMember(dest => dest.DBUpdateDate, src => src.MapFrom(source => source.Updated))
                    .ForMember(dest => dest.GalacticPowerCharacters, src => src.MapFrom(source => source.GpChar))
                    .ForMember(dest => dest.GalacticPowerShips, src => src.MapFrom(source => source.GpShip))
                    .ForMember(dest => dest.Characters, src => src.MapFrom(source => source.Roster.Where(p => p.Type == RosterType.Char)))
                    .ForMember(dest => dest.Ships, src => src.MapFrom(source => source.Roster.Where(p => p.Type == RosterType.Ship)))
                    .ForMember(dest => dest.Arena, src => src.MapFrom(source => source.Arena))
                    .ForMember(dest => dest.LoadedFromCache, src => src.Ignore())
                    ;

                    cfg.CreateMap<GuildSWGoHHelp, Guild>()
                    .ForMember(dest => dest.GalacticPowerAverage, src => src.Ignore())
                    .ForMember(dest => dest.SWGoHUpdateDate, src => src.MapFrom(source => ConvertFromUnixTimestamp(source.Updated)))
                    .ForMember(dest => dest.LoadedFromCache, src => src.Ignore())
                    .ForMember(dest => dest.EntryUpdateDate, src => src.Ignore())
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.GuildName))
                    .ForMember(dest => dest.GalacticPower, src => src.MapFrom(source => source.GP))
                    .ForMember(dest => dest.Players, src => src.MapFrom(source => source.Roster))
                    ;


                    //guild config
                    cfg.CreateMap<GuildConfigDto, GuildConfig>()
                    .ForMember(dest => dest.LoadedFromCache, src => src.Ignore())
                    ;

                    cfg.CreateMap<SWGoHHelpRepository.Dto.Crew, SWGoH.Model.Crew>()
                    .ForMember(dest => dest.Id, src => src.MapFrom(source => source.UnitId))
                    ;


                    cfg.CreateMap<SWGoHAPIRepository.Dto.CharacterDto, CharacterConfig>()
                    .ForMember(dest => dest.LoadedFromCache, src => src.Ignore())
                    .ForMember(dest => dest.Abilities, src => src.Ignore())
                    .ForMember(dest => dest.Aliases, src => src.Ignore())
                    .ForMember(dest => dest.SWGoHUrl, src => src.Ignore())
                    .ForMember(dest => dest.Id, src => src.Ignore())
                    .ForMember(dest => dest.Command, src => src.MapFrom(source => source.NameId))
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.Name))
                    //.ForMember(dest => dest., src => src.MapFrom(source => source.GP))

                    ;

                    cfg.CreateMap<SWGoHAPIRepository.Dto.ShipDto, ShipConfig>()
                    .ForMember(dest => dest.LoadedFromCache, src => src.Ignore())
                    .ForMember(dest => dest.Aliases, src => src.Ignore())
                    .ForMember(dest => dest.SWGoHUrl, src => src.Ignore())
                    .ForMember(dest => dest.Id, src => src.Ignore())
                    .ForMember(dest => dest.Command, src => src.MapFrom(source => source.NameId))
                    .ForMember(dest => dest.Name, src => src.MapFrom(source => source.Name))
                    //.ForMember(dest => dest., src => src.MapFrom(source => source.GP))

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

        public class PrimaryModConverter : ITypeConverter<SWGoHHelpRepository.Dto.Mod, ModStat>
        {
            public ModStat Convert(SWGoHHelpRepository.Dto.Mod source, ModStat destination, ResolutionContext context)
            {
                var perc = 1000000.0;
                var flat = 100000000;
                bool isFlat = false;

                ModStat modStat = new ModStat();
                switch (source.PrimaryBonusType)
                {
                    case PrimaryBonusType.Accuracy:
                        modStat.StatType = ModStatType.Accuracy;
                        break;
                    case PrimaryBonusType.CriticalAvoidance:
                        modStat.StatType = ModStatType.CriticalAvoidance;
                        break;
                    case PrimaryBonusType.CriticalChance:
                        modStat.StatType = ModStatType.CriticalChance;
                        break;
                    case PrimaryBonusType.CriticalDamage:
                        modStat.StatType = ModStatType.CriticalDamage;
                        break;
                    case PrimaryBonusType.Defense:
                        modStat.StatType = ModStatType.Defense;
                        break;
                    case PrimaryBonusType.Health:
                        modStat.StatType = ModStatType.Health;
                        break;
                    case PrimaryBonusType.Offense:
                        modStat.StatType = ModStatType.Offense;
                        break;
                    case PrimaryBonusType.Potency:
                        modStat.StatType = ModStatType.Potency;
                        break;
                    case PrimaryBonusType.Protection:
                        modStat.StatType = ModStatType.Protection;
                        break;
                    case PrimaryBonusType.Tenacity:
                        modStat.StatType = ModStatType.Tenacity;
                        break;
                    case PrimaryBonusType.Speed:
                        modStat.StatType = ModStatType.Speed;
                        isFlat = true;

                        break;
                    default:
                        return null;
                }

                if (isFlat)
                {
                    modStat.ValueType = ModValueType.Flat;
                    modStat.Value = System.Convert.ToInt64(source.PrimaryBonusValue) / flat;
                }
                else
                {
                    modStat.ValueType = ModValueType.Percentage;
                    modStat.Value = System.Convert.ToInt64(source.PrimaryBonusValue) / perc;
                }

                return modStat;
            }
        }

        public class SecondaryModConverter : ITypeConverter<SWGoHHelpRepository.Dto.Mod, List<ModStat>>
        {
            public List<ModStat> Convert(SWGoHHelpRepository.Dto.Mod source, List<ModStat> destination, ResolutionContext context)
            {
                List<ModStat> modsStat = new List<ModStat>();
                modsStat.Add(GetModStat(source.SecondaryType1, source.SecondaryValue1));
                modsStat.Add(GetModStat(source.SecondaryType2, source.SecondaryValue2));
                modsStat.Add(GetModStat(source.SecondaryType3, source.SecondaryValue3));
                modsStat.Add(GetModStat(source.SecondaryType4, source.SecondaryValue4));


                //if (isFlat)
                //{
                //    modStat.ValueType = ModValueType.Flat;
                //    modStat.Value = System.Convert.ToInt64(value) / flat;
                //}
                //else
                //{
                //    modStat.ValueType = ModValueType.Percentage;
                //    modStat.Value = System.Convert.ToInt64(source.PrimaryBonusValue) / perc;
                //}

                return modsStat;
            }
        }

        public static ModStat GetModStat(SecondaryType sType, string value)
        {
            var perc = 1000000.0;
            var flat = 100000000;
            bool isFlat = false;

            ModStat modStat = new ModStat();

            switch (sType)
            {
                case SecondaryType.CriticalChance:
                    modStat.StatType = ModStatType.CriticalChance;
                    break;
                case SecondaryType.Potency:
                    modStat.StatType = ModStatType.Potency;
                    break;
                case SecondaryType.SecondaryTypeDefense:
                    modStat.StatType = ModStatType.Defense;
                    break;
                case SecondaryType.SecondaryTypeHealth:
                    modStat.StatType = ModStatType.Health;
                    break;
                case SecondaryType.SecondaryTypeOffense:
                    modStat.StatType = ModStatType.Offense;
                    break;
                case SecondaryType.SecondaryTypeProtection:
                    modStat.StatType = ModStatType.Protection;
                    break;
                case SecondaryType.Tenacity:
                    modStat.StatType = ModStatType.Tenacity;
                    break;

                case SecondaryType.Defense:
                    modStat.StatType = ModStatType.Defense;
                    isFlat = true;
                    break;
                case SecondaryType.Health:
                    modStat.StatType = ModStatType.Health;
                    isFlat = true;
                    break;
                case SecondaryType.Offense:
                    modStat.StatType = ModStatType.Offense;
                    isFlat = true;
                    break;
                case SecondaryType.Protection:
                    modStat.StatType = ModStatType.Protection;
                    isFlat = true;
                    break;
                case SecondaryType.Speed:
                    modStat.StatType = ModStatType.Speed;
                    isFlat = true;
                    break;
                default:
                    break;
            }

            if (isFlat)
            {
                modStat.ValueType = ModValueType.Flat;
                modStat.Value = System.Convert.ToInt64(value) / flat;
            }
            else
            {
                modStat.ValueType = ModValueType.Percentage;
                modStat.Value = System.Convert.ToInt64(value) / perc;
            }

            return modStat;
        }

        public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return System.Convert.ToDateTime(source);
            }
        }

        private static DateTime ConvertFromUnixTimestamp(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(timestamp);
        }
    }
}
