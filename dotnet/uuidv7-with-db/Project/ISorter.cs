namespace Project;

public interface ISorter
{
    string Label { get; }
    UuidSection[] HighlightedSections { get; }
    Task<Record[]> Sort(IEnumerable<Record> records);
}