namespace m8.common;

public static class MetricSuffix
{
    /// <summary>
    ///  Format a number using the largest metric suffix possible.
    /// </summary>
    /// <param name="number">Number to format</param>
    /// <param name="format">Format of the number before the suffix</param>
    /// <returns></returns>
    public static string ToStringWithMetricSuffix(this double number, string format)
    {
        string[] suffixes = ["", "K", "M", "G", "T", "P", "E"];

        int index = number > 0 ? (int)Math.Log10(number) / 3 : 0;

        // Calculate the value after applying the suffix.
        double value = number / Math.Pow(1000, index);

        return $"{value.ToString(format)}{suffixes[index]}";
    }
}
