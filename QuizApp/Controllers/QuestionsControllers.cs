using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Domain;

namespace QuizApp.Controllers;

// kontroller for å håndtere CRUD operasjoner for spørsmål i quizzer
public class QuestionsController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<QuestionsController> _logger;


    public QuestionsController(AppDbContext db, ILogger<QuestionsController> logger)
    {
        _db = db; _logger = logger;
    }

    //viser en liste over alle spørsmål knyttet til en quiz id. Sjekker først om listen eksisterer
    // så henter den spørsmål og deretter sender quiz-objektet til View
    public async Task<IActionResult> Index(int quizId)
    {
        var quiz = await _db.Quizzes.AsNoTracking().FirstOrDefaultAsync(q => q.Id == quizId);
        if (quiz == null) return NotFound();

        var items = await _db.Questions
            .Where(x => x.QuizId == quizId)
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();

        ViewBag.Quiz = quiz;
        return View(items);
    }

    // viser skjemaet for å opprette et nytt spørsmål
    public IActionResult Create(int quizId) => View(new Question { QuizId = quizId });
    

// håndterer forespørselen om å lagre et nytt spørsmål
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Question model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Questions.Add(model);
        await _db.SaveChangesAsync(); // lagrer endringene i databasen
        TempData["Success"] = "Question created.";
        // går tilbake til splrsmålene for quizen
        return RedirectToAction(nameof(Index), new { quizId = model.QuizId });
    }

// viser skjemaet for å redigere et eksisterende spørsmål
    public async Task<IActionResult> Edit(int id)
    {
        var q = await _db.Questions.FindAsync(id);
        return q == null ? NotFound() : View(q);
    }

// håndterer forespørselen for å oppdatere et eksisterende spørsmål
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Question model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);
        _db.Update(model); // setter modellen til endret status etter å ha validert model ID
        await _db.SaveChangesAsync();
        TempData["Success"] = "Question updated.";
        return RedirectToAction(nameof(Index), new { quizId = model.QuizId });
    }

// Viser bekreftelsesside før sletting
    public async Task<IActionResult> Delete(int id)
    {
        var q = await _db.Questions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return q == null ? NotFound() : View(q);
    }
    //Håndterer forespørselen for å utføre sletting
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)

    // finner spørsmålet i databasen for sletting, så omdirigeres til spørsmål-listen
    {
        var q = await _db.Questions.FindAsync(id);
        if (q != null)
        {
            var quizId = q.QuizId;
            _db.Questions.Remove(q);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Question deleted.";
            return RedirectToAction(nameof(Index), new { quizId });
        }
        // om den ikke finner spørsmålet, omdirigerer den til quiz oversikten
        return RedirectToAction("Index", "Quizzes");
    }
}
