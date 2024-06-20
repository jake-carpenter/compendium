namespace Project;

public record Record(int Id, string Uuid)
{
    public Record(int id, Guid uuid) : this(id, uuid.ToString())
    {
    }
}