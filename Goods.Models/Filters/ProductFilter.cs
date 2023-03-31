namespace Goods.Models.Filters;

/// <summary>
/// Represents a filter for querying products.
/// </summary>
public class ProductFilter
{
    /// <summary>
    /// Gets or sets the category ID to filter by.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the name to filter by.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description to filter by.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the minimum price to filter by.
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Gets or sets the maximum price to filter by.
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Gets or sets the custom fields to filter by.
    /// </summary>
    public Dictionary<string, string>? CustomFields { get; set; }
}