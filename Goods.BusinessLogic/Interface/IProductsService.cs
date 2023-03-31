using Goods.Models.Entities;
using Goods.Models.Filters;

namespace Goods.BusinessLogic.Interface;

/// <summary>
/// Interface for managing products in the system.
/// </summary>
public interface IProductsService
{
    /// <summary>
    /// Adds a new product to the system.
    /// </summary>
    /// <param name="product">The product to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The added product.</returns>
    Task<Product> AddProduct(Product product, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a list of products from the system, filtered by the specified criteria.
    /// </summary>
    /// <param name="productFilter">The filter criteria to apply.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The list of products that match the filter criteria.</returns>
    Task<IEnumerable<Product>> GetProducts(ProductFilter? productFilter, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a product from the system by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The product with the specified ID, or null if not found.</returns>
    Task<Product?> GetProduct(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Checks if a product has valid additional fields.
    /// </summary>
    /// <param name="product">The product to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the product has valid additional fields, false otherwise.</returns>
    Task<bool> CheckAdditionalFields(Product product, CancellationToken cancellationToken);
}