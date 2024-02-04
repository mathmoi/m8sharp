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
            new Move(Square.e4, Square.e5, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.f5, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.f4, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.f3, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.e3, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.d3, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.d4, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e4, Square.d5, Piece.WhiteKing, MoveType.Normal)
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
            new Move(Square.e1, Square.d1, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e1, Square.d2, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.e1, Square.f1, Piece.WhiteKing, MoveType.Normal)
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
            new Move(Square.a1, Square.a2, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.a1, Square.b2, Piece.WhiteKing, MoveType.Normal),
            new Move(Square.a1, Square.b1, Piece.WhiteKing, MoveType.Normal)
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
            new Move(Square.h8, Square.g8, Piece.BlackKing, MoveType.Normal),
            new Move(Square.h8, Square.g7, Piece.BlackKing, MoveType.Normal),
            new Move(Square.h8, Square.h7, Piece.BlackKing, MoveType.Normal)
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
            new Move(Square.f3, Square.d2, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.e1, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.d4, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.e5, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.g5, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.h4, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.h2, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.f3, Square.g1, Piece.WhiteKnight, MoveType.Normal)
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
            new Move(Square.e1, Square.g1, Piece.WhiteKing, MoveType.CastleKingSide),
            new Move(Square.e1, Square.c1, Piece.WhiteKing, MoveType.CastleQueenSide)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.MoveType == MoveType.CastleQueenSide || x.MoveType == MoveType.CastleKingSide).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_Chess960Position_KingCastlingReturned()
    {
        var board = new Board("rk2r3/8/8/8/8/8/4P3/RK2R3 w KQkq - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b1, Square.g1, Piece.WhiteKing, MoveType.CastleKingSide),
            new Move(Square.b1, Square.c1, Piece.WhiteKing, MoveType.CastleQueenSide)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.MoveType == MoveType.CastleQueenSide || x.MoveType == MoveType.CastleKingSide).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_PositionTraversedByRookAttacked_CastlingPossible()
    {
        var board = new Board("1k6/8/1r6/8/8/8/8/R3K3 w Q - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e1, Square.c1, Piece.WhiteKing, MoveType.CastleQueenSide)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.MoveType == MoveType.CastleQueenSide || x.MoveType == MoveType.CastleKingSide).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateQuietMoves_PositionTraversedByKingAttacked_CastlingImpossible()
    {
        var board = new Board("1k6/8/3r4/8/8/8/8/R3K3 w Q - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.MoveType == MoveType.CastleQueenSide || x.MoveType == MoveType.CastleKingSide).Should().BeEmpty();
    }

    [Fact]
    public void GenerateQuietMoves_PositionTraveledByRookOccupied_CastlingImpossible()
    {
        var board = new Board("1k6/8/8/8/8/8/8/RB2K3 w Q - 0 1");
        var actual = new List<Move>();

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.MoveType == MoveType.CastleQueenSide || x.MoveType == MoveType.CastleKingSide).Should().BeEmpty();
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
            new Move(Square.h7, Square.f8, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.h7, Square.f6, Piece.WhiteKnight, MoveType.Normal),
            new Move(Square.h7, Square.g5, Piece.WhiteKnight, MoveType.Normal)
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
            new Move(Square.b2, Square.b1, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.b3, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.b4, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.b5, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.b6, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.b7, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.b8, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.a2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.c2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.d2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.e2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.f2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.g2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.b2, Square.h2, Piece.WhiteRook, MoveType.Normal)
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
            new Move(Square.f6, Square.b6, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.c6, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.d6, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.e6, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.g6, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.f2, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.f3, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.f4, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.f5, Piece.WhiteRook, MoveType.Normal),
            new Move(Square.f6, Square.f7, Piece.WhiteRook, MoveType.Normal)
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
            new Move(Square.d5, Square.c6, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.d6, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.e6, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.e5, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.e4, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.d4, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.c4, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.c5, Piece.BlackQueen, MoveType.Normal),
            new Move(Square.d5, Square.f3, Piece.BlackQueen, MoveType.Normal)
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
            new Move(Square.d3, Square.d4, Piece.WhitePawn, MoveType.PawnMove),
            new Move(Square.f4, Square.f5, Piece.WhitePawn, MoveType.PawnMove)
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
            new Move(Square.d7, Square.d5, Piece.BlackPawn, MoveType.PawnDouble),
            new Move(Square.d7, Square.d6, Piece.BlackPawn, MoveType.PawnMove)
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
            new Move(Square.d7, Square.d6, Piece.BlackPawn, MoveType.PawnMove)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

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
            new Move(Square.f1, Square.e2, Piece.WhiteKing, Piece.BlackPawn, MoveType.Capture),
            new Move(Square.f1, Square.f2, Piece.WhiteKing, Piece.BlackRook, MoveType.Capture)
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
            new Move(Square.c4, Square.b2, Piece.BlackKnight, Piece.WhitePawn, MoveType.Capture),
            new Move(Square.c4, Square.d2, Piece.BlackKnight, Piece.WhitePawn, MoveType.Capture)
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
            new Move(Square.g4, Square.d7, Piece.BlackQueen, Piece.WhiteKnight, MoveType.Capture),
            new Move(Square.g4, Square.b4, Piece.BlackQueen, Piece.WhiteKnight, MoveType.Capture),
            new Move(Square.g4, Square.h4, Piece.BlackQueen, Piece.WhiteKnight, MoveType.Capture),
            new Move(Square.g4, Square.g2, Piece.BlackQueen, Piece.WhiteKnight, MoveType.Capture)

        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece == Piece.BlackQueen).Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GenerateCaptures Pawms

    [Fact]
    public void GenerateCaptures_OneCapturesAvailableOnLeft_OneMovesReturned()
    {
        var board = new Board("4k3/8/8/3n4/4P3/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e4, Square.d5, Piece.WhitePawn, Piece.BlackKnight, MoveType.Capture)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_OneCapturesAvailableOnRight_OneMovesReturned()
    {
        var board = new Board("4k3/8/8/5n2/4P3/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.e4, Square.f5, Piece.WhitePawn, Piece.BlackKnight, MoveType.Capture)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_OnePawnInPositionToPromote_FourMovesReturned()
    {
        var board = new Board("4k3/1P6/8/8/8/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteQueen,  MoveType.Promotion),
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteRook,   MoveType.Promotion),
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteBishop, MoveType.Promotion),
            new Move(Square.b7, Square.b8, Piece.WhitePawn, Piece.None, Piece.WhiteKnight, MoveType.Promotion)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_OnePawnInPositionToCaptureAndPromote_FourMovesReturned()
    {
        var board = new Board("1Rr1k3/1P6/8/8/8/8/8/4K3 w - - 0 1");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteQueen, MoveType.CapturePromotion),
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteRook, MoveType.CapturePromotion),
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteBishop, MoveType.CapturePromotion),
            new Move(Square.b7, Square.c8, Piece.WhitePawn, Piece.BlackRook, Piece.WhiteKnight, MoveType.CapturePromotion)
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Where(x => x.Piece.Type == PieceType.Pawn).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_PriseEnPassantPossible_PriseEnPassantReturned()
    {
        var board = new Board("rnbqkbnr/p2ppppp/8/1Pp5/8/8/1PPPPPPP/RNBQKBNR w KQkq c6 0 1");
        var actual = new List<Move>();
        var expected = new Move(Square.b5, Square.c6, Piece.WhitePawn, Piece.BlackPawn, MoveType.EnPassant);

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
            new Move(Square.a2, Square.a3, Piece.WhitePawn,   MoveType.PawnMove),   // a3
            new Move(Square.b2, Square.b3, Piece.WhitePawn,   MoveType.PawnMove),   // b3
            new Move(Square.c2, Square.c3, Piece.WhitePawn,   MoveType.PawnMove),   // c3
            new Move(Square.d2, Square.d3, Piece.WhitePawn,   MoveType.PawnMove),   // d3
            new Move(Square.e2, Square.e3, Piece.WhitePawn,   MoveType.PawnMove),   // e3
            new Move(Square.f2, Square.f3, Piece.WhitePawn,   MoveType.PawnMove),   // f3
            new Move(Square.g2, Square.g3, Piece.WhitePawn,   MoveType.PawnMove),   // g3
            new Move(Square.h2, Square.h3, Piece.WhitePawn,   MoveType.PawnMove),   // h3
            new Move(Square.a2, Square.a4, Piece.WhitePawn,   MoveType.PawnDouble), // a4
            new Move(Square.b2, Square.b4, Piece.WhitePawn,   MoveType.PawnDouble), // b4
            new Move(Square.c2, Square.c4, Piece.WhitePawn,   MoveType.PawnDouble), // c4
            new Move(Square.d2, Square.d4, Piece.WhitePawn,   MoveType.PawnDouble), // d4
            new Move(Square.e2, Square.e4, Piece.WhitePawn,   MoveType.PawnDouble), // e4
            new Move(Square.f2, Square.f4, Piece.WhitePawn,   MoveType.PawnDouble), // f4
            new Move(Square.g2, Square.g4, Piece.WhitePawn,   MoveType.PawnDouble), // g4
            new Move(Square.h2, Square.h4, Piece.WhitePawn,   MoveType.PawnDouble), // h4
            new Move(Square.b1, Square.a3, Piece.WhiteKnight, MoveType.Normal),     // Na3
            new Move(Square.b1, Square.c3, Piece.WhiteKnight, MoveType.Normal),     // Nc3
            new Move(Square.g1, Square.f3, Piece.WhiteKnight, MoveType.Normal),     // Nf3
            new Move(Square.g1, Square.h3, Piece.WhiteKnight, MoveType.Normal)      // Nh3
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
            new Move(Square.c1, Square.e2, Piece.WhiteKnight, MoveType.Normal), // Ne2
            new Move(Square.c1, Square.b3, Piece.WhiteKnight, MoveType.Normal), // Nb3#
            new Move(Square.c1, Square.d3, Piece.WhiteKnight, MoveType.Normal), // Nd3
            new Move(Square.d1, Square.f2, Piece.WhiteKnight, MoveType.Normal), // Nf2
            new Move(Square.d1, Square.c3, Piece.WhiteKnight, MoveType.Normal), // Nc3
            new Move(Square.d1, Square.e3, Piece.WhiteKnight, MoveType.Normal), // Ne3
            new Move(Square.b1, Square.c2, Piece.WhiteBishop, MoveType.Normal), // Bc2
            new Move(Square.b1, Square.d3, Piece.WhiteBishop, MoveType.Normal), // Bd3
            new Move(Square.b1, Square.e4, Piece.WhiteBishop, MoveType.Normal), // Be4
            new Move(Square.b1, Square.f5, Piece.WhiteBishop, MoveType.Normal), // Bf5
            new Move(Square.g1, Square.f2, Piece.WhiteBishop, MoveType.Normal), // Bf2
            new Move(Square.g1, Square.h2, Piece.WhiteBishop, MoveType.Normal), // Bh2
            new Move(Square.g1, Square.e3, Piece.WhiteBishop, MoveType.Normal), // Be3
            new Move(Square.g1, Square.d4, Piece.WhiteBishop, MoveType.Normal), // Bd4
            new Move(Square.g1, Square.c5, Piece.WhiteBishop, MoveType.Normal), // Bc5
            new Move(Square.a8, Square.a4, Piece.WhiteRook,   MoveType.Normal), // Ra4
            new Move(Square.a8, Square.a5, Piece.WhiteRook,   MoveType.Normal), // Ra5
            new Move(Square.a8, Square.a6, Piece.WhiteRook,   MoveType.Normal), // Ra6
            new Move(Square.a8, Square.a7, Piece.WhiteRook,   MoveType.Normal), // Ra7
            new Move(Square.a8, Square.b8, Piece.WhiteRook,   MoveType.Normal), // Rab8
            new Move(Square.a8, Square.c8, Piece.WhiteRook,   MoveType.Normal), // Rac8
            new Move(Square.a8, Square.d8, Piece.WhiteRook,   MoveType.Normal), // Rad8
            new Move(Square.a8, Square.e8, Piece.WhiteRook,   MoveType.Normal), // Rae8
            new Move(Square.a8, Square.f8, Piece.WhiteRook,   MoveType.Normal), // Raf8
            new Move(Square.a8, Square.g8, Piece.WhiteRook,   MoveType.Normal), // Rag8
            new Move(Square.h8, Square.h5, Piece.WhiteRook,   MoveType.Normal), // Rh5
            new Move(Square.h8, Square.h6, Piece.WhiteRook,   MoveType.Normal), // Rh6
            new Move(Square.h8, Square.h7, Piece.WhiteRook,   MoveType.Normal), // Rh7
            new Move(Square.h8, Square.b8, Piece.WhiteRook,   MoveType.Normal), // Rhb8
            new Move(Square.h8, Square.c8, Piece.WhiteRook,   MoveType.Normal), // Rhc8
            new Move(Square.h8, Square.d8, Piece.WhiteRook,   MoveType.Normal), // Rhd8
            new Move(Square.h8, Square.e8, Piece.WhiteRook,   MoveType.Normal), // Rhe8
            new Move(Square.h8, Square.f8, Piece.WhiteRook,   MoveType.Normal), // Rhf8
            new Move(Square.h8, Square.g8, Piece.WhiteRook,   MoveType.Normal), // Rhg8
            new Move(Square.d2, Square.c2, Piece.WhiteQueen,  MoveType.Normal), // Qdc2
            new Move(Square.d2, Square.e2, Piece.WhiteQueen,  MoveType.Normal), // Qde2
            new Move(Square.d2, Square.f2, Piece.WhiteQueen,  MoveType.Normal), // Qdf2
            new Move(Square.d2, Square.g2, Piece.WhiteQueen,  MoveType.Normal), // Qdg2
            new Move(Square.d2, Square.h2, Piece.WhiteQueen,  MoveType.Normal), // Qdh2
            new Move(Square.d2, Square.c3, Piece.WhiteQueen,  MoveType.Normal), // Qdc3
            new Move(Square.d2, Square.d3, Piece.WhiteQueen,  MoveType.Normal), // Q2d3
            new Move(Square.d2, Square.e3, Piece.WhiteQueen,  MoveType.Normal), // Qde3
            new Move(Square.d2, Square.b4, Piece.WhiteQueen,  MoveType.Normal), // Qdb4
            new Move(Square.d2, Square.d4, Piece.WhiteQueen,  MoveType.Normal), // Q2d4
            new Move(Square.d2, Square.f4, Piece.WhiteQueen,  MoveType.Normal), // Qdf4
            new Move(Square.d2, Square.a5, Piece.WhiteQueen,  MoveType.Normal), // Qda5
            new Move(Square.d2, Square.d5, Piece.WhiteQueen,  MoveType.Normal), // Q2d5
            new Move(Square.d2, Square.g5, Piece.WhiteQueen,  MoveType.Normal), // Qdg5
            new Move(Square.d2, Square.d6, Piece.WhiteQueen,  MoveType.Normal), // Q2d6
            new Move(Square.d2, Square.h6, Piece.WhiteQueen,  MoveType.Normal), // Qdh6
            new Move(Square.a3, Square.b3, Piece.WhiteQueen,  MoveType.Normal), // Qab3
            new Move(Square.a3, Square.c3, Piece.WhiteQueen,  MoveType.Normal), // Qac3
            new Move(Square.a3, Square.d3, Piece.WhiteQueen,  MoveType.Normal), // Qad3
            new Move(Square.a3, Square.e3, Piece.WhiteQueen,  MoveType.Normal), // Qae3
            new Move(Square.a3, Square.a4, Piece.WhiteQueen,  MoveType.Normal), // Qaa4
            new Move(Square.a3, Square.b4, Piece.WhiteQueen,  MoveType.Normal), // Qab4
            new Move(Square.a3, Square.a5, Piece.WhiteQueen,  MoveType.Normal), // Qaa5
            new Move(Square.a3, Square.c5, Piece.WhiteQueen,  MoveType.Normal), // Qac5
            new Move(Square.a3, Square.a6, Piece.WhiteQueen,  MoveType.Normal), // Qaa6
            new Move(Square.a3, Square.d6, Piece.WhiteQueen,  MoveType.Normal), // Qad6
            new Move(Square.a3, Square.a7, Piece.WhiteQueen,  MoveType.Normal), // Qaa7
            new Move(Square.a3, Square.e7, Piece.WhiteQueen,  MoveType.Normal), // Qae7
            new Move(Square.a3, Square.f8, Piece.WhiteQueen,  MoveType.Normal), // Qaf8
            new Move(Square.f3, Square.f1, Piece.WhiteQueen,  MoveType.Normal), // Qff1
            new Move(Square.f3, Square.h1, Piece.WhiteQueen,  MoveType.Normal), // Qfh1
            new Move(Square.f3, Square.e2, Piece.WhiteQueen,  MoveType.Normal), // Qfe2
            new Move(Square.f3, Square.f2, Piece.WhiteQueen,  MoveType.Normal), // Qff2
            new Move(Square.f3, Square.g2, Piece.WhiteQueen,  MoveType.Normal), // Qfg2
            new Move(Square.f3, Square.b3, Piece.WhiteQueen,  MoveType.Normal), // Qfb3
            new Move(Square.f3, Square.c3, Piece.WhiteQueen,  MoveType.Normal), // Qfc3
            new Move(Square.f3, Square.d3, Piece.WhiteQueen,  MoveType.Normal), // Qfd3
            new Move(Square.f3, Square.e3, Piece.WhiteQueen,  MoveType.Normal), // Qfe3
            new Move(Square.f3, Square.g3, Piece.WhiteQueen,  MoveType.Normal), // Qfg3
            new Move(Square.f3, Square.h3, Piece.WhiteQueen,  MoveType.Normal), // Qfh3
            new Move(Square.f3, Square.e4, Piece.WhiteQueen,  MoveType.Normal), // Qfe4
            new Move(Square.f3, Square.f4, Piece.WhiteQueen,  MoveType.Normal), // Qff4
            new Move(Square.f3, Square.g4, Piece.WhiteQueen,  MoveType.Normal), // Qfg4
            new Move(Square.f3, Square.d5, Piece.WhiteQueen,  MoveType.Normal), // Qfd5
            new Move(Square.f3, Square.f5, Piece.WhiteQueen,  MoveType.Normal), // Qff5
            new Move(Square.f3, Square.h5, Piece.WhiteQueen,  MoveType.Normal), // Qfh5
            new Move(Square.f3, Square.c6, Piece.WhiteQueen,  MoveType.Normal), // Qfc6
            new Move(Square.f3, Square.f6, Piece.WhiteQueen,  MoveType.Normal), // Qff6
            new Move(Square.f3, Square.b7, Piece.WhiteQueen,  MoveType.Normal), // Qfb7
            new Move(Square.f3, Square.f7, Piece.WhiteQueen,  MoveType.Normal), // Qff7
            new Move(Square.f3, Square.f8, Piece.WhiteQueen,  MoveType.Normal), // Qff8
            new Move(Square.c4, Square.f1, Piece.WhiteQueen,  MoveType.Normal), // Qcf1
            new Move(Square.c4, Square.c2, Piece.WhiteQueen,  MoveType.Normal), // Qcc2
            new Move(Square.c4, Square.e2, Piece.WhiteQueen,  MoveType.Normal), // Qce2
            new Move(Square.c4, Square.b3, Piece.WhiteQueen,  MoveType.Normal), // Qcb3
            new Move(Square.c4, Square.c3, Piece.WhiteQueen,  MoveType.Normal), // Qcc3
            new Move(Square.c4, Square.d3, Piece.WhiteQueen,  MoveType.Normal), // Qcd3
            new Move(Square.c4, Square.a4, Piece.WhiteQueen,  MoveType.Normal), // Qca4
            new Move(Square.c4, Square.b4, Piece.WhiteQueen,  MoveType.Normal), // Qcb4
            new Move(Square.c4, Square.d4, Piece.WhiteQueen,  MoveType.Normal), // Qcd4
            new Move(Square.c4, Square.e4, Piece.WhiteQueen,  MoveType.Normal), // Qce4
            new Move(Square.c4, Square.f4, Piece.WhiteQueen,  MoveType.Normal), // Qcf4
            new Move(Square.c4, Square.g4, Piece.WhiteQueen,  MoveType.Normal), // Qcg4
            new Move(Square.c4, Square.b5, Piece.WhiteQueen,  MoveType.Normal), // Qcb5
            new Move(Square.c4, Square.c5, Piece.WhiteQueen,  MoveType.Normal), // Qcc5
            new Move(Square.c4, Square.d5, Piece.WhiteQueen,  MoveType.Normal), // Qcd5
            new Move(Square.c4, Square.a6, Piece.WhiteQueen,  MoveType.Normal), // Qca6
            new Move(Square.c4, Square.c6, Piece.WhiteQueen,  MoveType.Normal), // Qcc6
            new Move(Square.c4, Square.e6, Piece.WhiteQueen,  MoveType.Normal), // Qce6
            new Move(Square.c4, Square.c7, Piece.WhiteQueen,  MoveType.Normal), // Qcc7
            new Move(Square.c4, Square.f7, Piece.WhiteQueen,  MoveType.Normal), // Qcf7
            new Move(Square.c4, Square.c8, Piece.WhiteQueen,  MoveType.Normal), // Qcc8
            new Move(Square.c4, Square.g8, Piece.WhiteQueen,  MoveType.Normal), // Qcg8
            new Move(Square.h4, Square.h1, Piece.WhiteQueen,  MoveType.Normal), // Qhh1
            new Move(Square.h4, Square.f2, Piece.WhiteQueen,  MoveType.Normal), // Qhf2
            new Move(Square.h4, Square.h2, Piece.WhiteQueen,  MoveType.Normal), // Qhh2
            new Move(Square.h4, Square.g3, Piece.WhiteQueen,  MoveType.Normal), // Qhg3
            new Move(Square.h4, Square.h3, Piece.WhiteQueen,  MoveType.Normal), // Qhh3
            new Move(Square.h4, Square.d4, Piece.WhiteQueen,  MoveType.Normal), // Qhd4
            new Move(Square.h4, Square.e4, Piece.WhiteQueen,  MoveType.Normal), // Qhe4
            new Move(Square.h4, Square.f4, Piece.WhiteQueen,  MoveType.Normal), // Qhf4
            new Move(Square.h4, Square.g4, Piece.WhiteQueen,  MoveType.Normal), // Qhg4
            new Move(Square.h4, Square.g5, Piece.WhiteQueen,  MoveType.Normal), // Qhg5
            new Move(Square.h4, Square.h5, Piece.WhiteQueen,  MoveType.Normal), // Qhh5
            new Move(Square.h4, Square.f6, Piece.WhiteQueen,  MoveType.Normal), // Qhf6
            new Move(Square.h4, Square.h6, Piece.WhiteQueen,  MoveType.Normal), // Qhh6
            new Move(Square.h4, Square.e7, Piece.WhiteQueen,  MoveType.Normal), // Qhe7
            new Move(Square.h4, Square.h7, Piece.WhiteQueen,  MoveType.Normal), // Qhh7
            new Move(Square.h4, Square.d8, Piece.WhiteQueen,  MoveType.Normal), // Qhd8
            new Move(Square.e5, Square.e2, Piece.WhiteQueen,  MoveType.Normal), // Qee2
            new Move(Square.e5, Square.h2, Piece.WhiteQueen,  MoveType.Normal), // Qeh2
            new Move(Square.e5, Square.c3, Piece.WhiteQueen,  MoveType.Normal), // Qec3
            new Move(Square.e5, Square.e3, Piece.WhiteQueen,  MoveType.Normal), // Qee3
            new Move(Square.e5, Square.g3, Piece.WhiteQueen,  MoveType.Normal), // Qeg3
            new Move(Square.e5, Square.d4, Piece.WhiteQueen,  MoveType.Normal), // Qed4
            new Move(Square.e5, Square.e4, Piece.WhiteQueen,  MoveType.Normal), // Qee4
            new Move(Square.e5, Square.f4, Piece.WhiteQueen,  MoveType.Normal), // Qef4
            new Move(Square.e5, Square.a5, Piece.WhiteQueen,  MoveType.Normal), // Qea5
            new Move(Square.e5, Square.b5, Piece.WhiteQueen,  MoveType.Normal), // Qeb5
            new Move(Square.e5, Square.c5, Piece.WhiteQueen,  MoveType.Normal), // Qec5
            new Move(Square.e5, Square.d5, Piece.WhiteQueen,  MoveType.Normal), // Qed5
            new Move(Square.e5, Square.f5, Piece.WhiteQueen,  MoveType.Normal), // Qef5
            new Move(Square.e5, Square.g5, Piece.WhiteQueen,  MoveType.Normal), // Qeg5
            new Move(Square.e5, Square.h5, Piece.WhiteQueen,  MoveType.Normal), // Qeh5
            new Move(Square.e5, Square.d6, Piece.WhiteQueen,  MoveType.Normal), // Qed6
            new Move(Square.e5, Square.e6, Piece.WhiteQueen,  MoveType.Normal), // Qee6
            new Move(Square.e5, Square.f6, Piece.WhiteQueen,  MoveType.Normal), // Qef6
            new Move(Square.e5, Square.c7, Piece.WhiteQueen,  MoveType.Normal), // Qec7
            new Move(Square.e5, Square.e7, Piece.WhiteQueen,  MoveType.Normal), // Qee7
            new Move(Square.e5, Square.g7, Piece.WhiteQueen,  MoveType.Normal), // Qeg7
            new Move(Square.e5, Square.b8, Piece.WhiteQueen,  MoveType.Normal), // Qeb8
            new Move(Square.e5, Square.e8, Piece.WhiteQueen,  MoveType.Normal), // Qee8
            new Move(Square.b6, Square.f2, Piece.WhiteQueen,  MoveType.Normal), // Qbf2
            new Move(Square.b6, Square.b3, Piece.WhiteQueen,  MoveType.Normal), // Qbb3
            new Move(Square.b6, Square.e3, Piece.WhiteQueen,  MoveType.Normal), // Qbe3
            new Move(Square.b6, Square.b4, Piece.WhiteQueen,  MoveType.Normal), // Qbb4
            new Move(Square.b6, Square.d4, Piece.WhiteQueen,  MoveType.Normal), // Qbd4
            new Move(Square.b6, Square.a5, Piece.WhiteQueen,  MoveType.Normal), // Qba5
            new Move(Square.b6, Square.b5, Piece.WhiteQueen,  MoveType.Normal), // Qbb5
            new Move(Square.b6, Square.c5, Piece.WhiteQueen,  MoveType.Normal), // Qbc5
            new Move(Square.b6, Square.a6, Piece.WhiteQueen,  MoveType.Normal), // Qba6
            new Move(Square.b6, Square.c6, Piece.WhiteQueen,  MoveType.Normal), // Qbc6
            new Move(Square.b6, Square.d6, Piece.WhiteQueen,  MoveType.Normal), // Qbd6
            new Move(Square.b6, Square.e6, Piece.WhiteQueen,  MoveType.Normal), // Qbe6
            new Move(Square.b6, Square.f6, Piece.WhiteQueen,  MoveType.Normal), // Qbf6
            new Move(Square.b6, Square.a7, Piece.WhiteQueen,  MoveType.Normal), // Qba7
            new Move(Square.b6, Square.b7, Piece.WhiteQueen,  MoveType.Normal), // Qbb7
            new Move(Square.b6, Square.c7, Piece.WhiteQueen,  MoveType.Normal), // Qbc7
            new Move(Square.b6, Square.b8, Piece.WhiteQueen,  MoveType.Normal), // Qbb8
            new Move(Square.b6, Square.d8, Piece.WhiteQueen,  MoveType.Normal), // Qbd8
            new Move(Square.g6, Square.c2, Piece.WhiteQueen,  MoveType.Normal), // Qgc2
            new Move(Square.g6, Square.g2, Piece.WhiteQueen,  MoveType.Normal), // Qgg2
            new Move(Square.g6, Square.d3, Piece.WhiteQueen,  MoveType.Normal), // Qgd3
            new Move(Square.g6, Square.g3, Piece.WhiteQueen,  MoveType.Normal), // Qgg3
            new Move(Square.g6, Square.e4, Piece.WhiteQueen,  MoveType.Normal), // Qge4
            new Move(Square.g6, Square.g4, Piece.WhiteQueen,  MoveType.Normal), // Qgg4
            new Move(Square.g6, Square.f5, Piece.WhiteQueen,  MoveType.Normal), // Qgf5
            new Move(Square.g6, Square.g5, Piece.WhiteQueen,  MoveType.Normal), // Qgg5
            new Move(Square.g6, Square.h5, Piece.WhiteQueen,  MoveType.Normal), // Qgh5
            new Move(Square.g6, Square.c6, Piece.WhiteQueen,  MoveType.Normal), // Qgc6
            new Move(Square.g6, Square.d6, Piece.WhiteQueen,  MoveType.Normal), // Qgd6
            new Move(Square.g6, Square.e6, Piece.WhiteQueen,  MoveType.Normal), // Qge6
            new Move(Square.g6, Square.f6, Piece.WhiteQueen,  MoveType.Normal), // Qgf6
            new Move(Square.g6, Square.h6, Piece.WhiteQueen,  MoveType.Normal), // Qgh6
            new Move(Square.g6, Square.f7, Piece.WhiteQueen,  MoveType.Normal), // Qgf7
            new Move(Square.g6, Square.g7, Piece.WhiteQueen,  MoveType.Normal), // Qgg7
            new Move(Square.g6, Square.h7, Piece.WhiteQueen,  MoveType.Normal), // Qgh7
            new Move(Square.g6, Square.e8, Piece.WhiteQueen,  MoveType.Normal), // Qge8
            new Move(Square.g6, Square.g8, Piece.WhiteQueen,  MoveType.Normal), // Qgg8
            new Move(Square.d7, Square.d3, Piece.WhiteQueen,  MoveType.Normal), // Q7d3
            new Move(Square.d7, Square.h3, Piece.WhiteQueen,  MoveType.Normal), // Qdh3
            new Move(Square.d7, Square.a4, Piece.WhiteQueen,  MoveType.Normal), // Qda4
            new Move(Square.d7, Square.d4, Piece.WhiteQueen,  MoveType.Normal), // Q7d4
            new Move(Square.d7, Square.g4, Piece.WhiteQueen,  MoveType.Normal), // Qdg4
            new Move(Square.d7, Square.b5, Piece.WhiteQueen,  MoveType.Normal), // Qdb5
            new Move(Square.d7, Square.d5, Piece.WhiteQueen,  MoveType.Normal), // Q7d5
            new Move(Square.d7, Square.f5, Piece.WhiteQueen,  MoveType.Normal), // Qdf5
            new Move(Square.d7, Square.c6, Piece.WhiteQueen,  MoveType.Normal), // Qdc6
            new Move(Square.d7, Square.d6, Piece.WhiteQueen,  MoveType.Normal), // Q7d6
            new Move(Square.d7, Square.e6, Piece.WhiteQueen,  MoveType.Normal), // Qde6
            new Move(Square.d7, Square.a7, Piece.WhiteQueen,  MoveType.Normal), // Qda7
            new Move(Square.d7, Square.b7, Piece.WhiteQueen,  MoveType.Normal), // Qdb7
            new Move(Square.d7, Square.c7, Piece.WhiteQueen,  MoveType.Normal), // Qdc7
            new Move(Square.d7, Square.e7, Piece.WhiteQueen,  MoveType.Normal), // Qde7
            new Move(Square.d7, Square.f7, Piece.WhiteQueen,  MoveType.Normal), // Qdf7
            new Move(Square.d7, Square.g7, Piece.WhiteQueen,  MoveType.Normal), // Qdg7
            new Move(Square.d7, Square.h7, Piece.WhiteQueen,  MoveType.Normal), // Qdh7
            new Move(Square.d7, Square.c8, Piece.WhiteQueen,  MoveType.Normal), // Qdc8
            new Move(Square.d7, Square.d8, Piece.WhiteQueen,  MoveType.Normal), // Qdd8
            new Move(Square.d7, Square.e8, Piece.WhiteQueen,  MoveType.Normal), // Qde8
            new Move(Square.e1, Square.f1, Piece.WhiteKing,   MoveType.Normal), // Kf1
            new Move(Square.e1, Square.e2, Piece.WhiteKing,   MoveType.Normal), // Ke2
            new Move(Square.e1, Square.f2, Piece.WhiteKing,   MoveType.Normal)  // Kf2
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
            new Move(Square.c1, Square.a2, Piece.WhiteKnight, Piece.BlackPawn, MoveType.Capture), // Nxa2>
            new Move(Square.d1, Square.b2, Piece.WhiteKnight, Piece.BlackPawn, MoveType.Capture), // Nxb2
            new Move(Square.b1, Square.a2, Piece.WhiteBishop, Piece.BlackPawn, MoveType.Capture), // Bxa2
            new Move(Square.d2, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture), // Qdxb2#
            new Move(Square.a3, Square.a2, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture), // Qaxa2#
            new Move(Square.a3, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture), // Qaxb2#
            new Move(Square.c4, Square.a2, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture), // Qcxa2#
            new Move(Square.e5, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture), // Qexb2#
            new Move(Square.b6, Square.b2, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture)  // Qbxb2#
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
            new Move(Square.a2, Square.a3, Piece.WhitePawn,   MoveType.PawnMove),   // a3
            new Move(Square.b2, Square.b3, Piece.WhitePawn,   MoveType.PawnMove),   // b3
            new Move(Square.g2, Square.g3, Piece.WhitePawn,   MoveType.PawnMove),   // g3
            new Move(Square.d5, Square.d6, Piece.WhitePawn,   MoveType.PawnMove),   // d6
            new Move(Square.a2, Square.a4, Piece.WhitePawn,   MoveType.PawnDouble), // a4
            new Move(Square.g2, Square.g4, Piece.WhitePawn,   MoveType.PawnDouble), // g4
            new Move(Square.c3, Square.b1, Piece.WhiteKnight, MoveType.Normal),     // Nb1
            new Move(Square.c3, Square.d1, Piece.WhiteKnight, MoveType.Normal),     // Nd1
            new Move(Square.c3, Square.a4, Piece.WhiteKnight, MoveType.Normal),     // Na4
            new Move(Square.c3, Square.b5, Piece.WhiteKnight, MoveType.Normal),     // Nb5
            new Move(Square.e5, Square.d3, Piece.WhiteKnight, MoveType.Normal),     // Nd3
            new Move(Square.e5, Square.c4, Piece.WhiteKnight, MoveType.Normal),     // Nc4
            new Move(Square.e5, Square.g4, Piece.WhiteKnight, MoveType.Normal),     // Ng4
            new Move(Square.e5, Square.c6, Piece.WhiteKnight, MoveType.Normal),     // Nc6
            new Move(Square.d2, Square.c1, Piece.WhiteBishop, MoveType.Normal),     // Bc1
            new Move(Square.d2, Square.e3, Piece.WhiteBishop, MoveType.Normal),     // Be3
            new Move(Square.d2, Square.f4, Piece.WhiteBishop, MoveType.Normal),     // Bf4
            new Move(Square.d2, Square.g5, Piece.WhiteBishop, MoveType.Normal),     // Bg5
            new Move(Square.d2, Square.h6, Piece.WhiteBishop, MoveType.Normal),     // Bh6
            new Move(Square.e2, Square.d1, Piece.WhiteBishop, MoveType.Normal),     // Bd1
            new Move(Square.e2, Square.f1, Piece.WhiteBishop, MoveType.Normal),     // Bf1
            new Move(Square.e2, Square.d3, Piece.WhiteBishop, MoveType.Normal),     // Bd3
            new Move(Square.e2, Square.c4, Piece.WhiteBishop, MoveType.Normal),     // Bc4
            new Move(Square.e2, Square.b5, Piece.WhiteBishop, MoveType.Normal),     // Bb5
            new Move(Square.a1, Square.b1, Piece.WhiteRook,   MoveType.Normal),     // Rb1
            new Move(Square.a1, Square.c1, Piece.WhiteRook,   MoveType.Normal),     // Rc1
            new Move(Square.a1, Square.d1, Piece.WhiteRook,   MoveType.Normal),     // Rd1
            new Move(Square.h1, Square.f1, Piece.WhiteRook,   MoveType.Normal),     // Rf1
            new Move(Square.h1, Square.g1, Piece.WhiteRook,   MoveType.Normal),     // Rg1
            new Move(Square.f3, Square.d3, Piece.WhiteQueen,  MoveType.Normal),     // Qd3
            new Move(Square.f3, Square.e3, Piece.WhiteQueen,  MoveType.Normal),     // Qe3
            new Move(Square.f3, Square.g3, Piece.WhiteQueen,  MoveType.Normal),     // Qg3
            new Move(Square.f3, Square.f4, Piece.WhiteQueen,  MoveType.Normal),     // Qf4
            new Move(Square.f3, Square.g4, Piece.WhiteQueen,  MoveType.Normal),     // Qg4
            new Move(Square.f3, Square.f5, Piece.WhiteQueen,  MoveType.Normal),     // Qf5
            new Move(Square.f3, Square.h5, Piece.WhiteQueen,  MoveType.Normal),     // Qh5
            new Move(Square.e1, Square.d1, Piece.WhiteKing,   MoveType.Normal),     // Kd1
            new Move(Square.e1, Square.f1, Piece.WhiteKing,   MoveType.Normal),     // Kf1
            new Move(Square.e1, Square.g1, Piece.WhiteKing,   MoveType.CastleKingSide), // O-O
            new Move(Square.e1, Square.c1, Piece.WhiteKing,   MoveType.CastleQueenSide) // O-O-O
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateQuietMoves(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GenerateCaptures_KiwipetePosition_AllCapturesReturned()
    {
        var board = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq");
        var actual = new List<Move>();
        var expected = new List<Move>()
        {  
            new Move(Square.g2, Square.h3, Piece.WhitePawn,   Piece.BlackPawn, MoveType.Capture),   // gxh3
            new Move(Square.d5, Square.e6, Piece.WhitePawn,   Piece.BlackPawn, MoveType.Capture),   // dxe6
            new Move(Square.e5, Square.g6, Piece.WhiteKnight, Piece.BlackPawn, MoveType.Capture),   // Nxg6
            new Move(Square.e5, Square.d7, Piece.WhiteKnight, Piece.BlackPawn, MoveType.Capture),   // Nxd7
            new Move(Square.e5, Square.f7, Piece.WhiteKnight, Piece.BlackPawn, MoveType.Capture),   // Nxf7
            new Move(Square.e2, Square.a6, Piece.WhiteBishop, Piece.BlackBishop, MoveType.Capture), // Bxa6
            new Move(Square.f3, Square.h3, Piece.WhiteQueen,  Piece.BlackPawn, MoveType.Capture),   // Qxh3
            new Move(Square.f3, Square.f6, Piece.WhiteQueen,  Piece.BlackKnight, MoveType.Capture)  // Qxf6
        };

        m8.chess.MoveGeneration.MoveGeneration.GenerateCaptures(board, actual);

        actual.Should().BeEquivalentTo(expected);
    }

    #endregion
}
