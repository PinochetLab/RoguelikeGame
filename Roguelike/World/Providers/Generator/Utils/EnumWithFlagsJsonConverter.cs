using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Roguelike.World.Providers.Generator.Utils;

/// <summary>
/// JSON serialization for `[Flags]` based `enum's` as `string[]`
/// </summary>
/// <see href="https://github.com/dotnet/runtime/issues/31081#issuecomment-848697673">based on this model</see>
public class EnumWithFlagsJsonConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    private readonly Dictionary<TEnum, string> enumToString = new();
    private readonly Dictionary<string, TEnum> stringToEnum = new();
    private readonly TEnum[] values;

    public EnumWithFlagsJsonConverter()
    {
        var type = typeof(TEnum);
        values = Enum.GetValues<TEnum>();

        foreach (var value in values)
        {
            var enumMember = type.GetMember(value.ToString())[0];
            var attr = enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                .Cast<EnumMemberAttribute>()
                .FirstOrDefault();

            stringToEnum.Add(value.ToString(), value);

            if (attr?.Value != null)
            {
                enumToString.Add(value, attr.Value);
                stringToEnum.Add(attr.Value, value);
            }
            else
            {
                enumToString.Add(value, value.ToString());
            }
        }
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return default(TEnum);
            case JsonTokenType.StartArray:
                var ret = default(TEnum);
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;
                    var stringValue = reader.GetString();
                    if (stringToEnum.TryGetValue(stringValue, out var _enumValue))
                    {
                        ret = Or(ret, _enumValue);
                    }
                }

                return ret;
            default:
                throw new JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var flag in values)
        {
            if (!value.HasFlag(flag)) continue;

            var v = Convert.ToInt32(flag);
            if (v == 0)
            {
                // handle "0" case which HasFlag matches to all values
                // --> only write "0" case if it is the only value present
                if (value.Equals(flag))
                {
                    writer.WriteStringValue(enumToString[flag]);
                }
            }
            else
            {
                writer.WriteStringValue(enumToString[flag]);
            }
        }

        writer.WriteEndArray();
    }

    /// <summary>
    /// Combine two enum flag values into single enum value.
    /// </summary>
    // <see href="https://stackoverflow.com/a/24172851/5219886">based on this SO</see>
    private static TEnum Or(TEnum a, TEnum b) =>
        Enum.GetUnderlyingType(a.GetType()) != typeof(ulong)
            ? (TEnum)Enum.ToObject(a.GetType(), Convert.ToInt64(a) | Convert.ToInt64(b))
            : (TEnum)Enum.ToObject(a.GetType(), Convert.ToUInt64(a) | Convert.ToUInt64(b));
}