using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Interfaces;
using QuizApp.Data;
using QuizApp.Domain;

namespace QuizApp.Core.Repositories;

// dette er verktøyet som lagrer og henter quizer fra databasen
public class QuizRepository : IQuizRepository
{
    // db er koblingen til databasen og kan ikke endres etter den er satt
    private readonly AppDbContext _db;
    public QuizRepository(AppDbContext db) => _db = db;

// Hent alle quizzer fra databasen
    public Task<List<Quiz>> GetAllAsync() =>
        _db.Quizzes.AsNoTracking().OrderBy(q => q.Title).ToListAsync();

// henter en quiz etter ID og tar med spørsmpl og svar
    public Task<Quiz?> GetWithChildrenAsync(int id) =>
        _db.Quizzes
          .Include(q => q.Questions)
            .ThenInclude(q => q.Options)
          .FirstOrDefaultAsync(q => q.Id == id);

// en rask måte å finne en quiz på
    public Task<Quiz?> FindAsync(int id) => _db.Quizzes.FindAsync(id).AsTask();

// legger til en ny quiz
    public async Task AddAsync(Quiz quiz)
    {
        _db.Quizzes.Add(quiz);
        await _db.SaveChangesAsync();
    }

// endre en quiz
    public async Task UpdateAsync(Quiz quiz)
    {
        _db.Quizzes.Update(quiz);
        await _db.SaveChangesAsync();
    }

// slett en quiz
    public async Task DeleteAsync(int id)
    {
        var forekomst = await _db.Quizzes.FindAsync(id);
        if (forekomst != null)
        {
            _db.Quizzes.Remove(forekomst);
            await _db.SaveChangesAsync();
        }
    }

// Finne en quiz i databasen
    public Task<bool> ExistsAsync(int id) => _db.Quizzes.AnyAsync(q => q.Id == id);
}
