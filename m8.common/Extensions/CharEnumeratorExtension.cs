namespace m8.common.Extensions
{
    /// <summary>
    ///  Class containings extension methods for the CharEnumerator class.
    /// </summary>
    public static class CharEnumeratorExtension
    {
        /// <summary>
        ///  Skipe white spaces on a CharEnumerator
        /// </summary>
        /// <param name="enumerator">Enumerator to use</param>
        /// <param name="hasNext">
        ///  Indicate if the enumerator Current can be called.</param>
        /// <returns>
        ///  True if the enumerator end has not been reached</returns>
        public static bool SkipWhiteSpaces(this CharEnumerator enumerator, bool hasNext)
        {
            while (hasNext && char.IsWhiteSpace(enumerator.Current))
            {
                hasNext = enumerator.MoveNext();
            }

            return hasNext;
        }
    }
}
