using Goods.Api.ViewObjects;
using Goods.Models.Entities;

namespace Goods.Api.Extensions;

/// <summary>
/// Contains extension methods for mapping between <see cref="Category"/> entities and <see cref="CategoryViewObject"/> DTOs.
/// </summary>
public static class CategoryExtensions
{
    /// <summary>
    /// Maps a <see cref="CategoryViewObject"/> DTO to a <see cref="Category"/> entity.
    /// </summary>
    /// <param name="categoryViewObject">The <see cref="CategoryViewObject"/> to map.</param>
    /// <returns>The resulting <see cref="Category"/> entity.</returns>
    public static Category ToDbEntity(this CategoryViewObject categoryViewObject)
    {
        return new Category
        {
            Id = categoryViewObject.Id,
            Name = categoryViewObject.Name,
            AdditionalFields = categoryViewObject.AdditionalFields
        };
    }

    /// <summary>
    /// Maps a <see cref="Category"/> entity to a <see cref="CategoryViewObject"/> DTO.
    /// </summary>
    /// <param name="category">The <see cref="Category"/> to map.</param>
    /// <returns>The resulting <see cref="CategoryViewObject"/> DTO.</returns>
    public static CategoryViewObject ToViewObject(this Category category)
    {
        return new CategoryViewObject
        {
            Id = category.Id,
            Name = category.Name,
            AdditionalFields = category.AdditionalFields
        };
    }

    /// <summary>
    /// Maps a collection of <see cref="Category"/> entities to a collection of <see cref="CategoryViewObject"/> DTOs.
    /// </summary>
    public static IEnumerable<CategoryViewObject> ToCategoryViewObjects(this IEnumerable<Category> categories)
    {
        return categories.Select(category => category.ToViewObject());
    }
}