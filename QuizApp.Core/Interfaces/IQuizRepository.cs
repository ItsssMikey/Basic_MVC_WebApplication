using QuizApp.Domain;

namespace QuizApp.Core.Interfaces;

//Dette er grensesnittet. Alle klasser som implementerer IQuizRepository
    // må ha nøyaktig disse metodene
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
