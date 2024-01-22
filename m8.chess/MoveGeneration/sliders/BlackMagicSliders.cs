using m8.chess.MoveGeneration.sliders.magics;
using m8.common;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace m8.chess.MoveGeneration.sliders;

/// <summary>
///  Class containing sliding piece attacks generator using the Black Magic implementation
///  and overlappings attacks sets.
/// </summary>
public static unsafe class BlackMagicSliders
{
    private const nuint MAGIC_ATTACK_TABLE_SIZE = 75181; // 587kb

    private static Bitboard* ptrMagicAttackTable;
    private static BlackMagic[] rookBlackMagics;
    private static BlackMagic[] bishopBlackMagics;

    #region Magics constants

    private static BlackMagicConstants[] rookMagicsConstants =
    {
        new BlackMagicConstants(0x2080002840011380ul,    0, 4095),
        new BlackMagicConstants(0x0020000800100020ul, 2046, 4093),
        new BlackMagicConstants(0x0040100040080005ul, 2044, 4091),
        new BlackMagicConstants(0x0040080040040002ul, 2044, 4091),
        new BlackMagicConstants(0x0040020040040001ul, 2044, 4091),
        new BlackMagicConstants(0x0020200080010204ul, 2034, 4093),
        new BlackMagicConstants(0x0040008001000040ul, 2044, 4091),
        new BlackMagicConstants(0x02000881004c0822ul,    0, 4095),
        new BlackMagicConstants(0x5800180044000c00ul,  829, 2687),
        new BlackMagicConstants(0x8000100400080010ul, 1024, 2047),
        new BlackMagicConstants(0x0800080402010008ul, 2943, 3967),
        new BlackMagicConstants(0x0000202004000200ul, 3072, 4095),
        new BlackMagicConstants(0x0000200100020020ul, 3072, 4095),
        new BlackMagicConstants(0x0000200080010020ul, 3072, 4095),
        new BlackMagicConstants(0x0000200080200040ul, 3072, 4095),
        new BlackMagicConstants(0x8280078020081040ul,  986, 3030),
        new BlackMagicConstants(0x4040002000100020ul, 1020, 3067),
        new BlackMagicConstants(0x0010000802040008ul, 3071, 4094),
        new BlackMagicConstants(0x0002000804010008ul, 3071, 4095),
        new BlackMagicConstants(0x0a02002004002002ul, 2912, 3935),
        new BlackMagicConstants(0x0002001001008010ul, 3072, 4095),
        new BlackMagicConstants(0x8091002000802001ul, 1015, 2038),
        new BlackMagicConstants(0x0600004040008001ul, 2976, 3999),
        new BlackMagicConstants(0x2440802000400020ul, 1467, 3515),
        new BlackMagicConstants(0x1040200010080010ul, 1786, 3835),
        new BlackMagicConstants(0x0000080010040010ul, 3072, 4095),
        new BlackMagicConstants(0x0204010008020008ul, 3039, 4063),
        new BlackMagicConstants(0x0000020020040020ul, 3072, 4095),
        new BlackMagicConstants(0x0000010020020020ul, 3072, 4095),
        new BlackMagicConstants(0x0000008020010020ul, 3072, 4095),
        new BlackMagicConstants(0x1000400040008001ul, 2816, 3839),
        new BlackMagicConstants(0x2008200020004081ul, 1524, 3583),
        new BlackMagicConstants(0x2440001000200020ul, 1468, 3515),
        new BlackMagicConstants(0x8004000800100010ul, 1024, 2047),
        new BlackMagicConstants(0x2000400800400410ul, 2304, 3583),
        new BlackMagicConstants(0x0d00200200200400ul, 2864, 3887),
        new BlackMagicConstants(0x0010200100200200ul, 3071, 4094),
        new BlackMagicConstants(0x00a0200080200100ul, 3062, 4085),
        new BlackMagicConstants(0x0080008000404001ul, 3064, 4087),
        new BlackMagicConstants(0x0416802000200040ul, 1982, 4030),
        new BlackMagicConstants(0x094000070c001800ul, 2045, 3947),
        new BlackMagicConstants(0x0610080102000400ul, 2974, 3998),
        new BlackMagicConstants(0x000002c009004010ul, 2848, 4095),
        new BlackMagicConstants(0x0000020004002020ul, 3072, 4095),
        new BlackMagicConstants(0x0800010002002020ul, 2944, 3967),
        new BlackMagicConstants(0x0200010000802020ul, 3040, 4063),
        new BlackMagicConstants(0x9000004000802020ul,  768, 1791),
        new BlackMagicConstants(0x0800000e0a00c802ul, 2177, 3967),
        new BlackMagicConstants(0x0a40002000100020ul, 1884, 3931),
        new BlackMagicConstants(0x0080080400100010ul, 3064, 4087),
        new BlackMagicConstants(0x0002000804010008ul, 3071, 4095),
        new BlackMagicConstants(0x8000200200040020ul, 1024, 2047),
        new BlackMagicConstants(0x1010020020010020ul, 2815, 3838),
        new BlackMagicConstants(0x2e00010020008020ul, 2336, 3359),
        new BlackMagicConstants(0x0000200040008020ul, 3072, 4095),
        new BlackMagicConstants(0x0148380006840028ul, 2202, 4075),
        new BlackMagicConstants(0x0489802012010042ul,    0, 4095),
        new BlackMagicConstants(0x0000800101108c41ul, 1107, 4095),
        new BlackMagicConstants(0x0000041080400822ul,  128, 4095),
        new BlackMagicConstants(0x0100080410402002ul,  240, 4079),
        new BlackMagicConstants(0x0900003007e00402ul,  369, 3983),
        new BlackMagicConstants(0x1048001004a80402ul,  437, 3835),
        new BlackMagicConstants(0x080c000800448402ul, 1139, 3967),
        new BlackMagicConstants(0x0200810020840142ul,    0, 4095)
    };

    private static BlackMagicConstants[] bishopMagicsConstants =
    {
        new BlackMagicConstants(0xa002004040084001ul, 1283, 1535),
        new BlackMagicConstants(0x4000802040040041ul, 2950, 3071),
        new BlackMagicConstants(0x9000101010004008ul, 1732, 1791),
        new BlackMagicConstants(0x0080800800a004e0ul, 3908, 4087),
        new BlackMagicConstants(0x0c00040060001000ul, 3624, 3903),
        new BlackMagicConstants(0x0900010080126401ul, 3784, 3951),
        new BlackMagicConstants(0x400800a000801800ul, 2874, 3071),
        new BlackMagicConstants(0xa300005020010018ul, 1226, 1487),
        new BlackMagicConstants(0x040e008000800801ul, 3940, 4031),
        new BlackMagicConstants(0x140f008010100003ul, 3706, 3775),
        new BlackMagicConstants(0x0108001010100040ul, 4020, 4079),
        new BlackMagicConstants(0x42c8008004008040ul, 2856, 3027),
        new BlackMagicConstants(0x006e004000800000ul, 3910, 4089),
        new BlackMagicConstants(0x0808100080802002ul, 3864, 3967),
        new BlackMagicConstants(0x01190000a0008019ul, 3881, 4078),
        new BlackMagicConstants(0x608800005000400cul, 2354, 2551),
        new BlackMagicConstants(0x1400120040802001ul, 3716, 3775),
        new BlackMagicConstants(0x0000040080101001ul, 4036, 4095),
        new BlackMagicConstants(0x4400080010102008ul, 2880, 3007),
        new BlackMagicConstants(0x0000088080080104ul, 3904, 4095),
        new BlackMagicConstants(0x0400040040042020ul, 3712, 4031),
        new BlackMagicConstants(0x0c00021001008080ul, 3712, 3903),
        new BlackMagicConstants(0xa018010010008010ul, 1431, 1534),
        new BlackMagicConstants(0x0708008c08004008ul, 3880, 3983),
        new BlackMagicConstants(0xe040800800802004ul,  448,  507),
        new BlackMagicConstants(0x0200002bc0801004ul, 4008, 4063),
        new BlackMagicConstants(0x1040008100102002ul, 3708, 3835),
        new BlackMagicConstants(0x0000400401001004ul, 3584, 4095),
        new BlackMagicConstants(0x1200200800800808ul, 3040, 3807),
        new BlackMagicConstants(0x0020400220010040ul, 3902, 4093),
        new BlackMagicConstants(0x0020080100202002ul, 3986, 4093),
        new BlackMagicConstants(0x1108040100002002ul, 3744, 3823),
        new BlackMagicConstants(0x020000801f004008ul, 4017, 4063),
        new BlackMagicConstants(0x0000004007002006ul, 4050, 4095),
        new BlackMagicConstants(0x0008100080001004ul, 3968, 4095),
        new BlackMagicConstants(0x2100004010010010ul, 3056, 3567),
        new BlackMagicConstants(0x0200802002008008ul, 3552, 4063),
        new BlackMagicConstants(0x0800802002000101ul, 3776, 3967),
        new BlackMagicConstants(0x8080200800801002ul, 1980, 2039),
        new BlackMagicConstants(0x0100100400400801ul, 4020, 4079),
        new BlackMagicConstants(0x08000080800c0019ul, 3866, 3967),
        new BlackMagicConstants(0x080000404006800cul, 3866, 3967),
        new BlackMagicConstants(0x0040080080200010ul, 3900, 4091),
        new BlackMagicConstants(0x0c00000040104028ul, 3696, 3903),
        new BlackMagicConstants(0x4178010020010008ul, 2857, 3048),
        new BlackMagicConstants(0x0000810040020001ul, 3968, 4095),
        new BlackMagicConstants(0x070040102000b001ul, 3924, 3983),
        new BlackMagicConstants(0x1000102004007c01ul, 3785, 3839),
        new BlackMagicConstants(0x8040002002801402ul, 1847, 2043),
        new BlackMagicConstants(0x0080001001007212ul, 3897, 4087),
        new BlackMagicConstants(0x100a001010005022ul, 3692, 3839),
        new BlackMagicConstants(0x9148021c00400d00ul, 1618, 1771),
        new BlackMagicConstants(0x000a400080200108ul, 3940, 4095),
        new BlackMagicConstants(0x020b000060100140ul, 4024, 4063),
        new BlackMagicConstants(0x20008000400800f6ul, 3525, 3583),
        new BlackMagicConstants(0x0000002008140138ul, 4026, 4095),
        new BlackMagicConstants(0x6000100020028014ul, 2299, 2559),
        new BlackMagicConstants(0x0009d40000804040ul, 3917, 4095),
        new BlackMagicConstants(0x240a000410100060ul, 3362, 3519),
        new BlackMagicConstants(0x000003144000400dul, 3942, 4095),
        new BlackMagicConstants(0x2088000000802001ul, 3420, 3575),
        new BlackMagicConstants(0x028e800000802002ul, 3996, 4055),
        new BlackMagicConstants(0x000e000040500801ul, 4028, 4095),
        new BlackMagicConstants(0x0000404040404001ul, 3843, 4095)
    };

    #endregion

    #region Type constructor and initialization

    static BlackMagicSliders()
    {
        ptrMagicAttackTable = (Bitboard*)NativeMemory.AlignedAlloc(MAGIC_ATTACK_TABLE_SIZE * (nuint)sizeof(BlackMagic), (nuint)sizeof(BlackMagic));
        NativeMemory.Clear(ptrMagicAttackTable, MAGIC_ATTACK_TABLE_SIZE);

        rookBlackMagics = new BlackMagic[64];
        bishopBlackMagics = new BlackMagic[64];
        InitializeBlackMagics();

        AppDomain.CurrentDomain.ProcessExit += BlackMagicSliders_Dtor;
    }

    private static void BlackMagicSliders_Dtor(object? sender, EventArgs e)
    {
        NativeMemory.AlignedFree(ptrMagicAttackTable);
    }

    private static void InitializeBlackMagics()
    {
        var ptrNext = ptrMagicAttackTable;

        // We iterate over the board by block of four squares. For each of theses blocks
        // will share a section of the ptrMagicAttackTable for both rooks and bishops.
        for (Rank rank = Rank.First; rank.IsValid; rank = rank.MoveUp(2))
        {
            for (File file = File.a; file.IsValid; file = file.MoveRight(2))
            {
                var sq = new Square(file, rank);

                // Rook in lower left corner
                var firstRookSizeRequired = InitializeBlackMagic(PieceType.Rook, sq, ptrNext);

                // Rook in upper right corner
                var secondRookSizeRequired = InitializeBlackMagic(PieceType.Rook, sq.MoveUp().MoveRight(), ptrNext);

                // Bishop in lower right corner
                var firstBishopSizeRequired = InitializeBlackMagic(PieceType.Bishop, sq.MoveRight(), ptrNext);

                // Bishop in upper left
                InitializeBlackMagic(PieceType.Bishop, sq.MoveUp(), ptrNext + firstBishopSizeRequired);

                ptrNext += int.Max(firstRookSizeRequired, secondRookSizeRequired);

                // Rook in lower right corner
                firstRookSizeRequired = InitializeBlackMagic(PieceType.Rook, sq.MoveRight(), ptrNext);

                // Rook in upper left corner
                secondRookSizeRequired = InitializeBlackMagic(PieceType.Rook, sq.MoveUp(), ptrNext);

                // Bishop in lower left corner
                firstBishopSizeRequired = InitializeBlackMagic(PieceType.Bishop, sq, ptrNext);

                // Bishop in upper right
                InitializeBlackMagic(PieceType.Bishop, sq.MoveUp().MoveRight(), ptrNext + firstBishopSizeRequired);

                ptrNext += int.Max(firstRookSizeRequired, secondRookSizeRequired);
            }
        }
    }

    private static int InitializeBlackMagic(PieceType pieceType,
                                            Square sq,
                                            Bitboard* ptrAttackTablePiece)
    {
        BlackMagic[] blackMagicArray = pieceType == PieceType.Rook ? rookBlackMagics : bishopBlackMagics;
        BlackMagicConstants[] magicConstantArray = pieceType == PieceType.Rook ? rookMagicsConstants : bishopMagicsConstants;
        Func<Square, Bitboard, Bitboard> GetAttacks = pieceType == PieceType.Rook ? NaiveSliders.GetRooksAttacks
                                                                                  : NaiveSliders.GetBishopAttacks;

        BlackMagicConstants magicConstants = magicConstantArray[sq.Value];
        Bitboard* ptrCorrectedAttackTable = ptrAttackTablePiece - magicConstants.MinIndex;
        
        var relevants = GenerateRelevantOccupancyMask(sq, pieceType);
        var notMask = ~relevants;
        var postMask = GetAttacks(sq, Bitboard.Empty);

        var blackMagic = new BlackMagic(ptrCorrectedAttackTable,
                                        notMask,
                                        postMask,
                                        magicConstants.Magic);

        Console.WriteLine($"{sq} {pieceType}");
        foreach (var occupancy in Bitboard.GenerateAllVariations(relevants))
        {
            var index = blackMagic.GetIndex(occupancy);
            *(ptrCorrectedAttackTable + index) |= GetAttacks(sq, occupancy);
        }

        blackMagicArray[sq.Value] = blackMagic;

        return magicConstants.AttackTableRange;
    }

    /// <summary>
    ///  Generate a bitboard of the squares which are relevant for the generation of 
    ///  attacks of a slider piece on a given square. 
    /// </summary>
    internal static Bitboard GenerateRelevantOccupancyMask(Square sq, PieceType pieceType)
    {
        Debug.Assert(sq.IsValid);

        var ray = Ray.Instances[sq.Value];

        if (pieceType == PieceType.Rook)
        {

            return (ray[Direction.North] & ~Rank.Eight.Bitboard)
                 | (ray[Direction.East] & ~File.h.Bitboard)
                 | (ray[Direction.South] & ~Rank.First.Bitboard)
                 | (ray[Direction.West] & ~File.a.Bitboard);
        }
        else if (pieceType == PieceType.Bishop)
        {
            var NOT_SIDES_MASK = new Bitboard(0x007e7e7e7e7e7e00ul);

            return (ray[Direction.NorthEast]
                    | ray[Direction.SouthEast]
                    | ray[Direction.SouthWest]
                    | ray[Direction.NorthWest]) & NOT_SIDES_MASK;
        }

        throw new ArgumentException("The pieceTypeParameter must be Rook or Bishop.", nameof(pieceType));
    }

    #endregion

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
        return rookBlackMagics[sq.Value].GetAttacks(occupancy);
    }

    /// <summary>
    ///  Returns a bitboard of the squares attacked by a bishop on a give square, given a 
    ///  specified board occupancy.
    /// </summary>
    /// <param name="sq">Square where the bishop is</param>
    /// <param name="occupancy">
    ///  Bitboard representing all occupied squares on the board</param>
    /// <returns>A bitboard representing all squares attacked by the bishop</returns>
    public static Bitboard GetBishopAttacks(Square sq, Bitboard occupancy)
    {
        return bishopBlackMagics[sq.Value].GetAttacks(occupancy);
    }
}
