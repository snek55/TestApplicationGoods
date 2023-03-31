using Goods.Api.Extensions;
using Goods.Api.ViewObjects;
using Goods.BusinessLogic.Interface;
using Goods.Models.Entities;
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

        var viewObject = result?.ToViewObject();

        if (viewObject is not null)
        {
            viewObject.Photo = await _productsService.GetPhoto(result!.Photo, cancellationToken);
        }

        return Ok(viewObject);
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
        var products = (await _productsService.GetProducts(productFilter, cancellationToken)).ToList();
        var result = new List<ProductViewObject>(products.Count);

        foreach (var product in products)
        {
            var productViewObject = product.ToViewObject();

            productViewObject.Photo = await _productsService.GetPhoto(product.Photo, cancellationToken);

            result.Add(productViewObject);
        }

        return Ok(result);
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
        if (productViewObject.Photo.Length == 0)
        {
            return BadRequest("No image uploaded");
        }

        var dbEntity = productViewObject.ToDbEntity();

        if (!await _productsService.CheckAdditionalFields(dbEntity, cancellationToken))
        {
            return BadRequest("The additional fields from the group did not match with the fields from the product.");
        }
        var fileName = await _productsService.SavePhoto(productViewObject.Photo, cancellationToken);

        dbEntity.Id = 0;
        dbEntity.Photo = fileName;

        var category = await _productsService.AddProduct(dbEntity, cancellationToken);
        var result = category.ToViewObject();

        return Created($"/{result.Id}", result);
    }
}