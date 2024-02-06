using m8.chess.MoveGeneration;
using System.Diagnostics;

namespace m8.chess.Perft;

/// <summary>
///  Class containing the perft test functionality
/// </summary>
public class Perft
{
    private IPerftObserver _observer;
    private Board _board;
    private uint _depth;

    private List<Move>[] _moves = new List<Move>[16];

    /// <summary>
    ///  Constructor.
    /// </summary>
    public Perft(Board board,
                 uint depth,
                 IPerftObserver observer)
    {
        _board = board;
        _depth = depth;
        _observer = observer;

        for(var i = 0; i < _moves.Length; ++i)
        {
            _moves[i] = new List<Move>(256);
        }
    }

    public void RunPerftTest()
    {
        var sw = Stopwatch.StartNew();
        ulong totalCount = 0;

        // TODO : We must use a better data structure as move list
        var moves = _moves[_depth];
        MoveGeneration.MoveGeneration.GenerateQuietMoves(_board, moves);
        MoveGeneration.MoveGeneration.GenerateCaptures(_board, moves);

        for (var i = 0; i < moves.Count; ++i)
        {
            Move move = moves[i];
            var unmakeInfo = _board.Make(move);

            ulong partialCount = _depth == 1 ? 1 : RecursivePerft(_depth - 1);
            totalCount += partialCount;

            _board.Unmake(move, unmakeInfo);

            _observer.OnPartialResult(moves[i], partialCount);
        }

        var elapsed = sw.Elapsed;

        _observer.OnPerftCompleted(totalCount, elapsed);
    }

    private ulong RecursivePerft(uint depth)
    {
        ulong count = 0;

        var moves = _moves[depth];
        moves.Clear();

        MoveGeneration.MoveGeneration.GenerateQuietMoves(_board, moves);
        MoveGeneration.MoveGeneration.GenerateCaptures(_board, moves);

        for (var i = 0; i < moves.Count; ++i)
        {
            Move move = moves[i];
            var unmakeInfo = _board.Make(move);

            count += depth == 1 ? 1 : RecursivePerft(depth - 1);

            _board.Unmake(move, unmakeInfo);
        }

        return count;
    }
}
