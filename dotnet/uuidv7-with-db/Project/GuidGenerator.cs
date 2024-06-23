namespace Project;

public class GuidGenerator : BaseGenerator
{
    public override string Label => "GUID (UUIDv4)";

    protected override Guid CreateUuid()
    {
        return Guid.NewGuid();
    }
}
