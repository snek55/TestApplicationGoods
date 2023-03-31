namespace Goods.Api.ViewObjects;

/// <summary>
/// Represents a view object for a product.
/// </summary>
public class ProductViewObject
{
    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the ID of the category to which the product belongs.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the photo of the product.
    /// </summary>
    public IFormFile Photo { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets a dictionary of custom fields for the product.
    /// </summary>
    public Dictionary<string, string>? CustomFields { get; set; }
}