using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Core.Interfaces;
using QuizApp.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

//MVC
builder.Services.AddControllersWithViews();

// EF Core som bruker SQlite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// DAL
builder.Services.AddScoped<IQuizRepository, QuizRepository>();

var app = builder.Build();

// error håndtering
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
