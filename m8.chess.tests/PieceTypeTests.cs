namespace m8.chess.tests;

/// <summary>
///  Test for the PieceType structure
/// </summary>
public class PieceTypeTests
{
    [Fact]
    public void Constructor_WithValidChar_ShouldCreateCorrectValues()
    {
        Assert.Equal(PieceType.Pawn,   new PieceType('p'));
        Assert.Equal(PieceType.Rook,   new PieceType('r'));
        Assert.Equal(PieceType.Bishop, new PieceType('b'));
        Assert.Equal(PieceType.Knight, new PieceType('n'));
        Assert.Equal(PieceType.Queen,  new PieceType('q'));
        Assert.Equal(PieceType.King,   new PieceType('k'));

        Assert.Equal(PieceType.Pawn,   new PieceType('P'));
        Assert.Equal(PieceType.Rook,   new PieceType('R'));
        Assert.Equal(PieceType.Bishop, new PieceType('B'));
        Assert.Equal(PieceType.Knight, new PieceType('N'));
        Assert.Equal(PieceType.Queen,  new PieceType('Q'));
        Assert.Equal(PieceType.King,   new PieceType('K'));
    }

    [Fact]
    public void Constructor_WithInvalidChar_ShouldSetToNone()
    {
        // Arrange & Act
        var invalidPiece = new PieceType('X');

        // Assert
        Assert.Equal(PieceType.None, invalidPiece);
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

        Assert.True(piece_type.IsValid);
    }

    [Fact]
    public void IsValid_InvalidPiece_ReturnsFalse()
    {
        Assert.False(PieceType.None.IsValid);
    }
}
