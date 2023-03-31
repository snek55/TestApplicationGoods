using Goods.BusinessLogic.Interface;
using Goods.BusinessLogic.Services;
using Goods.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

// Apply migrations at startup
using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetService<GoodsDbContext>())
    {
        context.Database.Migrate();
    }
}

app.MapControllers();

app.Run();

void AddServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddControllers();
    webApplicationBuilder.Services.AddEndpointsApiExplorer();
    webApplicationBuilder.Services.AddSwaggerGen();
    webApplicationBuilder.Services.AddDbContext<GoodsDbContext>(options =>
        options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));

    webApplicationBuilder.Services.AddScoped<IProductCategoriesService, ProductCategoriesService>();
    webApplicationBuilder.Services.AddScoped<IProductsService, ProductsService>();
}