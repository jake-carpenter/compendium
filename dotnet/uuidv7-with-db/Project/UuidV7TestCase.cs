using UUIDNext;

namespace Project;

public class UuidV7TestCase(MsSqlRepo repo) : ITestCase
{
    public IRepo Repo => repo;
    public string Label => "UUIDv7 (Non-DB-Friendly)";

    public IEnumerable<Record> Generate(int count)
    {
        return Enumerable
            .Range(1, count)
            .Select(_ => Uuid.NewRandom())
            .OrderBy(uuid => uuid)
            .Select((uuid, id) => new Record(id, uuid))
            .ToArray();
    }
}

public class UuidV7DbFriendlyTestCase(MsSqlRepo repo) : ITestCase
{
    public IRepo Repo => repo;
    public string Label => "UUIDv7 (DB-Friendly)";

    public IEnumerable<Record> Generate(int count)
    {
        return Enumerable
            .Range(1, count)
            .Select(_ => Uuid.NewDatabaseFriendly(Database.SqlServer))
            .OrderBy(uuid => uuid)
            .Select((uuid, id) => new Record(id, uuid))
            .ToArray();
    }
}