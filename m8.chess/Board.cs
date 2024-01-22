using m8.chess.Exceptions;
using m8.common;
using m8.common.Extensions;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace m8.chess;

/// <summary>
///  Represents a chess board containing the position of a chess game.
/// </summary>
public class Board
{
    #region Constants

    private const int SQUARES_ON_BOARD = 64;

    public const string STARTING_POSITION_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    #endregion

    #region Private Fields

    private Piece[] _board = new Piece[SQUARES_ON_BOARD];
    private Bitboard[] _pieces = new Bitboard[Piece.MAX_VALUE + 1];
    private Bitboard[] _colors = new Bitboard[Color.MAX_VALUE + 1];
    private File[] _castlingFiles = new File[2 + 1];
    private File _enPassantFile = File.Invalid;
    private CastlingOptions _castlingOptions = CastlingOptions.None;
    private Color _sideToMove;
    private uint _halfMoveClock = 0;
    private uint _fullMoveNumber = 1;

    #endregion

    #region Contructors

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="fen">
    ///  fen string representing the position to initiate the board</param>
    public Board(string fen = STARTING_POSITION_FEN)
    {
        var it = fen.GetEnumerator();
        var hasNext = it.MoveNext();

        try
        {
            hasNext = LoadXFenPiecePlacement(it, hasNext);
            hasNext = LoadXFenSideToMove(it, hasNext);
            hasNext = LoadXFenCastlingOptions(it, hasNext);
            hasNext = LoadXFenEnPassantSquare(it, hasNext);
            hasNext = LoadXFenHalfMoveClock(it, hasNext);
            _       = LoadXFenFullMoveNumber(it, hasNext);

        }
        catch (InvalidFenException ex)
        {
            throw new InvalidFenException(fen, ex.Reason, ex);
        }
    }

    private bool LoadXFenPiecePlacement(CharEnumerator it, bool hasNext)
    {            
        var file = File.a;
        var rank = Rank.Eight;

        hasNext = it.SkipWhiteSpaces(hasNext);

        while (hasNext && !char.IsWhiteSpace(it.Current))
        {
            // If the current character is a digit we increment the current column
            // value.
            if (char.IsDigit(it.Current))
            {
                var positions = it.Current - '0';
                file = file.MoveRight((sbyte)positions);
            }
            else
            {
                // If the character is a piece we add it to the board at the current
                // position.
                Piece piece = new(it.Current);
                if (piece.IsValid)
                {
                    if (!file.IsValid || !rank.IsValid)
                    {
                        throw new InvalidFenException("Invalid piece placement");
                    }

                    Square sq = new(file, rank);
                    this.AddPiece(sq, piece);
                    file = file.MoveRight();
                }
                else if (it.Current == '/')
                {
                    file = File.a;
                    rank = rank.MoveDown();
                }
                else
                {
                    throw new InvalidFenException("Invalid piece placement");
                }
            }

            hasNext = it.MoveNext();
        }

        return hasNext;
    }

    private bool LoadXFenSideToMove(CharEnumerator it, bool hasNext)
    {
        hasNext = it.SkipWhiteSpaces(hasNext);

        if (!hasNext)
        {
            throw new InvalidFenException("Side to move missing");
        }

        if (it.Current == 'w')
        {
            _sideToMove = Color.White;
        }
        else if (it.Current == 'b')
        {
            _sideToMove = Color.Black;
        }
        else
        {
            throw new InvalidFenException($"Unexpected character ('{it.Current}') as side to move");
        }

        it.MoveNext();

        return hasNext;
    }

    private bool LoadXFenCastlingOptions(CharEnumerator it, bool hasNext)
    {
        hasNext = it.SkipWhiteSpaces(hasNext);

        SetCastlingFile(CastlingSide.QueenSide, File.Invalid);
        SetCastlingFile(CastlingSide.KingSide,  File.Invalid);

        while (hasNext && !char.IsWhiteSpace(it.Current))
        {
            if (it.Current != '-')
            {
                Color color = char.IsUpper(it.Current) ? Color.White : Color.Black;
                Rank firstRank = Rank.First.FlipForBlack(color);
                Piece rook = new(color, PieceType.Rook);
                Square kingSquare = this.GetKingPosition(color);
                File rookFile = File.Invalid;
                CastlingSide castlingSide = CastlingSide.None;

                char lowerCurrentChar = char.ToLower(it.Current);
                if (lowerCurrentChar == 'q')
                {
                    castlingSide = CastlingSide.QueenSide;
                    rookFile = File.AllFiles.Where(x => x < kingSquare.File
                                                   && this[new Square(x, firstRank)].IsValid
                                                   && this[new Square(x, firstRank)] == rook)
                                            .FirstOrDefault();
                    if (!rookFile.IsValid)
                    {
                        throw new InvalidFenException($"No rook on the left of {color} king");
                    }

                }
                else if (lowerCurrentChar == 'k')
                {
                    castlingSide = CastlingSide.KingSide;
                    rookFile = File.AllFiles.Reverse()
                                            .Where(x => x > kingSquare.File
                                                   && this[new Square(x, firstRank)].IsValid
                                                   && this[new Square(x, firstRank)] == rook)
                                            .FirstOrDefault();
                    if (!rookFile.IsValid)
                    {
                        throw new InvalidFenException($"No rook on the right of {color} king");
                    }
                }
                else if ('a' <= lowerCurrentChar && lowerCurrentChar <= 'h')
                {
                    rookFile = new(lowerCurrentChar);
                    castlingSide = rookFile < kingSquare.File ? CastlingSide.QueenSide : CastlingSide.KingSide;
                }

                if (castlingSide != CastlingSide.None)
                {
                    if (GetCastlingFile(castlingSide).IsValid && GetCastlingFile(castlingSide) != rookFile)
                    {
                        throw new InvalidFenException($"ambiguous ({castlingSide}) columns for white and black");
                    }

                    Square rookSquare = new(rookFile, firstRank);
                    if (!this[rookSquare].IsValid || this[rookSquare] != rook)
                    {
                        throw new InvalidFenException($"no rook on square {rookSquare}");
                    }

                    SetCastlingFile(castlingSide, rookFile);
                    SetCastlingOption(color, castlingSide);
                }
            }

            hasNext = it.MoveNext();
        }

        // If we have not determined the castling columns by parsing the fen string
        // we use the default values for traditional chess.
        if (!GetCastlingFile(CastlingSide.QueenSide).IsValid)
        {
            SetCastlingFile(CastlingSide.QueenSide, File.a);
        }

        if (!GetCastlingFile(CastlingSide.KingSide).IsValid)
        {
            SetCastlingFile(CastlingSide.KingSide, File.h);
        }

        return hasNext;
    }

    private bool LoadXFenEnPassantSquare(CharEnumerator it, bool hasNext)
    {
        hasNext = it.SkipWhiteSpaces(hasNext);

        if (!hasNext)
        {
            return false;
        }

        if (it.Current == '-')
        {
            hasNext = it.MoveNext();
        }
        else if ('a' <= it.Current && it.Current <= 'h')
        {
            _enPassantFile = new File(it.Current);

            hasNext = it.MoveNext();
            if (hasNext && '1' <= it.Current && it.Current <= '8')
            {
                hasNext = it.MoveNext();
            }
        }
        else
        {
            throw new InvalidFenException($"Unexpected character ('{it.Current}') for the en passant square");
        }


        return hasNext;
    }

    private bool LoadXFenHalfMoveClock(CharEnumerator it, bool hasNext)
    {
        hasNext = it.SkipWhiteSpaces(hasNext);

        while (hasNext && char.IsDigit(it.Current))
        {
            _halfMoveClock *= 10;
            _halfMoveClock += (uint)(it.Current - '0');

            hasNext = it.MoveNext();
        }

        if (hasNext && !char.IsWhiteSpace(it.Current))
        {
            throw new InvalidFenException($"Unexpected character ('{it.Current}') for the half move clock");
        }
        return hasNext;
    }

    private bool LoadXFenFullMoveNumber(CharEnumerator it, bool hasNext)
    {
        hasNext = it.SkipWhiteSpaces(hasNext);

        _fullMoveNumber = 0;

        while (hasNext && char.IsDigit(it.Current))
        {
            _fullMoveNumber *= 10;
            _fullMoveNumber += (uint)(it.Current - '0');

            hasNext = it.MoveNext();
        }

        if (hasNext && !char.IsWhiteSpace(it.Current))
        {
            throw new InvalidFenException($"Unexpected character ('{it.Current}') for the full move number");
        }

        return hasNext;
    }

    #endregion

    #region Private mutators

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetCastlingFile(CastlingSide side, File file)
    {
        Debug.Assert(side == CastlingSide.QueenSide || side == CastlingSide.KingSide);
        _castlingFiles[(int)side - 1] = file;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetCastlingOption(Color color, CastlingSide side)
    {
        _castlingOptions |= CastlingOptionsHelpers.Create(color, side);
    }

    #endregion

    #region Public accessors

    /// <summary>
    ///  Returns the side to move next.
    /// </summary>
    public Color SideToMove
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _sideToMove;
    }

    /// <summary>
    ///  Returns the file of the pawn that can be taken en passant.
    /// </summary>
    public File EnPassantFile
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _enPassantFile;
    }

    /// <summary>
    ///  Accessor allowing to get the piece on a given square.
    /// </summary>
    /// <param name="sq">Square to get the piece from</param>
    /// <returns></returns>
    public Piece this[Square sq]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(sq.IsValid);
            return _board[sq.Value];
        }
    }

    /// <summary>
    ///  Accessor allowing to get a bitboard representing the position of a specific 
    ///  piece type.
    /// </summary>
    public Bitboard this[Piece pieceType]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(pieceType.IsValid);
            return _pieces[(byte)pieceType];
        }
    }

    /// <summary>
    ///  Accessor allowing to get a bitboard representing the position of all the pieces 
    ///  of a specific color.
    /// </summary>
    public Bitboard this[Color color]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _colors[(byte)color];
    }

    /// <summary>
    ///  Return a bitboard of all occupied squares.
    /// </summary>
    public Bitboard Occupied
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _colors[(byte)Color.White] | _colors[(byte)Color.Black];
    }

    /// <summary>
    ///  Returns the castling options
    /// </summary>
    public CastlingOptions CastlingOptions
    {
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        get => _castlingOptions;
    }
            
    /// <summary>
    ///  Returns the castling file for a specified side
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public File GetCastlingFile(CastlingSide side)
    {
        Debug.Assert(Enum.IsDefined<CastlingSide>(side));
        return _castlingFiles[(int)side - 1];
    }

    /// <summary>
    /// Retrieves the position of the king based on the specified color.
    /// </summary>
    /// <param name="color">The color of the king to find.</param>
    /// <returns>The <see cref="Square"/> representing the position of the king.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Square GetKingPosition(Color color)
    {
        Piece king = new(color, PieceType.King);
        return new((byte)_pieces[(int)king].LSB);
    }

    /// <summary>
    ///  Return the number of halfmoves (or moves by one player) since the last 
    ///  capture or pawn advance. This is used for the fifty-move rule.
    /// </summary>
    public uint HalfMoveClock
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _halfMoveClock;
    }

    /// <summary>
    ///  Return the number of the full move. It starts at 1 and is incremented after
    ///  each move by Black. 
    /// </summary>
    public uint FullMoveNumber
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _fullMoveNumber;
    }

    #region FEN property

    /// <summary>
    ///  Returns a FEN string representing the current position
    /// </summary>
    public string FEN
    {
        get
        {
            var sb = new StringBuilder();

            AppendPiecePlacementData(sb);

            sb.Append(_sideToMove.Character)
              .Append(' ');

            AppendCastingAvailabilities(sb);
            AppendEnPassantTargetSquare(sb);

            sb.Append(_halfMoveClock)
              .Append(' ')
              .Append(_fullMoveNumber);

            return sb.ToString();
        }
    }

    private void AppendPiecePlacementData(StringBuilder sb)
    {
        uint emptySquaresSkipped = 0;
        foreach (var rank in Rank.AllRanks.Reverse())
        {
            foreach (var file in File.AllFiles)
            {
                var sq = new Square(file, rank);
                var piece = this[sq];
                if (piece.IsValid)
                {
                    if (0 < emptySquaresSkipped)
                    {
                        sb.Append(emptySquaresSkipped);
                        emptySquaresSkipped = 0;
                    }
                    sb.Append(piece.Character);
                }
                else
                {
                    ++emptySquaresSkipped;
                }
            }

            if (0 < emptySquaresSkipped)
            {
                sb.Append(emptySquaresSkipped);
                emptySquaresSkipped = 0;
            }

            if (rank > Rank.First)
            {
                sb.Append('/');
            }
        }
        sb.Append(' ');
    }

    private void AppendCastingAvailabilities(StringBuilder sb)
    {
        var anyCastle = AppendCastlingAvailability(sb, Color.White, CastlingSide.KingSide);
        anyCastle |= AppendCastlingAvailability(sb, Color.White, CastlingSide.QueenSide);
        anyCastle |= AppendCastlingAvailability(sb, Color.Black, CastlingSide.KingSide);
        anyCastle |= AppendCastlingAvailability(sb, Color.Black, CastlingSide.QueenSide);

        if (!anyCastle)
        {
            sb.Append('-');
        }
        sb.Append(' ');
    }

    private bool AppendCastlingAvailability(StringBuilder sb, Color color, CastlingSide side)
    {
        var castlingOption = CastlingOptionsHelpers.Create(color, side);
        var canCastle = (_castlingOptions & castlingOption) == castlingOption;

        if (canCastle)
        {
            var rooks = _pieces[(byte)new Piece(color, PieceType.Rook)];
            rooks &= Rank.First.FlipForBlack(color).Bitboard;
            var outterRookSquare = new Square((byte)(side == CastlingSide.KingSide ? rooks.MSB : rooks.LSB));
            var outterRookFile = outterRookSquare.File;

            var castlingColumn = this.GetCastlingFile(side);
            if (outterRookFile == castlingColumn)
            {
                sb.Append(castlingOption.GetCastlingCharacter());
            }
            else
            {
                var c =  castlingColumn.ToString();
                c = color == Color.White ? c.ToUpper() : c.ToLower();
                sb.Append(c);
            }
        }

        return canCastle;
    }

    private void AppendEnPassantTargetSquare(StringBuilder sb)
    {
        if (_enPassantFile.IsValid)
        {
            sb.Append(new Square(_enPassantFile, Rank.Fifth.FlipForBlack(_sideToMove)));
        }
        else
        {
            sb.Append('-');
        }
        sb.Append(' ');
    }

    #endregion

    /// <summary>
    ///  Returns a representation of the board as a string
    /// </summary>
    /// <example>
    ///   ╔═▼═╤═══╤═══╤═══╤═══╤═══╤═══╤═▼═╗
    /// 8 ║▶R◀│▶N◀│▶B◀│▶Q◀│▶K◀│▶B◀│▶N◀│▶R◀║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 7 ║▶P◀│▶P◀│▶P◀│   │▶P◀│   │▶P◀│▶P◀║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 6 ║   │ ⬩ │   │ ⬩ │   │ ⬩ │   │ ⬩ ║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 5 ║ ⬩ │   │ ⬩ │▶P◀│ P │▶P◀│ ⬩ │   ║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 4 ║   │ ⬩ │   │ ⬩ │   │ ⬩ │   │ ⬩ ║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 3 ║ ⬩ │   │ ⬩ │   │ ⬩ │   │ ⬩ │   ║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 2 ║ P │ P │ P │ P │   │ P │ P │ P ║
    ///   ╟───┼───┼───┼───┼───┼───┼───┼───╢
    /// 1 ║ R │ N │ B │ Q │ K │ B │ N │ R ║
    /// =>╚═▲═╧═══╧═══╧═══╧═══╧═══╧═══╧═▲═╝
    ///     a   b   c   d   e   f   g   h
    ///                         △
    /// </example>
    /// <remarks>
    ///  The value returned as a text representation of the current position status. The 
    ///  pieces are represented by their usual English characters. The black pieces are
    ///  framed by two triangles to differentiate them from the white pieces.
    ///  
    ///  There is an arrow displayed to the left of the board either at the top to denote
    ///  that it is black's turn to play or at the bottom if it is white's turn to play.
    ///  
    ///  Triangles appear in the border near the original squares of the rooks if they can
    ///  still castle.
    ///  
    ///  If a pawn can be taken in passing, a triangle appears under the letter 
    ///  representing the column.
    /// </remarks>
    public override string ToString()
    {
        var sb = new StringBuilder();

        // Indicator if black is the side to move
        sb.Append(_sideToMove == Color.Black ? "=>" : "  ");

        // Top border
        sb.Append('╔');
        foreach (var file in File.AllFiles)
        {
            var canCastle = this.GetCastlingFile(CastlingSide.QueenSide) == file
                            && (this.CastlingOptions & CastlingOptions.BlackQueenside) == CastlingOptions.BlackQueenside;
            canCastle |= this.GetCastlingFile(CastlingSide.KingSide) == file
                            && (this.CastlingOptions & CastlingOptions.BlackKingside) == CastlingOptions.BlackKingside;

            sb.Append(canCastle ? "═▼═" : "═══");

            if (file != File.h)
            {
                sb.Append('╤');
            }
        }
        sb.AppendLine("╗");

        foreach (var rank in Rank.AllRanks.Reverse())
        {
            // Rank indicator and left border
            sb.Append(rank.ToString())
              .Append(" ║");

            foreach (var file in File.AllFiles)
            {
                var sq = new Square(file, rank);
                var piece = this[sq];
                if (piece.IsValid)
                {
                    if (piece.Color == Color.White)
                    {
                        sb.Append(' ')
                          .Append(piece.Type.Character)
                          .Append(' ');
                    }
                    else
                    {
                        sb.Append('▶')
                          .Append(piece.Type.Character)
                          .Append('◀');
                    }
                }
                else
                {
                    if ((byte)file % 2 == (byte)rank % 2)
                    {
                        sb.Append(" ⬩ ");
                    }
                    else
                    {
                        sb.Append("   ");
                    }
                    
                }
                
                if (file != File.h)
                {
                    sb.Append('│');
                }
            }

            // Right border 
            sb.AppendLine("║");

            // Rank separator
            if (rank != Rank.First)
            {
                sb.AppendLine("  ╟───┼───┼───┼───┼───┼───┼───┼───╢");
            }
        }

        // Indicator if white is the side to move
        sb.Append(_sideToMove == Color.White ? "=>" : "  ");

        // Bottom border
        sb.Append('╚');
        foreach (var file in File.AllFiles)
        {
            var canCastle = this.GetCastlingFile(CastlingSide.QueenSide) == file
                            && (this.CastlingOptions & CastlingOptions.WhiteQueenside) == CastlingOptions.WhiteQueenside;
            canCastle |= this.GetCastlingFile(CastlingSide.KingSide) == file
                            && (this.CastlingOptions & CastlingOptions.WhiteKingside) == CastlingOptions.WhiteKingside;

            sb.Append(canCastle ? "═▲═" : "═══");

            if (file != File.h)
            {
                sb.Append('╧');
            }
        }
        sb.AppendLine("╝");

        sb.Append("    a   b   c   d   e   f   g   h");

        if (this.EnPassantFile.IsValid)
        {
            sb.AppendLine();
            sb.Append(' ', 4 + (byte)this.EnPassantFile * 4);
            sb.Append('△');
        }

        return sb.ToString();
    }

    #endregion

    #region Public mutators

    /// <summary>
    ///  Add a piece on the board.
    /// </summary>
    /// <param name="sq">Square on wich to add the piece</param>
    /// <param name="piece">Piece to add</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void AddPiece(Square sq, Piece piece)
    {
        Debug.Assert(sq.IsValid, "The square is invalid");
        Debug.Assert(piece.IsValid, "The piece is invalid");
        Debug.Assert(!this[sq].IsValid, "The square is not empty");

        _board[sq.Value] = piece;
        _pieces[(int)piece] = _pieces[(int)piece].Set(sq.Value);
        _colors[(int)piece.Color] = _colors[(int)piece.Color].Set(sq.Value);
    }

    #endregion
}
