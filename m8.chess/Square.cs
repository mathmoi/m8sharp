using m8.common;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace m8.chess;

/// <summary>
///  Represents a square on a chess board
/// </summary>
public readonly struct Square
{
    private readonly byte _value;

    #region Constructors

    /// <summary>
    ///  Constructor from a value
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square(int value)
    {
        _value = (byte)value;
    }

    /// <summary>
    ///  Constructor from a value
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square(byte value)
    {
        _value = value;
    }

    /// <summary>
    ///  Default constructor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square()
    : this(byte.MaxValue) { }

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="file">File of the square</param>
    /// <param name="rank">Rank of the square</param>
    public Square(File file, Rank rank)
    {
        Debug.Assert(file.IsValid);
        Debug.Assert(rank.IsValid);
        _value = (byte)((rank.Value) << 3 | file.Value);
    }

    /// <summary>
    ///  Constructor from a string representation of the square.
    /// </summary>
    /// <param name="sq">String containing two characters representing the square</param>
    public Square(string sq)
        : this(new File(sq[0]), new Rank(sq[1]))
    {
        Debug.Assert(sq.Length == 2);
    }

    #endregion

    #region Static instances

    public static readonly Square a1 = new(File.a, Rank.First);
    public static readonly Square b1 = new(File.b, Rank.First);
    public static readonly Square c1 = new(File.c, Rank.First);
    public static readonly Square d1 = new(File.d, Rank.First);
    public static readonly Square e1 = new(File.e, Rank.First);
    public static readonly Square f1 = new(File.f, Rank.First);
    public static readonly Square g1 = new(File.g, Rank.First);
    public static readonly Square h1 = new(File.h, Rank.First);

    public static readonly Square a2 = new(File.a, Rank.Second);
    public static readonly Square b2 = new(File.b, Rank.Second);
    public static readonly Square c2 = new(File.c, Rank.Second);
    public static readonly Square d2 = new(File.d, Rank.Second);
    public static readonly Square e2 = new(File.e, Rank.Second);
    public static readonly Square f2 = new(File.f, Rank.Second);
    public static readonly Square g2 = new(File.g, Rank.Second);
    public static readonly Square h2 = new(File.h, Rank.Second);

    public static readonly Square a3 = new(File.a, Rank.Third);
    public static readonly Square b3 = new(File.b, Rank.Third);
    public static readonly Square c3 = new(File.c, Rank.Third);
    public static readonly Square d3 = new(File.d, Rank.Third);
    public static readonly Square e3 = new(File.e, Rank.Third);
    public static readonly Square f3 = new(File.f, Rank.Third);
    public static readonly Square g3 = new(File.g, Rank.Third);
    public static readonly Square h3 = new(File.h, Rank.Third);

    public static readonly Square a4 = new(File.a, Rank.Fourth);
    public static readonly Square b4 = new(File.b, Rank.Fourth);
    public static readonly Square c4 = new(File.c, Rank.Fourth);
    public static readonly Square d4 = new(File.d, Rank.Fourth);
    public static readonly Square e4 = new(File.e, Rank.Fourth);
    public static readonly Square f4 = new(File.f, Rank.Fourth);
    public static readonly Square g4 = new(File.g, Rank.Fourth);
    public static readonly Square h4 = new(File.h, Rank.Fourth);

    public static readonly Square a5 = new(File.a, Rank.Fifth);
    public static readonly Square b5 = new(File.b, Rank.Fifth);
    public static readonly Square c5 = new(File.c, Rank.Fifth);
    public static readonly Square d5 = new(File.d, Rank.Fifth);
    public static readonly Square e5 = new(File.e, Rank.Fifth);
    public static readonly Square f5 = new(File.f, Rank.Fifth);
    public static readonly Square g5 = new(File.g, Rank.Fifth);
    public static readonly Square h5 = new(File.h, Rank.Fifth);

    public static readonly Square a6 = new(File.a, Rank.Sixth);
    public static readonly Square b6 = new(File.b, Rank.Sixth);
    public static readonly Square c6 = new(File.c, Rank.Sixth);
    public static readonly Square d6 = new(File.d, Rank.Sixth);
    public static readonly Square e6 = new(File.e, Rank.Sixth);
    public static readonly Square f6 = new(File.f, Rank.Sixth);
    public static readonly Square g6 = new(File.g, Rank.Sixth);
    public static readonly Square h6 = new(File.h, Rank.Sixth);

    public static readonly Square a7 = new(File.a, Rank.Seventh);
    public static readonly Square b7 = new(File.b, Rank.Seventh);
    public static readonly Square c7 = new(File.c, Rank.Seventh);
    public static readonly Square d7 = new(File.d, Rank.Seventh);
    public static readonly Square e7 = new(File.e, Rank.Seventh);
    public static readonly Square f7 = new(File.f, Rank.Seventh);
    public static readonly Square g7 = new(File.g, Rank.Seventh);
    public static readonly Square h7 = new(File.h, Rank.Seventh);

    public static readonly Square a8 = new(File.a, Rank.Eight);
    public static readonly Square b8 = new(File.b, Rank.Eight);
    public static readonly Square c8 = new(File.c, Rank.Eight);
    public static readonly Square d8 = new(File.d, Rank.Eight);
    public static readonly Square e8 = new(File.e, Rank.Eight);
    public static readonly Square f8 = new(File.f, Rank.Eight);
    public static readonly Square g8 = new(File.g, Rank.Eight);
    public static readonly Square h8 = new(File.h, Rank.Eight);

    /// <summary>
    ///  Represent an invalid square.
    /// </summary>
    /// <remarks>This value can be used to represent the absence of a square</remarks>
    public static readonly Square Invalid = new(byte.MaxValue);

    /// <summary>
    ///  All the ranks from First to Eight
    /// </summary>
    public static IEnumerable<Square> AllSquares
    {
        get
        {
            for (byte value = 0; value < 64; ++value)
            {
                yield return new Square(value);
            }
        }
    }

    #endregion

    #region Operators

    /// <summary>
    ///  Overloading the plus operator between a square and a integer value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Square operator+(Square sq, int delta)
    {
        return new Square((byte)(sq._value + delta));
    }

    #endregion

    #region Comparison operators

    /// <summary>
    ///  Determine if an instance is less than another one.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the lhs is to the left of rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Square lhs, Square rhs)
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
    /// <returns>True if the lhs is to the right of rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Square lhs, Square rhs)
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
    /// <returns>True if the lhs is to the left or the same as rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Square lhs, Square rhs)
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
    /// <returns>True if the lhs is to the right or the same as rhs</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Square lhs, Square rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value >= rhs._value;
    }

    /// <summary>
    ///  Determine if two Square instances are equal.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are equals</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Square lhs, Square rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value == rhs._value;
    }

    /// <summary>
    ///  Determine if two Square instances are differents.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are differents</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Square lhs, Square rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value != rhs._value;
    }

    /// <summary>
    ///  Determine if the current instance is equals to another object.
    /// </summary>
    /// <param name="obj">The other object</param>
    /// <returns>True if the other object is a Square and its value is equal</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        if (!this.IsValid)
        {
            return false;
        }

        if (obj is Square other)
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

    #region Mutators

    /// <summary>
    ///  Returns a new Square that is positioned a specified number of positions above
    ///  the current Square.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square MoveUp(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new Square((byte)(_value + positions * 8));
    }

    /// <summary>
    ///  Returns a new Square that is positioned a specified number of positions bellow
    ///  the current Square.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square MoveDown(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new Square((byte)(_value - positions * 8));
    }

    /// <summary>
    ///  Returns a new Square that is positioned a specified number of positions left of
    ///  the current Square.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square MoveLeft(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new Square((byte)(_value - positions));
    }

    /// <summary>
    ///  Returns a new Square that is positioned a specified number of positions right of
    ///  the current Square.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square MoveRight(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new Square((byte)(_value + positions));
    }

    #endregion

    #region Accessors

    /// <summary>
    ///  Indicate if the current objects represents a valid square.
    /// </summary>
    public readonly bool IsValid => (_value & 0xc0) == 0;

    /// <summary>
    ///  Returns the File of the square
    /// </summary>
    public readonly File File
    {
        get
        {
            Debug.Assert(IsValid);
            return new((byte)(_value & 0x07));
        }
    }

    /// <summary>
    ///  Returns the Rank of the square.
    /// </summary>
    public readonly Rank Rank
    {
        get
        {
            Debug.Assert(IsValid);
            return new((byte)(_value >> 3));
        }
    }

    /// <summary>
    ///  Gets a bitboard representing the square
    /// </summary>
    public Bitboard Bitboard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(IsValid);
            return Bitboard.CreateSingleBit(_value);
        }
    }

    /// <summary>
    ///  Returns the internal value for this instance. This can be used to index an array.
    /// </summary>
    public byte Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value;
    }

    /// <summary>
    ///  Returns the 0x88 index of the current square.
    /// </summary>
    public int Index0x88
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value + (_value & ~7);
    }

    /// <summary>
    ///  Returns a string representing the current file
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (!this.IsValid)
        {
            return "None";
        }
        return File.ToString() + Rank.ToString();
    }

    #endregion
}
