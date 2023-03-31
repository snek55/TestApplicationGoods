namespace Goods.Api.ViewObjects;

/// <summary>
/// Represents a view object for a category.
/// </summary>
public class CategoryViewObject
{
    /// <summary>
    /// Gets or sets the ID of the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets additional fields for the category.
    /// </summary>
    public string[]? AdditionalFields { get; set; }
}