namespace m8.chess;

/// <summary>
///  Informations returned by the Make method that are needed to unmake the move
/// </summary>
public readonly struct UnmakeInfo
{
    /// <summary>
    ///  Constructor
    /// </summary>
    public UnmakeInfo(File enPassantFile, CastlingOptions castlingOptions, uint halfMoveClock)
    {
        EnPassantFile = enPassantFile;
        CastlingOptions = castlingOptions;
        HalfMoveClock = halfMoveClock;
    }

    /// <summary>
    ///  File where an en passant capture is possible before the move is made
    /// </summary>
    public File EnPassantFile { get; }

    /// <summary>
    ///  Castling options before the move is made
    /// </summary>
    public CastlingOptions CastlingOptions { get; }

    /// <summary>
    ///  Half move clock before the move is made
    /// </summary>
    public uint HalfMoveClock { get; }
}
