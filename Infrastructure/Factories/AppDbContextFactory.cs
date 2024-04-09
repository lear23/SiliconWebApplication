using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace Infrastructure.Factories;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SkolaUppgifter\ASP.NET\SiliconWebApplication\Infrastructure\Data\Sili_LocalDataBase.mdf;Integrated Security=True;Connect Timeout=30");

        return new AppDbContext(optionsBuilder.Options);
    }
}
