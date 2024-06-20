namespace Project;

public interface ITestCase
{
    IRepo Repo { get; }
    string Label { get; }
    IEnumerable<Record> Generate(int count);
}