using Microsoft.EntityFrameworkCore;
using NSubstituteUsage.API.Database;
using NSubstituteUsage.API.Dtos;
using NSubstituteUsage.API.Entities;

namespace NSubstituteUsage.API.Repositories;

public class HeroRepository : IHeroRepository
{
    private readonly AppDataContext _context;

    public HeroRepository(AppDataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<HeroDto>> GetAllHeroesAsync()
    {
        var heroes = await _context.Heroes.Select(h => new HeroDto
        {
            Id = h.Id,
            Name = h.Name,
            AlterEgo = h.AlterEgo
        }).ToListAsync();

        return heroes;
    }

    public async Task<HeroDto> GetHeroByIdAsync(Guid heroId)
    {
        var hero = await _context.Heroes.FindAsync(heroId);

        if (hero is not null)
            return new HeroDto
            {
                Id = hero.Id,
                Name = hero.Name,
                AlterEgo = hero.AlterEgo
            };

        return null;
    }
    

    public async Task<HeroDto> AddHeroAsync(HeroDto newHero)
    {
        await _context.Heroes.AddAsync(new Hero()
        {
            Id = newHero.Id,
            Name = newHero.Name,
            AlterEgo = newHero.AlterEgo
        });

        await _context.SaveChangesAsync();
        
        return newHero;
    }
}