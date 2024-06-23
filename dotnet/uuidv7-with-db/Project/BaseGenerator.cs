namespace Project;

public abstract class BaseGenerator
{
    public abstract string Label { get; }
    protected abstract Guid CreateUuid();

    public async Task<Record[]> Generate(Settings settings)
    {
        var min = (int)Math.Floor(settings.MinimumDelay.TotalMilliseconds);
        var max = (int)Math.Ceiling(settings.MaximumDelay.TotalMilliseconds);
        var random = new Random();

        var records = new Record[settings.Count];
        for (var i = 0; i < records.Length; i++)
        {
            var id = i + 1;
            records[i] = new Record(id, CreateUuid());
            await Task.Delay(random.Next(min, max));
        }

        return records;
    }
}