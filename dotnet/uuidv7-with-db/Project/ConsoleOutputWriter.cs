using System.Text;
using Spectre.Console;

namespace Project;

public class ConsoleOutputWriter
{
    private struct Color
    {
        public const string PrimarySort = "blue";
        public const string SecondarySort = "green";
        public const string TertiarySort = "teal";
        public const string Uuid = "grey";
        public const string GeneratorLabel = "yellow";
        public const string Table = "grey11";
    }

    public Task WriteOutput(Results results)
    {
        var sorters = results.First().SorterRuns.Select(x => x.Sorter).DistinctBy(x => x.Label).ToArray();

        PrintEmptyLines(2);

        foreach (var generatorRun in results)
        {
            PrintHeader(generatorRun.Generator.Label);
            AnsiConsole.MarkupLine(GetColumnHeaders(sorters));
            PrintLine(sorters.Length);

            for (var i = 0; i < generatorRun.Generated.Count; i++)
            {
                var generatedUuid = generatorRun.Generated[i].Uuid;
                AnsiConsole.Markup($"[{Color.Uuid}]{i,5}[/]  {generatedUuid}  ");

                PrintEachSorterUuid(generatorRun, i);
                PrintEmptyLines();
            }

            PrintLine(sorters.Length);
            PrintEmptyLines();
        }

        return Task.CompletedTask;
    }

    private static void PrintHeader(string label)
    {
        AnsiConsole.Markup($"[{Color.Table}]Generator:[/]");
        AnsiConsole.Markup($"   [{Color.GeneratorLabel}]{label}[/]");
        AnsiConsole.Markup($"   [{Color.Table}]Legend:[/] [{Color.PrimarySort}]Primary sort[/]");
        AnsiConsole.Markup($"   [{Color.SecondarySort}]Secondary sort[/]");
        AnsiConsole.Markup($"   [{Color.TertiarySort}]Tertiary sort[/]");
        AnsiConsole.MarkupLine("");
    }

    private static void PrintEachSorterUuid(GeneratorRun generatorRun, int index)
    {
        foreach (var run in generatorRun.SorterRuns)
        {
            var uuid = run.Records[index].Uuid.ToString();

            for (var uuidIndex = 0; uuidIndex < uuid.Length; uuidIndex++)
            {
                var color = GetHighlightColor(run.Sorter.HighlightedSections.GetSortPriority(uuidIndex));
                AnsiConsole.Markup($"[{color}]{uuid[uuidIndex]}[/]");
            }

            AnsiConsole.Write("  ");
        }
    }

    private static string GetHighlightColor(SortPriority priority) => priority switch
    {
        SortPriority.Primary => Color.PrimarySort,
        SortPriority.Secondary => Color.SecondarySort,
        SortPriority.Tertiary => Color.TertiarySort,
        _ => string.Empty
    };

    private static void PrintEmptyLines(int count = 1)
    {
        AnsiConsole.WriteLine(new string('\n', count - 1));
    }

    private static void PrintLine(int sorterCount)
    {
        AnsiConsole.Markup($"[{Color.Table}]{new string('-', 5)}[/]");

        var line = new string('-', 36);

        for (var i = 0; i < sorterCount + 1; i++)
        {
            AnsiConsole.Markup($"[{Color.Table}]--{line}[/]");
        }

        AnsiConsole.Write("\n");
    }

    private static string GetColumnHeaders(IEnumerable<ISorter> sorters)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"[{Color.Table}]");
        stringBuilder.Append("Index".PadRight(7));
        stringBuilder.Append("As Generated".PadRight(38));

        foreach (var sorter in sorters)
        {
            stringBuilder.Append(sorter.Label.PadRight(38));
        }

        stringBuilder.Append("[/]");
        return stringBuilder.ToString();
    }
}