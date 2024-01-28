using m8.common;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace m8.chess.MoveGeneration.sliders.magics;

/// <summary>
///  Class implemeting functionalities that allows to find magic numbers for 
///  sliding pieces move generation.
/// </summary>
public static class MagicFinder
{
    private static Random Rnd = new Random();

    /// <summary>
    ///  Find black magic constants to use for sliders move generation.
    /// </summary>
    /// <param name="pieceType">Type of piece, must be Rook or Bishop</param>
    /// <param name="timeToTry">
    ///  Time to try to improve the best magic found for each square.</param>
    /// <returns></returns>
    public static IEnumerable<BlackMagicConstants> FindMagics(PieceType pieceType, TimeSpan timeToTry)
    {
        Debug.Assert(pieceType == PieceType.Rook || pieceType == PieceType.Bishop);

        var result = new BlackMagicConstants[64];
        
        foreach (var sq in Square.AllSquares)
        {
            result[sq.Value] = FindMagic(sq, pieceType, timeToTry);
        }

        return result;
    }

    private static BlackMagicConstants FindMagic(Square sq, PieceType pieceType, TimeSpan timeToTry)
    {
        var BOARD_EDGES = new Bitboard(0xff818181818181fful);
        const int MAX_RANGE_SIZE = 1 << 12;
        const int FIXED_SHIFT = 64 - 12;

        Bitboard mask = BlackMagicSliders.GenerateRelevantOccupancyMask(sq, pieceType);
        var notMask = ~mask;
        var expectedRange = 1 << mask.PopCount;
        var occupancies = Bitboard.GenerateAllVariations(mask).ToArray();
        var attacks = GenerateAttacks(sq, occupancies, pieceType);

        var used = new Bitboard[MAX_RANGE_SIZE];

        int min;
        int max;
        var best = new BlackMagicConstants()
        {
            MaxIndex = int.MaxValue,
            MinIndex = 0
        };

        var sw = new Stopwatch();
        sw.Start();

        // Loop until we find an optimal magic or we ran out of time
        do
        {
            // Loop until we find a valid magic
            ulong magic;
            bool fail;
            do
            {
                magic = GenerateRandomMagic(FIXED_SHIFT);

                min = int.MaxValue;
                max = int.MinValue;
                fail = false;
                Array.Fill(used, Bitboard.Full);

                // Loop on all occupancies and check that the magic does not generate the
                // same index for two occupancies with differents attacks.
                int indexOccupancy = 0;
                while (!fail && indexOccupancy < occupancies.Length)
                {
                    int magicIndex = CalculateMagicIndex(occupancies[indexOccupancy], notMask, magic, FIXED_SHIFT);

                    if (used[magicIndex] == Bitboard.Full)
                    {
                        // This index was not previsously used
                        used[magicIndex] = attacks[indexOccupancy];
                        min = int.Min(min, magicIndex);
                        max = int.Max(max, magicIndex);
                    }
                    else
                    {
                        // The index was previously used. Fail if the attacks were
                        // differents
                        fail = used[magicIndex] != attacks[indexOccupancy];
                    }

                    ++indexOccupancy;
                }
            } while (fail);

            if (max - min < best.MaxIndex - best.MinIndex)
            {
                Console.WriteLine($"New best range {max - min} / {expectedRange} = {(max-min)/ (double)expectedRange}");
                best = new BlackMagicConstants(magic, min, max);
                sw.Restart();
            }

        } while (expectedRange < (best.MaxIndex - best.MinIndex + 1) && sw.Elapsed < timeToTry);

        Console.WriteLine($"Choosend {best.MaxIndex - best.MinIndex} / {expectedRange} = {(best.MaxIndex - best.MinIndex) / (double)expectedRange}");

        return best;
    }

    private static Bitboard[] GenerateAttacks(Square sq, Bitboard[] occupancies, PieceType pieceType)
    {
        Debug.Assert(pieceType == PieceType.Rook || pieceType == PieceType.Bishop);
        Debug.Assert(sq.IsValid);

        var attacks = new Bitboard[occupancies.Length];

        for (int index = 0; index < occupancies.Length; ++index)
        {
            attacks[index] = pieceType == PieceType.Rook ? NaiveSliders.GetRooksAttacks(sq, occupancies[index])
                                                         : NaiveSliders.GetBishopAttacks(sq, occupancies[index]);
        }

        return attacks;
    }

    /// <summary>
    ///  Generate a random magic number with few bits sets and incorporate the given shift
    ///  in the upper six bits of the magic number.
    /// </summary>
    private static ulong GenerateRandomMagic(ulong shift)
    {
        var buffer = new byte[32];
        Rnd.NextBytes(buffer);
        var first = BitConverter.ToUInt64(buffer, 0);
        var second = BitConverter.ToUInt64(buffer, 8);
        var third = BitConverter.ToUInt64(buffer, 16);
        return first & second & third;
    }
    private static int CalculateMagicIndex(Bitboard occupancy, Bitboard notMask, ulong magic, int shift)
    {
        return (int)(((occupancy | notMask).Value * magic) >> shift);
    }
}
