using Project;

const int count = 100;
const string connectionString =
    "TrustServerCertificate=True;Data Source=localhost,9987;User Id=sa;Password=Password123;Database=master";

var mssqlRepo = new MsSqlRepo(connectionString);
await mssqlRepo.Prepare();

var testCases = new[] { new GuidTestCase(mssqlRepo) };
var runner = new TestRunner(testCases);

await runner.Execute(count);
return 0;

