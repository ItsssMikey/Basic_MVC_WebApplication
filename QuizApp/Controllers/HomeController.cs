using Microsoft.AspNetCore.Mvc;// brukes for Controller og ActionResult
using Microsoft.EntityFrameworkCore; // gir Funksjonalitet for Entity Framework Core
using Microsoft.Extensions.Logging; // For logging av og debugging
using QuizApp.Data;// gir tiilgang til AppDbContext som er databasemiljøet

namespace QuizApp.Controllers;

///hovedkontrolleren for applikasjonen.
public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<HomeController> _logger;

// konstruktøren injiserer databasemiljøet (_db) og loggeren
    public HomeController(AppDbContext db, ILogger<HomeController> logger)
    {
        _db = db;
        _logger = logger;
    }
 // Hovedsiden som henter totalt antall quizer og de 5 nyeste
    public async Task<IActionResult> Index()
    {
       
        var count = await _db.Quizzes.CountAsync();
        var latest = await _db.Quizzes
            .AsNoTracking() //sporer ikke endringer siden dataen kun skal vises
            .OrderByDescending(q => q.CreatedUtc)
            .Take(5)
            .ToListAsync();

// Sender telling til visningen og sender listen over quizzer som modell
        ViewBag.TotalQuizzes = count;
        return View(latest);
    }
        // Viser personvern siden
    public IActionResult Privacy() => View();

    // Viser feilsiden som hindrer mellomlagringer av feilmeldinger
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
