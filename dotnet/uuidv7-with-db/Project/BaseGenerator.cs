namespace Project;

public abstract class BaseGenerator
{
    public abstract string Label { get; }
    protected abstract Guid CreateUuid();

    public async IAsyncEnumerable<Record> Generate(Settings settings)
    {
        var min = (int)Math.Floor(settings.MinimumDelay.TotalMilliseconds);
        var max = (int)Math.Ceiling(settings.MaximumDelay.TotalMilliseconds);
        var random = new Random();

        for (var i = 0; i < settings.Count; i++)
        {
            var id = i + 1;
            var record = new Record(id, CreateUuid());
            await Task.Delay(random.Next(min, max));
            yield return record;
        }
    }
}