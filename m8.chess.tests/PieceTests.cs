using FluentAssertions;

namespace m8.chess.tests;

/// <summary>
///  Test for the Piece structure
/// </summary>
public class PieceTests
{
    [Fact]
    public void Character_ValidPiece_ReturnsCorrectCharacter()
    {
        Piece.WhitePawn.Character.Should().Be('P');
        Piece.WhiteRook.Character.Should().Be('R');
        Piece.WhiteBishop.Character.Should().Be('B');
        Piece.WhiteKnight.Character.Should().Be('N');
        Piece.WhiteQueen.Character.Should().Be('Q');
        Piece.WhiteKing.Character.Should().Be('K');

        Piece.BlackPawn.Character.Should().Be('p');
        Piece.BlackRook.Character.Should().Be('r');
        Piece.BlackBishop.Character.Should().Be('b');
        Piece.BlackKnight.Character.Should().Be('n');
        Piece.BlackQueen.Character.Should().Be('q');
        Piece.BlackKing.Character.Should().Be('k');
    }
}
