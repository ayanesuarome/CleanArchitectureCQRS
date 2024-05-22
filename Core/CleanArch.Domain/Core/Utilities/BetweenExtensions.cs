namespace CleanArch.Domain.Core.Utilities;

public static class BetweenExtensions
{
    /// <summary>
    /// Between check <![CDATA[min <= value <= max]]> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">the value to check.</param>
    /// <param name="min">Inclusive minimum border.</param>
    /// <param name="max">Inclusive maximum border.</param>
    /// <returns>True if the value is between the min & max; otherwise, false.</returns>
    public static bool InclusiveBetween<T>(this T value, T min, T max) where T : IComparable<T>
    {
        return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
    }
}
