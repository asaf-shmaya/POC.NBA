namespace NBA.Models
{

    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Internal
    {
        [JsonProperty("pubDateTime")]
        public string PubDateTime { get; set; }

        [JsonProperty("igorPath")]
        public string IgorPath { get; set; }

        [JsonProperty("xslt")]
        public string Xslt { get; set; }

        [JsonProperty("xsltForceRecompile")]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public bool XsltForceRecompile { get; set; }

        [JsonProperty("xsltInCache")]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public bool XsltInCache { get; set; }

        [JsonProperty("xsltCompileTimeMillis")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long XsltCompileTimeMillis { get; set; }

        [JsonProperty("xsltTransformTimeMillis")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long XsltTransformTimeMillis { get; set; }

        [JsonProperty("consolidatedDomKey")]
        public string ConsolidatedDomKey { get; set; }

        [JsonProperty("endToEndTimeMillis")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long EndToEndTimeMillis { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                HeightFeetUnionConverter.Singleton,
                HeightFeetEnumConverter.Singleton,
                HeightMetersConverter.Singleton,
                JerseyUnionConverter.Singleton,
                JerseyEnumConverter.Singleton,
                PosConverter.Singleton,
                PosFullConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class PurpleParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
    }

    internal class FluffyParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
    }

    internal class HeightFeetUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(HeightFeetUnion) || t == typeof(HeightFeetUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    if (stringValue == "-")
                    {
                        return new HeightFeetUnion { Enum = HeightFeetEnum.Empty };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new HeightFeetUnion { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type HeightFeetUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (HeightFeetUnion)untypedValue;
            if (value.Enum != null)
            {
                if (value.Enum == HeightFeetEnum.Empty)
                {
                    serializer.Serialize(writer, "-");
                    return;
                }
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type HeightFeetUnion");
        }

        public static readonly HeightFeetUnionConverter Singleton = new HeightFeetUnionConverter();
    }

    internal class HeightFeetEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(HeightFeetEnum) || t == typeof(HeightFeetEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "-")
            {
                return HeightFeetEnum.Empty;
            }
            throw new Exception("Cannot unmarshal type HeightFeetEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (HeightFeetEnum)untypedValue;
            if (value == HeightFeetEnum.Empty)
            {
                serializer.Serialize(writer, "-");
                return;
            }
            throw new Exception("Cannot marshal type HeightFeetEnum");
        }

        public static readonly HeightFeetEnumConverter Singleton = new HeightFeetEnumConverter();
    }

    internal class HeightMetersConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(HeightMeters) || t == typeof(HeightMeters?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return HeightMeters.Empty;
                case "1.75":
                    return HeightMeters.The175;
                case "1.78":
                    return HeightMeters.The178;
                case "1.8":
                    return HeightMeters.The18;
                case "1.83":
                    return HeightMeters.The183;
                case "1.85":
                    return HeightMeters.The185;
                case "1.88":
                    return HeightMeters.The188;
                case "1.9":
                    return HeightMeters.The19;
                case "1.93":
                    return HeightMeters.The193;
                case "1.96":
                    return HeightMeters.The196;
                case "1.98":
                    return HeightMeters.The198;
                case "2.01":
                    return HeightMeters.The201;
                case "2.03":
                    return HeightMeters.The203;
                case "2.06":
                    return HeightMeters.The206;
                case "2.08":
                    return HeightMeters.The208;
                case "2.11":
                    return HeightMeters.The211;
                case "2.13":
                    return HeightMeters.The213;
                case "2.16":
                    return HeightMeters.The216;
                case "2.18":
                    return HeightMeters.The218;
                case "2.21":
                    return HeightMeters.The221;
            }
            throw new Exception("Cannot unmarshal type HeightMeters");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (HeightMeters)untypedValue;
            switch (value)
            {
                case HeightMeters.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case HeightMeters.The175:
                    serializer.Serialize(writer, "1.75");
                    return;
                case HeightMeters.The178:
                    serializer.Serialize(writer, "1.78");
                    return;
                case HeightMeters.The18:
                    serializer.Serialize(writer, "1.8");
                    return;
                case HeightMeters.The183:
                    serializer.Serialize(writer, "1.83");
                    return;
                case HeightMeters.The185:
                    serializer.Serialize(writer, "1.85");
                    return;
                case HeightMeters.The188:
                    serializer.Serialize(writer, "1.88");
                    return;
                case HeightMeters.The19:
                    serializer.Serialize(writer, "1.9");
                    return;
                case HeightMeters.The193:
                    serializer.Serialize(writer, "1.93");
                    return;
                case HeightMeters.The196:
                    serializer.Serialize(writer, "1.96");
                    return;
                case HeightMeters.The198:
                    serializer.Serialize(writer, "1.98");
                    return;
                case HeightMeters.The201:
                    serializer.Serialize(writer, "2.01");
                    return;
                case HeightMeters.The203:
                    serializer.Serialize(writer, "2.03");
                    return;
                case HeightMeters.The206:
                    serializer.Serialize(writer, "2.06");
                    return;
                case HeightMeters.The208:
                    serializer.Serialize(writer, "2.08");
                    return;
                case HeightMeters.The211:
                    serializer.Serialize(writer, "2.11");
                    return;
                case HeightMeters.The213:
                    serializer.Serialize(writer, "2.13");
                    return;
                case HeightMeters.The216:
                    serializer.Serialize(writer, "2.16");
                    return;
                case HeightMeters.The218:
                    serializer.Serialize(writer, "2.18");
                    return;
                case HeightMeters.The221:
                    serializer.Serialize(writer, "2.21");
                    return;
            }
            throw new Exception("Cannot marshal type HeightMeters");
        }

        public static readonly HeightMetersConverter Singleton = new HeightMetersConverter();
    }

    internal class JerseyUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(JerseyUnion) || t == typeof(JerseyUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case "":
                            return new JerseyUnion { Enum = JerseyEnum.Empty };
                        case "00":
                            return new JerseyUnion { Enum = JerseyEnum.The00 };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new JerseyUnion { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type JerseyUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (JerseyUnion)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case JerseyEnum.Empty:
                        serializer.Serialize(writer, "");
                        return;
                    case JerseyEnum.The00:
                        serializer.Serialize(writer, "00");
                        return;
                }
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type JerseyUnion");
        }

        public static readonly JerseyUnionConverter Singleton = new JerseyUnionConverter();
    }

    internal class JerseyEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(JerseyEnum) || t == typeof(JerseyEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return JerseyEnum.Empty;
                case "00":
                    return JerseyEnum.The00;
            }
            throw new Exception("Cannot unmarshal type JerseyEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (JerseyEnum)untypedValue;
            switch (value)
            {
                case JerseyEnum.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case JerseyEnum.The00:
                    serializer.Serialize(writer, "00");
                    return;
            }
            throw new Exception("Cannot marshal type JerseyEnum");
        }

        public static readonly JerseyEnumConverter Singleton = new JerseyEnumConverter();
    }

    internal class PosConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Pos) || t == typeof(Pos?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return Pos.Empty;
                case "C":
                    return Pos.C;
                case "C-F":
                    return Pos.CF;
                case "F":
                    return Pos.F;
                case "F-C":
                    return Pos.FC;
                case "F-G":
                    return Pos.FG;
                case "G":
                    return Pos.G;
                case "G-F":
                    return Pos.GF;
            }
            throw new Exception("Cannot unmarshal type Pos");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Pos)untypedValue;
            switch (value)
            {
                case Pos.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case Pos.C:
                    serializer.Serialize(writer, "C");
                    return;
                case Pos.CF:
                    serializer.Serialize(writer, "C-F");
                    return;
                case Pos.F:
                    serializer.Serialize(writer, "F");
                    return;
                case Pos.FC:
                    serializer.Serialize(writer, "F-C");
                    return;
                case Pos.FG:
                    serializer.Serialize(writer, "F-G");
                    return;
                case Pos.G:
                    serializer.Serialize(writer, "G");
                    return;
                case Pos.GF:
                    serializer.Serialize(writer, "G-F");
                    return;
            }
            throw new Exception("Cannot marshal type Pos");
        }

        public static readonly PosConverter Singleton = new PosConverter();
    }

    internal class PosFullConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PosFull) || t == typeof(PosFull?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return PosFull.Empty;
                case "Center":
                    return PosFull.Center;
                case "Center-Forward":
                    return PosFull.CenterForward;
                case "Forward":
                    return PosFull.Forward;
                case "Forward-Center":
                    return PosFull.ForwardCenter;
                case "Forward-Guard":
                    return PosFull.ForwardGuard;
                case "Guard":
                    return PosFull.Guard;
                case "Guard-Forward":
                    return PosFull.GuardForward;
            }
            throw new Exception("Cannot unmarshal type PosFull");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PosFull)untypedValue;
            switch (value)
            {
                case PosFull.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case PosFull.Center:
                    serializer.Serialize(writer, "Center");
                    return;
                case PosFull.CenterForward:
                    serializer.Serialize(writer, "Center-Forward");
                    return;
                case PosFull.Forward:
                    serializer.Serialize(writer, "Forward");
                    return;
                case PosFull.ForwardCenter:
                    serializer.Serialize(writer, "Forward-Center");
                    return;
                case PosFull.ForwardGuard:
                    serializer.Serialize(writer, "Forward-Guard");
                    return;
                case PosFull.Guard:
                    serializer.Serialize(writer, "Guard");
                    return;
                case PosFull.GuardForward:
                    serializer.Serialize(writer, "Guard-Forward");
                    return;
            }
            throw new Exception("Cannot marshal type PosFull");
        }

        public static readonly PosFullConverter Singleton = new PosFullConverter();
    }

    internal class ConfNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ConfName) || t == typeof(ConfName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return ConfName.Empty;
                case "East":
                    return ConfName.East;
                case "Intl":
                    return ConfName.Intl;
                case "Sacramento":
                    return ConfName.Sacramento;
                case "Utah":
                    return ConfName.Utah;
                case "West":
                    return ConfName.West;
                case "summer":
                    return ConfName.Summer;
            }
            throw new Exception("Cannot unmarshal type ConfName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ConfName)untypedValue;
            switch (value)
            {
                case ConfName.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case ConfName.East:
                    serializer.Serialize(writer, "East");
                    return;
                case ConfName.Intl:
                    serializer.Serialize(writer, "Intl");
                    return;
                case ConfName.Sacramento:
                    serializer.Serialize(writer, "Sacramento");
                    return;
                case ConfName.Utah:
                    serializer.Serialize(writer, "Utah");
                    return;
                case ConfName.West:
                    serializer.Serialize(writer, "West");
                    return;
                case ConfName.Summer:
                    serializer.Serialize(writer, "summer");
                    return;
            }
            throw new Exception("Cannot marshal type ConfName");
        }

        public static readonly ConfNameConverter Singleton = new ConfNameConverter();
    }

    internal class DivNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DivName) || t == typeof(DivName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return DivName.Empty;
                case "Atlantic":
                    return DivName.Atlantic;
                case "Central":
                    return DivName.Central;
                case "East":
                    return DivName.East;
                case "Northwest":
                    return DivName.Northwest;
                case "Pacific":
                    return DivName.Pacific;
                case "Southeast":
                    return DivName.Southeast;
                case "Southwest":
                    return DivName.Southwest;
                case "West":
                    return DivName.West;
            }
            throw new Exception("Cannot unmarshal type DivName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DivName)untypedValue;
            switch (value)
            {
                case DivName.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case DivName.Atlantic:
                    serializer.Serialize(writer, "Atlantic");
                    return;
                case DivName.Central:
                    serializer.Serialize(writer, "Central");
                    return;
                case DivName.East:
                    serializer.Serialize(writer, "East");
                    return;
                case DivName.Northwest:
                    serializer.Serialize(writer, "Northwest");
                    return;
                case DivName.Pacific:
                    serializer.Serialize(writer, "Pacific");
                    return;
                case DivName.Southeast:
                    serializer.Serialize(writer, "Southeast");
                    return;
                case DivName.Southwest:
                    serializer.Serialize(writer, "Southwest");
                    return;
                case DivName.West:
                    serializer.Serialize(writer, "West");
                    return;
            }
            throw new Exception("Cannot marshal type DivName");
        }

        public static readonly DivNameConverter Singleton = new DivNameConverter();
    }
}
