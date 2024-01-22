using m8.Commands;
using System.CommandLine;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var rootCommand = new RootCommand("m8, a chess engine by Mathieu Pagé");
rootCommand.SetHandler(() => Console.WriteLine("TODO UCI"));

rootCommand.AddCommand(new HelpCommand(rootCommand));
rootCommand.AddCommand(new PerftCommand());
rootCommand.AddCommand(new MagicsCommand());

return rootCommand.Invoke(Environment.GetCommandLineArgs().Skip(1).ToArray());