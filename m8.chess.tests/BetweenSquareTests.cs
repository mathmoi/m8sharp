using FluentAssertions;
using m8.common;

namespace m8.chess.tests;

public class BetweenSquareTests
{
    [Theory]
    [InlineData("c2", "g6", 0x0000002010080000ul)]
    [InlineData("g6", "c2", 0x0000002010080000ul)]
    [InlineData("g6", "c1", 0x0000000000000000ul)]
    [InlineData("e3", "g1", 0x0000000000002000ul)]
    [InlineData("g1", "e3", 0x0000000000002000ul)]
    public void GetBetween(string fromString, string toString, ulong expectedValue)
    {
        var from = new Square(fromString);
        var to = new Square(toString);
        var expected = new Bitboard(expectedValue);

        var actual = BetweenSquare.GetBetween(from, to);

        actual.Should().Be(expected);
    }
}
