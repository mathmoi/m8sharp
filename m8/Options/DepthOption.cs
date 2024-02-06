using System.CommandLine;

namespace m8.Options;

/// <summary>
///  Depth to pass a depth to m8 for a perft test.
/// </summary>
internal class DepthOption : Option<uint>
{
    /// <summary>
    ///  Constructor
    /// </summary>
    public DepthOption()
        : base(aliases:     ["--depth", "-d"],
               description: "Depth of the fen test.")
    {
        IsRequired = true;
    }
}
