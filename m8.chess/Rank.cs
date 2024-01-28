using m8.common;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace m8.chess;

/// <summary>
///  Represents a rank on a chess board.
/// </summary>
/// <remarks>
///  Constructor
/// </remarks>
/// <param name="value">
///  Value of the rank. Valid rank values are 0 to 7.</param>
public readonly struct Rank(byte value)
{
    private readonly byte _value = value;

    #region Constructors

    /// <summary>
    ///  Default constructor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rank()
    : this(byte.MaxValue) { }

    /// <summary>
    ///  Constructor from a file character
    /// </summary>
    /// <param name="c"></param>
    public Rank(char c)
    : this((byte)(c - '1'))
    {
        Debug.Assert(this.IsValid);
    }

    #endregion

    #region Static instances

    /// <summary>
    ///  First rank
    /// </summary>
    public static readonly Rank First = new(0);

    /// <summary>
    ///  Second rank
    /// </summary>
    public static readonly Rank Second = new(1);

    /// <summary>
    ///  Third rank
    /// </summary>
    public static readonly Rank Third = new(2);

    /// <summary>
    ///  Fourth rank
    /// </summary>
    public static readonly Rank Fourth = new(3);

    /// <summary>
    ///  Fifth rank
    /// </summary>
    public static readonly Rank Fifth = new(4);

    /// <summary>
    ///  Sixth rank
    /// </summary>
    public static readonly Rank Sixth = new(5);

    /// <summary>
    ///  Seventh rank
    /// </summary>
    public static readonly Rank Seventh = new(6);

    /// <summary>
    ///  Eight rank
    /// </summary>
    public static readonly Rank Eight = new(7);

    /// <summary>
    ///  All the ranks from First to Eight
    /// </summary>
    public static readonly IEnumerable<Rank> AllRanks = [First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eight];

    /// <summary>
    ///  Represent an invalid rank.
    /// </summary>
    /// <remarks>This value can be used to represent the absence of a rank,</remarks>
    public static readonly Rank Invalid = new(byte.MaxValue);

    #endregion

    #region Comparison operators

    /// <summary>
    ///  Determine if an instance is less than another one.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the lhs is bellow rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Rank lhs, Rank rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value < rhs._value;
    }

    /// <summary>
    ///  Determine if an instance is greater than another one.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the lhs is above rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Rank lhs, Rank rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value > rhs._value;
    }

    /// <summary>
    ///  Determine if an instance is less than or equal to another one.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the lhs is to the bellow or the same as rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Rank lhs, Rank rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value <= rhs._value;
    }

    /// <summary>
    ///  Determine if an instance is greateror equal than another one.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the lhs is above or the same as rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Rank lhs, Rank rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value >= rhs._value;
    }

    /// <summary>
    ///  Determine if two Rank instances are equal.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are equals</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rank lhs, Rank rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value == rhs._value;
    }

    /// <summary>
    ///  Determine if two Rank instances are different.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are different</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rank lhs, Rank rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value != rhs._value;
    }

    /// <summary>
    ///  Determine if the current instance is equals to another object.
    /// </summary>
    /// <param name="obj">The other object</param>
    /// <returns>True if the other object is a Rank and its value is equal</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        if (!this.IsValid)
        {
            return false;
        }

        if (obj is Rank other)
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

    #region Public methods

    /// <summary>
    ///  Indicate if the current objects represents a valid rank.
    /// </summary>
    public readonly bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (_value & 0xf8) == 0;
    }

    /// <summary>
    ///  Return the internal value of the instance.
    /// </summary>
    public byte Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(this.IsValid);
            return _value;
        }
    }

    /// <summary>
    ///  Returns a new Rank that is positioned a specified number of position to 
    ///  above the current rank
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rank MoveUp(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new Rank((byte)(_value + positions));
    }

    /// <summary>
    ///  Returns a new File that is positioned a specified number of position bellow
    ///  the current Rank.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rank MoveDown(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new Rank((byte)(_value - positions));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rank FlipForBlack(Color color)
    {
        if (color == Color.Black)
        {
            return new Rank((byte)(7 - _value));
        }
        return this;
    }

    /// <summary>
    ///  Gets a bitboard of the squares of the rank
    /// </summary>
    public Bitboard Bitboard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(IsValid);
            return new Bitboard(0x00000000000000fful << (8 * _value));
        }
    }

    /// <summary>
    ///  Returns a string representing the current instance.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (!this.IsValid)
        {
            return "None";
        }
        return (1 + _value).ToString();
    }

    #endregion
}