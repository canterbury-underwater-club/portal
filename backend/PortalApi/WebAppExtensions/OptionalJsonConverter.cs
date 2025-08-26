using System.Text.Json;
using System.Text.Json.Serialization;

namespace CanterburyUnderwater.PortalApi.WebAppExtensions;

public readonly struct Optional<T>(T? value)
{
    public bool HasValue { get; } = true;
    public T? Value { get; } = value;

    public static implicit operator Optional<T>(T? value)
    {
        return new Optional<T>(value);
    }
}

public sealed class OptionalConverter<T> : JsonConverter<Optional<T>>
{
    public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // This is only called if the property is PRESENT in JSON.
        var value = JsonSerializer.Deserialize<T>(ref reader, options);
        return new Optional<T>(value);
    }

    public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            writer.WriteNullValue();
            return;
        }

        JsonSerializer.Serialize(writer, value.Value, options);
    }
}

public sealed class OptionalConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var inner = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(OptionalConverter<>).MakeGenericType(inner);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}