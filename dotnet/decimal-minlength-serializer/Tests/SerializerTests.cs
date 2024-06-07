using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Benchmark;

namespace Tests;

public class Theories : TheoryData<string, string>
{
    public Theories()
    {
        Add("1", "1");
        Add("1.0", "1");
        Add("1.00", "1");
        Add("1.000", "1");
        Add("1.10", "1.1");
        Add("1.100", "1.1");
        Add("1.110", "1.11");
        Add("1.01", "1.01");
        Add("1.00005", "1.00005");
    }
}

public class UnitTest1
{
    [Theory]
    [ClassData(typeof(Theories))]
    public void Attempt1(string value, string expected)
    {
        var sut = new Attempt1();
        var actual = ExerciseSut(sut, decimal.Parse(value));
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Theories))]
    public void Attempt2(string value, string expected)
    {
        var sut = new Attempt2();
        var actual = ExerciseSut(sut, decimal.Parse(value));
        Assert.Equal(expected, actual);
    }

    private static string ExerciseSut(JsonConverter<decimal> sut, decimal value)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        var options = JsonSerializerOptions.Default;

        sut.Write(writer, value, options);
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}