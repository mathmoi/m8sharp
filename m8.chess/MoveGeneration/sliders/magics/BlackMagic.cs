using m8.common;
using System.Runtime.CompilerServices;

namespace m8.chess.MoveGeneration.sliders.magics;

internal unsafe struct BlackMagic
{
    private readonly Bitboard* _ptrAttackTable;
    private readonly Bitboard _notMask;
    private readonly Bitboard _postMask;
    private readonly ulong    _blackMagic;

    public BlackMagic(Bitboard* ptrAttackTable,
                      Bitboard notMask,
                      Bitboard postMask,
                      ulong blackMagic)
    {
        _ptrAttackTable = ptrAttackTable;
        _notMask = notMask;
        _postMask = postMask;
        _blackMagic = blackMagic;
    }

    /// <summary>
    ///  returns of the attack bitboard for a given occupancy
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetIndex(Bitboard occupancy)
    {
        var index = (ulong)(occupancy | _notMask);
        index *= _blackMagic;
        index >>= 64 - 12;
        return index;
    }

    /// <summary>
    ///  Using the black magic number, generate a bitboard of the attacks given the 
    ///  occupancy of the board.
    /// </summary>
    /// <param name="occupancy">Bitboard of all occupied squares</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bitboard GetAttacks(Bitboard occupancy)
    {
        return _ptrAttackTable[GetIndex(occupancy)] & _postMask;
    }
}
