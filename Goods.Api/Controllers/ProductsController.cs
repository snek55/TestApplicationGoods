using Goods.Api.Extensions;
using Goods.Api.ViewObjects;
using Goods.BusinessLogic.Interface;
using Goods.Models.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Goods.Api.Controllers;

/// <summary>
/// Controller for managing products.
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    /// <summary>
    /// Gets a single product by ID.
    /// </summary>
    /// <param name="id">ID of the product to get.</param>
    /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
    /// <returns>The product with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _productsService.GetProduct(id, cancellationToken);

        return Ok(result?.ToViewObject());
    }

    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
    /// <param name="productFilter">(optional): A filter object that specifies the criteria for filtering the products.</param>
    /// <returns>List of all products.</returns>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, [FromQuery] ProductFilter? productFilter = null)
    {
        var result = await _productsService.GetProducts(productFilter, cancellationToken);

        return Ok(result.ToProductViewObjects());
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="productViewObject">The product to create.</param>
    /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
    /// <returns>The created product.</returns>
    [HttpPost]
    public async Task<IActionResult> Post(ProductViewObject productViewObject, CancellationToken cancellationToken)
    {
        var dbEntity = productViewObject.ToDbEntity();
        
        if (!await _productsService.CheckAdditionalFields(dbEntity, cancellationToken))
        {
            return BadRequest();
        }
        dbEntity.Id = 0;

        var category = await _productsService.AddProduct(dbEntity, cancellationToken);
        var result = category.ToViewObject();

        return Created($"/{result.Id}", result);
    }
}