using Microsoft.EntityFrameworkCore;
using NSubstituteUsage.API.Entities;

namespace NSubstituteUsage.API.Database;

public class AppDataContext : DbContext
{
    public DbSet<Hero> Heroes { get; set; }

    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
        
    }
}