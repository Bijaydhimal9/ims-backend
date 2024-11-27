namespace Application.Common.Dtos;

/// <summary>
/// Base search criteria.
/// </summary>
public class BaseSearchCriteria
{
    
    /// <summary>
    /// Gets or sets the page.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the search.
    /// </summary>
    private string? _search;

    /// <summary>
    /// Gets or sets the search.
    /// </summary>
    public string? Search
    {
        get { return _search; }
        set { _search = value?.Trim().ToLower(); }
    }

    /// <summary>
    /// Gets or sets the sort by.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets the sort type.
    /// </summary>
    public SortType SortType { get; set; } = SortType.Ascending;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseSearchCriteria"/> class.
    /// </summary>
    public BaseSearchCriteria()
    {
    }
}
