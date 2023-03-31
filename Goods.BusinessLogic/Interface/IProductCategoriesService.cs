using Goods.Models.Entities;

namespace Goods.BusinessLogic.Interface;

/// <summary>
/// Represents a service for managing product categories.
/// </summary>
public interface IProductCategoriesService
{
    /// <summary>
    /// Adds a new category to the system.
    /// </summary>
    /// <param name="category">The category to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Category"/> instance representing the newly added category.</returns>
    Task<Category> AddCategory(Category category, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves all categories in the system.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Category"/> instances representing the categories in the system.</returns>
    Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the category with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Category"/> instance representing the category with the specified ID, or null if no such category exists.</returns>
    Task<Category?> GetCategory(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the category with the specified ID from the system.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteCategory(int id, CancellationToken cancellationToken);
}