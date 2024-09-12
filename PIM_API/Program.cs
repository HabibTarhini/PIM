using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using PIM.Business.ProductBusiness;
using PIM.Business.PurchaseOrder;
using PIM.Business.PurchaseReceiptBusiness;
using PIM.Business.Supplier;
using PIM.Repository;
using PIM_Middleware;




var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    logger.Info("Starting Prog");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Host.UseNLog();

    // Add the DbContext
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<ISupplierBusiness, SupplierBusiness>();
    builder.Services.AddScoped<IPurchaseOrderBusiness, PurchaseOrderBusiness>();
    builder.Services.AddScoped<IPurchaseReceiptBusiness, PurchaseReceiptBusiness>();
    builder.Services.AddScoped<IProductBusiness, ProductBusiness>();




    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<PIMMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}