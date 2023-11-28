namespace m8.chess.tests
{
    /// <summary>
    ///  Tests for the Rank struct.
    /// </summary>
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

            Assert.True(rank.IsValid);
        }

        [Fact]
        public void IsValid_InvalidRank_ReturnsFalse()
        {
            Assert.False(Rank.Invalid.IsValid);
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
        public void ExplicitOperatorByte_AllRanks_CorrectValues(char rank_char, byte expected)
        {
            Rank rank = new(rank_char);

            byte actual = (byte)rank;

            Assert.Equal(expected, actual);
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

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('1',  1, '2')]
        [InlineData('3',  3, '6')]
        [InlineData('7',  0, '7')]
        [InlineData('5', -1, '4')]
        [InlineData('8', -4, '4')]
        public void MoveUp_ExpectedRankReturned(char sut_char, sbyte positions, char expected_char)
        {
            Rank sut = new(sut_char);
            Rank expected = new(expected_char);

            Rank actual = sut.MoveUp(positions);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('5',  1, '4')]
        [InlineData('7',  3, '4')]
        [InlineData('2',  0, '2')]
        [InlineData('5', -1, '6')]
        [InlineData('1', -4, '5')]
        public void MoveDown_ExpectedRankReturned(char sut_char, sbyte positions, char expected_char)
        {
            Rank sut = new(sut_char);
            Rank expected = new(expected_char);

            Rank actual = sut.MoveDown(positions);

            Assert.Equal(expected, actual);
        }
    }
}
