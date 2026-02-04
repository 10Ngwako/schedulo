using Microsoft.EntityFrameworkCore;
using Schedulo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScheduloDbContext>(options =>
    options.UseSqlite("Data Source=schedulo.db"));



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ScheduloDbContext>();
    DbSeeder.Seed(db);
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();


app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
