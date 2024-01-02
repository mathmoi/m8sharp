using m8.common;

namespace m8.chess.tests;

/// <summary>
///  Tests for the File struct.
/// </summary>
public class FileTests
{
    [Theory]
    [InlineData('a')]
    [InlineData('b')]
    [InlineData('c')]
    [InlineData('d')]
    [InlineData('e')]
    [InlineData('f')]
    [InlineData('g')]
    [InlineData('h')]
    public void IsValid_AllFiles_ReturnsTrue(char file_char)
    {
        File file = new(file_char);

        Assert.True(file.IsValid);
    }

    [Fact]
    public void IsValid_InvalidFile_ReturnsFalse()
    {
        Assert.False(File.Invalid.IsValid);
    }

    [Theory]
    [InlineData('a', 0)]
    [InlineData('b', 1)]
    [InlineData('c', 2)]
    [InlineData('d', 3)]
    [InlineData('e', 4)]
    [InlineData('f', 5)]
    [InlineData('g', 6)]
    [InlineData('h', 7)]
    public void ExplicitOperatorByte_AllFiles_CorrectValues(char file_char, byte expected)
    {
        File file = new(file_char);

        byte actual = (byte)file;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('a', "a")]
    [InlineData('b', "b")]
    [InlineData('c', "c")]
    [InlineData('d', "d")]
    [InlineData('e', "e")]
    [InlineData('f', "f")]
    [InlineData('g', "g")]
    [InlineData('h', "h")]
    public void ToString_AllFiles_CorrectValues(char file_char, string expected)
    {
        File file = new(file_char);

        string actual = file.ToString();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('h',  1, 'g')]
    [InlineData('g',  3, 'd')]
    [InlineData('d',  0, 'd')]
    [InlineData('b', -1, 'c')]
    [InlineData('a', -4, 'e')]
    public void MoveLeft_ExpectedFileReturned(char sut_char, sbyte positions, char expected_char)
    {
        File sut = new(sut_char);
        File expected = new(expected_char);

        File actual = sut.MoveLeft(positions);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('a', 1, 'b')]
    [InlineData('b', 3, 'e')]
    [InlineData('d', 0, 'd')]
    [InlineData('e', -1, 'd')]
    [InlineData('h', -4, 'd')]
    public void MoveRight_ExpectedFileReturned(char sut_char, sbyte positions, char expected_char)
    {
        File sut = new(sut_char);
        File expected = new(expected_char);

        File actual = sut.MoveRight(positions);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData('a', 0x0101010101010101ul)]
    [InlineData('b', 0x0202020202020202ul)]
    [InlineData('c', 0x0404040404040404ul)]
    [InlineData('d', 0x0808080808080808ul)]
    [InlineData('e', 0x1010101010101010ul)]
    [InlineData('f', 0x2020202020202020ul)]
    [InlineData('g', 0x4040404040404040ul)]
    [InlineData('h', 0x8080808080808080ul)]
    public void Bitboard_AllValidValues_CorrectBitboardReturned(char sut_char, ulong expectedValue)
    {
        var sut = new File(sut_char);
        var expectedBb = new Bitboard(expectedValue);

        var result = sut.Bitboard;

        Assert.Equal(expectedBb, result);
    }
}
