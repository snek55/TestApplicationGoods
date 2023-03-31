namespace Goods.Models.Entities;

/// <summary>
/// Represents a category that a product can belong to.
/// </summary>
public class Category
{
    /// <summary>
    /// The unique identifier of the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// An optional array of additional fields that can be associated with the category.
    /// </summary>
    public string[]? AdditionalFields { get; set; }
}