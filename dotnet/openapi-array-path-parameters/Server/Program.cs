using Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.ParameterFilter<MatrixPathParameterFilter>();
    cfg.ParameterFilter<SimplePathParameterFilter>();
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/foos/{ids}", ([MatrixPath] string ids) => ids.Split(','));
app.MapGet("/bars/{ids}", ([SimplePath] string ids) => ids.Split(','));

app.Run();
