// See https://aka.ms/new-console-template for more information
using System.CommandLine;

var rootCommand = new RootCommand { new Apply() };

await rootCommand.InvokeAsync(args);
