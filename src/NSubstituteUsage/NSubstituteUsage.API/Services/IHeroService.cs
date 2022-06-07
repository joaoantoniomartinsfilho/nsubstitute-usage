using NSubstituteUsage.API.Dtos;

namespace NSubstituteUsage.API.Services;

public interface IHeroService
{
    Task<IEnumerable<HeroDto>> GetAllHeroesAsync();
    Task<HeroDto> GetHeroByIdAsync(Guid heroId);
    Task<HeroDto> AddHeroAsync(HeroDto newHero);
}