using m8.common;
using m8.chess;
using m8.chess.Perft;
using m8.Options;
using System.CommandLine;

namespace m8.Commands;

/// <summary>
///  Command executing a perft test
/// </summary>
internal class PerftCommand : Command, IPerftObserver
{
    /// <summary>
    ///  Constructor
    /// </summary>
    public PerftCommand()
        : base("perft", "Run a perft tests and display the results.")
    {
        var fenOption = new FenOption();
        var depthOption = new DepthOption();

        AddOption(fenOption);
        AddOption(depthOption);

        this.SetHandler(HandlePerftCommand, fenOption, depthOption);
    }

    private void HandlePerftCommand(string fen, uint depth)
    {
        var board = new Board(fen);
        Console.WriteLine(board);
        Console.WriteLine();

        var perft = new Perft(board, depth, this);
        perft.RunPerftTest();
        
    }

    public void OnPartialResult(Move move, ulong count)
    {
        // TODO : Utiliser la SAN notation?
        Console.WriteLine($" {move}\t{count}");
    }

    public void OnPerftCompleted(ulong count, TimeSpan duration)
    {
        var nps = count / duration.TotalSeconds;

        Console.WriteLine();
        Console.WriteLine($" Nodes: {count}");
        Console.WriteLine($" Time: {duration.TotalSeconds}");
        Console.WriteLine($" Nodes per second: {nps.ToStringWithMetricSuffix("0.###")}");
    }
}