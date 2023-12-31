namespace m8.chess.tests;

/// <summary>
///  Test for the Piece structure
/// </summary>
public class PieceTests
{
    [Fact]
    public void Character_ValidPiece_ReturnsCorrectCharacter()
    {
        Assert.Equal('P', Piece.WhitePawn.Character);
        Assert.Equal('R', Piece.WhiteRook.Character);
        Assert.Equal('B', Piece.WhiteBishop.Character);
        Assert.Equal('N', Piece.WhiteKnight.Character);
        Assert.Equal('Q', Piece.WhiteQueen.Character);
        Assert.Equal('K', Piece.WhiteKing.Character);

        Assert.Equal('p', Piece.BlackPawn.Character);
        Assert.Equal('r', Piece.BlackRook.Character);
        Assert.Equal('b', Piece.BlackBishop.Character);
        Assert.Equal('n', Piece.BlackKnight.Character);
        Assert.Equal('q', Piece.BlackQueen.Character);
        Assert.Equal('k', Piece.BlackKing.Character);
    }
}
