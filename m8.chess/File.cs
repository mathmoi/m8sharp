using m8.common;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace m8.chess;

/// <summary>
///  Represents a file on a chess board.
/// </summary>
/// <remarks>
///  Constructor
/// </remarks>
/// <param name="value">
///  Value of the file. Valid file values are 0 to 7.</param>
public readonly struct File(byte value)
{
    private readonly byte _value = value;

    #region Constructors

    /// <summary>
    ///  Default constructor
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public File()
    : this(byte.MaxValue) { }

    /// <summary>
    ///  Constructor from a file character
    /// </summary>
    /// <param name="c"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public File(char c)
    : this ((byte)(c - 'a'))
    {
        Debug.Assert(this.IsValid);
    }

    #endregion

    #region Static instances

    /// <summary>
    ///  File a
    /// </summary>
    public static readonly File a = new(0);

    /// <summary>
    ///  File b
    /// </summary>
    public static readonly File b = new(1);

    /// <summary>
    ///  File c
    /// </summary>
    public static readonly File c = new(2);

    /// <summary>
    ///  File d
    /// </summary>
    public static readonly File d = new(3);

    /// <summary>
    ///  File e
    /// </summary>
    public static readonly File e = new(4);

    /// <summary>
    ///  File f
    /// </summary>
    public static readonly File f = new(5);

    /// <summary>
    ///  File g
    /// </summary>
    public static readonly File g = new(6);

    /// <summary>
    ///  File h
    /// </summary>
    public static readonly File h = new(7);

    /// <summary>
    ///  Represent an invalid file.
    /// </summary>
    /// <remarks>This value can be used to represent the absence of a file</remarks>
    public static readonly File Invalid = new(byte.MaxValue);

    /// <summary>
    /// Collection of all the files.
    /// </summary>
    public static readonly File[] AllFiles = { File.a, File.b, File.c, File.d, File.e, File.f, File.g, File.h };

    #endregion

    #region Static methods

    /// <summary>
    ///  Explicit convertion operator to a byte
    /// </summary>
    /// <param name="file">Object to convert</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(File file)
    {
        Debug.Assert(file.IsValid);
        return file._value;
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
    public static bool operator <(File lhs, File rhs)
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
    public static bool operator >(File lhs, File rhs)
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
    public static bool operator <=(File lhs, File rhs)
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
    public static bool operator >=(File lhs, File rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value >= rhs._value;
    }

    /// <summary>
    ///  Determine if two File instances are equal.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are equals</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(File lhs, File rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value == rhs._value;
    }

    /// <summary>
    ///  Determine if two File instances are differents.
    /// </summary>
    /// <param name="lhs">Left hand side instance to compare</param>
    /// <param name="rhs">Right hand dide instance to compare</param>
    /// <returns>True if the two instances are differents</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(File lhs, File rhs)
    {
        Debug.Assert(lhs.IsValid);
        Debug.Assert(rhs.IsValid);
        return lhs._value != rhs._value;
    }

    /// <summary>
    ///  Determine if the current instance is equals to another object.
    /// </summary>
    /// <param name="obj">The other object</param>
    /// <returns>True if the other object is a File and its value is equal</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        if (!this.IsValid)
        {
            return false;
        }
        
        if (obj is File other)
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
    ///  Indicate if the current objects represents a valid file.
    /// </summary>
    public readonly bool IsValid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (_value & 0xf8) == 0;
    }

    /// <summary>
    ///  Returns a new File that is positioned a specified number of position to the 
    ///  left of the current File.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public File MoveLeft(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new File((byte)(_value - positions));
    }

    /// <summary>
    ///  Returns a new File that is positioned a specified number of position to the 
    ///  right of the current File.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public File MoveRight(sbyte positions = 1)
    {
        Debug.Assert(this.IsValid);
        return new File((byte)(_value + positions));
    }

    /// <summary>
    ///  Gets a bitboard of the squares of the file
    /// </summary>
    public Bitboard Bitboard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(IsValid);
            return new Bitboard(0x0101010101010101ul << _value);
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
        return ((char)('a' + _value)).ToString();
    }

    #endregion

}
