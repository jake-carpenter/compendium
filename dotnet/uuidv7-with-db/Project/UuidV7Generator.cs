using UUIDNext;

namespace Project;

public class UuidV7Generator : BaseGenerator
{
    public override string Label => "UUIDv7 (Non-DB-Friendly)";

    protected override Guid CreateUuid()
    {
        return Uuid.NewSequential();
    }
}