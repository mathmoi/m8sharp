using System.CommandLine;
using System.Diagnostics;

namespace m8.Commands;

/// <summary>
///  Commnand that display a message that explain how to use the commandslines.
/// </summary>
internal class HelpCommand : Command
{
    private readonly RootCommand _rootCommand;

    /// <summary>
    ///  Constructor
    /// </summary>
    public HelpCommand(RootCommand rootCommand)
        : base ("help", "Display the help information")
    {
        Debug.Assert(rootCommand != null);

        _rootCommand = rootCommand;
        this.SetHandler(HandleHelpCommand);
    }

    private void HandleHelpCommand()
    {
        _rootCommand.Invoke("-h");
    }
}
