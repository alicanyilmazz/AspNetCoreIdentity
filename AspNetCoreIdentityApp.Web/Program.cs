using AspNetCoreIdentityApp.Core.Configuration;
using AspNetCoreIdentityApp.Core.DependencyInjection;
using AspNetCoreIdentityApp.Shared.Extensions.Exception;
using AspNetCoreIdentityApp.Shared.Extensions.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.UseCustomValidationResponse();
builder.Services.InstallServices(builder.Configuration,AppDomain.CurrentDomain.GetAssemblies());
builder.Services.InstallConfigurations(builder.Configuration, AppDomain.CurrentDomain.GetAssemblies());

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
