using DynamicQuery.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbCtxOptions = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
builder.Services.AddSingleton(_ => new DynamicDbContext(dbCtxOptions));

var app = builder.Build();

app.MapGet("/orders", async (
    [FromServices] DynamicDbContext ctx,
    string? filter = null,
    string? order = null,
    string? expand = null) =>
{
    var entries = await ctx.Orders
            .DynamicWhere(filter)
            .DynamicOrderBy(order)
            .DynamicInclude(expand)
            .ToListAsync();

    return entries;
});

using (var serviceScope = app.Services.CreateScope())
{
    var ctx = serviceScope.ServiceProvider.GetRequiredService<DynamicDbContext>();
    var customer = new Customer { Id = "C1" };

    ctx.Add(customer);
    ctx.Add(new Order { Type = "D1", Customer = customer, Lines = new List<OrderLine> { new() { Quantity = 1 }} });
    ctx.Add(new Order { Type = "D2", Customer = customer, Lines = new List<OrderLine> { new() { Quantity = 2 }} });

    await ctx.SaveChangesAsync();
}

app.Run();



