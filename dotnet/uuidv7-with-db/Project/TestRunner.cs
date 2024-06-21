namespace Project;

public class TestRunner(IEnumerable<BaseTestCase> testCases)
{
    public async Task Execute(int count, TimeSpan minDelay, TimeSpan maxDelay)
    {
        foreach (var test in testCases)
        {
            await test.Repo.Prepare();
            var records = await test.Generate(count, minDelay, maxDelay);
            var sorted = records.Select(x => Guid.Parse(x.Uuid)).OrderBy(x=>x).ToArray();
            await test.Repo.Write(records);

            var rows = (await test.Repo.Read()).ToArray();

            Console.WriteLine($"Test: {test.Label}");
            Console.WriteLine("{0, -8}{1, -38}{2, -38}{3}", "Index", "As Generated", "C# Sorted", "SQL Server Sorted");
            PrintLine();

            for (var i = 0; i < rows.Length; i++)
            {
                Console.Write($"{i,5}   ");
                Console.Write(records[i].Uuid);
                Console.Write("  ");
                Console.Write(sorted[i]);
                Console.Write("  ");
                Console.WriteLine(rows[i].Uuid);
            }

            PrintLine();
            Console.WriteLine();
        }
    }

    private static void PrintLine()
    {
        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
    }
}