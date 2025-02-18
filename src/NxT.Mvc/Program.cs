﻿using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NxT.Infrastructure.Data.Migrations;
using NxT.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var providerName = args.Length > 0 ? args[0] : string.Empty;
builder.Services.AddInfrastructure(builder.Configuration, providerName);

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

ApplyMigrations(builder.Configuration);

app.Run();

void ApplyMigrations(IConfiguration configuration)
{
    var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    
    Migrator.Migrate(scope.ServiceProvider, configuration, providerName);
}