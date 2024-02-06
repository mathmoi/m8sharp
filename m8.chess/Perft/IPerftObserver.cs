namespace m8.chess.Perft;

/// <summary>
///  Interface of an observer of a perft test
/// </summary>
public interface IPerftObserver
{
    /// <summary>
    ///  Method called when a partial result is completed.
    /// </summary>
    void OnPartialResult(Move move, ulong count);

    /// <summary>
    ///  Method called when the perft test is completed
    /// </summary>
    /// <param name="count">Perft result</param>
    /// <param name="duration">Duration of the test</param>
    void OnPerftCompleted(ulong count, TimeSpan duration);
}
