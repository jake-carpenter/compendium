namespace Project;

public class GuidTestCase(MsSqlRepo repo) : BaseTestCase
{
    public override string Label => "GUID (UUIDv4)";
    public override IRepo Repo => repo;

    protected override Guid CreateUuid()
    {
        return Guid.NewGuid();
    }
}
