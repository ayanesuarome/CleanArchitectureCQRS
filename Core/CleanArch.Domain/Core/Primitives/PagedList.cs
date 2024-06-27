using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CleanArch.Domain.Core.Primitives;

/// <summary>
/// Represents the generic paged list.
/// </summary>
/// <typeparam name="T">The type of list.</typeparam>
public sealed class PagedList<T>
{
    private PagedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        Page = page == 0 ? 1 : page;
        PageSize = pageSize == 0 ? 10 : pageSize;
        TotalCount = totalCount;
        Items = items.ToList();
    }

    /// <summary>
    /// Gets the current page.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Gets the page size. The maximum page size is 100.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets the total number of items.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Gets the flag indicating whether the next page exists.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Gets the flag indicating whether the previous page exists.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// Gets the items.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        int totalCount = await query.CountAsync();

        page = page == 0 ? 1 : page;
        pageSize = pageSize == 0 ? 10 : pageSize;

        T[] items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();

        return new(items, page, pageSize, totalCount);
    }

    public static PagedList<T> Map<TModel>(PagedList<TModel> pagedList, IReadOnlyCollection<T> valuesToMap)
    {
        return new(valuesToMap, pagedList.Page, pagedList.PageSize, pagedList.TotalCount);
    }
}
