using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QuizApp.Data;
//dette er for EF core når den lager migrasjonene sine
//kjøres ikke som en del av selve applikasjonen
public class DesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=quizapp.db")
            .Options;

        return new AppDbContext(options);
    }
}
