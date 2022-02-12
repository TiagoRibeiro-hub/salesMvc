var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("SalesConnection"));
});

//Seeding
builder.Services.AddScoped<SeedingService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Services
builder.Services.AddScoped<IDepartmentsService, DepartmentsImplement>();
builder.Services.AddScoped<ISellersService, SellersImplement>();
builder.Services.AddScoped<ISalesRecordsService, SalesRecordsImplement>();
builder.Services.AddScoped<ErrorViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseRequestLocalization(LocalizationOptionsService.Localizations());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
