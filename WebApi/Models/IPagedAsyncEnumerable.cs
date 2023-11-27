namespace WebApi.Models;

public interface IPagedAsyncEnumerable<out T> : IAsyncEnumerable<T>
{
    #region Properties

    int Page { get; }

    int Total { get; }

    #endregion
}