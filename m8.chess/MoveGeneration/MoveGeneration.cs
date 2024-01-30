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
        GenerateCastlingMoves(board, moves);
        GenerateKnightMoves(board, targetFilter, moves);
        GeneratesRookLikeMoves(board, targetFilter, moves);
        GeneratesBishopLikeMoves(board, targetFilter, moves);
        GeneratePawnQuietMoves(board, moves);
    }

    /// <summary>
    ///  Generate all the quiet moves.
    /// </summary>
    public static void GenerateCaptures(Board board, IList<Move> moves)
    {
        var targetFilter = board[board.SideToMove.Opposite];

        GenerateKingMoves(board, targetFilter, moves);
        GenerateKnightMoves(board, targetFilter, moves);
        GeneratesRookLikeMoves(board, targetFilter, moves);
        GeneratesBishopLikeMoves(board, targetFilter, moves);

        GeneratePawnCaptures(board, targetFilter, moves);
        GeneratePawnPriseEnPassant(board, moves);
        GeneratePawnPromotions(board, 8 - 16 * board.SideToMove.Value, ~board.Occupied, moves);
        GeneratePawnPromotions(board, 7 - 16 * board.SideToMove.Value, board[board.SideToMove.Opposite], moves);
        GeneratePawnPromotions(board, 9 - 16 * board.SideToMove.Value, board[board.SideToMove.Opposite], moves);
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
                    var bbOpponents = board[board.SideToMove.Opposite];

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

    #region Pawn moves generation

    private static void GeneratePawnQuietMoves(Board board,
                                               IList<Move> moves)
    {
        Piece piece = new Piece(board.SideToMove, PieceType.Pawn);
        int forwardDelta = 8 - 16 * board.SideToMove.Value;

        // Generate the standard one square forward moves. We need to exclude pawns on 
        // the 7th rank that will generate promotions.
        Bitboard destinations = board[piece] & ~Rank.Seventh.FlipForBlack(board.SideToMove).Bitboard;
        destinations = destinations.Shift(forwardDelta);
        destinations &= ~board.Occupied;
        UnpackPawnMoves(board, destinations, piece, -forwardDelta, moves);

        // Generate the two squares moves
        destinations = destinations & Rank.Third.FlipForBlack(board.SideToMove).Bitboard;
        destinations = destinations.Shift(forwardDelta);
        destinations &= ~board.Occupied;
        UnpackPawnMoves(board, destinations, piece, -forwardDelta * 2, moves);
    }

    private static void GeneratePawnCaptures(Board board,
                                             Bitboard targetFilter,
                                             IList<Move> moves)
    {
        Piece piece = new Piece(board.SideToMove, PieceType.Pawn);

        // We must exclude the captures that are also promotions
        targetFilter &= ~Rank.Eight.FlipForBlack(board.SideToMove).Bitboard;

        // Go left
        int delta = 7 - 16 * board.SideToMove.Value;
        var destinations = board[piece] & ~File.a.Bitboard;
        destinations = destinations.Shift(delta);
        destinations &= targetFilter;
        UnpackPawnMoves(board, destinations, piece, -delta, moves);

        // Go right
        delta = 9 - 16 * board.SideToMove.Value;
        destinations = board[piece] & ~File.h.Bitboard;
        destinations = destinations.Shift(delta);
        destinations &= targetFilter;
        UnpackPawnMoves(board, destinations, piece, -delta, moves);
    }

    private static void GeneratePawnPriseEnPassant(Board board,
                                                   IList<Move> moves)
    {
        if (board.EnPassantFile.IsValid)
        {
            var piece = new Piece(board.SideToMove, PieceType.Pawn);
            var rankFrom = Rank.Fifth.FlipForBlack(board.SideToMove);

            if (board.EnPassantFile > File.a)
            {
                var from = new Square(board.EnPassantFile.MoveLeft(), rankFrom);
                if (board[from] == piece)
                {
                    var to = new Square(board.EnPassantFile, Rank.Sixth.FlipForBlack(board.SideToMove));
                    var captured = new Piece(board.SideToMove.Opposite, PieceType.Pawn);
                    moves.Add(new Move(from, to, piece, captured));
                }
            }

            if (board.EnPassantFile < File.h)
            {
                var from = new Square(board.EnPassantFile.MoveRight(), rankFrom);
                if (board[from] == piece)
                {
                    var to = new Square(board.EnPassantFile, Rank.Sixth.FlipForBlack(board.SideToMove));
                    var captured = new Piece(board.SideToMove.Opposite, PieceType.Pawn);
                    moves.Add(new Move(from, to, piece, captured));
                }
            }
        }
    }

    // IDEA : This method (and others) are called from a single place. Maybe it would be a good idea to inline them even if they are not small.
    private static void GeneratePawnPromotions(Board board,
                                               int delta,
                                               Bitboard targetFilter,
                                               IList<Move> moves)
    {
        Piece piece = new Piece(board.SideToMove, PieceType.Pawn);

        Bitboard destinations = board[piece] & Rank.Seventh.FlipForBlack(board.SideToMove).Bitboard;
        destinations = destinations.Shift(delta);
        destinations &= targetFilter;

        while (destinations.Any)
        {
            var to = new Square(destinations.LSB);
            var from = new Square(to.Value - delta);
            moves.Add(new Move(from, to, piece, board[to], new Piece(board.SideToMove, PieceType.Queen)));
            moves.Add(new Move(from, to, piece, board[to], new Piece(board.SideToMove, PieceType.Rook)));
            moves.Add(new Move(from, to, piece, board[to], new Piece(board.SideToMove, PieceType.Bishop)));
            moves.Add(new Move(from, to, piece, board[to], new Piece(board.SideToMove, PieceType.Knight)));

            destinations = destinations.RemoveLSB();
        }
    }

    // TODO : Test if the inlining is justified
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void UnpackPawnMoves(Board board, Bitboard destinations, Piece piece, int fromDelta, IList<Move> moves)
    {
        while (destinations.Any)
        {
            var to = new Square(destinations.LSB);
            var from = new Square(to.Value + fromDelta);
            moves.Add(new Move(from, to, piece, board[to]));

            destinations = destinations.RemoveLSB();
        }
    }

    #endregion
}
