using System.CommandLine;

namespace m8.Options;

/// <summary>
///  Option to pass a fen string to m8.
/// </summary>
internal class FenOption : Option<string>
{
    /// <summary>
    ///  Constructor
    /// </summary>
    public FenOption()
        : base(aliases:         ["--fen", "-f"],
               getDefaultValue: () => m8.chess.Board.STARTING_POSITION_FEN,
               description:     "FEN string(or XFEN) representing the chess position to use.")
    {}
}
