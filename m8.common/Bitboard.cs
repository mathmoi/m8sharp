using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace m8.common;

/// <summary>
///  Represents a binary (true/false) state for each square of a chess board.
/// </summary>
public readonly struct Bitboard : IEquatable<Bitboard>
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

    #region Static methods

    /// <summary>
    ///  Given a bitboard mask generate all the variations of the bitboard where only the 
    ///  bit set in the mask can be set to one in the variations.
    /// </summary>
    public static IEnumerable<Bitboard> GenerateAllVariations(Bitboard mask)
    {
        var numberOfVariations = 1ul << mask.PopCount;

        for (ulong variation = 0; variation < numberOfVariations; ++variation)
        {
            yield return new Bitboard(Bmi2.X64.ParallelBitDeposit(variation, mask.Value));
        }
    }

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
    ///  Indicate if the bitboard has any bit set to one.
    /// </summary>
    public bool Any
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _value != 0ul;
        }
    }

    /// <summary>
    ///  Return the internal value of the instance.
    /// </summary>
    public ulong Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value;
    }

    /// <summary>
    ///  Returns a string representing the Bitboard value.
    /// </summary>
    public override string ToString()
    {
        return "0x" + _value.ToString("x");
    }

    /// <summary>
    ///  Return the value of the bitboard in a user friendly table of 8x8 
    ///  binary numbers.
    /// </summary>
    public string ToBinaryBoardString()
    {
        var sb = new StringBuilder();

        for (int rank = 7; 0 <= rank; --rank)
        {
            for (int file = 0; file < 8; ++file)
            {
                sb.Append(this[rank * 8 + file] ? '1' : '0');
            }
            sb.AppendLine();
        }

        return sb.ToString();
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

    /// <summary>
    ///  Remove the least significatn bit set to one.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard RemoveLSB()
    {
        Debug.Assert(_value != 0);
        return new Bitboard(_value & (_value - 1));
    }

    /// <summary>
    ///  Rotate the bit in the bitboard to the left
    /// </summary>
    /// <param name="offset">Number of bits to rotate</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard RotateLeft(int offset) => new Bitboard(BitOperations.RotateLeft(_value, offset));

    /// <summary>
    ///  Rotate the bit in the bitboard to the right
    /// </summary>
    /// <param name="offset">Number of bits to rotate</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard RotateRight(int offset) => new Bitboard(BitOperations.RotateRight(_value, offset));

    /// <summary>
    ///  Shift the bitboard left or right by the amount of the offset depending on the 
    ///  offset sign.
    /// </summary>
    /// <param name="offset">
    ///  Length to shift. If the value is positive a left shift is applied. If the value 
    ///  is negative a right shift is applied.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard Shift(int offset)
    {
        if (offset > 0)
        {
            return new Bitboard(_value << offset);
        }
        else
        {
            return new Bitboard(Value >> -offset);
        }
    }


    /// <summary>
    ///  Verify if another instance is equal to this instance.
    /// </summary>
    public bool Equals(Bitboard other)
    {
        return this._value == other._value;
    }

    /// <summary>
    ///  Verify if another object is equal to this instance.
    /// </summary>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Bitboard other && this._value == other._value;
    }

    /// <summary>
    ///  Get a hash code for this instance
    /// </summary>
    public override int GetHashCode()
    {
        return _value.GetHashCode();
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

    /// <summary>
    ///  Overload of the operator <<
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard operator <<(Bitboard bb, int shiftAmount)
    {
        return new Bitboard(bb._value << shiftAmount);
    }

    /// <summary>
    ///  Overload of the operator >>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard operator >>(Bitboard bb, int shiftAmount)
    {
        return new Bitboard(bb._value >> shiftAmount);
    }

    /// <summary>
    ///  Overload the not operator (~)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard operator ~(Bitboard bb)
    {
        return new Bitboard(~bb._value);
    }

    /// <summary>
    ///  Overload the equality operator (==)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Bitboard left, Bitboard right)
    {
        return left._value == right._value;
    }

    /// <summary>
    ///  Overload the not equal operator (!=)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Bitboard left, Bitboard right)
    {
        return left._value != right._value;
    }

    #endregion
}
