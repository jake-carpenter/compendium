namespace Project;

public interface IRepo
{
    Task Prepare();
    Task Write(IEnumerable<Record> records);
    Task<IEnumerable<Record>> Read();
}