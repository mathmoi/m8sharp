using FluentAssertions;

namespace m8.chess.tests;

public class PieceTypeTests
{
    [Fact]
    public void Constructor_WithValidChar_ShouldCreateCorrectValues()
    {
        new PieceType('p').Should().Be(PieceType.Pawn);
        new PieceType('r').Should().Be(PieceType.Rook);
        new PieceType('b').Should().Be(PieceType.Bishop);
        new PieceType('n').Should().Be(PieceType.Knight);
        new PieceType('q').Should().Be(PieceType.Queen);
        new PieceType('k').Should().Be(PieceType.King);

        new PieceType('P').Should().Be(PieceType.Pawn);
        new PieceType('R').Should().Be(PieceType.Rook);
        new PieceType('B').Should().Be(PieceType.Bishop);
        new PieceType('N').Should().Be(PieceType.Knight);
        new PieceType('Q').Should().Be(PieceType.Queen);
        new PieceType('K').Should().Be(PieceType.King);
    }

    [Fact]
    public void Constructor_WithInvalidChar_ShouldSetToNone()
    {
        var invalidPiece = new PieceType('X');

        invalidPiece.Should().Be(PieceType.None);
    }

    [Theory]
    [InlineData('p')]
    [InlineData('r')]
    [InlineData('b')]
    [InlineData('n')]
    [InlineData('q')]
    [InlineData('k')]
    public void IsValid_AllPiecesType_ReturnsTrue(char piece_char)
    {
        PieceType piece_type = new(piece_char);

        piece_type.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_InvalidPiece_ReturnsFalse()
    {
        PieceType.None.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Character_ValidPiece_ReturnsCorrectCharacter()
    {
        PieceType.Pawn.Character.Should().Be('P');
        PieceType.Rook.Character.Should().Be('R');
        PieceType.Bishop.Character.Should().Be('B');
        PieceType.Knight.Character.Should().Be('N');
        PieceType.Queen.Character.Should().Be('Q');
        PieceType.King.Character.Should().Be('K');
    }
}
