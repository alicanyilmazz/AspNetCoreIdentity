using AspNetCoreIdentityApp.Core.Entities;
using AspNetCoreIdentityApp.Core.Repositories;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.UnitOfWork;
using AspNetCoreIdentityApp.Data.Context;
using AspNetCoreIdentityApp.Data.Repositories;
using AspNetCoreIdentityApp.Data.UnitOfWork;
using AspNetCoreIdentityApp.Service.Services;
using AspNetCoreIdentityApp.Shared.Extensions.Exception;
using AspNetCoreIdentityApp.Shared.Extensions.Validation;
using AspNetCoreIdentityApp.Web.Configuration.DependencyInjection;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.UseCustomValidationResponse();
builder.Services.InstallServices(builder.Configuration,typeof(IServiceInstaller).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseCustomException();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
