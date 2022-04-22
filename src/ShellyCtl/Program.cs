using System.CommandLine;

var rootCommand = new RootCommand { new Apply() };

await rootCommand.InvokeAsync(args);
