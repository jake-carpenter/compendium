using System.ComponentModel;
using Spectre.Console.Cli;

namespace Project;

[Description("Run all test cases")]
public class RunAllCommand : AsyncCommand<Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        const string connectionString =
            "TrustServerCertificate=True;Data Source=localhost,9987;User Id=sa;Password=Password123;Database=master";

        BaseGenerator[] generators = [new GuidGenerator(), new UuidV7Generator(),new UuidV8Generator()];
        ISorter[] sorters = [new CSharpSorter(), new MsSqlSorter(connectionString)];

        await new TestRunner(generators, sorters).Execute(settings);
        return 0;
    }
}