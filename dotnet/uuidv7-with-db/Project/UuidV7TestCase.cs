using UUIDNext;

namespace Project;

public class UuidV7TestCase(MsSqlRepo repo) : BaseTestCase
{
    public override IRepo Repo => repo;
    public override string Label => "UUIDv7 (Non-DB-Friendly)";

    protected override Guid CreateUuid()
    {
        return Uuid.NewSequential();
    }
}