namespace DotNetDojo.Models;

public class PagedAsyncEnumerable<T> : IPagedAsyncEnumerable<T>
{
    #region Fields

    private readonly IAsyncEnumerable<T> myCollection;

    #endregion

    #region Properties

    public int Page { get; }

    public int Total { get; }

    #endregion

    #region Constructors

    public PagedAsyncEnumerable(IAsyncEnumerable<T> collection, int total, int page)
    {
        this.myCollection = collection;
        this.Total = total;
        this.Page = page;
    }

    #endregion

    #region Methods

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return this.myCollection.GetAsyncEnumerator(cancellationToken);
    }

    #endregion
}