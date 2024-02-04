namespace m8.chess;

/// <summary>
///  Types of moves
/// </summary>
public enum MoveType
{
    /// <summary>
    ///  Normal move of a piece
    /// </summary>
    Normal = 0,

    /// <summary>
    ///  Capture of a piece
    /// </summary>
    Capture = 1,

    /// <summary>
    ///  Castling king side
    /// </summary>
    CastleKingSide = 2,

    /// <summary>
    ///  Castling queen side
    /// </summary>
    CastleQueenSide = 3,

    /// <summary>
    ///  Simple pawn move
    /// </summary>
    PawnMove = 4,

    /// <summary>
    ///  Pawn moving two square from it's initial position
    /// </summary>
    PawnDouble = 5,

    /// <summary>
    ///  En passant capture
    /// </summary>
    EnPassant = 6,

    /// <summary>
    ///  Promotion
    /// </summary>
    Promotion = 7,

    /// <summary>
    ///  Capture and promotion combo
    /// </summary>
    CapturePromotion = 8,
}
