using FluentAssertions;
using m8.common;

namespace m8.chess.tests;

/// <summary>
///  Tests for the Square struct.
/// </summary>
public class SquareTests
{
    [Fact]
    public void File_AllSquares_CorrectFile()
    {
        Square.a1.File.Should().Be(File.a);
        Square.a2.File.Should().Be(File.a);
        Square.a3.File.Should().Be(File.a);
        Square.a4.File.Should().Be(File.a);
        Square.a5.File.Should().Be(File.a);
        Square.a6.File.Should().Be(File.a);
        Square.a7.File.Should().Be(File.a);
        Square.a8.File.Should().Be(File.a);
        Square.b1.File.Should().Be(File.b);
        Square.b2.File.Should().Be(File.b);
        Square.b3.File.Should().Be(File.b);
        Square.b4.File.Should().Be(File.b);
        Square.b5.File.Should().Be(File.b);
        Square.b6.File.Should().Be(File.b);
        Square.b7.File.Should().Be(File.b);
        Square.b8.File.Should().Be(File.b);
        Square.c1.File.Should().Be(File.c);
        Square.c2.File.Should().Be(File.c);
        Square.c3.File.Should().Be(File.c);
        Square.c4.File.Should().Be(File.c);
        Square.c5.File.Should().Be(File.c);
        Square.c6.File.Should().Be(File.c);
        Square.c7.File.Should().Be(File.c);
        Square.c8.File.Should().Be(File.c);
        Square.d1.File.Should().Be(File.d);
        Square.d2.File.Should().Be(File.d);
        Square.d3.File.Should().Be(File.d);
        Square.d4.File.Should().Be(File.d);
        Square.d5.File.Should().Be(File.d);
        Square.d6.File.Should().Be(File.d);
        Square.d7.File.Should().Be(File.d);
        Square.d8.File.Should().Be(File.d);
        Square.e1.File.Should().Be(File.e);
        Square.e2.File.Should().Be(File.e);
        Square.e3.File.Should().Be(File.e);
        Square.e4.File.Should().Be(File.e);
        Square.e5.File.Should().Be(File.e);
        Square.e6.File.Should().Be(File.e);
        Square.e7.File.Should().Be(File.e);
        Square.e8.File.Should().Be(File.e);
        Square.f1.File.Should().Be(File.f);
        Square.f2.File.Should().Be(File.f);
        Square.f3.File.Should().Be(File.f);
        Square.f4.File.Should().Be(File.f);
        Square.f5.File.Should().Be(File.f);
        Square.f6.File.Should().Be(File.f);
        Square.f7.File.Should().Be(File.f);
        Square.f8.File.Should().Be(File.f);
        Square.g1.File.Should().Be(File.g);
        Square.g2.File.Should().Be(File.g);
        Square.g3.File.Should().Be(File.g);
        Square.g4.File.Should().Be(File.g);
        Square.g5.File.Should().Be(File.g);
        Square.g6.File.Should().Be(File.g);
        Square.g7.File.Should().Be(File.g);
        Square.g8.File.Should().Be(File.g);
        Square.h1.File.Should().Be(File.h);
        Square.h2.File.Should().Be(File.h);
        Square.h3.File.Should().Be(File.h);
        Square.h4.File.Should().Be(File.h);
        Square.h5.File.Should().Be(File.h);
        Square.h6.File.Should().Be(File.h);
        Square.h7.File.Should().Be(File.h);
        Square.h8.File.Should().Be(File.h);
    }

    [Fact]
    public void Rank_AllSquares_CorrectRank()
    {
        Square.a1.Rank.Should().Be(Rank.First);
        Square.a2.Rank.Should().Be(Rank.Second);
        Square.a3.Rank.Should().Be(Rank.Third);
        Square.a4.Rank.Should().Be(Rank.Fourth);
        Square.a5.Rank.Should().Be(Rank.Fifth);
        Square.a6.Rank.Should().Be(Rank.Sixth);
        Square.a7.Rank.Should().Be(Rank.Seventh);
        Square.a8.Rank.Should().Be(Rank.Eight);
        Square.b1.Rank.Should().Be(Rank.First);
        Square.b2.Rank.Should().Be(Rank.Second);
        Square.b3.Rank.Should().Be(Rank.Third);
        Square.b4.Rank.Should().Be(Rank.Fourth);
        Square.b5.Rank.Should().Be(Rank.Fifth);
        Square.b6.Rank.Should().Be(Rank.Sixth);
        Square.b7.Rank.Should().Be(Rank.Seventh);
        Square.b8.Rank.Should().Be(Rank.Eight);
        Square.c1.Rank.Should().Be(Rank.First);
        Square.c2.Rank.Should().Be(Rank.Second);
        Square.c3.Rank.Should().Be(Rank.Third);
        Square.c4.Rank.Should().Be(Rank.Fourth);
        Square.c5.Rank.Should().Be(Rank.Fifth);
        Square.c6.Rank.Should().Be(Rank.Sixth);
        Square.c7.Rank.Should().Be(Rank.Seventh);
        Square.c8.Rank.Should().Be(Rank.Eight);
        Square.d1.Rank.Should().Be(Rank.First);
        Square.d2.Rank.Should().Be(Rank.Second);
        Square.d3.Rank.Should().Be(Rank.Third);
        Square.d4.Rank.Should().Be(Rank.Fourth);
        Square.d5.Rank.Should().Be(Rank.Fifth);
        Square.d6.Rank.Should().Be(Rank.Sixth);
        Square.d7.Rank.Should().Be(Rank.Seventh);
        Square.d8.Rank.Should().Be(Rank.Eight);
        Square.e1.Rank.Should().Be(Rank.First);
        Square.e2.Rank.Should().Be(Rank.Second);
        Square.e3.Rank.Should().Be(Rank.Third);
        Square.e4.Rank.Should().Be(Rank.Fourth);
        Square.e5.Rank.Should().Be(Rank.Fifth);
        Square.e6.Rank.Should().Be(Rank.Sixth);
        Square.e7.Rank.Should().Be(Rank.Seventh);
        Square.e8.Rank.Should().Be(Rank.Eight);
        Square.f1.Rank.Should().Be(Rank.First);
        Square.f2.Rank.Should().Be(Rank.Second);
        Square.f3.Rank.Should().Be(Rank.Third);
        Square.f4.Rank.Should().Be(Rank.Fourth);
        Square.f5.Rank.Should().Be(Rank.Fifth);
        Square.f6.Rank.Should().Be(Rank.Sixth);
        Square.f7.Rank.Should().Be(Rank.Seventh);
        Square.f8.Rank.Should().Be(Rank.Eight);
        Square.g1.Rank.Should().Be(Rank.First);
        Square.g2.Rank.Should().Be(Rank.Second);
        Square.g3.Rank.Should().Be(Rank.Third);
        Square.g4.Rank.Should().Be(Rank.Fourth);
        Square.g5.Rank.Should().Be(Rank.Fifth);
        Square.g6.Rank.Should().Be(Rank.Sixth);
        Square.g7.Rank.Should().Be(Rank.Seventh);
        Square.g8.Rank.Should().Be(Rank.Eight);
        Square.h1.Rank.Should().Be(Rank.First);
        Square.h2.Rank.Should().Be(Rank.Second);
        Square.h3.Rank.Should().Be(Rank.Third);
        Square.h4.Rank.Should().Be(Rank.Fourth);
        Square.h5.Rank.Should().Be(Rank.Fifth);
        Square.h6.Rank.Should().Be(Rank.Sixth);
        Square.h7.Rank.Should().Be(Rank.Seventh);
        Square.h8.Rank.Should().Be(Rank.Eight);
    }

    [Fact]
    public void IsValid_AllSquares_ReturnsTrue()
    {
        Square.a1.IsValid.Should().BeTrue();
        Square.b1.IsValid.Should().BeTrue();
        Square.c1.IsValid.Should().BeTrue();
        Square.d1.IsValid.Should().BeTrue();
        Square.e1.IsValid.Should().BeTrue();
        Square.f1.IsValid.Should().BeTrue();
        Square.g1.IsValid.Should().BeTrue();
        Square.h1.IsValid.Should().BeTrue();
        Square.a2.IsValid.Should().BeTrue();
        Square.b2.IsValid.Should().BeTrue();
        Square.c2.IsValid.Should().BeTrue();
        Square.d2.IsValid.Should().BeTrue();
        Square.e2.IsValid.Should().BeTrue();
        Square.f2.IsValid.Should().BeTrue();
        Square.g2.IsValid.Should().BeTrue();
        Square.h2.IsValid.Should().BeTrue();
        Square.a3.IsValid.Should().BeTrue();
        Square.b3.IsValid.Should().BeTrue();
        Square.c3.IsValid.Should().BeTrue();
        Square.d3.IsValid.Should().BeTrue();
        Square.e3.IsValid.Should().BeTrue();
        Square.f3.IsValid.Should().BeTrue();
        Square.g3.IsValid.Should().BeTrue();
        Square.h3.IsValid.Should().BeTrue();
        Square.a4.IsValid.Should().BeTrue();
        Square.b4.IsValid.Should().BeTrue();
        Square.c4.IsValid.Should().BeTrue();
        Square.d4.IsValid.Should().BeTrue();
        Square.e4.IsValid.Should().BeTrue();
        Square.f4.IsValid.Should().BeTrue();
        Square.g4.IsValid.Should().BeTrue();
        Square.h4.IsValid.Should().BeTrue();
        Square.a5.IsValid.Should().BeTrue();
        Square.b5.IsValid.Should().BeTrue();
        Square.c5.IsValid.Should().BeTrue();
        Square.d5.IsValid.Should().BeTrue();
        Square.e5.IsValid.Should().BeTrue();
        Square.f5.IsValid.Should().BeTrue();
        Square.g5.IsValid.Should().BeTrue();
        Square.h5.IsValid.Should().BeTrue();
        Square.a6.IsValid.Should().BeTrue();
        Square.b6.IsValid.Should().BeTrue();
        Square.c6.IsValid.Should().BeTrue();
        Square.d6.IsValid.Should().BeTrue();
        Square.e6.IsValid.Should().BeTrue();
        Square.f6.IsValid.Should().BeTrue();
        Square.g6.IsValid.Should().BeTrue();
        Square.h6.IsValid.Should().BeTrue();
        Square.a7.IsValid.Should().BeTrue();
        Square.b7.IsValid.Should().BeTrue();
        Square.c7.IsValid.Should().BeTrue();
        Square.d7.IsValid.Should().BeTrue();
        Square.e7.IsValid.Should().BeTrue();
        Square.f7.IsValid.Should().BeTrue();
        Square.g7.IsValid.Should().BeTrue();
        Square.h7.IsValid.Should().BeTrue();
        Square.a8.IsValid.Should().BeTrue();
        Square.b8.IsValid.Should().BeTrue();
        Square.c8.IsValid.Should().BeTrue();
        Square.d8.IsValid.Should().BeTrue();
        Square.e8.IsValid.Should().BeTrue();
        Square.f8.IsValid.Should().BeTrue();
        Square.g8.IsValid.Should().BeTrue();
        Square.h8.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_InvalidSquare_ReturnsFalse()
    {
        Square.Invalid.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ToString_AllSquares_CorrectRepresentation()
    {
        Square.a1.ToString().Should().Be("a1");
        Square.b1.ToString().Should().Be("b1");
        Square.c1.ToString().Should().Be("c1");
        Square.d1.ToString().Should().Be("d1");
        Square.e1.ToString().Should().Be("e1");
        Square.f1.ToString().Should().Be("f1");
        Square.g1.ToString().Should().Be("g1");
        Square.h1.ToString().Should().Be("h1");
        Square.a2.ToString().Should().Be("a2");
        Square.b2.ToString().Should().Be("b2");
        Square.c2.ToString().Should().Be("c2");
        Square.d2.ToString().Should().Be("d2");
        Square.e2.ToString().Should().Be("e2");
        Square.f2.ToString().Should().Be("f2");
        Square.g2.ToString().Should().Be("g2");
        Square.h2.ToString().Should().Be("h2");
        Square.a3.ToString().Should().Be("a3");
        Square.b3.ToString().Should().Be("b3");
        Square.c3.ToString().Should().Be("c3");
        Square.d3.ToString().Should().Be("d3");
        Square.e3.ToString().Should().Be("e3");
        Square.f3.ToString().Should().Be("f3");
        Square.g3.ToString().Should().Be("g3");
        Square.h3.ToString().Should().Be("h3");
        Square.a4.ToString().Should().Be("a4");
        Square.b4.ToString().Should().Be("b4");
        Square.c4.ToString().Should().Be("c4");
        Square.d4.ToString().Should().Be("d4");
        Square.e4.ToString().Should().Be("e4");
        Square.f4.ToString().Should().Be("f4");
        Square.g4.ToString().Should().Be("g4");
        Square.h4.ToString().Should().Be("h4");
        Square.a5.ToString().Should().Be("a5");
        Square.b5.ToString().Should().Be("b5");
        Square.c5.ToString().Should().Be("c5");
        Square.d5.ToString().Should().Be("d5");
        Square.e5.ToString().Should().Be("e5");
        Square.f5.ToString().Should().Be("f5");
        Square.g5.ToString().Should().Be("g5");
        Square.h5.ToString().Should().Be("h5");
        Square.a6.ToString().Should().Be("a6");
        Square.b6.ToString().Should().Be("b6");
        Square.c6.ToString().Should().Be("c6");
        Square.d6.ToString().Should().Be("d6");
        Square.e6.ToString().Should().Be("e6");
        Square.f6.ToString().Should().Be("f6");
        Square.g6.ToString().Should().Be("g6");
        Square.h6.ToString().Should().Be("h6");
        Square.a7.ToString().Should().Be("a7");
        Square.b7.ToString().Should().Be("b7");
        Square.c7.ToString().Should().Be("c7");
        Square.d7.ToString().Should().Be("d7");
        Square.e7.ToString().Should().Be("e7");
        Square.f7.ToString().Should().Be("f7");
        Square.g7.ToString().Should().Be("g7");
        Square.h7.ToString().Should().Be("h7");
        Square.a8.ToString().Should().Be("a8");
        Square.b8.ToString().Should().Be("b8");
        Square.c8.ToString().Should().Be("c8");
        Square.d8.ToString().Should().Be("d8");
        Square.e8.ToString().Should().Be("e8");
        Square.f8.ToString().Should().Be("f8");
        Square.g8.ToString().Should().Be("g8");
        Square.h8.ToString().Should().Be("h8");
    }

    [Theory]
    [InlineData("a1", 0x0000000000000001ul)]
    [InlineData("b1", 0x0000000000000002ul)]
    [InlineData("c1", 0x0000000000000004ul)]
    [InlineData("d1", 0x0000000000000008ul)]
    [InlineData("e1", 0x0000000000000010ul)]
    [InlineData("f1", 0x0000000000000020ul)]
    [InlineData("g1", 0x0000000000000040ul)]
    [InlineData("h1", 0x0000000000000080ul)]
    [InlineData("a2", 0x0000000000000100ul)]
    [InlineData("b2", 0x0000000000000200ul)]
    [InlineData("c2", 0x0000000000000400ul)]
    [InlineData("d2", 0x0000000000000800ul)]
    [InlineData("e2", 0x0000000000001000ul)]
    [InlineData("f2", 0x0000000000002000ul)]
    [InlineData("g2", 0x0000000000004000ul)]
    [InlineData("h2", 0x0000000000008000ul)]
    [InlineData("a3", 0x0000000000010000ul)]
    [InlineData("b3", 0x0000000000020000ul)]
    [InlineData("c3", 0x0000000000040000ul)]
    [InlineData("d3", 0x0000000000080000ul)]
    [InlineData("e3", 0x0000000000100000ul)]
    [InlineData("f3", 0x0000000000200000ul)]
    [InlineData("g3", 0x0000000000400000ul)]
    [InlineData("h3", 0x0000000000800000ul)]
    [InlineData("a4", 0x0000000001000000ul)]
    [InlineData("b4", 0x0000000002000000ul)]
    [InlineData("c4", 0x0000000004000000ul)]
    [InlineData("d4", 0x0000000008000000ul)]
    [InlineData("e4", 0x0000000010000000ul)]
    [InlineData("f4", 0x0000000020000000ul)]
    [InlineData("g4", 0x0000000040000000ul)]
    [InlineData("h4", 0x0000000080000000ul)]
    [InlineData("a5", 0x0000000100000000ul)]
    [InlineData("b5", 0x0000000200000000ul)]
    [InlineData("c5", 0x0000000400000000ul)]
    [InlineData("d5", 0x0000000800000000ul)]
    [InlineData("e5", 0x0000001000000000ul)]
    [InlineData("f5", 0x0000002000000000ul)]
    [InlineData("g5", 0x0000004000000000ul)]
    [InlineData("h5", 0x0000008000000000ul)]
    [InlineData("a6", 0x0000010000000000ul)]
    [InlineData("b6", 0x0000020000000000ul)]
    [InlineData("c6", 0x0000040000000000ul)]
    [InlineData("d6", 0x0000080000000000ul)]
    [InlineData("e6", 0x0000100000000000ul)]
    [InlineData("f6", 0x0000200000000000ul)]
    [InlineData("g6", 0x0000400000000000ul)]
    [InlineData("h6", 0x0000800000000000ul)]
    [InlineData("a7", 0x0001000000000000ul)]
    [InlineData("b7", 0x0002000000000000ul)]
    [InlineData("c7", 0x0004000000000000ul)]
    [InlineData("d7", 0x0008000000000000ul)]
    [InlineData("e7", 0x0010000000000000ul)]
    [InlineData("f7", 0x0020000000000000ul)]
    [InlineData("g7", 0x0040000000000000ul)]
    [InlineData("h7", 0x0080000000000000ul)]
    [InlineData("a8", 0x0100000000000000ul)]
    [InlineData("b8", 0x0200000000000000ul)]
    [InlineData("c8", 0x0400000000000000ul)]
    [InlineData("d8", 0x0800000000000000ul)]
    [InlineData("e8", 0x1000000000000000ul)]
    [InlineData("f8", 0x2000000000000000ul)]
    [InlineData("g8", 0x4000000000000000ul)]
    [InlineData("h8", 0x8000000000000000ul)]
    public void Bitboard_AllValidValues_CorrectBitboardReturned(string sutString, ulong expectedValue)
    {
        var sut = new Square(sutString);
        var expectedBb = new Bitboard(expectedValue);

        var result = sut.Bitboard;

        result.Should().Be(expectedBb);
    }
}
