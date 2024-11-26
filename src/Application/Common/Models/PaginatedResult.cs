namespace Application.Common.Models;

/// <summary>
/// Represents a paginated result
/// </summary>
public class PaginatedResult<T>
{
    /// <summary>
    /// Gets or sets the items in the paginated result
    /// </summary>
    public IEnumerable<T> Items { get; set; }
    /// <summary>
    /// Gets or sets the total count of items
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Gets or sets the page number
    /// </summary>
    public int PageNumber { get; set; }
    /// <summary>
    /// Gets or sets the page size
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// Gets or sets the total pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    /// <summary>
    /// Gets or sets a value indicating whether there is a previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;
    /// <summary>
    /// Gets or sets a value indicating whether there is a next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;
}