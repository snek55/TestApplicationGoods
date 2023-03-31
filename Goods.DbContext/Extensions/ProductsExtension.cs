using System.Text;
using Goods.Models.Entities;
using Goods.Models.Filters;
using Microsoft.EntityFrameworkCore;

namespace Goods.DbContext.Extensions;

/// <summary>
/// Extension methods for filtering and querying Product entities.
/// </summary>
public static class ProductsExtension
{
    /// <summary>
    /// Adds filters to an IQueryable of Product based on a ProductFilter parameter.
    /// </summary>
    /// <param name="products">The IQueryable of Product to add filters to.</param>
    /// <param name="filter">The ProductFilter containing the filters to add.</param>
    /// <returns>The IQueryable of Product with the added filters.</returns>
    public static IQueryable<Product> AddFilters(this IQueryable<Product> products, ProductFilter? filter)
    {
        if (filter is null)
        {
            return products;
        }

        if (filter.CategoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == filter.CategoryId.Value);
        }

        if (filter.Name is not null)
        {
            products = products.Where(p => p.Name.Contains(filter.Name));
        }

        if (filter.Description is not null)
        {
            products = products.Where(p => p.Description.Contains(filter.Description));
        }

        if (filter.MinPrice.HasValue)
        {
            products = products.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        return products;
    }

    /// <summary>
    /// Adds custom filters to a DbSet of Product based on a Dictionary parameter.
    /// </summary>
    /// <param name="products">The DbSet of Product to add filters to.</param>
    /// <param name="customFields">The custom filters to add.</param>
    /// <returns>The IQueryable of Product with the added custom filters.</returns>
    public static IQueryable<Product> CustomFilter(this DbSet<Product> products, Dictionary<string, string> customFields)
    {
        if (customFields.Any())
        {
            var strBuilder = new StringBuilder("SELECT * FROM \"Products\" as c");

            foreach (var (key, value) in customFields)
            {
                strBuilder.Append($" WHERE c.\"CustomFields\"->>'{key}' = '{value}'");
            }

            return products.FromSqlRaw(strBuilder.ToString());
        }

        return products;
    }
}