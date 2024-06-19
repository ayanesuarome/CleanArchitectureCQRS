using System.Linq.Expressions;

namespace CleanArch.Application.Extensions;

/// <summary>
/// Extension method to work with IQueryable.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Sorts the elements of a sequence in descending or ascending.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="query"/>.</typeparam>
    /// <typeparam name="TKey">The property to sort.</typeparam>
    /// <param name="query">The query to order.</param>
    /// <param name="keySelector">A function to extract the key for sorting.</param>
    /// <param name="sortOrder">Sort order 'asc' or 'desc'. If no value is provided, the expression is ordered in ascending.</param>
    /// <returns>An <see cref="IOrderedQueryable{TSource}"/> whose elements are sorted.</returns>
    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
        this IQueryable<TSource> query,
        Expression<Func<TSource, TKey>> keySelector,
        string? sortOrder)
    {
        if (sortOrder?.ToLower() == "desc")
        {
            return query.OrderByDescending(keySelector);
        }

        return query.OrderBy(keySelector);
    }
}
