using FluentAssertions;
using m8.chess.MoveGeneration.sliders;
using m8.common;

namespace m8.chess.tests.MoveGeneration.sliders;

public class NaiveSlidersTests
{
    [Fact]
    public void GetRooksAttacks_E4EmptyBoard_CorrectAttacksReturned()
    {
        var expected = new Bitboard(0x10101010ef101010ul);

        var actual = NaiveSliders.GetRooksAttacks(Square.e4, Bitboard.Empty);

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetRooksAttacks_E4EdgeOfBoardOccupied_CorrectAttacksReturned()
    {
        var expected = new Bitboard(0x10101010ef101010ul);
        var occupancy = new Bitboard(0x1000000081000010ul);

        var actual = NaiveSliders.GetRooksAttacks(Square.e4, occupancy);

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetRooksAttacks_E4SquaresNextToRookOccupied_CorrectAttacksReturned()
    {
        var expected = new Bitboard(0x0000001028100000ul);
        var occupancy = new Bitboard(0x0000001028100000ul);

        var actual = NaiveSliders.GetRooksAttacks(Square.e4, occupancy);

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetBishopAttacks_E4EmptyBoard_CorrectAttacksReturned()
    {
        var expected = new Bitboard(0x182442800284482ul);

        var actual = NaiveSliders.GetBishopAttacks(Square.e4, Bitboard.Empty);

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetBishopAttacks_E4EdgeOfBoardOccupied_CorrectAttacksReturned()
    {
        var expected = new Bitboard(0x182442800284482ul);
        var occupancy = new Bitboard(0x180000000000082ul);

        var actual = NaiveSliders.GetBishopAttacks(Square.e4, occupancy);

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetBishopAttacks_E4SquaresNextToRookOccupied_CorrectAttacksReturned()
    {
        var expected = new Bitboard(0x0000002800280000ul);
        var occupancy = new Bitboard(0x0000002800280000ul);

        var actual = NaiveSliders.GetBishopAttacks(Square.e4, occupancy);

        actual.Should().Be(expected);
    }
}
