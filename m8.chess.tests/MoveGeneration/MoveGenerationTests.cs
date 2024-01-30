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

    #region Move generation "integrated" tests

    [Fact]
    public void GenerateQuietMoves_StartingPosition_AllQuietMovesReturned()
    {
        var board = new Board();
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.a2, Square.a3, Piece.WhitePawn),   // a3
            new Move(Square.b2, Square.b3, Piece.WhitePawn),   // b3
            new Move(Square.c2, Square.c3, Piece.WhitePawn),   // c3
            new Move(Square.d2, Square.d3, Piece.WhitePawn),   // d3
            new Move(Square.e2, Square.e3, Piece.WhitePawn),   // e3
            new Move(Square.f2, Square.f3, Piece.WhitePawn),   // f3
            new Move(Square.g2, Square.g3, Piece.WhitePawn),   // g3
            new Move(Square.h2, Square.h3, Piece.WhitePawn),   // h3
            new Move(Square.a2, Square.a4, Piece.WhitePawn),   // a4
            new Move(Square.b2, Square.b4, Piece.WhitePawn),   // b4
            new Move(Square.c2, Square.c4, Piece.WhitePawn),   // c4
            new Move(Square.d2, Square.d4, Piece.WhitePawn),   // d4
            new Move(Square.e2, Square.e4, Piece.WhitePawn),   // e4
            new Move(Square.f2, Square.f4, Piece.WhitePawn),   // f4
            new Move(Square.g2, Square.g4, Piece.WhitePawn),   // g4
            new Move(Square.h2, Square.h4, Piece.WhitePawn),   // h4
            new Move(Square.b1, Square.a3, Piece.WhiteKnight), // Na3
            new Move(Square.b1, Square.c3, Piece.WhiteKnight), // Nc3
            new Move(Square.g1, Square.f3, Piece.WhiteKnight), // Nf3
            new Move(Square.g1, Square.h3, Piece.WhiteKnight)  // Nh3
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_PositionWithLotsOfMoves_AllQuietMovesReturned()
    {
        var board = new Board("R6R/3Q4/1Q4Q1/4Q3/2Q4Q/Q4Q2/pp1Q4/kBNNK1B1 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.c1, Square.e2, Piece.WhiteKnight), // Ne2
            new Move(Square.c1, Square.b3, Piece.WhiteKnight), // Nb3#
            new Move(Square.c1, Square.d3, Piece.WhiteKnight), // Nd3
            new Move(Square.d1, Square.f2, Piece.WhiteKnight), // Nf2
            new Move(Square.d1, Square.c3, Piece.WhiteKnight), // Nc3
            new Move(Square.d1, Square.e3, Piece.WhiteKnight), // Ne3
            new Move(Square.b1, Square.c2, Piece.WhiteBishop), // Bc2
            new Move(Square.b1, Square.d3, Piece.WhiteBishop), // Bd3
            new Move(Square.b1, Square.e4, Piece.WhiteBishop), // Be4
            new Move(Square.b1, Square.f5, Piece.WhiteBishop), // Bf5
            new Move(Square.g1, Square.f2, Piece.WhiteBishop), // Bf2
            new Move(Square.g1, Square.h2, Piece.WhiteBishop), // Bh2
            new Move(Square.g1, Square.e3, Piece.WhiteBishop), // Be3
            new Move(Square.g1, Square.d4, Piece.WhiteBishop), // Bd4
            new Move(Square.g1, Square.c5, Piece.WhiteBishop), // Bc5
            new Move(Square.a8, Square.a4, Piece.WhiteRook),   // Ra4
            new Move(Square.a8, Square.a5, Piece.WhiteRook),   // Ra5
            new Move(Square.a8, Square.a6, Piece.WhiteRook),   // Ra6
            new Move(Square.a8, Square.a7, Piece.WhiteRook),   // Ra7
            new Move(Square.a8, Square.b8, Piece.WhiteRook),   // Rab8
            new Move(Square.a8, Square.c8, Piece.WhiteRook),   // Rac8
            new Move(Square.a8, Square.d8, Piece.WhiteRook),   // Rad8
            new Move(Square.a8, Square.e8, Piece.WhiteRook),   // Rae8
            new Move(Square.a8, Square.f8, Piece.WhiteRook),   // Raf8
            new Move(Square.a8, Square.g8, Piece.WhiteRook),   // Rag8
            new Move(Square.h8, Square.h5, Piece.WhiteRook),   // Rh5
            new Move(Square.h8, Square.h6, Piece.WhiteRook),   // Rh6
            new Move(Square.h8, Square.h7, Piece.WhiteRook),   // Rh7
            new Move(Square.h8, Square.b8, Piece.WhiteRook),   // Rhb8
            new Move(Square.h8, Square.c8, Piece.WhiteRook),   // Rhc8
            new Move(Square.h8, Square.d8, Piece.WhiteRook),   // Rhd8
            new Move(Square.h8, Square.e8, Piece.WhiteRook),   // Rhe8
            new Move(Square.h8, Square.f8, Piece.WhiteRook),   // Rhf8
            new Move(Square.h8, Square.g8, Piece.WhiteRook),   // Rhg8
            new Move(Square.d2, Square.c2, Piece.WhiteQueen),  // Qdc2
            new Move(Square.d2, Square.e2, Piece.WhiteQueen),  // Qde2
            new Move(Square.d2, Square.f2, Piece.WhiteQueen),  // Qdf2
            new Move(Square.d2, Square.g2, Piece.WhiteQueen),  // Qdg2
            new Move(Square.d2, Square.h2, Piece.WhiteQueen),  // Qdh2
            new Move(Square.d2, Square.c3, Piece.WhiteQueen),  // Qdc3
            new Move(Square.d2, Square.d3, Piece.WhiteQueen),  // Q2d3
            new Move(Square.d2, Square.e3, Piece.WhiteQueen),  // Qde3
            new Move(Square.d2, Square.b4, Piece.WhiteQueen),  // Qdb4
            new Move(Square.d2, Square.d4, Piece.WhiteQueen),  // Q2d4
            new Move(Square.d2, Square.f4, Piece.WhiteQueen),  // Qdf4
            new Move(Square.d2, Square.a5, Piece.WhiteQueen),  // Qda5
            new Move(Square.d2, Square.d5, Piece.WhiteQueen),  // Q2d5
            new Move(Square.d2, Square.g5, Piece.WhiteQueen),  // Qdg5
            new Move(Square.d2, Square.d6, Piece.WhiteQueen),  // Q2d6
            new Move(Square.d2, Square.h6, Piece.WhiteQueen),  // Qdh6
            new Move(Square.a3, Square.b3, Piece.WhiteQueen),  // Qab3
            new Move(Square.a3, Square.c3, Piece.WhiteQueen),  // Qac3
            new Move(Square.a3, Square.d3, Piece.WhiteQueen),  // Qad3
            new Move(Square.a3, Square.e3, Piece.WhiteQueen),  // Qae3
            new Move(Square.a3, Square.a4, Piece.WhiteQueen),  // Qaa4
            new Move(Square.a3, Square.b4, Piece.WhiteQueen),  // Qab4
            new Move(Square.a3, Square.a5, Piece.WhiteQueen),  // Qaa5
            new Move(Square.a3, Square.c5, Piece.WhiteQueen),  // Qac5
            new Move(Square.a3, Square.a6, Piece.WhiteQueen),  // Qaa6
            new Move(Square.a3, Square.d6, Piece.WhiteQueen),  // Qad6
            new Move(Square.a3, Square.a7, Piece.WhiteQueen),  // Qaa7
            new Move(Square.a3, Square.e7, Piece.WhiteQueen),  // Qae7
            new Move(Square.a3, Square.f8, Piece.WhiteQueen),  // Qaf8
            new Move(Square.f3, Square.f1, Piece.WhiteQueen),  // Qff1
            new Move(Square.f3, Square.h1, Piece.WhiteQueen),  // Qfh1
            new Move(Square.f3, Square.e2, Piece.WhiteQueen),  // Qfe2
            new Move(Square.f3, Square.f2, Piece.WhiteQueen),  // Qff2
            new Move(Square.f3, Square.g2, Piece.WhiteQueen),  // Qfg2
            new Move(Square.f3, Square.b3, Piece.WhiteQueen),  // Qfb3
            new Move(Square.f3, Square.c3, Piece.WhiteQueen),  // Qfc3
            new Move(Square.f3, Square.d3, Piece.WhiteQueen),  // Qfd3
            new Move(Square.f3, Square.e3, Piece.WhiteQueen),  // Qfe3
            new Move(Square.f3, Square.g3, Piece.WhiteQueen),  // Qfg3
            new Move(Square.f3, Square.h3, Piece.WhiteQueen),  // Qfh3
            new Move(Square.f3, Square.e4, Piece.WhiteQueen),  // Qfe4
            new Move(Square.f3, Square.f4, Piece.WhiteQueen),  // Qff4
            new Move(Square.f3, Square.g4, Piece.WhiteQueen),  // Qfg4
            new Move(Square.f3, Square.d5, Piece.WhiteQueen),  // Qfd5
            new Move(Square.f3, Square.f5, Piece.WhiteQueen),  // Qff5
            new Move(Square.f3, Square.h5, Piece.WhiteQueen),  // Qfh5
            new Move(Square.f3, Square.c6, Piece.WhiteQueen),  // Qfc6
            new Move(Square.f3, Square.f6, Piece.WhiteQueen),  // Qff6
            new Move(Square.f3, Square.b7, Piece.WhiteQueen),  // Qfb7
            new Move(Square.f3, Square.f7, Piece.WhiteQueen),  // Qff7
            new Move(Square.f3, Square.f8, Piece.WhiteQueen),  // Qff8
            new Move(Square.c4, Square.f1, Piece.WhiteQueen),  // Qcf1
            new Move(Square.c4, Square.c2, Piece.WhiteQueen),  // Qcc2
            new Move(Square.c4, Square.e2, Piece.WhiteQueen),  // Qce2
            new Move(Square.c4, Square.b3, Piece.WhiteQueen),  // Qcb3
            new Move(Square.c4, Square.c3, Piece.WhiteQueen),  // Qcc3
            new Move(Square.c4, Square.d3, Piece.WhiteQueen),  // Qcd3
            new Move(Square.c4, Square.a4, Piece.WhiteQueen),  // Qca4
            new Move(Square.c4, Square.b4, Piece.WhiteQueen),  // Qcb4
            new Move(Square.c4, Square.d4, Piece.WhiteQueen),  // Qcd4
            new Move(Square.c4, Square.e4, Piece.WhiteQueen),  // Qce4
            new Move(Square.c4, Square.f4, Piece.WhiteQueen),  // Qcf4
            new Move(Square.c4, Square.g4, Piece.WhiteQueen),  // Qcg4
            new Move(Square.c4, Square.b5, Piece.WhiteQueen),  // Qcb5
            new Move(Square.c4, Square.c5, Piece.WhiteQueen),  // Qcc5
            new Move(Square.c4, Square.d5, Piece.WhiteQueen),  // Qcd5
            new Move(Square.c4, Square.a6, Piece.WhiteQueen),  // Qca6
            new Move(Square.c4, Square.c6, Piece.WhiteQueen),  // Qcc6
            new Move(Square.c4, Square.e6, Piece.WhiteQueen),  // Qce6
            new Move(Square.c4, Square.c7, Piece.WhiteQueen),  // Qcc7
            new Move(Square.c4, Square.f7, Piece.WhiteQueen),  // Qcf7
            new Move(Square.c4, Square.c8, Piece.WhiteQueen),  // Qcc8
            new Move(Square.c4, Square.g8, Piece.WhiteQueen),  // Qcg8
            new Move(Square.h4, Square.h1, Piece.WhiteQueen),  // Qhh1
            new Move(Square.h4, Square.f2, Piece.WhiteQueen),  // Qhf2
            new Move(Square.h4, Square.h2, Piece.WhiteQueen),  // Qhh2
            new Move(Square.h4, Square.g3, Piece.WhiteQueen),  // Qhg3
            new Move(Square.h4, Square.h3, Piece.WhiteQueen),  // Qhh3
            new Move(Square.h4, Square.d4, Piece.WhiteQueen),  // Qhd4
            new Move(Square.h4, Square.e4, Piece.WhiteQueen),  // Qhe4
            new Move(Square.h4, Square.f4, Piece.WhiteQueen),  // Qhf4
            new Move(Square.h4, Square.g4, Piece.WhiteQueen),  // Qhg4
            new Move(Square.h4, Square.g5, Piece.WhiteQueen),  // Qhg5
            new Move(Square.h4, Square.h5, Piece.WhiteQueen),  // Qhh5
            new Move(Square.h4, Square.f6, Piece.WhiteQueen),  // Qhf6
            new Move(Square.h4, Square.h6, Piece.WhiteQueen),  // Qhh6
            new Move(Square.h4, Square.e7, Piece.WhiteQueen),  // Qhe7
            new Move(Square.h4, Square.h7, Piece.WhiteQueen),  // Qhh7
            new Move(Square.h4, Square.d8, Piece.WhiteQueen),  // Qhd8
            new Move(Square.e5, Square.e2, Piece.WhiteQueen),  // Qee2
            new Move(Square.e5, Square.h2, Piece.WhiteQueen),  // Qeh2
            new Move(Square.e5, Square.c3, Piece.WhiteQueen),  // Qec3
            new Move(Square.e5, Square.e3, Piece.WhiteQueen),  // Qee3
            new Move(Square.e5, Square.g3, Piece.WhiteQueen),  // Qeg3
            new Move(Square.e5, Square.d4, Piece.WhiteQueen),  // Qed4
            new Move(Square.e5, Square.e4, Piece.WhiteQueen),  // Qee4
            new Move(Square.e5, Square.f4, Piece.WhiteQueen),  // Qef4
            new Move(Square.e5, Square.a5, Piece.WhiteQueen),  // Qea5
            new Move(Square.e5, Square.b5, Piece.WhiteQueen),  // Qeb5
            new Move(Square.e5, Square.c5, Piece.WhiteQueen),  // Qec5
            new Move(Square.e5, Square.d5, Piece.WhiteQueen),  // Qed5
            new Move(Square.e5, Square.f5, Piece.WhiteQueen),  // Qef5
            new Move(Square.e5, Square.g5, Piece.WhiteQueen),  // Qeg5
            new Move(Square.e5, Square.h5, Piece.WhiteQueen),  // Qeh5
            new Move(Square.e5, Square.d6, Piece.WhiteQueen),  // Qed6
            new Move(Square.e5, Square.e6, Piece.WhiteQueen),  // Qee6
            new Move(Square.e5, Square.f6, Piece.WhiteQueen),  // Qef6
            new Move(Square.e5, Square.c7, Piece.WhiteQueen),  // Qec7
            new Move(Square.e5, Square.e7, Piece.WhiteQueen),  // Qee7
            new Move(Square.e5, Square.g7, Piece.WhiteQueen),  // Qeg7
            new Move(Square.e5, Square.b8, Piece.WhiteQueen),  // Qeb8
            new Move(Square.e5, Square.e8, Piece.WhiteQueen),  // Qee8
            new Move(Square.b6, Square.f2, Piece.WhiteQueen),  // Qbf2
            new Move(Square.b6, Square.b3, Piece.WhiteQueen),  // Qbb3
            new Move(Square.b6, Square.e3, Piece.WhiteQueen),  // Qbe3
            new Move(Square.b6, Square.b4, Piece.WhiteQueen),  // Qbb4
            new Move(Square.b6, Square.d4, Piece.WhiteQueen),  // Qbd4
            new Move(Square.b6, Square.a5, Piece.WhiteQueen),  // Qba5
            new Move(Square.b6, Square.b5, Piece.WhiteQueen),  // Qbb5
            new Move(Square.b6, Square.c5, Piece.WhiteQueen),  // Qbc5
            new Move(Square.b6, Square.a6, Piece.WhiteQueen),  // Qba6
            new Move(Square.b6, Square.c6, Piece.WhiteQueen),  // Qbc6
            new Move(Square.b6, Square.d6, Piece.WhiteQueen),  // Qbd6
            new Move(Square.b6, Square.e6, Piece.WhiteQueen),  // Qbe6
            new Move(Square.b6, Square.f6, Piece.WhiteQueen),  // Qbf6
            new Move(Square.b6, Square.a7, Piece.WhiteQueen),  // Qba7
            new Move(Square.b6, Square.b7, Piece.WhiteQueen),  // Qbb7
            new Move(Square.b6, Square.c7, Piece.WhiteQueen),  // Qbc7
            new Move(Square.b6, Square.b8, Piece.WhiteQueen),  // Qbb8
            new Move(Square.b6, Square.d8, Piece.WhiteQueen),  // Qbd8
            new Move(Square.g6, Square.c2, Piece.WhiteQueen),  // Qgc2
            new Move(Square.g6, Square.g2, Piece.WhiteQueen),  // Qgg2
            new Move(Square.g6, Square.d3, Piece.WhiteQueen),  // Qgd3
            new Move(Square.g6, Square.g3, Piece.WhiteQueen),  // Qgg3
            new Move(Square.g6, Square.e4, Piece.WhiteQueen),  // Qge4
            new Move(Square.g6, Square.g4, Piece.WhiteQueen),  // Qgg4
            new Move(Square.g6, Square.f5, Piece.WhiteQueen),  // Qgf5
            new Move(Square.g6, Square.g5, Piece.WhiteQueen),  // Qgg5
            new Move(Square.g6, Square.h5, Piece.WhiteQueen),  // Qgh5
            new Move(Square.g6, Square.c6, Piece.WhiteQueen),  // Qgc6
            new Move(Square.g6, Square.d6, Piece.WhiteQueen),  // Qgd6
            new Move(Square.g6, Square.e6, Piece.WhiteQueen),  // Qge6
            new Move(Square.g6, Square.f6, Piece.WhiteQueen),  // Qgf6
            new Move(Square.g6, Square.h6, Piece.WhiteQueen),  // Qgh6
            new Move(Square.g6, Square.f7, Piece.WhiteQueen),  // Qgf7
            new Move(Square.g6, Square.g7, Piece.WhiteQueen),  // Qgg7
            new Move(Square.g6, Square.h7, Piece.WhiteQueen),  // Qgh7
            new Move(Square.g6, Square.e8, Piece.WhiteQueen),  // Qge8
            new Move(Square.g6, Square.g8, Piece.WhiteQueen),  // Qgg8
            new Move(Square.d7, Square.d3, Piece.WhiteQueen),  // Q7d3
            new Move(Square.d7, Square.h3, Piece.WhiteQueen),  // Qdh3
            new Move(Square.d7, Square.a4, Piece.WhiteQueen),  // Qda4
            new Move(Square.d7, Square.d4, Piece.WhiteQueen),  // Q7d4
            new Move(Square.d7, Square.g4, Piece.WhiteQueen),  // Qdg4
            new Move(Square.d7, Square.b5, Piece.WhiteQueen),  // Qdb5
            new Move(Square.d7, Square.d5, Piece.WhiteQueen),  // Q7d5
            new Move(Square.d7, Square.f5, Piece.WhiteQueen),  // Qdf5
            new Move(Square.d7, Square.c6, Piece.WhiteQueen),  // Qdc6
            new Move(Square.d7, Square.d6, Piece.WhiteQueen),  // Q7d6
            new Move(Square.d7, Square.e6, Piece.WhiteQueen),  // Qde6
            new Move(Square.d7, Square.a7, Piece.WhiteQueen),  // Qda7
            new Move(Square.d7, Square.b7, Piece.WhiteQueen),  // Qdb7
            new Move(Square.d7, Square.c7, Piece.WhiteQueen),  // Qdc7
            new Move(Square.d7, Square.e7, Piece.WhiteQueen),  // Qde7
            new Move(Square.d7, Square.f7, Piece.WhiteQueen),  // Qdf7
            new Move(Square.d7, Square.g7, Piece.WhiteQueen),  // Qdg7
            new Move(Square.d7, Square.h7, Piece.WhiteQueen),  // Qdh7
            new Move(Square.d7, Square.c8, Piece.WhiteQueen),  // Qdc8
            new Move(Square.d7, Square.d8, Piece.WhiteQueen),  // Qdd8
            new Move(Square.d7, Square.e8, Piece.WhiteQueen),  // Qde8
            new Move(Square.e1, Square.f1, Piece.WhiteKing),   // Kf1
            new Move(Square.e1, Square.e2, Piece.WhiteKing),   // Ke2
            new Move(Square.e1, Square.f2, Piece.WhiteKing)    // Kf2
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_PositionWithLotsOfMoves_AllQuietMovesReturned()
    {
        var board = new Board("R6R/3Q4/1Q4Q1/4Q3/2Q4Q/Q4Q2/pp1Q4/kBNNK1B1 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.c1, Square.a2, Piece.WhiteKnight, Piece.BlackPawn), // Nxa2>
            new Move(Square.d1, Square.b2, Piece.WhiteKnight, Piece.BlackPawn), // Nxb2
            new Move(Square.b1, Square.a2, Piece.WhiteBishop, Piece.BlackPawn), // Bxa2
            new Move(Square.d2, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn), // Qdxb2#
            new Move(Square.a3, Square.a2, Piece.WhiteQueen,  Piece.BlackPawn), // Qaxa2#
            new Move(Square.a3, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn), // Qaxb2#
            new Move(Square.c4, Square.a2, Piece.WhiteQueen,  Piece.BlackPawn), // Qcxa2#
            new Move(Square.e5, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn), // Qexb2#
            new Move(Square.b6, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn)  // Qbxb2#
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_KiwipetePosition_AllQuietMovesReturned()
    {
        var board = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.a2, Square.a3, Piece.WhitePawn),   // a3
            new Move(Square.b2, Square.b3, Piece.WhitePawn),   // b3
            new Move(Square.g2, Square.g3, Piece.WhitePawn),   // g3
            new Move(Square.d5, Square.d6, Piece.WhitePawn),   // d6
            new Move(Square.a2, Square.a4, Piece.WhitePawn),   // a4
            new Move(Square.g2, Square.g4, Piece.WhitePawn),   // g4
            new Move(Square.c3, Square.b1, Piece.WhiteKnight), // Nb1
            new Move(Square.c3, Square.d1, Piece.WhiteKnight), // Nd1
            new Move(Square.c3, Square.a4, Piece.WhiteKnight), // Na4
            new Move(Square.c3, Square.b5, Piece.WhiteKnight), // Nb5
            new Move(Square.e5, Square.d3, Piece.WhiteKnight), // Nd3
            new Move(Square.e5, Square.c4, Piece.WhiteKnight), // Nc4
            new Move(Square.e5, Square.g4, Piece.WhiteKnight), // Ng4
            new Move(Square.e5, Square.c6, Piece.WhiteKnight), // Nc6
            new Move(Square.d2, Square.c1, Piece.WhiteBishop), // Bc1
            new Move(Square.d2, Square.e3, Piece.WhiteBishop), // Be3
            new Move(Square.d2, Square.f4, Piece.WhiteBishop), // Bf4
            new Move(Square.d2, Square.g5, Piece.WhiteBishop), // Bg5
            new Move(Square.d2, Square.h6, Piece.WhiteBishop), // Bh6
            new Move(Square.e2, Square.d1, Piece.WhiteBishop), // Bd1
            new Move(Square.e2, Square.f1, Piece.WhiteBishop), // Bf1
            new Move(Square.e2, Square.d3, Piece.WhiteBishop), // Bd3
            new Move(Square.e2, Square.c4, Piece.WhiteBishop), // Bc4
            new Move(Square.e2, Square.b5, Piece.WhiteBishop), // Bb5
            new Move(Square.a1, Square.b1, Piece.WhiteRook),   // Rb1
            new Move(Square.a1, Square.c1, Piece.WhiteRook),   // Rc1
            new Move(Square.a1, Square.d1, Piece.WhiteRook),   // Rd1
            new Move(Square.h1, Square.f1, Piece.WhiteRook),   // Rf1
            new Move(Square.h1, Square.g1, Piece.WhiteRook),   // Rg1
            new Move(Square.f3, Square.d3, Piece.WhiteQueen),  // Qd3
            new Move(Square.f3, Square.e3, Piece.WhiteQueen),  // Qe3
            new Move(Square.f3, Square.g3, Piece.WhiteQueen),  // Qg3
            new Move(Square.f3, Square.f4, Piece.WhiteQueen),  // Qf4
            new Move(Square.f3, Square.g4, Piece.WhiteQueen),  // Qg4
            new Move(Square.f3, Square.f5, Piece.WhiteQueen),  // Qf5
            new Move(Square.f3, Square.h5, Piece.WhiteQueen),  // Qh5
            new Move(Square.e1, Square.d1, Piece.WhiteKing),   // Kd1
            new Move(Square.e1, Square.f1, Piece.WhiteKing),   // Kf1
            new Move(Square.e1, Square.g1, Piece.WhiteKing, CastlingSide.KingSide), // O-O
            new Move(Square.e1, Square.c1, Piece.WhiteKing, CastlingSide.QueenSide) // O-O-O
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_KiwipetePosition_AllQuietMovesReturned()
    {
        var board = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {  
            new Move(Square.g2, Square.h3, Piece.WhitePawn,   Piece.BlackPawn),   // gxh3
            new Move(Square.d5, Square.e6, Piece.WhitePawn,   Piece.BlackPawn),   // dxe6
            new Move(Square.e5, Square.g6, Piece.WhiteKnight, Piece.BlackPawn),   // Nxg6
            new Move(Square.e5, Square.d7, Piece.WhiteKnight, Piece.BlackPawn),   // Nxd7
            new Move(Square.e5, Square.f7, Piece.WhiteKnight, Piece.BlackPawn),   // Nxf7
            new Move(Square.e2, Square.a6, Piece.WhiteBishop, Piece.BlackBishop), // Bxa6
            new Move(Square.f3, Square.h3, Piece.WhiteQueen,  Piece.BlackPawn),   // Qxh3
            new Move(Square.f3, Square.f6, Piece.WhiteQueen,  Piece.BlackKnight)  // Qxf6
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    #endregion
}
