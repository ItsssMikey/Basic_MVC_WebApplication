using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Interfaces;
using QuizApp.Data;
using QuizApp.Domain;

namespace QuizApp.Core.Repositories;

/// <summary>EF Core-backed repository for quizzes.</summary>
public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _db;
    public QuizRepository(AppDbContext db) => _db = db;

    public Task<List<Quiz>> GetAllAsync() =>
        _db.Quizzes.AsNoTracking().OrderBy(q => q.Title).ToListAsync();

    public Task<Quiz?> GetWithChildrenAsync(int id) =>
        _db.Quizzes
          .Include(q => q.Questions)
            .ThenInclude(q => q.Options)
          .FirstOrDefaultAsync(q => q.Id == id);

    public Task<Quiz?> FindAsync(int id) => _db.Quizzes.FindAsync(id).AsTask();

    public async Task AddAsync(Quiz quiz)
    {
        _db.Quizzes.Add(quiz);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Quiz quiz)
    {
        _db.Quizzes.Update(quiz);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Quizzes.FindAsync(id);
        if (entity != null)
        {
            _db.Quizzes.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }

    public Task<bool> ExistsAsync(int id) => _db.Quizzes.AnyAsync(q => q.Id == id);
}
