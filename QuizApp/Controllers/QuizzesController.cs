using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Interfaces;
using QuizApp.Domain;

namespace QuizApp.Controllers;

public class QuizzesController : Controller
{
    private readonly IQuizRepository _repo;
    private readonly ILogger<QuizzesController> _logger;

    public QuizzesController(IQuizRepository repo, ILogger<QuizzesController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
        => View(await _repo.GetAllAsync());

    public async Task<IActionResult> Details(int id)
    {
        var quiz = await _repo.GetWithChildrenAsync(id);
        return quiz == null ? NotFound() : View(quiz);
    }

    public IActionResult Create() => View(new Quiz());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Quiz quiz)
    {
        if (!ModelState.IsValid) return View(quiz);
        await _repo.AddAsync(quiz);
        TempData["Success"] = "Quiz created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var quiz = await _repo.FindAsync(id);
        return quiz == null ? NotFound() : View(quiz);
    }

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
        {
            _logger.LogError(ex, "Concurrency editing quiz {QuizId}", id);
            if (!await _repo.ExistsAsync(id)) return NotFound();
            TempData["Error"] = "Someone else modified this quiz. Please retry.";
            return View(quiz);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var quiz = await _repo.FindAsync(id);
        return quiz == null ? NotFound() : View(quiz);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        TempData["Success"] = "Quiz deleted.";
        return RedirectToAction(nameof(Index));
    }
}
