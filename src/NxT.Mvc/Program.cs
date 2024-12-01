using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NxT.Mvc.Data;
using NxT.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("db") ??
        throw new InvalidOperationException("Connection string 'db' not found."),
        opt => opt.MigrationsAssembly("NxT.Mvc")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add data service for seeding
builder.Services.AddScoped<SeedingService>();

// Add seller service to the scope
builder.Services.AddScoped<SellerService>();

// Add department service to the scope
builder.Services.AddScoped<DepartmentService>();

// Add sales record service to the scope
builder.Services.AddScoped<SalesRecordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Applying default localization and formatting (en-US)
var enUS = new CultureInfo("en-US");
app.UseRequestLocalization(
    new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(enUS),
        SupportedCultures = [enUS],
        SupportedUICultures = [enUS]
    }
);


using var serviceScope = app.Services.CreateScope();
var services = serviceScope.ServiceProvider;
var seedingService = services.GetRequiredService<SeedingService>();
seedingService.Seed();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();