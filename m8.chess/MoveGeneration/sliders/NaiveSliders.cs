using m8.common;

namespace m8.chess.MoveGeneration.sliders;

/// <summary>
///  Class containing sliding piece attacks generator using a naive implementation
///  (without any optimisation efforts).
/// </summary>
public static class NaiveSliders
{
    /// <summary>
    ///  Returns a bitboard of the squares attacked by a rook on a give square, given a 
    ///  specified board occupancy.
    /// </summary>
    /// <param name="sq">Square where the rook is</param>
    /// <param name="occupancy">
    ///  Bitboard representing all occupied squares on the board</param>
    /// <returns>A bitboard representing all squares attacked by the rook</returns>
    public static Bitboard GetRooksAttacks(Square sq, Bitboard occupancy)
    {
        var attacks = GetAttacksDirection(sq, occupancy, sq => sq.Rank < Rank.Eight, sq => sq.MoveUp());
        attacks    |= GetAttacksDirection(sq, occupancy, sq => sq.Rank > Rank.First, sq => sq.MoveDown());
        attacks    |= GetAttacksDirection(sq, occupancy, sq => sq.File > File.a,     sq => sq.MoveLeft());
        attacks    |= GetAttacksDirection(sq, occupancy, sq => sq.File < File.h,     sq => sq.MoveRight());
        return attacks;
    }

    /// <summary>
    ///  Returns a bitboard of the squares attacked by a bishop on a give square, given a 
    ///  specified board occupancy.
    /// </summary>
    /// <param name="sq">Square where the rook is</param>
    /// <param name="occupancy">
    ///  Bitboard representing all occupied squares on the board</param>
    /// <returns>A bitboard representing all squares attacked by the rook</returns>
    public static Bitboard GetBishopAttacks(Square sq, Bitboard occupancy)
    {
        var attacks = GetAttacksDirection(sq, occupancy, 
                                          sq => sq.Rank < Rank.Eight && sq.File < File.h,
                                          sq => sq.MoveUp().MoveRight());
        attacks    |= GetAttacksDirection(sq, occupancy,
                                          sq => sq.Rank > Rank.First && sq.File < File.h,
                                          sq => sq.MoveDown().MoveRight());
        attacks    |= GetAttacksDirection(sq, occupancy,
                                          sq => sq.Rank > Rank.First && sq.File > File.a,
                                          sq => sq.MoveDown().MoveLeft());
        attacks    |= GetAttacksDirection(sq, occupancy,
                                          sq => sq.Rank < Rank.Eight && sq.File > File.a,
                                          sq => sq.MoveUp().MoveLeft());
        return attacks;
    }
    private static Bitboard GetAttacksDirection(Square sq,
                                                Bitboard occupancy,
                                                Func<Square, bool> canContinue,
                                                Func<Square, Square> moveNext)
    {
        var attacks = Bitboard.Empty;

        while (canContinue(sq) && !occupancy[sq.Value])
        {
            sq = moveNext(sq);
            attacks = attacks.Set(sq.Value);
        }

        return attacks;
    }
}
