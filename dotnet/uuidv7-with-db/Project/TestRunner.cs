namespace Project;

public class TestRunner(IEnumerable<ITestCase> testCases)
{
    public async Task Execute(int count)
    {
        foreach (var test in testCases)
        {
            await test.Repo.Prepare();
            var records = test.Generate(count).ToArray();
            await test.Repo.Write(records);

            var rows = (await test.Repo.Read()).ToArray();

            Console.WriteLine($"Test: {test.Label}");
            Console.WriteLine("{0, -8}{1, -38}{2}", "Index", "C# Sorted", "DB Sorted");
            PrintLine();

            for (var i = 0; i < rows.Length; i++)
            {
                Console.Write($"{i,5}   ");
                Console.Write(records[i].Uuid);
                Console.Write("  ");
                Console.WriteLine(rows[i].Uuid);
            }

            PrintLine();
            Console.WriteLine();
        }
    }

    private static void PrintLine()
    {
        Console.WriteLine("----------------------------------------------------------------------------------");
    }
}