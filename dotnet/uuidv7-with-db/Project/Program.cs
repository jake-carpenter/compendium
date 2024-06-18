using Dapper;
using Microsoft.Data.SqlClient;

const int count = 10;
const string connectionString =
    "TrustServerCertificate=True;Data Source=localhost,9987;User Id=sa;Password=Password123;Database=master";

Console.WriteLine("Creating GUIDs...");
var records = Enumerable
    .Range(1, count)
    .Select(id => new Record(id, Guid.NewGuid()))
    .ToArray();

Console.WriteLine("Connecting to SQL Server...");
await using var con = new SqlConnection(connectionString);
await con.OpenAsync();

Console.WriteLine("Writing to SQL Server...");
await WriteToSqlServer(con);

Console.WriteLine("Querying back from SQL Server...");
var rows = (await ReadFromSqlServer(con)).ToArray();

Console.WriteLine("{0, -8}{1, -38}{2}", "Index", "C# Sorted GUID", "MSSQL Sorted GUID");
Console.WriteLine("----------------------------------------------------------------------------------");
for (var i = 0; i < rows.Length; i++)
{
    Console.Write($"{i,5}   ");
    Console.Write(records[i].Guid);
    Console.Write("  ");
    Console.WriteLine(rows[i].Guid);
}

return 0;

async Task WriteToSqlServer(SqlConnection connection)
{
    await connection.ExecuteAsync("if object_id('Guids') is null create table Guids (Id int, Guid uniqueidentifier)");
    await connection.ExecuteAsync("truncate table Guids");

    foreach (var record in records)
    {
        await connection.ExecuteAsync("insert into Guids (Id, Guid) values (@id, @guid)", record);
    }
}

async Task<IEnumerable<Record>> ReadFromSqlServer(SqlConnection connection)
{
    return await connection.QueryAsync<Record>("select Id, Guid from Guids order by Guid");
}

public record Record
{
    public Record(int id, Guid guid)
    {
        Id = id;
        Guid = guid;
    }

    public int Id { get; init; }
    public Guid Guid { get; init; }
}