namespace m8.common.tests
{
    /// <summary>
    ///  Tests for the Bitboard struct
    /// </summary>
    public class BitboardTests
    {
        [Fact]
        public void CreateSingleBit_Index0_CorrectBitboardReturned()
        {
            Bitboard expected = new(0x0000000000000001ul);

            Bitboard actual = Bitboard.CreateSingleBit(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateSingleBit_Index63_CorrectBitboardReturned()
        {
            Bitboard expected = new(0x8000000000000000ul);

            Bitboard actual = Bitboard.CreateSingleBit(63);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Set_SetABitInEmptyBB_CorrectBBCreated()
        {
            Bitboard expected = new(0x0000000000000100ul);

            Bitboard actual = Bitboard.Empty.Set(8);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Index_BitSetToOne_ReturnsTrue()
        {
            Bitboard sut = new(0xffffffff00000000ul);

            var result = sut[52];

            Assert.True(result);
        }

        [Fact]
        public void Index_BitSetTZero_ReturnsFalse()
        {
            Bitboard sut = new(0xffffffff00000000ul);

            var result = sut[13];

            Assert.False(result);
        }
    }
}