namespace Project;

public class CSharpSorter : ISorter
{
    public string Label => "C# Sorted";

    public UuidSection[] HighlightedSections =>
        [UuidSection.TimeLow, UuidSection.TimeMid, UuidSection.TimeHighAndVersion];

    public Task<Record[]> Sort(IEnumerable<Record> records)
    {
        return Task.FromResult(records.OrderBy(x => x.Uuid).ToArray());
    }
}