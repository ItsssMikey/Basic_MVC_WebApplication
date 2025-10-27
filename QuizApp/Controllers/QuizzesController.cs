using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Interfaces;
using QuizApp.Domain;

namespace QuizApp.Controllers;

// kntroller for å håndtere CRUD operasjoner for quizzer
public class QuizzesController : Controller
{
    private readonly IQuizRepository _repo;
    private readonly ILogger<QuizzesController> _logger;

// konstruktør
    public QuizzesController(IQuizRepository repo, ILogger<QuizzesController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

// viser en liste over alle quizer og bruker repository for å hente alle
    public async Task<IActionResult> Index()
        => View(await _repo.GetAllAsync());

// viser detaljer for en spesifikk quiz
    public async Task<IActionResult> Details(int id)
    {
        var quiz = await _repo.GetWithChildrenAsync(id);
        return quiz == null ? NotFound() : View(quiz);
    }

// viser skjema for å opprette en ny quiz
    public IActionResult Create() => View(new Quiz());

// håndterer forespørsel for å håndtere ny quiz
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Quiz quiz) // Legger til i databasen via repository
    {
        if (!ModelState.IsValid) return View(quiz); // går tilbake til listen
        await _repo.AddAsync(quiz);
        TempData["Success"] = "Quiz created.";
        return RedirectToAction(nameof(Index));
    }

// viser skjema for å redigere en eksisterende quiz
    public async Task<IActionResult> Edit(int id)
    {
        var quiz = await _repo.FindAsync(id);
        return quiz == null ? NotFound() : View(quiz);
    }

// håndterer forespørsel for å redigere eksisterende quiz
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Quiz quiz)
    {
        if (id != quiz.Id) return BadRequest();
        if (!ModelState.IsValid) return View(quiz);

        try
        {
            await _repo.UpdateAsync(quiz);
            TempData["Success"] = "Quiz updated.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException ex)
        // logger feil om det er samtidighetskonflikt of sjekker om quizzen faktisk eksisterer
        // her er et av eksempelene på kode vi har fått fra AI 
        {
            _logger.LogError(ex, "Concurrency editing quiz {QuizId}", id);
            if (!await _repo.ExistsAsync(id)) return NotFound();
            TempData["Error"] = "Someone else modified this quiz. Please retry.";
            return View(quiz);
        }
    }

// viser bekreftelses-side før sletting
    public async Task<IActionResult> Delete(int id)
    {
        var quiz = await _repo.FindAsync(id);
        return quiz == null ? NotFound() : View(quiz);
    }
    // håndterer forespørsel for sletting
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        TempData["Success"] = "Quiz deleted.";
        return RedirectToAction(nameof(Index));
    }
}
