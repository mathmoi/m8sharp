using FluentAssertions;

namespace m8.chess.tests.MoveGeneration;

public class MoveGenerationTests
{
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

    // TODO : Ajouter un test pour vérifier que cette position à 218 coups et qu'on peut bien les générer : R6R/3Q4/1Q4Q1/4Q3/2Q4Q/Q4Q2/pp1Q4/kBNNK1B1 b - - 0 1
}
