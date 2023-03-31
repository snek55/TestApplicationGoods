using Goods.Api.Extensions;
using Goods.Api.ViewObjects;
using Goods.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Goods.Api.Controllers;

/// <summary>
/// Controller for managing product categories.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoriesService _productCategoriesService;
    public ProductCategoriesController(IProductCategoriesService productCategoriesService)
    {
        _productCategoriesService = productCategoriesService;
    }

    /// <summary>
    /// Retrieves all product categories.
    /// </summary>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A list of all product categories.</returns>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _productCategoriesService.GetCategories(cancellationToken);
        
        return Ok(result.ToCategoryViewObjects());
    }

    /// <summary>
    /// Retrieves a specific product category by its ID.
    /// </summary>
    /// <param name="id">The ID of the product category to retrieve.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>The product category with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _productCategoriesService.GetCategories(cancellationToken);
        
        return Ok(result.ToCategoryViewObjects());
    }

    /// <summary>
    /// Creates a new product category.
    /// </summary>
    /// <param name="categoryViewObject">The product category to create.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>The created product category.</returns>
    [HttpPost]
    public async Task<IActionResult> Post(CategoryViewObject categoryViewObject, CancellationToken cancellationToken)
    {
        var dbEntity = categoryViewObject.ToDbEntity();
        dbEntity.Id = 0;

        var category = await _productCategoriesService.AddCategory(dbEntity, cancellationToken);
        var result = category.ToViewObject();

        return Created($"/{result.Id}", result);
    }

    /// <summary>
    /// Deletes a product category by its ID.
    /// </summary>
    /// <param name="id">The ID of the product category to delete.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A response indicating the result of the deletion.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _productCategoriesService.DeleteCategory(id, cancellationToken);

        return Ok();
    }
}