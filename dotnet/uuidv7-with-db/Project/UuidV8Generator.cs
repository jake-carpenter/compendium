using UUIDNext;

namespace Project;

public class UuidV8Generator : BaseGenerator
{
    public override string Label => "UUIDv8 (DB-Friendly)";
    
    protected override Guid CreateUuid()
    {
        return Uuid.NewDatabaseFriendly(Database.SqlServer);
    }
}