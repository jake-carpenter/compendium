using Dapper;
using Microsoft.Data.SqlClient;

namespace Project;

public class MsSqlSorter(string connectionString) : ISorter
{
    public string Label => "MSSQL Sorted";

    public UuidSection[] HighlightedSections =>
        [UuidSection.Node, UuidSection.ClockSeqAndReserved, UuidSection.ClockSeqLow];

    public async Task<Record[]> Sort(IEnumerable<Record> records)
    {
        await Prepare();
        await Write(records);
        var results = await Read();
        return results.ToArray();
    }

    private async Task Prepare()
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(
            "if object_id('Uuids') is null create table Uuids (Id int, Uuid uniqueidentifier)");

        await connection.ExecuteAsync("truncate table Uuids");
    }

    private async Task Write(IEnumerable<Record> records)
    {
        await using var connection = new SqlConnection(connectionString);

        // Meh bulk insert.
        foreach (var record in records)
        {
            await connection.ExecuteAsync("insert into Uuids (Id, Uuid) values (@id, @uuid)", record);
        }
    }

    private async Task<IEnumerable<Record>> Read()
    {
        await using var connection = new SqlConnection(connectionString);

        var results = await connection.QueryAsync<(int, Guid)>("select Id, Uuid from Uuids order by Uuid");

        return results.Select(x => new Record(x.Item1, x.Item2));
    }
}