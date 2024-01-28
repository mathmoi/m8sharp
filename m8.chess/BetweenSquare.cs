using m8.chess.MoveGeneration.sliders.magics;
using m8.common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace m8.chess;

/// <summary>
///  Static class containing a method that can provide a bitboard of the squares between 
///  two squares.
/// </summary>
public unsafe static class BetweenSquare
{
    private static Bitboard* ptrBetween;

    #region Static initialization and destruction

    static BetweenSquare()
    {
        ptrBetween = (Bitboard*)NativeMemory.AllocZeroed(240, (nuint)sizeof(Bitboard)) + 120;
        AppDomain.CurrentDomain.ProcessExit += BetweenSquareDtor;

        InitializePtrBetween    ();
    }

    /// <summary>
    ///  We generate all the difference variation with a1 as the from square. For the
    ///  destination square we go north, northeast and east. We also need to do the same
    ///  from the h1 square going northweast to cover the fourth direction that we can't
    ///  do from a1. For each pair generated this way can also compute it's reverse by
    ///  swaping the origin and destination square.
    /// </summary>
    private static void InitializePtrBetween()
    {
        InitializePtrBetweeDirection(Square.a1, Square.a8, 8);
        InitializePtrBetweeDirection(Square.a1, Square.h8, 9);
        InitializePtrBetweeDirection(Square.a1, Square.h1, 1);
        InitializePtrBetweeDirection(Square.h1, Square.a8, 7);
    }

    private static void InitializePtrBetweeDirection(Square firstSquare, Square lastSquare, int deltaDirection)
    {
        var between = Bitboard.Empty;
        for (var sq = firstSquare + 2 * deltaDirection; sq.IsValid && sq <= lastSquare; sq += deltaDirection)
        {
            between = between.Set(sq.Value - deltaDirection);
            *(ptrBetween + sq.Index0x88 - firstSquare.Index0x88) = between.RotateRight(firstSquare.Value);
            *(ptrBetween + firstSquare.Index0x88 - sq.Index0x88) = between.RotateRight(sq.Value);
        }
    }

    private static void BetweenSquareDtor(object? sender, EventArgs e)
    {
        if (ptrBetween != default)
        {
            NativeMemory.Free(ptrBetween - 120);
        }
    }

    #endregion
    /// <summary>
    ///  Returns a bitboard of all the square between two squares
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Bitboard GetBetween(Square from, Square to)
    {
        return (*(ptrBetween + to.Index0x88 - from.Index0x88)).RotateLeft(from.Value);
    }
}
