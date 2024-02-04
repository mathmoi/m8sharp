using m8.common;
using m8.common.Collections;
using System.Runtime.CompilerServices;

namespace m8.chess;

internal struct Ray
{
    [InlineArray(8)]
    private struct Rays
    {
        public Bitboard _element0;
    }

    private readonly Rays _rays;

    #region Constructor
    private Ray(Square sq)
    {
        _rays[(int)Direction.North]     = CreateDirectionRay(sq, x => x.Rank < Rank.Eight,
                                                                 x => x.MoveUp());
        _rays[(int)Direction.NorthEast] = CreateDirectionRay(sq, x => x.Rank < Rank.Eight && x.File < File.h,
                                                                 x => x.MoveUp().MoveRight());
        _rays[(int)Direction.East]      = CreateDirectionRay(sq, x => x.File < File.h,
                                                                 x => x.MoveRight());
        _rays[(int)Direction.SouthEast] = CreateDirectionRay(sq, x => x.Rank > Rank.First && x.File < File.h,
                                                                 x => x.MoveDown().MoveRight());
        _rays[(int)Direction.South]     = CreateDirectionRay(sq, x => x.Rank > Rank.First,
                                                                 x => x.MoveDown());
        _rays[(int)Direction.SouthWest] = CreateDirectionRay(sq, x => x.Rank > Rank.First && x.File > File.a,
                                                                 x => x.MoveDown().MoveLeft());
        _rays[(int)Direction.West]      = CreateDirectionRay(sq, x => x.File > File.a,
                                                                 x => x.MoveLeft());
        _rays[(int)Direction.NorthWest] = CreateDirectionRay(sq, x => x.Rank < Rank.Eight && x.File > File.a,
                                                                 x => x.MoveUp().MoveLeft());
    }
    private static Bitboard CreateDirectionRay(Square sq, Func<Square, bool> continueCondition, Func<Square, Square> move)
    {
        var result = Bitboard.Empty;
        while (continueCondition(sq))
        {
            sq = move(sq);
            result |= sq.Bitboard;
        }
        return result;
    }

    #endregion

    #region Static instances

    public static UnsafeArray<Ray> Instances = CreateInstances();

    private static UnsafeArray<Ray> CreateInstances()
    {
        var instances = new UnsafeArray<Ray>(64);

        foreach(var sq in Square.AllSquares)
        {
            instances[sq.Value] = new Ray(sq);
        }

        return instances;
    }

    #endregion

    #region Accessors

    /// <summary>
    ///  Access the ray for a single direction
    /// </summary>
    public Bitboard this[Direction direction]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _rays[(int)direction];
    }

    #endregion
}
