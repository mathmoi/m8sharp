namespace m8.chess.Exceptions;

/// <summary>
///  Exception thrown when a fen string cannot be parsed.
/// </summary>
public class InvalidFenException : FormatException
{
    public InvalidFenException(String fen, String reason, Exception innerException)
    : base($"Unable to parse fen string (\"{fen}\") : {reason}", innerException)
    {
        FEN = fen;
        Reason = reason;
    }

    public InvalidFenException(String reason)
    : base($"Unable to parse fen string : {reason}")
    {
        Reason = reason;
    }

    /// <summary>
    ///  Fen string that cannot be parsed.
    /// </summary>
    public string? FEN { get; init; }

    /// <summary>
    ///  Reason the FEN could not be parsed
    /// </summary>
    public string Reason { get; init; }
}
