namespace Project;

public interface ISorter
{
    string Label { get; }
    Task<Record[]> Sort(IEnumerable<Record> records);
}