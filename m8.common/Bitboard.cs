using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace m8.common;

/// <summary>
///  Represents a binary (true/false) state for each square of a chess board.
/// </summary>
public readonly struct Bitboard
{
    private readonly ulong _value;

    #region Static instances

    /// <summary>
    ///  A Bitboard with all bits to zero.
    /// </summary>
    public static readonly Bitboard Empty = new(0x0000000000000000ul);

    /// <summary>
    ///  A bitboard with all bits set to one.
    /// </summary>
    public static readonly Bitboard Full = new(0xfffffffffffffffful);

    #endregion

    #region Constructor and builders

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="value">Value of the bitboard</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard(ulong value)
    {
        _value = value;
    }

    /// <summary>
    ///  Create a Bitboard with a single bit set.
    /// </summary>
    /// <param name="index">Index of the bit to set</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard CreateSingleBit(int index)
    {
        Debug.Assert(index < 64);
        return new(0x1ul << index);
    }

    #endregion

    #region Public properties and accessors

    /// <summary>
    ///  Get the value of a given bit using the index operator.
    /// </summary>
    public bool this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return (_value & (1ul << index)) != 0;
        }
    }

    /// <summary>
    ///  Get the position of the least significant bit set to one.
    /// </summary>
    public int LSB
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(_value != 0);
            return BitOperations.TrailingZeroCount(_value);
        }
    }

    /// <summary>
    ///  Get the position of the most significant bit set to one.
    /// </summary>
    public int MSB
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(_value != 0);
            return BitOperations.Log2(_value);
        }
    }

    /// <summary>
    ///  Returns the number of bits set to one.
    /// </summary>
    public int PopCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return BitOperations.PopCount(_value);
        }
    }

    /// <summary>
    ///  Returns a string representing the Bitboard value.
    /// </summary>
    public override string ToString()
    {
        return "0x" + _value.ToString("x");
    }

    #endregion

    #region Public mutators

    /// <summary>
    ///  Returns a copy of the bitboard with a given bit set.
    /// </summary>
    /// <param name="index">Index of the bit to set</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard Set(int index)
    {
        Debug.Assert(index < 64);
        return new(_value | (1ul << index));
    }

    /// <summary>
    ///  Returns a copy of the bitboard with a given bit unset.
    /// </summary>
    /// <param name="index">Index of the bit to unset</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard Unset(int index)
    {
        Debug.Assert(index < 64);
        return new(_value & ~(1ul << index));
    }

    #endregion

    #region Operators overloading

    /// <summary>
    ///  Overload of the operator &
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard operator &(Bitboard left, Bitboard right)
    {
        return new Bitboard(left._value &  right._value);
    }

    /// <summary>
    ///  Overload of the operator |
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard operator |(Bitboard left, Bitboard right)
    {
        return new Bitboard(left._value | right._value);
    }

    /// <summary>
    ///  Overload of the operator ^
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard operator ^(Bitboard left, Bitboard right)
    {
        return new Bitboard(left._value ^ right._value);
    }

    #endregion
}
