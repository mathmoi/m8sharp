using m8.common;
using m8.common.Collections;
using System.Runtime.CompilerServices;

namespace m8.chess.MoveGeneration;

/// <summary>
///  Class responsible to generate moves for a position on 
/// </summary>
public static class MoveGeneration
{
    /// <summary>
    ///  Generate all the quiet moves.
    /// </summary>
    public static void GenerateQuietMoves(Board board, IList<Move> moves)
    {
        GenerateKingMoves(board, moves);
        GenerateKnightMoves(board, moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateKingMoves(Board board, IList<Move> moves)
    {
        GenerateSimpleMoves(board,
                            new Piece(board.SideToMove, PieceType.King),
                            Attacks.KingAttaks,
                            moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateKnightMoves(Board board, IList<Move> moves)
    {
        GenerateSimpleMoves(board,
                            new Piece(board.SideToMove, PieceType.Knight),
                            Attacks.KnightAttaks,
                            moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateSimpleMoves(Board board,
                                            Piece piece,
                                            UnsafeArray<Bitboard> attackTable,
                                            IList<Move> moves)
    {
        var origins = board[piece];
        while (origins)
        {
            var from = new Square(origins.LSB);
            origins = origins.RemoveLSB();

            var targets = attackTable[(byte)from];
            while (targets)
            {
                var to = new Square(targets.LSB);
                targets = targets.RemoveLSB();

                moves.Add(new Move(from, to, piece));
            }
        }        
    }
}
