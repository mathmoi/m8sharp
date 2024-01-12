using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace m8.chess;

public readonly struct Move
{
    private readonly uint _value;

    #region Constants

    private const int FROM_SIZE       = 6;
    private const int TO_SIZE         = 6;
    private const int CASTLING_SIZE   = 2;
    private const int PIECE_SIZE      = 4;
    private const int TAKEN_SIZE      = 4;
    private const int PROMOTE_TO_SIZE = 4;

    private const int FROM_OFFSET       = 0;
    private const int TO_OFFSET         = FROM_OFFSET     + FROM_SIZE;
    private const int CASTLING_OFFSET   = TO_OFFSET       + TO_SIZE;
    private const int PIECE_OFFSET      = CASTLING_OFFSET + CASTLING_SIZE;
    private const int TAKEN_OFFSET      = PIECE_OFFSET    + PIECE_SIZE;
    private const int PROMOTE_TO_OFFSET = TAKEN_OFFSET    + TAKEN_SIZE;

    #endregion

    #region Constructors

    /// <summary>
    ///  Constructor for a simple move
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Move(Square from, Square to, Piece piece)
    {
        Debug.Assert(from.IsValid);
        Debug.Assert(to.IsValid);
        Debug.Assert(piece.IsValid);

        _value = (uint)(byte)from << FROM_OFFSET
               | (uint)(byte)to << TO_OFFSET
               | (uint)(byte)piece << PIECE_OFFSET;
    }

    /// <summary>
    ///  Constructor for capture
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Move(Square from, Square to, Piece piece, Piece taken)
    {
        Debug.Assert(from.IsValid);
        Debug.Assert(to.IsValid);
        Debug.Assert(piece.IsValid);

        _value = (uint)(byte)from << FROM_OFFSET
               | (uint)(byte)to << TO_OFFSET
               | (uint)(byte)piece << PIECE_OFFSET
               | (uint)(byte)taken << TAKEN_OFFSET;
    }

    /// <summary>
    ///  Constructor for a promotion
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Move(Square from, Square to, Piece piece, Piece taken, Piece promoteTo)
    {
        Debug.Assert(from.IsValid);
        Debug.Assert(to.IsValid);
        Debug.Assert(piece.IsValid);
        Debug.Assert(promoteTo.IsValid);

        _value = (uint)(byte)from << FROM_OFFSET
               | (uint)(byte)to << TO_OFFSET
               | (uint)(byte)piece << PIECE_OFFSET
               | (uint)(byte)taken << TAKEN_OFFSET
               | (uint)(byte)promoteTo << PROMOTE_TO_OFFSET;
    }

    /// <summary>
    ///  Constructor for a castling move
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Move(Square from, Square to, Piece piece, CastlingSide castlingSide)
    {
        Debug.Assert(from.IsValid);
        Debug.Assert(to.IsValid);
        Debug.Assert(piece.IsValid);
        Debug.Assert(Enum.IsDefined<CastlingSide>(castlingSide));

        _value = (uint)(byte)from      << FROM_OFFSET
               | (uint)(byte)to        << TO_OFFSET
               | (uint)castlingSide    << CASTLING_OFFSET
               | (uint)(byte)piece     << PIECE_OFFSET;
    }

    #endregion

    #region Public accessors

    /// <summary>
    ///  Original square of the piece moved
    /// </summary>
    public Square From
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new((byte)((_value >> FROM_OFFSET) & ((1 << FROM_SIZE) - 1)));
    }

    /// <summary>
    ///  Target square of the piece moved
    /// </summary>
    public Square To
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new((byte)((_value >> TO_OFFSET) & ((1 << TO_SIZE) - 1)));
    }

    /// <summary>
    ///  Piece moved
    /// </summary>
    public Piece Piece
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new((byte)((_value >> PIECE_OFFSET) & ((1 << PIECE_SIZE) - 1)));
    }

    /// <summary>
    ///  Piece taken
    /// </summary>
    public Piece Taken
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new((byte)((_value >> TAKEN_OFFSET) & ((1 << TAKEN_SIZE) - 1)));
    }

    /// <summary>
    ///  Piece promoted to
    /// </summary>
    public Piece PromoteTo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new((byte)((_value >> PROMOTE_TO_OFFSET) & ((1 << PROMOTE_TO_SIZE) - 1)));
    }

    /// <summary>
    ///  Castling side
    /// </summary>
    public CastlingSide CastlingSide
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (CastlingSide)((_value >> CASTLING_OFFSET) & ((1 << CASTLING_SIZE) - 1));
    }

    #endregion
}
