namespace m8.chess;

/// <summary>
/// Represents the castling options available in a chess game.
/// </summary>
[Flags]
public enum CastlingOptions
{
    /// <summary>
    /// Indicates that no castling options are available.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates that White can castle on the king's side (short castling).
    /// </summary>
    WhiteKingside = 1,

    /// <summary>
    /// Indicates that Black can castle on the king's side (short castling).
    /// </summary>
    BlackKingside = 2,

    /// <summary>
    /// Indicates that White can castle on the queen's side (long castling).
    /// </summary>
    WhiteQueenside = 4,

    /// <summary>
    /// Indicates that Black can castle on the queen's side (long castling).
    /// </summary>
    BlackQueenside = 8,

    /// <summary>
    ///  Indicate that all castling options are available.
    /// </summary>
    All = WhiteKingside | BlackKingside | WhiteQueenside | BlackQueenside
}
