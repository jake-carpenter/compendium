using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Project;

public class PostgresSorter(string connectionString) : ISorter
{
    public string Label => "Postgres Sorted";

    public UuidSection[] HighlightedSections =>
        [UuidSection.TimeLow, UuidSection.TimeMid, UuidSection.TimeHighAndVersion];

    public async Task<Record[]> Sort(IEnumerable<Record> records)
    {
        await Prepare();
        await Write(records);
        var results = await Read();
        return results.ToArray();
    }

    private async Task Prepare()
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.ExecuteAsync("create table if not exists uuids (id int primary key, uuid uuid)");
        await connection.ExecuteAsync("truncate table uuids");
    }

    private async Task Write(IEnumerable<Record> records)
    {
        await using var connection = new NpgsqlConnection(connectionString);

        // Meh bulk insert.
        foreach (var record in records)
        {
            await connection.ExecuteAsync("insert into uuids (id, uuid) values (@id, @uuid)", record);
        }
    }

    private async Task<IEnumerable<Record>> Read()
    {
        await using var connection = new NpgsqlConnection(connectionString);

        var results = await connection.QueryAsync<(int, Guid)>("select id, uuid from uuids order by uuid");

        return results.Select(x => new Record(x.Item1, x.Item2));
    }
}