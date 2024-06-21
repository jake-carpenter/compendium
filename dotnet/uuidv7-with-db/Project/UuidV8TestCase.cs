using UUIDNext;

namespace Project;

public class UuidV8TestCase(MsSqlRepo repo) : BaseTestCase
{
    public override IRepo Repo => repo;
    public override string Label => "UUIDv7 (DB-Friendly)";
    
    protected override Guid CreateUuid()
    {
        return Uuid.NewDatabaseFriendly(Database.SqlServer);
    }
}