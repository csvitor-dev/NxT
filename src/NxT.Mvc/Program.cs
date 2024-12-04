using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NxT.Infrastructure.Extensions;
using NxT.Mvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Applying default localization and formatting (en-US)
var enUs = new CultureInfo("en-US");
app.UseRequestLocalization(
    new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(enUs),
        SupportedCultures = [enUs],
        SupportedUICultures = [enUs]
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