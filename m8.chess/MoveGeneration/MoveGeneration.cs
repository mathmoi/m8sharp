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
        GenerateCastlingMoves(board, moves);
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

    private static void GenerateCastlingMoves(Board board,
                                              IList<Move> moves)
    {
        for (var side = CastlingSide.KingSide; side <= CastlingSide.QueenSide; ++side)
        {
            if (board.CastlingOptions.HasFlag(CastlingOptionsHelpers.Create(board.SideToMove, side)))
            {
                // IDEA : There is lot of calculation here that could be computed once when we setup the position. Would it be faster?
                var king = new Piece(board.SideToMove, PieceType.King);
                var bbKing = board[king];
                var rank = Rank.First.FlipForBlack(board.SideToMove);
                var kingFromSq = new Square(bbKing.LSB);
                var kingToSq = new Square(side == CastlingSide.KingSide ? File.g : File.c, rank);
                var rookFromSq = new Square(board.GetCastlingFile(side), rank);
                var rookToSq = new Square(side == CastlingSide.KingSide ? File.f : File.d, rank);

                // Check if any of the travel squares are occupied
                var bbTravelKing = BetweenSquare.GetBetween(kingFromSq, kingToSq);
                var bbTravelRook = BetweenSquare.GetBetween(rookFromSq, rookToSq);
                var occupied = board.Occupied;
                occupied ^= bbKing | rookFromSq.Bitboard;
                var bbTravelOccupied = (bbTravelKing | bbTravelRook) & occupied;

                if (!bbTravelOccupied.Any)
                {
                    // Check that the origin of the king, all the squared traveled by the
                    // king and the destination of the king are not under attack.
                    var bbAttacksToCheck = bbTravelKing | bbKing | kingToSq.Bitboard;
                    var bbOpponents = board[!board.SideToMove];

                    var attackers = Bitboard.Empty;
                    while (!attackers.Any && bbAttacksToCheck.Any)
                    {
                        Square sq = new Square(bbAttacksToCheck.LSB);
                        bbAttacksToCheck = bbAttacksToCheck.RemoveLSB();
                        attackers = Attacks.AttacksTo(board, sq) & bbOpponents;
                    }

                    if (!attackers.Any)
                    {
                        moves.Add(new Move(kingFromSq, kingToSq, king, side));
                    }
                }
            }
        }
    }
}
