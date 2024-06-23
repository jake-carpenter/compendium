using Spectre.Console;

namespace Project;

public class TestRunner(BaseGenerator[] generators, ISorter[] sorters)
{
    public async Task Execute(Settings settings)
    {
        var outputWriter = new ConsoleOutputWriter();
        var results = await GenerateAndSort(settings);
        await outputWriter.WriteOutput(results);
    }

    private async Task<Results> GenerateAndSort(Settings settings)
    {
        PrintStatusHeader(settings);
        return await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var results = new Results();

                var incrementValue = 100.0 / settings.Count;

                foreach (var generator in generators)
                {
                    var task = ctx.AddTask(FormatProgressLabel(generator.Label));
                    var records = new List<Record>(settings.Count);

                    await foreach (var record in generator.Generate(settings))
                    {
                        task.Increment(incrementValue);
                        records.Add(record);
                    }

                    var generatorRun = new GeneratorRun(generator, records, new List<SorterRun>(sorters.Length));
                    results.Add(generatorRun);

                    foreach (var sorter in sorters)
                    {
                        generatorRun.SorterRuns.Add(new SorterRun(sorter, await sorter.Sort(records)));
                    }
                }

                return results;
            });
    }

    private static void PrintStatusHeader(Settings settings)
    {
        const string template =
            "Generating [yellow]{0}[/] UUIDs each. Delay between [yellow]{1}ms[/] and [yellow]{2}ms[/]";

        var minMs = settings.MinimumDelay.Milliseconds.ToString("N0");
        var maxMs = settings.MaximumDelay.Milliseconds.ToString("N0");
        var msg = string.Format(template, settings.Count, minMs, maxMs);

        AnsiConsole.MarkupLine(msg);
    }

    private static string FormatProgressLabel(string label)
    {
        return $"{label,-25}";
    }
}
