namespace Project;

public class GuidTestCase(MsSqlRepo repo) : ITestCase
{
    public string Label => "GUID (UUIDv4)";

    public IRepo Repo => repo;

    public IEnumerable<Record> Generate(int count)
    {
        return Enumerable
            .Range(1, count)
            .Select(_ => Guid.NewGuid())
            .OrderBy(uuid => uuid)
            .Select((uuid, id) => new Record(id, uuid))
            .ToArray();
    }
}