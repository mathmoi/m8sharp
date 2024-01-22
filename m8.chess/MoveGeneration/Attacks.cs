using m8.chess.MoveGeneration.sliders.magics;
using m8.common;
using m8.common.Collections;
using System.Runtime.InteropServices;

namespace m8.chess.MoveGeneration;

/// <summary>
///  Classes containings methods about how to piece attacks squares on a chess board.
/// </summary>
public static class Attacks
{
    public static readonly UnsafeArray<Bitboard> kingAttacks;
    public static readonly UnsafeArray<Bitboard> knightAttacks;

    #region Static construction and initialization

    /// <summary>
    ///  Static constructor
    /// </summary>
    static Attacks()
    {
        kingAttacks   = InitializeKingAttacks();
        knightAttacks = InitializeKnightAttacks();
    }

    private static UnsafeArray<Bitboard> InitializeKingAttacks()
    {
        var deltas = new (sbyte, sbyte)[] { (-1,  1), (0,  1), (1,  1),
                                            (-1,  0), /* K  */ (1,  0),
                                            (-1, -1), (0, -1), (1, -1)};

        return InitializeSimpleAttacks(deltas);
    }

    private static UnsafeArray<Bitboard> InitializeKnightAttacks()
    {
        var deltas = new (sbyte, sbyte)[] {         (-1,  2), (1,  2),
                                           (-2,  1),                   (2,  1),
                                                          /* N */
                                           (-2, -1),                   (2, -1),
                                                    (-1, -2), (1, -2)};
        return InitializeSimpleAttacks(deltas);
    }

    private static UnsafeArray<Bitboard> InitializeSimpleAttacks((sbyte, sbyte)[] deltas)
    {
        UnsafeArray<Bitboard> result = new(64);

        foreach (var sq in Square.AllSquares)
        {
            var attacks = Bitboard.Empty;

            foreach (var (fileDelta, rankDelta) in deltas)
            {
                var targetFile = sq.File.MoveRight(fileDelta);
                var targetRank = sq.Rank.MoveUp(rankDelta);

                if (targetFile.IsValid && targetRank.IsValid)
                {
                    var targetSquare = new Square(targetFile, targetRank);
                    attacks = attacks.Set(targetSquare.Value);
                }
            }

            result[sq.Value] = attacks;
        }

        return result;
    }

    #endregion
}
