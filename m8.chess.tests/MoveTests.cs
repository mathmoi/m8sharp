namespace m8.chess.tests;

public class MoveTests
{
    [Fact]
    public void Ctor_SimpleMove_AllPropertiesCorects()
    {
        var from = Square.b3;
        var to = Square.c4;
        var piece = Piece.WhiteQueen;

        var move = new Move(from, to, piece);

        Assert.Equal(from, move.From);
        Assert.Equal(to, move.To);
        Assert.Equal(piece, move.Piece);
        Assert.False(move.Taken.IsValid);
        Assert.False(move.PromoteTo.IsValid);
        Assert.Equal(CastlingSide.None, move.CastlingSide);
    }

    [Fact]
    public void Ctor_CaptureMove_AllPropertiesCorects()
    {
        var from = Square.h1;
        var to = Square.h8;
        var piece = Piece.WhiteRook;
        var taken = Piece.BlackRook;

        var move = new Move(from, to, piece, taken);

        Assert.Equal(from, move.From);
        Assert.Equal(to, move.To);
        Assert.Equal(piece, move.Piece);
        Assert.Equal(taken, move.Taken);
        Assert.False(move.PromoteTo.IsValid);
        Assert.Equal(CastlingSide.None, move.CastlingSide);
    }

    [Fact]
    public void Ctor_PromotionMove_AllPropertiesCorects()
    {
        var from = Square.e7;
        var to = Square.e8;
        var piece = Piece.WhitePawn;
        var taken = Piece.None;
        var promoteTo = Piece.WhiteQueen;

        var move = new Move(from, to, piece, taken, promoteTo);

        Assert.Equal(from, move.From);
        Assert.Equal(to, move.To);
        Assert.Equal(piece, move.Piece);
        Assert.False(taken.IsValid);
        Assert.Equal(promoteTo, move.PromoteTo);
        Assert.Equal(CastlingSide.None, move.CastlingSide);
    }
}
