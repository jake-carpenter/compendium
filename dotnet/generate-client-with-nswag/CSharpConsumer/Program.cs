using CSharpConsumer.ServerClient;

var client = new Client(new HttpClient
{
    BaseAddress = new Uri("http://localhost:5106")
});

try
{
    await client.CreateFooAsync(new Foo { Bar = "not-found" });
}
catch (ApiException ex) when (ex.StatusCode == 404)
{
    Console.WriteLine($"Not found result was: HTTP {ex.StatusCode}");
}

var createdFoo = await client.CreateFooAsync(new Foo { Bar = "found" });
Console.WriteLine($"Created foo: {createdFoo.Id} {createdFoo.Bar}");
