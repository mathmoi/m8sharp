﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace m8.chess;

/// <summary>
///  Represents a piece type
/// </summary>
public readonly struct PieceType
{
    #region Constants

    private const byte NO_PIECE_TYPE_VALUE = 0;
    private const byte PAWN_VALUE          = 1;
    private const byte KNIGHT_VALUE        = 2;
    private const byte KING_VALUE          = 3;
    private const byte QUEEN_VALUE         = 4;
    private const byte BISHOP_VALUE        = 5;
    private const byte ROOK_VALUE          = 6;
                                                   
    private const char PAWN_CHAR           = 'P';
    private const char KNIGHT_CHAR         = 'N';
    private const char KING_CHAR           = 'K';
    private const char QUEEN_CHAR          = 'Q';
    private const char BISHOP_CHAR         = 'B';
    private const char ROOK_CHAR           = 'R';

    private const string NO_PIECE_TYPE_NAME = "None";
    private const string PAWN_NAME          = "Pawn";
    private const string KNIGHT_NAME        = "Knight";
    private const string KING_NAME          = "King";
    private const string QUEEN_NAME         = "Queen";
    private const string BISHOP_NAME        = "Bishop";
    private const string ROOK_NAME          = "Rook";

    private const string NO_PIECE_TYPE_REPRESENTATION = "None";

    private static readonly Dictionary<char, byte> CHAR_TO_VALUE_MAP = new Dictionary<char, byte>
    {
        { PAWN_CHAR,   PAWN_VALUE },
        { KNIGHT_CHAR, KNIGHT_VALUE },
        { KING_CHAR,   KING_VALUE },
        { QUEEN_CHAR,  QUEEN_VALUE },
        { BISHOP_CHAR, BISHOP_VALUE },
        { ROOK_CHAR,   ROOK_VALUE }
    };

    private static readonly Dictionary<byte, char> VALUE_TO_CHAR_MAP = new Dictionary<byte, char>
    {
        { PAWN_VALUE,   PAWN_CHAR },
        { KNIGHT_VALUE, KNIGHT_CHAR },
        { KING_VALUE,   KING_CHAR },
        { QUEEN_VALUE,  QUEEN_CHAR },
        { BISHOP_VALUE, BISHOP_CHAR },
        { ROOK_VALUE,   ROOK_CHAR }
    };

    private static readonly Dictionary<byte, string> VALUE_TO_NAME_MAP = new Dictionary<byte, string>
    {
        { PAWN_VALUE,   PAWN_NAME },
        { KNIGHT_VALUE, KNIGHT_NAME },
        { KING_VALUE,   KING_NAME },
        { QUEEN_VALUE,  QUEEN_NAME },
        { BISHOP_VALUE, BISHOP_NAME },
        { ROOK_VALUE,   ROOK_NAME }
    };

    #endregion

    #region Private fields

    private readonly byte _value;

    #endregion

    #region Constructor

    /// <summary>
    ///  Default constructor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PieceType()
    : this(byte.MaxValue) { }

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PieceType(byte value)
    {
        _value = value;
    }

    /// <summary>
    ///  Constructor from a character representing the piece.
    /// </summary>
    /// <param name="c">Character representing the piece.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PieceType(char c)
    {
        if (!CHAR_TO_VALUE_MAP.TryGetValue(char.ToUpper(c), out _value))
        {
            _value = NO_PIECE_TYPE_VALUE;
        }
    }

    #endregion

    #region Static instances

    /// <summary>
    ///  No piece type
    /// </summary>
    public static readonly PieceType None = new PieceType(NO_PIECE_TYPE_VALUE);

    /// <summary>
    ///  Pawn
    /// </summary>
    public static readonly PieceType Pawn = new PieceType(PAWN_VALUE);

    /// <summary>
    ///  Knight
    /// </summary>
    public static readonly PieceType Knight = new PieceType(KNIGHT_VALUE);

    /// <summary>
    ///  King
    /// </summary>
    public static readonly PieceType King = new PieceType(KING_VALUE);

    /// <summary>
    ///  Queen
    /// </summary>
    public static readonly PieceType Queen = new PieceType(QUEEN_VALUE);

    /// <summary>
    ///  Bishop
    /// </summary>
    public static readonly PieceType Bishop = new PieceType(BISHOP_VALUE);

    /// <summary>
    ///  Rook
    /// </summary>
    public static readonly PieceType Rook = new PieceType(ROOK_VALUE);

    #endregion

    #region Static Methods

    /// <summary>
    ///  Convert a byte into a PieceType
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator PieceType(byte value) => new(value);

    #endregion

    #region Comparison operators

    /// <summary>
    ///  Determine if two Piece types instances are equal.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are equals</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(PieceType lhs, PieceType rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value == rhs._value;
    }

    /// <summary>
    ///  Determine if two PieceType instances are differents.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are differents</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(PieceType lhs, PieceType rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value != rhs._value;
    }

    /// <summary>
    ///  Verify if another instance is equal to this instance.
    /// </summary>
    public bool Equals(PieceType other)
    {
        return this._value == other._value;
    }

    /// <summary>
    ///  Verify if another object is equal to this instance.
    /// </summary>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is PieceType other && this._value == other._value;
    }

    /// <summary>
    ///  Get a hash code for this instance
    /// </summary>
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    #endregion

    #region Public properties

    /// <summary>
    ///  Indicate if the current valie is a valid PieceType
    /// </summary>
    public bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => 1 <= _value && _value <= 6;
    }

    /// <summary>
    ///  Return the internal value of the instance.
    /// </summary>
    public byte Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value;
    }

    /// <summary>
    ///  Returns the character representing this piece type
    /// </summary>
    public char Character
    {
        get
        {
            Debug.Assert(IsValid);
            return VALUE_TO_CHAR_MAP[_value];
        }
    }

    #endregion

    #region Public methods

    /// <summary>
    ///  Returns a string representing the piece name
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (VALUE_TO_NAME_MAP.TryGetValue(_value, out string? result))
        {
            return result;
        }
        else
        {
            return NO_PIECE_TYPE_NAME;
        }
    }

    #endregion
}
