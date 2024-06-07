using System.Text.Json;
using System.Text.Json.Serialization;

namespace Benchmark;

public class Default : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}

/// <summary>
/// G format specifier
/// </summary>
public class Attempt1 : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        var stringValue = value.ToString("G0");
        writer.WriteNumberValue(decimal.Parse(stringValue));
    }
}

/// <summary>
/// Normalizer
/// </summary>
public class Attempt2 : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        var min = value / 1.0000000000000000000000000000m;
        writer.WriteNumberValue(min);
    }
}