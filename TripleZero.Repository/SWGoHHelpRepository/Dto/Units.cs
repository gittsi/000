﻿namespace TripleZero.Repository.SWGoHHelpRepository.Dto
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Units
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alignment")]
        public long Alignment { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("crew")]
        public List<Crew> Crew { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }

    public enum TypeEnum { Char, Ship };

    public partial class Units
    {
        public static Dictionary<string, Units> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Units>>(json, ConverterUnits.Settings);
    }

    public static class SerializeUnits
    {
        public static string ToJson(this Dictionary<string, Units> self) => JsonConvert.SerializeObject(self, ConverterUnits.Settings);
    }

    internal static class ConverterUnits
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "char":
                    return TypeEnum.Char;
                case "ship":
                    return TypeEnum.Ship;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Char:
                    serializer.Serialize(writer, "char");
                    return;
                case TypeEnum.Ship:
                    serializer.Serialize(writer, "ship");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
