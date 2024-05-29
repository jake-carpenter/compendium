var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/foos", (Foo foo) =>
{
    if (foo.Bar == "not-found")
        return Results.NotFound();

    var createdFoo = new CreatedFoo(1, foo.Bar);
    return Results.Created("/foos/1", createdFoo);
})
.WithName("CreateFoo")
.Produces<CreatedFoo>(201)
.Produces(404)
.WithOpenApi();

app.Run();

public record Foo(string Bar);
public record CreatedFoo(int Id, string Bar);
