using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Data;

namespace QuizApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<HomeController> _logger;

    public HomeController(AppDbContext db, ILogger<HomeController> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var count = await _db.Quizzes.CountAsync();
        var latest = await _db.Quizzes
            .AsNoTracking()
            .OrderByDescending(q => q.CreatedUtc)
            .Take(5)
            .ToListAsync();

        ViewBag.TotalQuizzes = count;
        return View(latest);
    }

    public IActionResult Privacy() => View();

    // Friendly error page (wired in Program.cs below)
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
