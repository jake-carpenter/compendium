using Project;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<RunAllCommand>("all");
});

return await app.RunAsync(args);