using FluentAssertions;
using m8.chess.Exceptions;

namespace m8.chess.tests;

/// <summary>
///  Test for the Board class.
/// </summary>
public class BoardTests
{
    #region Ctor tests

    [Fact]
    public void Ctor_InitialFenPosition_PieceCorrectlyPlaced()
    {
        Board board = new();

        board[Square.a8].Should().Be(Piece.BlackRook);
        board[Square.a8].Should().Be(Piece.BlackRook);
        board[Square.b8].Should().Be(Piece.BlackKnight);
        board[Square.c8].Should().Be(Piece.BlackBishop);
        board[Square.d8].Should().Be(Piece.BlackQueen);
        board[Square.e8].Should().Be(Piece.BlackKing);
        board[Square.f8].Should().Be(Piece.BlackBishop);
        board[Square.g8].Should().Be(Piece.BlackKnight);
        board[Square.h8].Should().Be(Piece.BlackRook);

        board[Square.a7].Should().Be(Piece.BlackPawn);
        board[Square.b7].Should().Be(Piece.BlackPawn);
        board[Square.c7].Should().Be(Piece.BlackPawn);
        board[Square.d7].Should().Be(Piece.BlackPawn);
        board[Square.e7].Should().Be(Piece.BlackPawn);
        board[Square.f7].Should().Be(Piece.BlackPawn);
        board[Square.g7].Should().Be(Piece.BlackPawn);
        board[Square.h7].Should().Be(Piece.BlackPawn);

        for (var rank = Rank.Sixth; Rank.Second < rank; rank = rank.MoveDown())
        {
            foreach (var file in File.AllFiles)
            {
                Square sq = new(file, rank);
                board[sq].IsValid.Should().BeFalse();
            }
        }
        
        board[Square.a2].Should().Be(Piece.WhitePawn);
        board[Square.b2].Should().Be(Piece.WhitePawn);
        board[Square.c2].Should().Be(Piece.WhitePawn);
        board[Square.d2].Should().Be(Piece.WhitePawn);
        board[Square.e2].Should().Be(Piece.WhitePawn);
        board[Square.f2].Should().Be(Piece.WhitePawn);
        board[Square.g2].Should().Be(Piece.WhitePawn);
        board[Square.h2].Should().Be(Piece.WhitePawn);

        board[Square.a1].Should().Be(Piece.WhiteRook);
        board[Square.b1].Should().Be(Piece.WhiteKnight);
        board[Square.c1].Should().Be(Piece.WhiteBishop);
        board[Square.d1].Should().Be(Piece.WhiteQueen);
        board[Square.e1].Should().Be(Piece.WhiteKing);
        board[Square.f1].Should().Be(Piece.WhiteBishop);
        board[Square.g1].Should().Be(Piece.WhiteKnight);
        board[Square.h1].Should().Be(Piece.WhiteRook);
    }

    [Fact]
    public void Ctor_InitialFenPosition_SideToMoveWhite()
    {
        var expected = Color.White;

        var sut = new Board();
        var actual = sut.SideToMove;

        actual.Should().Be(expected);
    }

    [Fact]
    public void Ctor_FenWithBlackToMove_SideToMoveBlack()
    {
        var fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1";
        var expected = Color.Black;

        var sut = new Board(fen);
        var actual = sut.SideToMove;

        actual.Should().Be(expected);
    }

    [Fact]
    public void Ctor_MissingSideToMove_Throws()
    {
        var fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR";

        var act = () => { new Board(fen); };

        act.Should().Throw<InvalidFenException>();
    }

    [Fact]
    public void Ctor_SideToMoveUnexpectedCharacter_Throws()
    {
        var fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR KQkq - 0 1";

        var act = () => { new Board(fen); };

        act.Should().Throw<InvalidFenException>();
    }

    [Fact]
    public void Ctor_InitialFenPosition_CastlingFileAreAH()
    {
        Board board = new();

        var castlingFileQueenSide = board.GetCastlingFile(CastlingSide.QueenSide);
        var castlingFileKingSide  = board.GetCastlingFile(CastlingSide.KingSide);

        castlingFileQueenSide.Should().Be(File.a);
        castlingFileKingSide.Should().Be(File.h);
    }

    [Fact]
    public void Ctor_PositionWhitCastlingOnBG_CastlingFileAreBG()
    {
        Board board = new("1r1k2r1/8/8/8/8/8/8/1R1K2R1 w KQkq - 0 1");

        board.GetCastlingFile(CastlingSide.QueenSide).Should().Be(File.b);
        board.GetCastlingFile(CastlingSide.KingSide).Should().Be(File.g);

        board.CastlingOptions.Should().Be(CastlingOptions.All);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnQueenSideOuterCastling_CastlingFileIsOuter()
    {
        Board board = new("1rrk4/8/8/8/8/8/8/1RRK4 w Qq - 0 1");

        board.GetCastlingFile(CastlingSide.QueenSide).Should().Be(File.b);

        board.CastlingOptions.Should().Be(CastlingOptions.WhiteQueenside | CastlingOptions.BlackQueenside);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnQueenSideInnerCastling_CastlingFileIsInner()
    {
        Board board = new("1rrk4/8/8/8/8/8/8/1RRK4 w Cc - 0 1");

        board.GetCastlingFile(CastlingSide.QueenSide).Should().Be(File.c);

        board.CastlingOptions.Should().Be(CastlingOptions.WhiteQueenside | CastlingOptions.BlackQueenside);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnKingSideOuterCastling_CastlingFileIsOuter()
    {
        Board board = new("3krr2/8/8/8/8/8/8/3KRR2 w Kk - 0 1");

        board.GetCastlingFile(CastlingSide.KingSide).Should().Be(File.f);

        board.CastlingOptions.Should().Be(CastlingOptions.WhiteKingside | CastlingOptions.BlackKingside);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnKingSideInnerCastling_CastlingFileIsInner()
    {
        Board board = new("3krr2/8/8/8/8/8/8/3KRR2 w Ee - 0 1");

        board.GetCastlingFile(CastlingSide.KingSide).Should().Be(File.e);

        board.CastlingOptions.Should().Be(CastlingOptions.WhiteKingside | CastlingOptions.BlackKingside);
    }

    [Fact]
    public void Ctor_AmbiguousCastlingColumns_Throws()
    {
        Action act = () => new Board("3k2rr/8/8/8/8/8/8/3KRR2 w Kk - 0 1");

        act.Should().Throw<InvalidFenException>();
    }

    [Fact]
    public void Ctor_InitialFenPosition_AllCastlingAllowed()
    {
        Board board = new();
        CastlingOptions expected = CastlingOptions.All;

        board.CastlingOptions.Should().Be(expected);
    }

    [Fact]
    public void Ctor_NoCastlingFen_NoCastlingAllowed()
    {
        Board board = new("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1");
        CastlingOptions expected = CastlingOptions.None;

        board.CastlingOptions.Should().Be(expected);
    }

    [Fact]
    public void Ctor_InvalidCastlingNoRookOnFile_Throws()
    {
        Action act = () => new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBN1 w KQkq - 0 1");

        act.Should().Throw<InvalidFenException>();
    }

    [Fact]
    public void Ctor_FENWhithEnPassant_EnPassantFileReadCorrectly()
    {
        const string fen = "4k3/8/8/3pP3/8/8/8/4K3 w - d6 0 1";
        File expected = File.d;

        var sut = new Board(fen);
        sut.EnPassantFile.Should().Be(expected);
    }

    [Fact]
    public void Ctor_FENWithHalfMoveClock18_HalfMoveClock18()
    {
        const string fen = "4k3/8/8/3pP3/8/8/8/4K3 w - - 18 40";
        uint expected = 18;

        var sut = new Board(fen);
        sut.HalfMoveClock.Should().Be(expected);
    }

    [Fact]
    public void Ctor_FENWithFullMoveNumber998_FullMoveNumberIs998()
    {
        const string fen = "4k3/8/8/3pP3/8/8/8/4K3 w - - 18 998";
        uint expected = 998;

        var sut = new Board(fen);
        sut.FullMoveNumber.Should().Be(expected);
    }

    #endregion
}
