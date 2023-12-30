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

        Assert.Equal(Piece.BlackRook,   board[Square.a8]);
        Assert.Equal(Piece.BlackKnight, board[Square.b8]);
        Assert.Equal(Piece.BlackBishop, board[Square.c8]);
        Assert.Equal(Piece.BlackQueen,  board[Square.d8]);
        Assert.Equal(Piece.BlackKing,   board[Square.e8]);
        Assert.Equal(Piece.BlackBishop, board[Square.f8]);
        Assert.Equal(Piece.BlackKnight, board[Square.g8]);
        Assert.Equal(Piece.BlackRook,   board[Square.h8]);

        Assert.Equal(Piece.BlackPawn,   board[Square.a7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.b7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.c7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.d7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.e7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.f7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.g7]);
        Assert.Equal(Piece.BlackPawn,   board[Square.h7]);

        for (var rank = Rank.Sixth; Rank.Second < rank; rank = rank.MoveDown())
        {
            foreach (var file in File.AllFiles)
            {
                Square sq = new(file, rank);
                Assert.False(board[sq].IsValid);
            }
        }
        
        Assert.Equal(Piece.WhitePawn,   board[Square.a2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.b2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.c2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.d2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.e2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.f2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.g2]);
        Assert.Equal(Piece.WhitePawn,   board[Square.h2]);

        Assert.Equal(Piece.WhiteRook,   board[Square.a1]);
        Assert.Equal(Piece.WhiteKnight, board[Square.b1]);
        Assert.Equal(Piece.WhiteBishop, board[Square.c1]);
        Assert.Equal(Piece.WhiteQueen,  board[Square.d1]);
        Assert.Equal(Piece.WhiteKing,   board[Square.e1]);
        Assert.Equal(Piece.WhiteBishop, board[Square.f1]);
        Assert.Equal(Piece.WhiteKnight, board[Square.g1]);
        Assert.Equal(Piece.WhiteRook,   board[Square.h1]);
    }

    [Fact]
    public void Ctor_InitialFenPosition_SideToMoveWhite()
    {
        var expected = Color.White;

        var sut = new Board();
        var actual = sut.SideToMove;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_FenWithBlackToMove_SideToMoveBlack()
    {
        var fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1";
        var expected = Color.Black;

        var sut = new Board(fen);
        var actual = sut.SideToMove;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_MissingSideToMove_Throws()
    {
        var fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR";

        Assert.Throws<InvalidFenException>(() => new Board(fen));
    }

    [Fact]
    public void Ctor_SideToMoveUnexpectedCharacter_Throws()
    {
        var fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR KQkq - 0 1";

        Assert.Throws<InvalidFenException>(() => new Board(fen));
    }

    [Fact]
    public void Ctor_InitialFenPosition_CastlingFileAreAH()
    {
        Board board = new();

        Assert.Equal(File.a, board.GetCastlingFile(CastlingSide.QueenSide));
        Assert.Equal(File.h, board.GetCastlingFile(CastlingSide.KingSide));
    }

    [Fact]
    public void Ctor_PositionWhitCastlingOnBG_CastlingFileAreBG()
    {
        Board board = new("1r1k2r1/8/8/8/8/8/8/1R1K2R1 w KQkq - 0 1");

        Assert.Equal(File.b, board.GetCastlingFile(CastlingSide.QueenSide));
        Assert.Equal(File.g, board.GetCastlingFile(CastlingSide.KingSide));

        Assert.Equal(CastlingOptions.All, board.CastlingOptions);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnQueenSideOuterCastling_CastlingFileIsOuter()
    {
        Board board = new("1rrk4/8/8/8/8/8/8/1RRK4 w Qq - 0 1");

        Assert.Equal(File.b, board.GetCastlingFile(CastlingSide.QueenSide));

        Assert.Equal(CastlingOptions.WhiteQueenside | CastlingOptions.BlackQueenside, board.CastlingOptions);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnQueenSideInnerCastling_CastlingFileIsInner()
    {
        Board board = new("1rrk4/8/8/8/8/8/8/1RRK4 w Cc - 0 1");

        Assert.Equal(File.c, board.GetCastlingFile(CastlingSide.QueenSide));

        Assert.Equal(CastlingOptions.WhiteQueenside | CastlingOptions.BlackQueenside, board.CastlingOptions);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnKingSideOuterCastling_CastlingFileIsOuter()
    {
        Board board = new("3krr2/8/8/8/8/8/8/3KRR2 w Kk - 0 1");

        Assert.Equal(File.f, board.GetCastlingFile(CastlingSide.KingSide));

        Assert.Equal(CastlingOptions.WhiteKingside | CastlingOptions.BlackKingside, board.CastlingOptions);
    }

    [Fact]
    public void Ctor_PositionTwoRookOnKingSideInnerCastling_CastlingFileIsInner()
    {
        Board board = new("3krr2/8/8/8/8/8/8/3KRR2 w Ee - 0 1");

        Assert.Equal(File.e, board.GetCastlingFile(CastlingSide.KingSide));

        Assert.Equal(CastlingOptions.WhiteKingside | CastlingOptions.BlackKingside, board.CastlingOptions);
    }

    [Fact]
    public void Ctor_AmbiguousCastlingColumns_Throws()
    {
        Assert.Throws<InvalidFenException>(() => new Board("3k2rr/8/8/8/8/8/8/3KRR2 w Kk - 0 1"));
    }

    [Fact]
    public void Ctor_InitialFenPosition_AllCastlingAllowed()
    {
        Board board = new();
        CastlingOptions expected = CastlingOptions.All;

        var actual = board.CastlingOptions;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_NoCastlingFen_NoCastlingAllowed()
    {
        Board board = new("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1");
        CastlingOptions expected = CastlingOptions.None;

        var actual = board.CastlingOptions;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_InvalidCastlingNoRookOnFile_Throws()
    {
        const string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBN1 w KQkq - 0 1";

        Assert.Throws<InvalidFenException>(() => new Board(fen));
    }

    [Fact]
    public void Ctor_FENWhithEnPassant_EnPassantFileReadCorrectly()
    {
        const string fen = "4k3/8/8/3pP3/8/8/8/4K3 w - d6 0 1";
        File expected = File.d;

        var sut = new Board(fen);
        var actual = sut.EnPassantFile;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_FENWithHalfMoveClock18_HalfMoveClock18()
    {
        const string fen = "4k3/8/8/3pP3/8/8/8/4K3 w - - 18 40";
        uint expected = 18;

        var sut = new Board(fen);
        var actual = sut.HalfMoveClock;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_FENWithFullMoveNumber998_FullMoveNumberIs998()
    {
        const string fen = "4k3/8/8/3pP3/8/8/8/4K3 w - - 18 998";
        uint expected = 998;

        var sut = new Board(fen);
        var actual = sut.FullMoveNumber;

        Assert.Equal(expected, actual);
    }

    #endregion
}
