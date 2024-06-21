using Project;

// Number of UUIDs to generate for each test
const int count = 20;

// Min and max timespans for random delay between UUID generation (accurate to millisecond).
var minDelay = TimeSpan.FromMilliseconds(100);
var maxDelay = TimeSpan.FromMilliseconds(500);

const string connectionString =
    "TrustServerCertificate=True;Data Source=localhost,9987;User Id=sa;Password=Password123;Database=master";

var mssqlRepo = new MsSqlRepo(connectionString);

BaseTestCase[] testCases =
[
    new GuidTestCase(mssqlRepo),
    new UuidV7TestCase(mssqlRepo),
    new UuidV8TestCase(mssqlRepo)
];

var runner = new TestRunner(testCases);

await runner.Execute(count, minDelay, maxDelay);
return 0;