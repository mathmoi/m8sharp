namespace m8.chess.tests;

/// <summary>
///  Tests for the Square struct.
/// </summary>
public class SquareTests
{
    [Fact]
    public void File_AllSquares_CorrectFile()
    {
        Assert.Equal(File.a, Square.a1.File);
        Assert.Equal(File.a, Square.a2.File);
        Assert.Equal(File.a, Square.a3.File);
        Assert.Equal(File.a, Square.a4.File);
        Assert.Equal(File.a, Square.a5.File);
        Assert.Equal(File.a, Square.a6.File);
        Assert.Equal(File.a, Square.a7.File);
        Assert.Equal(File.a, Square.a8.File);

        Assert.Equal(File.b, Square.b1.File);
        Assert.Equal(File.b, Square.b2.File);
        Assert.Equal(File.b, Square.b3.File);
        Assert.Equal(File.b, Square.b4.File);
        Assert.Equal(File.b, Square.b5.File);
        Assert.Equal(File.b, Square.b6.File);
        Assert.Equal(File.b, Square.b7.File);
        Assert.Equal(File.b, Square.b8.File);

        Assert.Equal(File.c, Square.c1.File);
        Assert.Equal(File.c, Square.c2.File);
        Assert.Equal(File.c, Square.c3.File);
        Assert.Equal(File.c, Square.c4.File);
        Assert.Equal(File.c, Square.c5.File);
        Assert.Equal(File.c, Square.c6.File);
        Assert.Equal(File.c, Square.c7.File);
        Assert.Equal(File.c, Square.c8.File);

        Assert.Equal(File.d, Square.d1.File);
        Assert.Equal(File.d, Square.d2.File);
        Assert.Equal(File.d, Square.d3.File);
        Assert.Equal(File.d, Square.d4.File);
        Assert.Equal(File.d, Square.d5.File);
        Assert.Equal(File.d, Square.d6.File);
        Assert.Equal(File.d, Square.d7.File);
        Assert.Equal(File.d, Square.d8.File);

        Assert.Equal(File.e, Square.e1.File);
        Assert.Equal(File.e, Square.e2.File);
        Assert.Equal(File.e, Square.e3.File);
        Assert.Equal(File.e, Square.e4.File);
        Assert.Equal(File.e, Square.e5.File);
        Assert.Equal(File.e, Square.e6.File);
        Assert.Equal(File.e, Square.e7.File);
        Assert.Equal(File.e, Square.e8.File);

        Assert.Equal(File.f, Square.f1.File);
        Assert.Equal(File.f, Square.f2.File);
        Assert.Equal(File.f, Square.f3.File);
        Assert.Equal(File.f, Square.f4.File);
        Assert.Equal(File.f, Square.f5.File);
        Assert.Equal(File.f, Square.f6.File);
        Assert.Equal(File.f, Square.f7.File);
        Assert.Equal(File.f, Square.f8.File);

        Assert.Equal(File.g, Square.g1.File);
        Assert.Equal(File.g, Square.g2.File);
        Assert.Equal(File.g, Square.g3.File);
        Assert.Equal(File.g, Square.g4.File);
        Assert.Equal(File.g, Square.g5.File);
        Assert.Equal(File.g, Square.g6.File);
        Assert.Equal(File.g, Square.g7.File);
        Assert.Equal(File.g, Square.g8.File);

        Assert.Equal(File.h, Square.h1.File);
        Assert.Equal(File.h, Square.h2.File);
        Assert.Equal(File.h, Square.h3.File);
        Assert.Equal(File.h, Square.h4.File);
        Assert.Equal(File.h, Square.h5.File);
        Assert.Equal(File.h, Square.h6.File);
        Assert.Equal(File.h, Square.h7.File);
        Assert.Equal(File.h, Square.h8.File);
    }

    [Fact]
    public void Rank_AllSquares_CorrectRank()
    {
        Assert.Equal(Rank.First,   Square.a1.Rank);
        Assert.Equal(Rank.Second,  Square.a2.Rank);
        Assert.Equal(Rank.Third,   Square.a3.Rank);
        Assert.Equal(Rank.Fourth,  Square.a4.Rank);
        Assert.Equal(Rank.Fifth,   Square.a5.Rank);
        Assert.Equal(Rank.Sixth,   Square.a6.Rank);
        Assert.Equal(Rank.Seventh, Square.a7.Rank);
        Assert.Equal(Rank.Eight,   Square.a8.Rank);

        Assert.Equal(Rank.First,   Square.b1.Rank);
        Assert.Equal(Rank.Second,  Square.b2.Rank);
        Assert.Equal(Rank.Third,   Square.b3.Rank);
        Assert.Equal(Rank.Fourth,  Square.b4.Rank);
        Assert.Equal(Rank.Fifth,   Square.b5.Rank);
        Assert.Equal(Rank.Sixth,   Square.b6.Rank);
        Assert.Equal(Rank.Seventh, Square.b7.Rank);
        Assert.Equal(Rank.Eight,   Square.b8.Rank);

        Assert.Equal(Rank.First,   Square.c1.Rank);
        Assert.Equal(Rank.Second,  Square.c2.Rank);
        Assert.Equal(Rank.Third,   Square.c3.Rank);
        Assert.Equal(Rank.Fourth,  Square.c4.Rank);
        Assert.Equal(Rank.Fifth,   Square.c5.Rank);
        Assert.Equal(Rank.Sixth,   Square.c6.Rank);
        Assert.Equal(Rank.Seventh, Square.c7.Rank);
        Assert.Equal(Rank.Eight,   Square.c8.Rank);

        Assert.Equal(Rank.First,   Square.d1.Rank);
        Assert.Equal(Rank.Second,  Square.d2.Rank);
        Assert.Equal(Rank.Third,   Square.d3.Rank);
        Assert.Equal(Rank.Fourth,  Square.d4.Rank);
        Assert.Equal(Rank.Fifth,   Square.d5.Rank);
        Assert.Equal(Rank.Sixth,   Square.d6.Rank);
        Assert.Equal(Rank.Seventh, Square.d7.Rank);
        Assert.Equal(Rank.Eight,   Square.d8.Rank);

        Assert.Equal(Rank.First,   Square.e1.Rank);
        Assert.Equal(Rank.Second,  Square.e2.Rank);
        Assert.Equal(Rank.Third,   Square.e3.Rank);
        Assert.Equal(Rank.Fourth,  Square.e4.Rank);
        Assert.Equal(Rank.Fifth,   Square.e5.Rank);
        Assert.Equal(Rank.Sixth,   Square.e6.Rank);
        Assert.Equal(Rank.Seventh, Square.e7.Rank);
        Assert.Equal(Rank.Eight,   Square.e8.Rank);

        Assert.Equal(Rank.First,   Square.f1.Rank);
        Assert.Equal(Rank.Second,  Square.f2.Rank);
        Assert.Equal(Rank.Third,   Square.f3.Rank);
        Assert.Equal(Rank.Fourth,  Square.f4.Rank);
        Assert.Equal(Rank.Fifth,   Square.f5.Rank);
        Assert.Equal(Rank.Sixth,   Square.f6.Rank);
        Assert.Equal(Rank.Seventh, Square.f7.Rank);
        Assert.Equal(Rank.Eight,   Square.f8.Rank);

        Assert.Equal(Rank.First,   Square.g1.Rank);
        Assert.Equal(Rank.Second,  Square.g2.Rank);
        Assert.Equal(Rank.Third,   Square.g3.Rank);
        Assert.Equal(Rank.Fourth,  Square.g4.Rank);
        Assert.Equal(Rank.Fifth,   Square.g5.Rank);
        Assert.Equal(Rank.Sixth,   Square.g6.Rank);
        Assert.Equal(Rank.Seventh, Square.g7.Rank);
        Assert.Equal(Rank.Eight,   Square.g8.Rank);

        Assert.Equal(Rank.First,   Square.h1.Rank);
        Assert.Equal(Rank.Second,  Square.h2.Rank);
        Assert.Equal(Rank.Third,   Square.h3.Rank);
        Assert.Equal(Rank.Fourth,  Square.h4.Rank);
        Assert.Equal(Rank.Fifth,   Square.h5.Rank);
        Assert.Equal(Rank.Sixth,   Square.h6.Rank);
        Assert.Equal(Rank.Seventh, Square.h7.Rank);
        Assert.Equal(Rank.Eight,   Square.h8.Rank);
    }

    [Fact]
    public void IsValid_AllSquares_ReturnsTrue()
    {
        Assert.True(Square.a1.IsValid);
        Assert.True(Square.b1.IsValid);
        Assert.True(Square.c1.IsValid);
        Assert.True(Square.d1.IsValid);
        Assert.True(Square.e1.IsValid);
        Assert.True(Square.f1.IsValid);
        Assert.True(Square.g1.IsValid);
        Assert.True(Square.h1.IsValid);

        Assert.True(Square.a2.IsValid);
        Assert.True(Square.b2.IsValid);
        Assert.True(Square.c2.IsValid);
        Assert.True(Square.d2.IsValid);
        Assert.True(Square.e2.IsValid);
        Assert.True(Square.f2.IsValid);
        Assert.True(Square.g2.IsValid);
        Assert.True(Square.h2.IsValid);

        Assert.True(Square.a3.IsValid);
        Assert.True(Square.b3.IsValid);
        Assert.True(Square.c3.IsValid);
        Assert.True(Square.d3.IsValid);
        Assert.True(Square.e3.IsValid);
        Assert.True(Square.f3.IsValid);
        Assert.True(Square.g3.IsValid);
        Assert.True(Square.h3.IsValid);

        Assert.True(Square.a4.IsValid);
        Assert.True(Square.b4.IsValid);
        Assert.True(Square.c4.IsValid);
        Assert.True(Square.d4.IsValid);
        Assert.True(Square.e4.IsValid);
        Assert.True(Square.f4.IsValid);
        Assert.True(Square.g4.IsValid);
        Assert.True(Square.h4.IsValid);

        Assert.True(Square.a5.IsValid);
        Assert.True(Square.b5.IsValid);
        Assert.True(Square.c5.IsValid);
        Assert.True(Square.d5.IsValid);
        Assert.True(Square.e5.IsValid);
        Assert.True(Square.f5.IsValid);
        Assert.True(Square.g5.IsValid);
        Assert.True(Square.h5.IsValid);

        Assert.True(Square.a6.IsValid);
        Assert.True(Square.b6.IsValid);
        Assert.True(Square.c6.IsValid);
        Assert.True(Square.d6.IsValid);
        Assert.True(Square.e6.IsValid);
        Assert.True(Square.f6.IsValid);
        Assert.True(Square.g6.IsValid);
        Assert.True(Square.h6.IsValid);

        Assert.True(Square.a7.IsValid);
        Assert.True(Square.b7.IsValid);
        Assert.True(Square.c7.IsValid);
        Assert.True(Square.d7.IsValid);
        Assert.True(Square.e7.IsValid);
        Assert.True(Square.f7.IsValid);
        Assert.True(Square.g7.IsValid);
        Assert.True(Square.h7.IsValid);

        Assert.True(Square.a8.IsValid);
        Assert.True(Square.b8.IsValid);
        Assert.True(Square.c8.IsValid);
        Assert.True(Square.d8.IsValid);
        Assert.True(Square.e8.IsValid);
        Assert.True(Square.f8.IsValid);
        Assert.True(Square.g8.IsValid);
        Assert.True(Square.h8.IsValid);
    }

    [Fact]
    public void IsValid_InvalidSquare_ReturnsFalse()
    {
        Assert.False(Square.Invalid.IsValid);
    }

    [Fact]
    public void ToString_AllSquares_CorrectRepresentation()
    {
        Assert.Equal("a1", Square.a1.ToString());
        Assert.Equal("b1", Square.b1.ToString());
        Assert.Equal("c1", Square.c1.ToString());
        Assert.Equal("d1", Square.d1.ToString());
        Assert.Equal("e1", Square.e1.ToString());
        Assert.Equal("f1", Square.f1.ToString());
        Assert.Equal("g1", Square.g1.ToString());
        Assert.Equal("h1", Square.h1.ToString());

        Assert.Equal("a2", Square.a2.ToString());
        Assert.Equal("b2", Square.b2.ToString());
        Assert.Equal("c2", Square.c2.ToString());
        Assert.Equal("d2", Square.d2.ToString());
        Assert.Equal("e2", Square.e2.ToString());
        Assert.Equal("f2", Square.f2.ToString());
        Assert.Equal("g2", Square.g2.ToString());
        Assert.Equal("h2", Square.h2.ToString());

        Assert.Equal("a3", Square.a3.ToString());
        Assert.Equal("b3", Square.b3.ToString());
        Assert.Equal("c3", Square.c3.ToString());
        Assert.Equal("d3", Square.d3.ToString());
        Assert.Equal("e3", Square.e3.ToString());
        Assert.Equal("f3", Square.f3.ToString());
        Assert.Equal("g3", Square.g3.ToString());
        Assert.Equal("h3", Square.h3.ToString());

        Assert.Equal("a4", Square.a4.ToString());
        Assert.Equal("b4", Square.b4.ToString());
        Assert.Equal("c4", Square.c4.ToString());
        Assert.Equal("d4", Square.d4.ToString());
        Assert.Equal("e4", Square.e4.ToString());
        Assert.Equal("f4", Square.f4.ToString());
        Assert.Equal("g4", Square.g4.ToString());
        Assert.Equal("h4", Square.h4.ToString());

        Assert.Equal("a5", Square.a5.ToString());
        Assert.Equal("b5", Square.b5.ToString());
        Assert.Equal("c5", Square.c5.ToString());
        Assert.Equal("d5", Square.d5.ToString());
        Assert.Equal("e5", Square.e5.ToString());
        Assert.Equal("f5", Square.f5.ToString());
        Assert.Equal("g5", Square.g5.ToString());
        Assert.Equal("h5", Square.h5.ToString());

        Assert.Equal("a6", Square.a6.ToString());
        Assert.Equal("b6", Square.b6.ToString());
        Assert.Equal("c6", Square.c6.ToString());
        Assert.Equal("d6", Square.d6.ToString());
        Assert.Equal("e6", Square.e6.ToString());
        Assert.Equal("f6", Square.f6.ToString());
        Assert.Equal("g6", Square.g6.ToString());
        Assert.Equal("h6", Square.h6.ToString());

        Assert.Equal("a7", Square.a7.ToString());
        Assert.Equal("b7", Square.b7.ToString());
        Assert.Equal("c7", Square.c7.ToString());
        Assert.Equal("d7", Square.d7.ToString());
        Assert.Equal("e7", Square.e7.ToString());
        Assert.Equal("f7", Square.f7.ToString());
        Assert.Equal("g7", Square.g7.ToString());
        Assert.Equal("h7", Square.h7.ToString());

        Assert.Equal("a8", Square.a8.ToString());
        Assert.Equal("b8", Square.b8.ToString());
        Assert.Equal("c8", Square.c8.ToString());
        Assert.Equal("d8", Square.d8.ToString());
        Assert.Equal("e8", Square.e8.ToString());
        Assert.Equal("f8", Square.f8.ToString());
        Assert.Equal("g8", Square.g8.ToString());
        Assert.Equal("h8", Square.h8.ToString());
    }
}
