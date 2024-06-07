using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Benchmark;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run(typeof(Program).Assembly);

public class DecimalJsonSerialization
{
    private Default _defaultConverter = null!;
    private Attempt1 _attempt1Converter = null!;
    private Attempt2 _attempt2Converter = null!;
    private JsonSerializerOptions _options = null!;

    public IEnumerable<decimal> TestValues() => [1m, 1.0m, 1.00m, 1.1m, 1.10m, 1.100m, 1.101m, 1.110m];
    
    [ParamsSource(nameof(TestValues))]
    public decimal Value { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _defaultConverter = new Default();
        _attempt1Converter = new Attempt1();
        _attempt2Converter = new Attempt2();
        _options = JsonSerializerOptions.Default;
    }
    
    
    [Benchmark(Description = "Default", Baseline = true)]
    public string Default()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        _defaultConverter.Write(writer, Value, _options);
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }
    
    [Benchmark(Description = "Attempt1", Baseline = false)]
    public string Attempt1()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        _attempt1Converter.Write(writer, Value, _options);
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }
    
    [Benchmark(Description = "Attempt2", Baseline = false)]
    public string Attempt2()
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        _attempt2Converter.Write(writer, Value, _options);
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}