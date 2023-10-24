using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Models;
<<<<<<< HEAD

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StoriesOfTheLandContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("StoriesOfTheLandContext") ?? throw new InvalidOperationException("Connection string 'StoriesOfTheLandContext' not found."));
    
});


=======
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StoriesOfTheLandContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StoriesOfTheLandContext") ?? throw new InvalidOperationException("Connection string 'StoriesOfTheLandContext' not found.")));
>>>>>>> master

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
<<<<<<< HEAD

//https://stackoverflow.com/questions/71461296/how-do-you-do-database-ensurecreated-in-aspnet-core-web-application-using-net

using (var scope = app.Services.CreateScope())
{
    StoriesOfTheLandContext dbContext = scope.ServiceProvider.GetRequiredService<StoriesOfTheLandContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    var Services = scope.ServiceProvider;
    SeedData.Initialize(Services);
=======
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
>>>>>>> master
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
<<<<<<< HEAD
=======

>>>>>>> master
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
<<<<<<< HEAD
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
=======
    pattern: "{controller=Home}/{action=Index}/{id?}");

>>>>>>> master

app.Run();
