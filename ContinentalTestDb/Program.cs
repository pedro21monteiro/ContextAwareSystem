using ContinentalTestDb.Data;
using ContinentalTestDb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ContinentalTestDbContext>(options =>
{
    var dbname = System.Environment.GetEnvironmentVariable("DBNAME") ?? "ContinentalTestDb";
    var dbhost = System.Environment.GetEnvironmentVariable("DBHOST") ?? "192.168.28.86";
    var dbuser = System.Environment.GetEnvironmentVariable("DBUSER") ?? "sa";
    var dbpass = System.Environment.GetEnvironmentVariable("DBPASS") ?? "xA6UCjFY";
    options.UseSqlServer("Data Source=" + dbhost + $";Database={dbname};User ID=" + dbuser + ";Password=" + dbpass + ";TrustServerCertificate=Yes;");
});

builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<HttpClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
