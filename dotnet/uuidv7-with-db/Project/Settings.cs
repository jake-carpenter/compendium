using System.ComponentModel;
using Spectre.Console.Cli;

namespace Project;

public class Settings : CommandSettings
{
    [Description("Number of UUIDs to generate for each test case")]
    [CommandOption("-c|--count <COUNT>")]
    [DefaultValue(10)]
    public required int Count { get; init; }

    [Description("Minimum delay timecode between UUID generation")]
    [CommandOption("-m|--min-delay <TIMECODE>")]
    [DefaultValue("00:00:00.100")]
    public required TimeSpan MinimumDelay { get; init; }

    [Description("Maximum delay timecode between UUID generation")]
    [CommandOption("-M|--max-delay <TIMECODE>")]
    [DefaultValue("00:00:00.500")]
    public required TimeSpan MaximumDelay { get; init; }
}