namespace Goods.Models.Entities;

/// <summary>
/// Represents a product that can be sold.
/// </summary>
public class Product
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The ID of the category that the product belongs to.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// The name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The filename of the product's photo.
    /// </summary>
    public string Photo { get; set; }

    /// <summary>
    /// The description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The category that the product belongs to.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// A dictionary of custom fields associated with the product.
    /// </summary>
    public Dictionary<string, string>? CustomFields { get; set; }
}