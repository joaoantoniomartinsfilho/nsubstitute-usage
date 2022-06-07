using NSubstituteUsage.API.Dtos;

namespace NSubstituteUsage.API.Repositories;

public interface IHeroRepository
{
    Task<IEnumerable<HeroDto>> GetAllHeroesAsync();
    Task<HeroDto> GetHeroByIdAsync(Guid heroId);
    Task<HeroDto> AddHeroAsync(HeroDto newHero);
}