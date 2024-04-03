using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.Services.CategoryServices;
using Property.Services.CityServices;
using Property.Services.ContinentServices;
using Property.Services.CountryServices;
using Property.Services.OtherServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.SubCategoryServices;
using Property.Services.SubCategoryServicesRealEstate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add dbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Add Injections
builder.Services.AddScoped<IContinentServices, ContinentServices>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ISubCategoryServicesRealEstate, SubCategoryServicesRealEstate>();
builder.Services.AddScoped<IProductServicesRealEstate, ProductServicesRealEstate>();
builder.Services.AddScoped<IOtherServices, OtherServices>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
