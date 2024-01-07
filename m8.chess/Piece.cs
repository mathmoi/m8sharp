using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace m8.chess;

public readonly struct Piece
{
    #region Constants

    /// <summary>
    ///  Maximum value of a piece.
    /// </summary>
    public const byte MAX_VALUE = 14;

    #endregion

    #region Private fields

    private readonly byte _value;

    #endregion

    #region Constructors

    /// <summary>
    ///  Constructor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Piece(byte value) => _value = value;

    /// <summary>
    ///  Default constructor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Piece()
    : this(byte.MaxValue) { }

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="color">Color of the piece</param>
    /// <param name="type">Type of the piece</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Piece(Color color, PieceType type)
    {
        _value = (byte)((((byte)color) << 3) | ((byte)type));
    }

    /// <summary>
    ///  Constructor from a character representing a piece. Uppercases letters are 
    ///  white, lowercase letters are black.
    /// </summary>
    /// <param name="c">Character representing a piece</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Piece(char c)
    : this(Char.IsUpper(c) ? Color.White : Color.Black, new PieceType(c))
    { }

    #endregion

    #region Static instances

    /// <summary>
    ///  Represent the absence of a piece.
    /// </summary>
    public static readonly Piece None = new(0);

    /// <summary>
    ///  White pawn
    /// </summary>
    public static readonly Piece WhitePawn = new(Color.White, PieceType.Pawn);

    /// <summary>
    ///  White rook
    /// </summary>
    public static readonly Piece WhiteRook = new(Color.White, PieceType.Rook);

    /// <summary>
    ///  White knight
    /// </summary>
    public static readonly Piece WhiteKnight = new(Color.White, PieceType.Knight);

    /// <summary>
    ///  White bishop
    /// </summary>
    public static readonly Piece WhiteBishop = new(Color.White, PieceType.Bishop);

    /// <summary>
    ///  White queen
    /// </summary>
    public static readonly Piece WhiteQueen = new(Color.White, PieceType.Queen);

    /// <summary>
    ///  White king
    /// </summary>
    public static readonly Piece WhiteKing = new(Color.White, PieceType.King);

    /// <summary>
    ///  Black pawn
    /// </summary>
    public static readonly Piece BlackPawn = new(Color.Black, PieceType.Pawn);

    /// <summary>
    ///  Black rook
    /// </summary>
    public static readonly Piece BlackRook = new(Color.Black, PieceType.Rook);

    /// <summary>
    ///  Black knight
    /// </summary>
    public static readonly Piece BlackKnight = new(Color.Black, PieceType.Knight);

    /// <summary>
    ///  Black bishop
    /// </summary>
    public static readonly Piece BlackBishop = new(Color.Black, PieceType.Bishop);

    /// <summary>
    ///  Black queen
    /// </summary>
    public static readonly Piece BlackQueen = new(Color.Black, PieceType.Queen);

    /// <summary>
    ///  Black king
    /// </summary>
    public static readonly Piece BlackKing = new(Color.Black, PieceType.King);

    #endregion

    #region Static methods

    /// <summary>
    ///  Extract the value of the Piece.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(Piece piece) => piece._value;

    /// <summary>
    ///  Convert a byte into a Piece
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Piece(byte value) => new(value);

    #endregion

    #region Comparison operators

    /// <summary>
    ///  Determine if two Piece instances are equal.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are equals</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Piece lhs, Piece rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value == rhs._value;
    }

    /// <summary>
    ///  Determine if two Piece instances are differents.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are differents</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Piece lhs, Piece rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value != rhs._value;
    }

    /// <summary>
    ///  Determine if the current instance is equals to another object.
    /// </summary>
    /// <param name="obj">The other object</param>
    /// <returns>True if the other object is a Piece and its value is equal</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        if (!this.IsValid)
        {
            return false;
        }

        if (obj is Piece other)
        {
            if (!other.IsValid)
            {
                return false;
            }

            return this == other;
        }

        return false;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _value.GetHashCode();

    #endregion

    #region Public properties

    /// <summary>
    ///  Color of the piece
    /// </summary>
    public Color Color
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (Color)(_value >> 3);
    }

    /// <summary>
    ///  Type of the piece
    /// </summary>
    public PieceType Type
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (PieceType)(_value & 0x7);
    }

    /// <summary>
    ///  Indicate if the current object represent a valid piece.
    /// </summary>
    public bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Type.IsValid;
    }


    /// <summary>
    ///  Returns the character representing this piece type
    /// </summary>
    public char Character
    {
        get
        {
            Debug.Assert(IsValid);
            return this.Color == Color.White
                    ? char.ToUpper(this.Type.Character)
                    : char.ToLower(this.Type.Character);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    ///  Returns the piece name in english (ex.: Black Queen).
    /// </summary>
    public override string ToString()
    {
        if (! this.IsValid)
        {
            return "None";
        }

        return $"{this.Color} {this.Type}";
    }

    #endregion
}
