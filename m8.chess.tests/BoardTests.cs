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

    #region Make tests

    [Fact]
    public void Make_AnyMove_SideToMoveSwitch()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.f3, Square.g4, Piece.WhiteQueen, MoveType.Normal);

        sut.Make(move);

        sut.SideToMove.Should().Be(Color.Black);
    }

    [Fact]
    public void Make_SimpleMove_PieceIsMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.f3, Square.g4, Piece.WhiteQueen, MoveType.Normal);

        sut.Make(move);

        sut[move.From].IsValid.Should().BeFalse();
        sut[move.To].Should().Be(Piece.WhiteQueen);
    }

    [Fact]
    public void Make_SimpleCapture_PieceIsMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.e5, Square.f7, Piece.WhiteKnight, Piece.BlackPawn, MoveType.Capture);

        sut.Make(move);

        sut[move.From].IsValid.Should().BeFalse();
        sut[move.To].Should().Be(Piece.WhiteKnight);
    }

    [Fact]
    public void Make_KingSideCastling_KingAndRookAreMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.e1, Square.g1, Piece.WhiteKing, MoveType.CastleKingSide);

        sut.Make(move);

        sut[Square.e1].IsValid.Should().BeFalse();
        sut[Square.h1].IsValid.Should().BeFalse();
        sut[Square.g1].Should().Be(Piece.WhiteKing);
        sut[Square.f1].Should().Be(Piece.WhiteRook);
    }

    [Fact]
    public void Make_QueenSideCastling_KingAndRookAreMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 0 1");
        var move = new Move(Square.e8, Square.c8, Piece.BlackKing, MoveType.CastleQueenSide);

        sut.Make(move);

        sut[Square.e8].IsValid.Should().BeFalse();
        sut[Square.a8].IsValid.Should().BeFalse();
        sut[Square.c8].Should().Be(Piece.BlackKing);
        sut[Square.d8].Should().Be(Piece.BlackRook);
    }

    [Fact]
    public void Make_PawnMove_PieceIsMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.d5, Square.d6, Piece.WhitePawn, MoveType.PawnMove);

        sut.Make(move);

        sut[Square.d5].IsValid.Should().BeFalse();
        sut[Square.d6].Should().Be(Piece.WhitePawn);
        sut.EnPassantFile.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Make_PawnMoveTwoSquares_EnPassantIsSet()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.a2, Square.a4, Piece.WhitePawn, MoveType.PawnDouble);

        sut.Make(move);

        sut.EnPassantFile.Should().Be(File.a);
    }

    [Fact]
    public void Make_AnyMoveAfterPawnDoublePush_EnPassantIsReset()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/Pp2P3/2N2Q1p/1PPBBPPP/R3K2R b KQkq a3 0 1");
        var move = new Move(Square.g6, Square.g5, Piece.BlackPawn, MoveType.PawnMove);

        sut.Make(move);

        sut.EnPassantFile.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Make_PawnCapture_PieceAreMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.d5, Square.e6, Piece.WhitePawn, Piece.BlackPawn, MoveType.Capture);

        sut.Make(move);

        sut[Square.d5].IsValid.Should().BeFalse();
        sut[Square.e6].Should().Be(Piece.WhitePawn);
    }

    [Fact]
    public void Make_PawnCaptureEnPassant_PieceAreMoved()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/Pp2P3/2N2Q1p/1PPBBPPP/R3K2R b KQkq a3 0 1");
        var move = new Move(Square.b4, Square.a3, Piece.BlackPawn, Piece.WhitePawn, MoveType.EnPassant);

        sut.Make(move);

        sut[Square.b4].IsValid.Should().BeFalse();
        sut[Square.a4].IsValid.Should().BeFalse();
        sut[Square.a3].Should().Be(Piece.BlackPawn);
    }

    [Fact]
    public void Make_PawnPromotion_PieceIsPromoted()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/Pp2P3/2N2Q2/1PPBBPpP/R3K2R b KQkq - 0 2");
        var move = new Move(Square.g2, Square.g1, Piece.BlackPawn, Piece.None, Piece.BlackKnight, MoveType.Promotion);

        sut.Make(move);

        sut[Square.g2].IsValid.Should().BeFalse();
        sut[Square.g1].Should().Be(Piece.BlackKnight);
    }

    [Fact]
    public void Make_PawnTakesAndPromote_PieceIsPromoted()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/Pp2P3/2N2Q2/1PPBBPpP/R3K2R b KQkq - 0 2");
        var move = new Move(Square.g2, Square.h1, Piece.BlackPawn, Piece.WhiteRook, Piece.BlackQueen, MoveType.CapturePromotion);

        sut.Make(move);

        sut[Square.g2].IsValid.Should().BeFalse();
        sut[Square.h1].Should().Be(Piece.BlackQueen);
    }

    [Fact]
    public void Make_CastlingMove_CastlingOptionsAreCleared()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.e1, Square.g1, Piece.WhiteKing, MoveType.CastleKingSide);

        sut.Make(move);

        sut.CastlingOptions.Should().Be(CastlingOptions.BlackKingside | CastlingOptions.BlackQueenside);
    }

    [Fact]
    public void Make_KingSideRookMove_KingSideMoveOptionsReset()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.h1, Square.g1, Piece.WhiteRook, MoveType.Normal);

        sut.Make(move);

        sut.CastlingOptions.Should().Be(CastlingOptions.BlackKingside | CastlingOptions.BlackQueenside | CastlingOptions.WhiteQueenside);
    }

    [Fact]
    public void Make_QueenSideRookMove_QueenSideMoveOptionsReset()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.a1, Square.b1, Piece.WhiteRook, MoveType.Normal);

        sut.Make(move);

        sut.CastlingOptions.Should().Be(CastlingOptions.BlackKingside | CastlingOptions.BlackQueenside | CastlingOptions.WhiteKingside);
    }

    [Fact]
    public void Make_WhiteMove_FullMoveClockNotChanged()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1");
        var move = new Move(Square.a1, Square.b1, Piece.WhiteRook, MoveType.Normal);

        sut.Make(move);

        sut.FullMoveNumber.Should().Be(1);
    }

    [Fact]
    public void Make_BlackMove_FullMoveClockIncremented()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 0 1");
        var move = new Move(Square.b6, Square.c4, Piece.BlackKnight, MoveType.Normal);

        sut.Make(move);

        sut.FullMoveNumber.Should().Be(2);
    }

    [Fact]
    public void Make_QuietMove_HalfMoveClockIncremented()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 12 1");
        var move = new Move(Square.b6, Square.c4, Piece.BlackKnight, MoveType.Normal);

        sut.Make(move);

        sut.HalfMoveClock.Should().Be(13);
    }

    [Fact]
    public void Make_PawnMove_HalfMoveClockReset()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 12 1");
        var move = new Move(Square.g6, Square.g5, Piece.BlackPawn, MoveType.PawnMove);

        sut.Make(move);

        sut.HalfMoveClock.Should().Be(0);
    }

    [Fact]
    public void Make_Capture_HalfMoveClockReset()
    {
        var sut = new Board("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 12 1");
        var move = new Move(Square.f6, Square.e4, Piece.BlackKnight, Piece.WhitePawn, MoveType.Capture);

        sut.Make(move);

        sut.HalfMoveClock.Should().Be(0);
    }

    [Fact]
    public void Make_Chess960QueenSideCastling_KingAndRookAreMoved()
    {
        var sut = new Board("2k2r1r/8/8/8/8/8/8/RRK5 w Bf - 0 1");
        var move = new Move(Square.c1, Square.c1, Piece.WhiteKing, MoveType.CastleQueenSide);

        sut.Make(move);

        sut[Square.b1].IsValid.Should().BeFalse();
        sut[Square.c1].Should().Be(Piece.WhiteKing);
        sut[Square.d1].Should().Be(Piece.WhiteRook);
    }

    [Fact]
    public void Make_Chess960KingSideCastling_KingAndRookAreMoved()
    {
        var sut = new Board("2k2r1r/8/8/8/8/8/8/RRK5 b Bf - 0 1");
        var move = new Move(Square.c8, Square.g8, Piece.BlackKing, MoveType.CastleKingSide);

        sut.Make(move);

        sut[Square.c8].IsValid.Should().BeFalse();
        sut[Square.f8].Should().Be(Piece.BlackRook);
        sut[Square.g8].Should().Be(Piece.BlackKing);
    }

    #endregion
}
