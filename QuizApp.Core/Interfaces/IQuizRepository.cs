using QuizApp.Domain;

namespace QuizApp.Core.Interfaces;

/// <summary>Provides async CRUD operations for Quiz aggregates.</summary>
public interface IQuizRepository
{
    Task<List<Quiz>> GetAllAsync();
    Task<Quiz?> GetWithChildrenAsync(int id);
    Task<Quiz?> FindAsync(int id);
    Task AddAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
