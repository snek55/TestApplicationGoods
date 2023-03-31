using Goods.BusinessLogic.Interface;
using Goods.DbContext;
using Goods.DbContext.Extensions;
using Goods.Models.Entities;
using Goods.Models.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Goods.BusinessLogic.Services;

/// <summary>
/// Service for managing products.
/// </summary>
public class ProductsService : IProductsService
{
    private readonly GoodsDbContext _dbContext;
    private readonly IProductCategoriesService _productCategoriesService;

    /// <summary>
    /// Constructor for the <see cref="ProductsService"/> class.
    /// </summary>
    /// <param name="goodsDbContext">The <see cref="GoodsDbContext"/> instance to use for accessing the database.</param>
    /// <param name="productCategoriesService">The <see cref="IProductCategoriesService"/> instance to use for managing product categories.</param>
    public ProductsService(GoodsDbContext goodsDbContext, IProductCategoriesService productCategoriesService)
    {
        _dbContext = goodsDbContext;
        _productCategoriesService = productCategoriesService;
    }

    /// <inheritdoc />
    public async Task<Product> AddProduct(Product product, CancellationToken cancellationToken)
    {
        var ent = await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ent.Entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> GetProducts(ProductFilter? productFilter, CancellationToken cancellationToken)
    {
        var array = _dbContext.Products
            .CustomFilter(productFilter?.CustomFields ?? new Dictionary<string, string>())
            .AddFilters(productFilter);

        return await array.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Product?> GetProduct(int id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

        return product;
    }

    /// <inheritdoc />
    public async Task<bool> CheckAdditionalFields(Product product, CancellationToken cancellationToken)
    {
        var category = await _productCategoriesService.GetCategory(product.CategoryId, cancellationToken);

        if (product.CustomFields is not null && category?.AdditionalFields is not null
                                             && product.CustomFields.Count <= category.AdditionalFields.Length)
        {
            return product.CustomFields.Keys.All(key => category.AdditionalFields.Contains(key));
        }

        return false;
    }

    /// <inheritdoc />
    public async Task<string> SavePhoto(IFormFile file, CancellationToken cancellationToken)
    {
        var newName = new Guid().ToString();
        var filePath = Path.Combine(Path.GetTempPath(), newName);
        
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return newName;
    }

    /// <inheritdoc />
    public async Task<IFormFile> GetPhoto(string name, CancellationToken cancellationToken)
    {
        var fileInfo = new FileInfo(name);
        
        var formFile = new FormFile(
            baseStream: new MemoryStream(await File.ReadAllBytesAsync(name, cancellationToken)),
            baseStreamOffset: 0,
            length: fileInfo.Length,
            name: "file",
            fileName: fileInfo.Name
        )
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/octet-stream"
        };

        return formFile;
    }
}