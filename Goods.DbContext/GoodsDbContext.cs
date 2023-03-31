using System.Text.Json;
using System.Text.Json.Serialization;
using Goods.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Goods.DbContext;

/// <summary>
/// Represents the database context for the goods application.
/// </summary>
public class GoodsDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GoodsDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public GoodsDbContext(DbContextOptions<GoodsDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the set of categories in the database.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the set of products in the database.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var opt = new JsonSerializerOptions {DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull};

        modelBuilder.Entity<Product>()
            .Property(e => e.CustomFields)
            .HasConversion(
                v => JsonSerializer.Serialize(v, opt),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, opt))
            .HasColumnType("jsonb");
    }
}