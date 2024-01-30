using FluentAssertions;

namespace m8.chess.tests.MoveGeneration;

public class MoveGenerationTests
{
    #region GenerateQuietMoves Kings

    [Fact]
    public void GenerateQuietMoves_WhiteKingOnE4_EightMovesGenerated()
    {
        var board = new Board("4k3/8/8/8/4K3/8/8/8 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e4, Square.e5, Piece.WhiteKing),
            new Move(Square.e4, Square.f5, Piece.WhiteKing),
            new Move(Square.e4, Square.f4, Piece.WhiteKing),
            new Move(Square.e4, Square.f3, Piece.WhiteKing),
            new Move(Square.e4, Square.e3, Piece.WhiteKing),
            new Move(Square.e4, Square.d3, Piece.WhiteKing),
            new Move(Square.e4, Square.d4, Piece.WhiteKing),
            new Move(Square.e4, Square.d5, Piece.WhiteKing)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_WhiteKingOnB6AllKingMovesBlocked_NoKingMovesGenerated()
    {
        var board = new Board("7k/PPP5/RKR5/NQN5/8/8/8/8 w - - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece == Piece.WhiteKing).Should().BeEmpty();
    }

    [Fact]
    public void GenerateQuietMoves_WhiteKingOnE1SomeMoveAreCaptures_OnlyQuietMovesGenerated()
    {
        var board = new Board("4k3/8/8/8/8/8/4pr2/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e1, Square.d1, Piece.WhiteKing),
            new Move(Square.e1, Square.d2, Piece.WhiteKing),
            new Move(Square.e1, Square.f1, Piece.WhiteKing)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_WhiteKingOnA1_ThreeMovesGenerated()
    {
        var board = new Board("4k3/8/8/8/8/8/8/K7 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.a1, Square.a2, Piece.WhiteKing),
            new Move(Square.a1, Square.b2, Piece.WhiteKing),
            new Move(Square.a1, Square.b1, Piece.WhiteKing)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_BlackKingOnH8_ThreeMovesGenerated()
    {
        var board = new Board("7k/8/8/8/8/8/8/K7 b - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.h8, Square.g8, Piece.BlackKing),
            new Move(Square.h8, Square.g7, Piece.BlackKing),
            new Move(Square.h8, Square.h7, Piece.BlackKing)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_WhiteKnightOnF3_EightMovesGeneratedForKnight()
    {

        var board = new Board("7k/8/8/8/8/5N2/8/K7 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.f3, Square.d2, Piece.WhiteKnight),
            new Move(Square.f3, Square.e1, Piece.WhiteKnight),
            new Move(Square.f3, Square.d4, Piece.WhiteKnight),
            new Move(Square.f3, Square.e5, Piece.WhiteKnight),
            new Move(Square.f3, Square.g5, Piece.WhiteKnight),
            new Move(Square.f3, Square.h4, Piece.WhiteKnight),
            new Move(Square.f3, Square.h2, Piece.WhiteKnight),
            new Move(Square.f3, Square.g1, Piece.WhiteKnight)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().Contain(expected);
    }

    #endregion

    #region GenerateQuietMoves Castling

    [Fact]
    public void GenerateQuietMovesTwoCastlingMovesAvailable_CastlingMovesAreGenerated()
    {
        var board = new Board("4k3/8/8/8/8/8/8/R3K2R w KQ - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e1, Square.g1, Piece.WhiteKing, CastlingSide.KingSide),
            new Move(Square.e1, Square.c1, Piece.WhiteKing, CastlingSide.QueenSide)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.CastlingSide != CastlingSide.None).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_Chess960Position_KingCastlingReturned()
    {
        var board = new Board("rk2r3/8/8/8/8/8/4P3/RK2R3 w KQkq - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b1, Square.g1, Piece.WhiteKing, CastlingSide.KingSide),
            new Move(Square.b1, Square.c1, Piece.WhiteKing, CastlingSide.QueenSide)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.CastlingSide != CastlingSide.None).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_PositionTraversedByRookAttacked_CastlingPossible()
    {
        var board = new Board("1k6/8/1r6/8/8/8/8/R3K3 w Q - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e1, Square.c1, Piece.WhiteKing, CastlingSide.QueenSide)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.CastlingSide != CastlingSide.None).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_PositionTraversedByKingAttacked_CastlingImpossible()
    {
        var board = new Board("1k6/8/3r4/8/8/8/8/R3K3 w Q - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.CastlingSide != CastlingSide.None).Should().BeEmpty();
    }

    [Fact]
    public void GenerateQuietMoves_PositionTraveledByRookOccupied_CastlingImpossible()
    {
        var board = new Board("1k6/8/8/8/8/8/8/RB2K3 w Q - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.CastlingSide != CastlingSide.None).Should().BeEmpty();
    }

    #endregion

    #region GenerateQuietMoves Knights

    [Fact]
    public void GenerateQuietMoves_BlackKnightOnG4AllKnightMovesBlocked_NoKnightMovesGenerated()
    {
        var board = new Board("4k3/8/5p1p/4p3/6n1/4p3/5p1p/3K4 b - - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece == Piece.BlackKnight).Should().BeEmpty();
    }

    [Fact]
    public void GenerateQuietMoves_WhiteKnightOnH7_ThreeMovesGeneratedForKnight()
    {
        var board = new Board("7k/7N/8/8/8/8/8/K7 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.h7, Square.f8, Piece.WhiteKnight),
            new Move(Square.h7, Square.f6, Piece.WhiteKnight),
            new Move(Square.h7, Square.g5, Piece.WhiteKnight)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().Contain(expected);
    }

    #endregion

    #region GenerateQuietMoves Rooks

    [Fact]
    public void GenerateQuietMoves_WhiteRookOnB2NoBlockers_14MovesGenerated()
    {
        var board = new Board("4k3/8/8/8/8/8/1R6/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b2, Square.b1, Piece.WhiteRook),
            new Move(Square.b2, Square.b3, Piece.WhiteRook),
            new Move(Square.b2, Square.b4, Piece.WhiteRook),
            new Move(Square.b2, Square.b5, Piece.WhiteRook),
            new Move(Square.b2, Square.b6, Piece.WhiteRook),
            new Move(Square.b2, Square.b7, Piece.WhiteRook),
            new Move(Square.b2, Square.b8, Piece.WhiteRook),
            new Move(Square.b2, Square.a2, Piece.WhiteRook),
            new Move(Square.b2, Square.c2, Piece.WhiteRook),
            new Move(Square.b2, Square.d2, Piece.WhiteRook),
            new Move(Square.b2, Square.e2, Piece.WhiteRook),
            new Move(Square.b2, Square.f2, Piece.WhiteRook),
            new Move(Square.b2, Square.g2, Piece.WhiteRook),
            new Move(Square.b2, Square.h2, Piece.WhiteRook)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece == Piece.WhiteRook).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_WhiteRookOnF6BlockersOnEdge_14MovesGenerated()
    {
        var board = new Board("4kn2/8/N4R1n/8/8/8/8/4KN2 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.f6, Square.b6, Piece.WhiteRook),
            new Move(Square.f6, Square.c6, Piece.WhiteRook),
            new Move(Square.f6, Square.d6, Piece.WhiteRook),
            new Move(Square.f6, Square.e6, Piece.WhiteRook),
            new Move(Square.f6, Square.g6, Piece.WhiteRook),
            new Move(Square.f6, Square.f2, Piece.WhiteRook),
            new Move(Square.f6, Square.f3, Piece.WhiteRook),
            new Move(Square.f6, Square.f4, Piece.WhiteRook),
            new Move(Square.f6, Square.f5, Piece.WhiteRook),
            new Move(Square.f6, Square.f7, Piece.WhiteRook)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece == Piece.WhiteRook).Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GenerateQuietMoves Bishops

    [Fact]
    public void GenerateQuietMoves_BlackBishopOnD6BlockersNextToBishop_NoMovesGenerated()
    {
        var board = new Board("4k3/2n1n3/3b4/2N1N3/8/8/8/4K3 b - - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece == Piece.BlackBishop).Should().BeEmpty();
    }

    #endregion

    #region GenerateQuietMoves Queen

    [Fact]
    public void GenerateQuietMoves_BlackQueenOnE5BlockersAwayFromRook_8MovesGenerated()
    {
        var board = new Board("n3k3/1n1p1n2/8/1N1q1n1n/8/1R1P4/3P2n1/4K2n b - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.d5, Square.c6, Piece.BlackQueen),
            new Move(Square.d5, Square.d6, Piece.BlackQueen),
            new Move(Square.d5, Square.e6, Piece.BlackQueen),
            new Move(Square.d5, Square.e5, Piece.BlackQueen),
            new Move(Square.d5, Square.e4, Piece.BlackQueen),
            new Move(Square.d5, Square.d4, Piece.BlackQueen),
            new Move(Square.d5, Square.c4, Piece.BlackQueen),
            new Move(Square.d5, Square.c5, Piece.BlackQueen),
            new Move(Square.d5, Square.f3, Piece.BlackQueen)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece == Piece.BlackQueen).Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GenerateQuietMoves Pawns

    [Fact]
    public void GenerateQuietMoves_NoPawnForSideToMove_NoMoves()
    {
        var board = new Board("3k4/3p4/8/8/8/8/8/3K4 w - - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEmpty();
    }

    [Fact]
    public void GenerateQuietMoves_TwoPawnsThatCanMoveForward_TwoMovesREturned()
    {
        var board = new Board("4k3/8/8/8/5P2/3P4/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.d3, Square.d4, Piece.WhitePawn),
            new Move(Square.f4, Square.f5, Piece.WhitePawn)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMovesBlockedPawn_NoMoves()
    {
        var board = new Board("4k3/8/8/8/3R4/3P4/8/4K3 w - - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEmpty();
    }

    [Fact]
    public void GenerateQuietMoves_PawnOnStartingRow_TwoMovesReturned()
    {
        var board = new Board("4k3/3p4/8/8/8/8/8/4K3 b - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.d7, Square.d5, Piece.BlackPawn),
            new Move(Square.d7, Square.d6, Piece.BlackPawn)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_PawnOnStartingRowDoubleMoveBlocked_OneMoveReturned()
    {
        var board = new Board("4k3/3p4/8/3n4/8/8/8/4K3 b - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.d7, Square.d6, Piece.BlackPawn)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    // TODO : GeneratePawnMoves_PawnOnStartingRowBlocked_NoMoves
    [Fact]
    public void GenerateQuietMoves_BlockedPawn_NoMoves()
    {
        var board = new Board("4k3/3p4/3n4/8/8/8/8/4K3 b - - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEmpty();
    }

    #endregion

    #region GenerateCaptures Kings

    [Fact]
    public void GenerateCaptures_WhiteKingOnF1TwoCapturesAvailable_TwoCapturesMovesGenerated()
    {
        var board = new Board("4k3/8/8/8/8/8/4pr2/5K2 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.f1, Square.e2, Piece.WhiteKing, Piece.BlackPawn),
            new Move(Square.f1, Square.f2, Piece.WhiteKing, Piece.BlackRook)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GenerateCaptures Knights

    [Fact]
    public void GenerateCaptures_BlackKnightOnC4TwoCapturesAvailable_TwoCapturesMovesGenerated()
    {
        var board = new Board("4k3/8/8/8/2n5/8/1P1P4/4K3 b - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.c4, Square.b2, Piece.BlackKnight, Piece.WhitePawn),
            new Move(Square.c4, Square.d2, Piece.BlackKnight, Piece.WhitePawn)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GenerateCaptures Queens

    [Fact]
    public void GenerateCaptures_BlackQueenOnG4OwnAndOpponentBlockers_OnlyOpponentAreCaptured()
    {
        var board = new Board("8/1k1N2N1/6r1/8/1N4qN/5r1r/4N1N1/4K3 b - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.g4, Square.d7, Piece.BlackQueen, Piece.WhiteKnight),
            new Move(Square.g4, Square.b4, Piece.BlackQueen, Piece.WhiteKnight),
            new Move(Square.g4, Square.h4, Piece.BlackQueen, Piece.WhiteKnight),
            new Move(Square.g4, Square.g2, Piece.BlackQueen, Piece.WhiteKnight)

        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece == Piece.BlackQueen).Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GenerateCaptures Pawms

    [Fact]
    public void GenerateQuietMoves_OneCapturesAvailableOnLeft_OneMovesReturned()
    {
        var board = new Board("4k3/8/8/3n4/4P3/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e4, Square.d5, Piece.WhitePawn, Piece.BlackKnight)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_OneCapturesAvailableOnRight_OneMovesReturned()
    {
        var board = new Board("4k3/8/8/5n2/4P3/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e4, Square.f5, Piece.WhitePawn, Piece.BlackKnight)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_OnePawnInPositionToPromote_FourMovesReturned()
    {
        var board = new Board("4k3/1P6/8/8/8/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteQueen),
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteRook),
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteBishop),
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteKnight)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_OnePawnInPositionToCaptureAndPromote_FourMovesReturned()
    {
        var board = new Board("1Rr1k3/1P6/8/8/8/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteQueen),
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteRook),
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteBishop),
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteKnight)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    // TODO : GeneratePawnCaptures_PriseEnPassantPossible_PriseEnPassantReturned
    [Fact]
    public void GenerateQuietMoves_PriseEnPassantPossible_PriseEnPassantReturned()
    {
        var board = new Board("rnbqkbnr/p2ppppp/8/1Pp5/8/8/1PPPPPPP/RNBQKBNR w KQkq c6 0 1");
        var actual = new List<Move>();
        var expected = new Move(Square.b5, Square.c6, Piece.WhitePawn, Piece.BlackPawn);

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Should().Contain(expected);
    }

    #endregion

    // TODO : Ajouter un test pour vérifier que cette position à 218 coups et qu'on peut bien les générer : R6R/3Q4/1Q4Q1/4Q3/2Q4Q/Q4Q2/pp1Q4/kBNNK1B1 b - - 0 1
}
