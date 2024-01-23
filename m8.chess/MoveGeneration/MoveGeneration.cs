using m8.chess.MoveGeneration.sliders;
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
        var targetFilter = ~board.Occupied;

        GenerateKingMoves(board, targetFilter, moves);
        GenerateKnightMoves(board, targetFilter, moves);
        GeneratesRookLikeMoves(board, targetFilter, moves);
        GeneratesBishopLikeMoves(board, targetFilter, moves);
    }

    /// <summary>
    ///  Generate all the quiet moves.
    /// </summary>
    public static void GenerateCaptures(Board board, IList<Move> moves)
    {
        var targetFilter = board[!board.SideToMove];

        GenerateKingMoves(board, targetFilter, moves);
        GenerateKnightMoves(board, targetFilter, moves);
        GeneratesRookLikeMoves(board, targetFilter, moves);
        GeneratesBishopLikeMoves(board, targetFilter, moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateKingMoves(Board board, Bitboard targetFilter, IList<Move> moves)
    {
        GenerateSimpleMoves(board,
                            new Piece(board.SideToMove, PieceType.King),
                            Attacks.kingAttacks,
                            targetFilter,
                            moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateKnightMoves(Board board, Bitboard targetFilter, IList<Move> moves)
    {
        GenerateSimpleMoves(board,
                            new Piece(board.SideToMove, PieceType.Knight),
                            Attacks.knightAttacks,
                            targetFilter,
                            moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateSimpleMoves(Board board,
                                            Piece piece,
                                            UnsafeArray<Bitboard> attackTable,
                                            Bitboard targetFilter,
                                            IList<Move> moves)
    {
        var origins = board[piece];
        while (origins.Any)
        {
            var from = new Square(origins.LSB);
            origins = origins.RemoveLSB();

            var targets = attackTable[from.Value];
            targets &= targetFilter;
            while (targets.Any)
            {
                var to = new Square(targets.LSB);
                targets = targets.RemoveLSB();

                moves.Add(new Move(from, to, piece, board[to]));
            }
        }        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GeneratesRookLikeMoves(Board board, Bitboard targetFilter, IList<Move> moves)
    {
        var bbFrom = board[new Piece(board.SideToMove, PieceType.Rook)]
                   | board[new Piece(board.SideToMove, PieceType.Queen)];
        GenerateSliderMoves(board, bbFrom, true, targetFilter, moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GeneratesBishopLikeMoves(Board board, Bitboard targetFilter, IList<Move> moves)
    {
        var bbFrom = board[new Piece(board.SideToMove, PieceType.Bishop)]
                   | board[new Piece(board.SideToMove, PieceType.Queen)];
        GenerateSliderMoves(board, bbFrom, false, targetFilter, moves);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateSliderMoves(Board board,
                                            Bitboard bbFrom,
                                            bool slideLikeRook,
                                            Bitboard targetFilter,
                                            IList<Move> moves)
    {
        while(bbFrom.Any)
        {
            var from = new Square(bbFrom.LSB);
            bbFrom = bbFrom.RemoveLSB();

            var piece = board[from];

            var bbTo = slideLikeRook ? BlackMagicSliders.GetRooksAttacks(from, board.Occupied)
                                     : BlackMagicSliders.GetBishopAttacks(from , board.Occupied);
            bbTo &= targetFilter;

            while (bbTo.Any)
            {
                var to = new Square(bbTo.LSB);
                bbTo = bbTo.RemoveLSB();

                moves.Add(new Move(from, to, piece, board[to]));
            }
        }
    }
}
