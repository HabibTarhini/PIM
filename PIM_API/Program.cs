using Microsoft.AspNetCore.Server.Kestrel.Core;
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
    builder.Services.AddHealthChecks();

    //if (Environment.OSVersion.Platform == PlatformID.Unix)
    //{
    //    builder.WebHost.ConfigureKestrel(options =>
    //    {
    //        options.ListenAnyIP(builder.Configuration.GetValue<int>("PortLinux")); // to listen for incoming http connection on port 5001
    //                                                                               // options.ListenAnyIP(7001, configure => configure.UseHttps()); // to listen for incoming https connection on port 7001
    //    });//linux

    //    //Remove Server header
    //    builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
    //}

    //builder.Services.AddEndpointsApiExplorer();

    //builder.Services.Configure<KestrelServerOptions>(options =>
    //{
    //    options.AllowSynchronousIO = true;
    //});
    //// If using IIS:
    //builder.Services.Configure<IISServerOptions>(options =>
    //{
    //    options.AllowSynchronousIO = true;
    //});


    //builder.Services.AddMvc(op =>
    //{
    //    op.EnableEndpointRouting = false;
    //    op.Filters.Add(typeof(ModelValidationFilter));
    //});

    //builder.Services.AddControllers(
    //    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


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