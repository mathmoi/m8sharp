namespace m8.chess;

/// <summary>
///  Represents the sides of a castling move.
/// </summary>
public enum CastlingSide
{
    /// <summary>
    ///  Represents neither castling side.
    /// </summary>
    None = 0,

    /// <summary>
    ///  Represents castling on the king's side.
    /// </summary>
    KingSide = 1,

    /// <summary>
    ///  Represents castling on the queen's side.
    /// </summary>
    QueenSide = 2
}
