using FluentAssertions;

namespace m8.chess.tests;

public class MoveTests
{
    [Fact]
    public void Ctor_SimpleMove_AllPropertiesCorects()
    {
        var from = Square.b3;
        var to = Square.c4;
        var piece = Piece.WhiteQueen;

        var move = new Move(from, to, piece, MoveType.Normal);

        move.From.Should().Be(from);
        move.To.Should().Be(to);
        move.Piece.Should().Be(piece);
        move.Taken.IsValid.Should().BeFalse();
        move.PromoteTo.IsValid.Should().BeFalse();
        move.MoveType.Should().Be(MoveType.Normal);
    }

    [Fact]
    public void Ctor_CaptureMove_AllPropertiesCorects()
    {
        var from = Square.h1;
        var to = Square.h8;
        var piece = Piece.WhiteRook;
        var taken = Piece.BlackRook;

        var move = new Move(from, to, piece, taken, MoveType.Capture);

        move.From.Should().Be(from);
        move.To.Should().Be(to);
        move.Piece.Should().Be(piece);
        move.Taken.Should().Be(taken);
        move.PromoteTo.IsValid.Should().BeFalse();
        move.MoveType.Should().Be(MoveType.Capture);
    }

    [Fact]
    public void Ctor_PromotionMove_AllPropertiesCorects()
    {
        var from = Square.e7;
        var to = Square.e8;
        var piece = Piece.WhitePawn;
        var taken = Piece.None;
        var promoteTo = Piece.WhiteQueen;

        var move = new Move(from, to, piece, taken, promoteTo, MoveType.Promotion);

        move.From.Should().Be(from);
        move.To.Should().Be(to);
        move.Piece.Should().Be(piece);
        taken.IsValid.Should().BeFalse();
        move.PromoteTo.Should().Be(promoteTo);
        move.MoveType.Should().Be(MoveType.Promotion);
    }
}
