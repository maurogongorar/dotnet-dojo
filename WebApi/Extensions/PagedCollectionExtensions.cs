namespace DotNetDojo.Extensions;

using DotNetDojo.Models;

public static class PagedCollectionExtensions
{
    #region Methods

    public static IPagedAsyncEnumerable<T> ToPagedEnumerable<T>(this IAsyncEnumerable<T> collection, int page, int total)
    {
        return new PagedAsyncEnumerable<T>(collection, total, page);
    }

    public static async Task<PagedCollection<T>> ToPagedCollectionAsync<T>(this IPagedAsyncEnumerable<T> collection)
    {
        return new PagedCollection<T>(collection.Total, collection.Page, await collection.ToListAsync());
    }

    #endregion
}