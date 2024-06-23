using System.Text;
using Spectre.Console;

namespace Project;

public class ConsoleOutputWriter
{
    public Task WriteOutput(Results results)
    {
        var sorters = results.First().SorterRuns.Select(x => x.Sorter).DistinctBy(x => x.Label).ToArray();

        PrintEmptyLines(2);

        foreach (var generatorRun in results)
        {
            AnsiConsole.MarkupLine($"[blue]Generator[/]: [yellow]{generatorRun.Generator.Label}[/]");
            AnsiConsole.MarkupLine(GetColumnHeaders(sorters));
            PrintLine();

            for (var i = 0; i < generatorRun.Generated.Length; i++)
            {
                AnsiConsole.Markup($"[grey]{i,5}[/]  {generatorRun.Generated[i].Uuid}  ");

                for (var j = 0; j < generatorRun.SorterRuns.Count; j++)
                {
                    AnsiConsole.Markup($"{generatorRun.SorterRuns[j].Records[i].Uuid}  ");
                }

                PrintEmptyLines();
            }

            PrintLine();
            PrintEmptyLines();
        }

        return Task.CompletedTask;
    }

    private static void PrintEmptyLines(int count = 1) => AnsiConsole.WriteLine(new string('\n', count - 1));
    private static void PrintLine() => AnsiConsole.MarkupLine($"[grey11]{GetLineString()}[/]");

    private static string GetColumnHeaders(IEnumerable<ISorter> sorters)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("[grey11]");
        stringBuilder.Append("Index".PadRight(7));
        stringBuilder.Append("As Generated".PadRight(38));

        foreach (var sorter in sorters)
        {
            stringBuilder.Append(sorter.Label.PadRight(38));
        }

        stringBuilder.Append("[/]");
        return stringBuilder.ToString();
    }

    private static string GetLineString() => new('-', 120);
}