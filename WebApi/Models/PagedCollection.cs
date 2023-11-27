namespace DotNetDojo.Models;

public class PagedCollection<T>
{
    #region Properties

    public ICollection<T> Contents { get; }

    public int Page { get; }

    public int PageCount => this.Contents.Count;

    public int Total { get; }

    #endregion

    #region Constructors

    public PagedCollection(int total, int page, ICollection<T> contents)
    {
        this.Total = total;
        this.Page = page;
        this.Contents = contents;
    }

    #endregion
}