using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Domain;

namespace QuizApp.Controllers;

public class QuestionsController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<QuestionsController> _logger;
    public QuestionsController(AppDbContext db, ILogger<QuestionsController> logger)
    {
        _db = db; _logger = logger;
    }

    // List questions for one quiz
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

    public IActionResult Create(int quizId) => View(new Question { QuizId = quizId });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Question model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Questions.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Question created.";
        return RedirectToAction(nameof(Index), new { quizId = model.QuizId });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var q = await _db.Questions.FindAsync(id);
        return q == null ? NotFound() : View(q);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Question model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);
        _db.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Question updated.";
        return RedirectToAction(nameof(Index), new { quizId = model.QuizId });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var q = await _db.Questions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return q == null ? NotFound() : View(q);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
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
        return RedirectToAction("Index", "Quizzes");
    }
}
