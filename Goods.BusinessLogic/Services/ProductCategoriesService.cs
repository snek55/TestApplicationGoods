using Goods.BusinessLogic.Interface;
using Goods.DbContext;
using Goods.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Goods.BusinessLogic.Services;

/// <summary>
/// Service for managing product categories.
/// </summary>
public class ProductCategoriesService : IProductCategoriesService
{
    private readonly GoodsDbContext _dbContext;

    /// <summary>
    /// Constructor for the <see cref="ProductCategoriesService"/> class.
    /// </summary>
    /// <param name="goodsDbContext">The <see cref="GoodsDbContext"/> instance to use for accessing the database.</param>
    public ProductCategoriesService(GoodsDbContext goodsDbContext)
    {
        _dbContext = goodsDbContext;
    }

    /// <inheritdoc />
    public async Task<Category> AddCategory(Category category,
        CancellationToken cancellationToken)
    {
        var ent = await _dbContext.Categories.AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ent.Entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
    {
        var array = _dbContext.Categories;

        return await array.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Category?> GetCategory(int id, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

        return category;
    }

    /// <inheritdoc />
    public async Task DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var category = await GetCategory(id, cancellationToken);

        if (category != null) _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}