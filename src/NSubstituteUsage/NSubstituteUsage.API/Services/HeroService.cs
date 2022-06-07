using NSubstituteUsage.API.Dtos;
using NSubstituteUsage.API.Repositories;

namespace NSubstituteUsage.API.Services;

public class HeroService : IHeroService
{
    private readonly IHeroRepository _heroRepository;

    public HeroService(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }
    
    public async Task<IEnumerable<HeroDto>> GetAllHeroesAsync()
    {
        var heroes = await _heroRepository.GetAllHeroesAsync();

        return heroes;
    }

    public async Task<HeroDto> GetHeroByIdAsync(Guid heroId)
    {
        var hero = await _heroRepository.GetHeroByIdAsync(heroId);

        return hero;
    }

    public async Task<HeroDto> AddHeroAsync(HeroDto newHero)
    {
        var heroFromDatabase = await GetHeroByIdAsync(newHero.Id);

        if (heroFromDatabase is not null)
            throw new Exception("There is a hero with the same Id in the system");

        var heroCreated = await _heroRepository.AddHeroAsync(newHero);

        return heroCreated;
    }
}