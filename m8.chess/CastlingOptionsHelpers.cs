namespace m8.chess;

/// <summary>
///  Class containing static method that can be used to to handle CastlingOptions
/// </summary>
internal static class CastlingOptionsHelpers
{
    /// <summary>
    ///  Create a castling option from the Color and CastlingSide
    /// </summary>
    public static CastlingOptions Create(Color color, CastlingSide side)
    {
        CastlingOptions option = side == CastlingSide.QueenSide ? CastlingOptions.WhiteQueenside : CastlingOptions.WhiteKingside;
        option = (CastlingOptions)((int)option << color.Value);
        return option;
    }

    public static char GetCastlingCharacter(this CastlingOptions options)
    {
        switch (options)
        {
            case CastlingOptions.WhiteKingside:
                return 'K';

            case CastlingOptions.WhiteQueenside:
                return 'Q';

            case CastlingOptions.BlackKingside:
                return 'k';

            case CastlingOptions.BlackQueenside:
                return 'q';

            default:
                throw new ArgumentException("Unable to determine the character representing the casting option if the value does not represent a single valid option.");
        }
    }
}
