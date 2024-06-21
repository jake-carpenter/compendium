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

        var mssqlRepo = new MsSqlRepo(connectionString);

        BaseTestCase[] testCases =
        [
            new GuidTestCase(mssqlRepo),
            new UuidV7TestCase(mssqlRepo),
            new UuidV8TestCase(mssqlRepo)
        ];

        var runner = new TestRunner(testCases);

        await runner.Execute(settings);
        return 0;
    }
}