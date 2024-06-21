namespace Project;

public abstract class BaseTestCase
{
    public abstract IRepo Repo { get; }
    public abstract string Label { get; }
    protected abstract Guid CreateUuid();

    public async Task<Record[]> Generate(int count, TimeSpan minDelay, TimeSpan maxDelay)
    {
        var min = (int)Math.Floor(minDelay.TotalMilliseconds);
        var max = (int)Math.Ceiling(maxDelay.TotalMilliseconds);
        var random = new Random();

        var records = new Record[count];
        for (var i = 0; i < records.Length; i++)
        {
            var id = i + 1;
            records[i] = new Record(id, CreateUuid());
            await Task.Delay(random.Next(min, max));
        }

        return records;
    }
}