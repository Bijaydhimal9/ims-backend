

namespace Application.Common.Dtos;
public class SearchResult<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
    public IList<T> Items { get; set; }
    public SearchResult()
    {
    }
}