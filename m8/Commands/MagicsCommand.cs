using System.CommandLine;
using m8.chess;
using m8.chess.MoveGeneration.sliders.magics;

namespace m8.Commands;

/// <summary>
///  Command searching for magics numbers
/// </summary>
internal class MagicsCommand : Command
{
    /// <summary>
    ///  Constructor
    /// </summary>
    public MagicsCommand()
        : base("magics", "Find magics number for the slider attacks generation")
    {
        this.SetHandler(HandleMagicsCommand);
    }

    private void HandleMagicsCommand()
    {
        var timeToTry = new TimeSpan((long)(30 * TimeSpan.TicksPerSecond));
        var rookMagics = MagicFinder.FindMagics(PieceType.Rook, timeToTry);
        var bishopMagics = MagicFinder.FindMagics(PieceType.Bishop, timeToTry);

        Console.WriteLine("Magics result for rook :");
        OutputMagics(rookMagics);
        Console.WriteLine();

        Console.WriteLine("Magics result for bishop :");
        OutputMagics(bishopMagics);
        Console.WriteLine();
    }

    private void OutputMagics(IEnumerable<BlackMagicConstants> magics)
    {
        foreach (var magic in magics)
        {
            Console.WriteLine(magic);
        }
    }
}