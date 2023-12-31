using m8.chess;
using m8.Options;
using System.CommandLine;

namespace m8.Commands;

/// <summary>
///  Command executing a perft test
/// </summary>
internal class PerftCommand : Command
{
    /// <summary>
    ///  Constructor
    /// </summary>
    public PerftCommand()
        : base("perft", "Run a perft tests and display the results.")
    {
        var fenOption = new FenOption();

        this.AddOption(fenOption);

        this.SetHandler<string>(HandlePerftCommand, fenOption);
    }

    private void HandlePerftCommand(string fen)
    {
        var board = new Board(fen);
        Console.WriteLine(board);
    }
}