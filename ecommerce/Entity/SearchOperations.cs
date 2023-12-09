namespace ecommerce;

public class FilteringParameters
{
    public int? CategoryId { get; set; }
    public string? Name { get; set; }

}

public class PagingParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class SortingParameters
{
    public string? OrderBy { get; set; }
    public string? SortOrder { get; set; } // 1: Ascending, -1: Descending
}
