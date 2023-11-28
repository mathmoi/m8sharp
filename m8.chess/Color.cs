using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace m8.chess
{
    public readonly struct Color
    {
        #region Constants

        private const byte WHITE_VALUE = 0;
        private const byte BLACK_VALUE = 1;

        public const byte MAX_VALUE = 1;

        #endregion

        #region Private fields

        private readonly byte _value;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructor
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        private Color(byte value)
        {
            Debug.Assert(value <= 1);
            _value = value;
        }

        #endregion

        #region Static instances

        /// <summary>
        ///  White
        /// </summary>
        public static Color White = new Color(WHITE_VALUE);

        /// <summary>
        ///  Black
        /// </summary>
        public static Color Black = new Color(BLACK_VALUE);

        #endregion

        #region Static Methods

        /// <summary>
        ///  Extract the value of the color.
        /// </summary>
        /// <param name="color">A byte corresponding to the internal value for the color</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator byte(Color color) => color._value;

        /// <summary>
        ///  Convert a byte into a Color
        /// </summary>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Color(byte value)
        {
            Debug.Assert(0 <= value && value <= 1);
            return new(value);
        }

        #endregion

        #region Comparison operators

        /// <summary>
        ///  Determine if two Color instances are equal.
        /// </summary>
        /// <param name="lhs">Left hand side instance to compare</param>
        /// <param name="rhs">Right hand dide instance to compare</param>
        /// <returns>True if the two instances are equals</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color lhs, Color rhs)
        {
            return lhs._value == rhs._value;
        }

        /// <summary>
        ///  Determine if two Color instances are different.
        /// </summary>
        /// <param name="lhs">Left hand side instance to compare</param>
        /// <param name="rhs">Right hand dide instance to compare</param>
        /// <returns>True if the two instances are different</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color lhs, Color rhs)
        {
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
            if (obj is Color other)
            {
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

        #region Operators overloading

        /// <summary>
        ///  Overload the not operator (!) to return the opposite color.
        /// </summary>
        /// <param name="color">Color to apply the operator to</param>
        /// <returns>Opposite color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator !(Color color)
        {
            return new Color((byte)(color._value ^ 1));
        }

        #endregion

        #region Public methods

        /// <summary>
        ///  Returns a string representing the name of the color.
        /// </summary>
        public override string ToString()
        {
            return _value switch
            {
                WHITE_VALUE => "White",
                BLACK_VALUE => "Black",
                _ => throw new NotImplementedException("This color value is not supported")
            };
        }

        #endregion
    }
}
