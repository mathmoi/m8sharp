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

    // TODO : Ajouter un test pour vérifier que cette position à 218 coups et qu'on peut bien les générer : R6R/3Q4/1Q4Q1/4Q3/2Q4Q/Q4Q2/pp1Q4/kBNNK1B1 b - - 0 1
}
