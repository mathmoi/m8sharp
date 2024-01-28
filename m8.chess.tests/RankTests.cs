using FluentAssertions;
using m8.common;

namespace m8.chess.tests;

public class RankTests
{
    [Theory]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('3')]
    [InlineData('4')]
    [InlineData('5')]
    [InlineData('6')]
    [InlineData('7')]
    [InlineData('8')]
    public void IsValid_AllRanks_ReturnsTrue(char rank_char)
    {
        Rank rank = new(rank_char);

        rank.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_InvalidRank_ReturnsFalse()
    {
        Rank.Invalid.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData('1', 0)]
    [InlineData('2', 1)]
    [InlineData('3', 2)]
    [InlineData('4', 3)]
    [InlineData('5', 4)]
    [InlineData('6', 5)]
    [InlineData('7', 6)]
    [InlineData('8', 7)]
    public void Value_AllRanks_CorrectValues(char rank_char, byte expected)
    {
        Rank rank = new(rank_char);

        byte actual = rank.Value;

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData('1', "1")]
    [InlineData('2', "2")]
    [InlineData('3', "3")]
    [InlineData('4', "4")]
    [InlineData('5', "5")]
    [InlineData('6', "6")]
    [InlineData('7', "7")]
    [InlineData('8', "8")]
    public void ToString_AllRanks_CorrectValues(char rank_char, string expected)
    {
        Rank rank = new(rank_char);

        string actual = rank.ToString();

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData('1', 1, '2')]
    [InlineData('3', 3, '6')]
    [InlineData('7', 0, '7')]
    [InlineData('5', -1, '4')]
    [InlineData('8', -4, '4')]
    public void MoveUp_ExpectedRankReturned(char sut_char, sbyte positions, char expected_char)
    {
        Rank sut = new(sut_char);
        Rank expected = new(expected_char);

        Rank actual = sut.MoveUp(positions);

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData('5', 1, '4')]
    [InlineData('7', 3, '4')]
    [InlineData('2', 0, '2')]
    [InlineData('5', -1, '6')]
    [InlineData('1', -4, '5')]
    public void MoveDown_ExpectedRankReturned(char sut_char, sbyte positions, char expected_char)
    {
        Rank sut = new(sut_char);
        Rank expected = new(expected_char);

        Rank actual = sut.MoveDown(positions);

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData('1', 0x00000000000000fful)]
    [InlineData('2', 0x000000000000ff00ul)]
    [InlineData('3', 0x0000000000ff0000ul)]
    [InlineData('4', 0x00000000ff000000ul)]
    [InlineData('5', 0x000000ff00000000ul)]
    [InlineData('6', 0x0000ff0000000000ul)]
    [InlineData('7', 0x00ff000000000000ul)]
    [InlineData('8', 0xff00000000000000ul)]
    public void Bitboard_AllValidValues_CorrectBitboardReturned(char sut_char, ulong expectedValue)
    {
        Rank sut = new(sut_char);
        var expectedBb = new Bitboard(expectedValue);

        var result = sut.Bitboard;

        result.Should().Be(expectedBb);
    }
}
