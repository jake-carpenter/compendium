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
        var results = new Results();

        foreach (var generator in generators)
        {
            AnsiConsole.MarkupLine($"Generating {settings.Count} {generator.Label} records...");
            var records = await generator.Generate(settings);
            var generatorRun = new GeneratorRun(generator, records, new List<SorterRun>(sorters.Length));
            results.Add(generatorRun);

            foreach (var sorter in sorters)
            {
                AnsiConsole.MarkupLine($"\tSorting using {sorter.Label}...");
                generatorRun.SorterRuns.Add(new SorterRun(sorter, await sorter.Sort(records)));
            }
        }

        return results;
    }
}