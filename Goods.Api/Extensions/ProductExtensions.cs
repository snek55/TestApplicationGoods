using Goods.Api.ViewObjects;
using Goods.Models.Entities;

namespace Goods.Api.Extensions;

/// <summary>
/// Contains extension methods for converting between <see cref="Product"/> and <see cref="ProductViewObject"/> types.
/// </summary>
public static class ProductExtensions
{
    /// <summary>
    /// Converts a <see cref="ProductViewObject"/> to a <see cref="Product"/>.
    /// </summary>
    /// <param name="productViewObject">The <see cref="ProductViewObject"/> to convert.</param>
    /// <returns>The converted <see cref="Product"/>.</returns>
    public static Product ToDbEntity(this ProductViewObject categoryViewObject)
    {
        return new Product
        {
            Id = categoryViewObject.Id,
            Name = categoryViewObject.Name,
            CategoryId = categoryViewObject.CategoryId,
            Photo = categoryViewObject.Photo,
            Description = categoryViewObject.Description,
            Price = categoryViewObject.Price,
            CustomFields = categoryViewObject.CustomFields
        };
    }

    /// <summary>
    /// Converts a <see cref="Product"/> to a <see cref="ProductViewObject"/>.
    /// </summary>
    /// <param name="product">The <see cref="Product"/> to convert.</param>
    /// <returns>The converted <see cref="ProductViewObject"/>.</returns>
    public static ProductViewObject ToViewObject(this Product category)
    {
        return new ProductViewObject
        {
            Id = category.Id,
            Name = category.Name,
            CategoryId = category.CategoryId,
            Photo = category.Photo,
            Description = category.Description,
            Price = category.Price,
            CustomFields = category.CustomFields
        };
    }

    /// <summary>
    /// Converts a collection of <see cref="Product"/> objects to a collection of <see cref="ProductViewObject"/> objects.
    /// </summary>
    /// <param name="products">The collection of <see cref="Product"/> objects to convert.</param>
    /// <returns>The collection of converted <see cref="ProductViewObject"/> objects.</returns>
    public static IEnumerable<ProductViewObject> ToProductViewObjects(this IEnumerable<Product> products)
    {
        return products.Select(product => product.ToViewObject());
    }
}