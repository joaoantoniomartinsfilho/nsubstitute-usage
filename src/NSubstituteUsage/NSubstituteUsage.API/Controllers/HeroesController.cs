using Microsoft.AspNetCore.Mvc;
using NSubstituteUsage.API.Dtos;
using NSubstituteUsage.API.Exceptions;
using NSubstituteUsage.API.Services;

namespace NSubstituteUsage.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{
    private readonly ILogger<HeroesController> _logger;
    private readonly IHeroService _heroService;

    public HeroesController(ILogger<HeroesController> logger, IHeroService heroService)
    {
        _logger = logger;
        _heroService = heroService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HeroDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _heroService.GetAllHeroesAsync());
    }
    
    [HttpGet("{heroId:guid}")]
    [ProducesResponseType(typeof(HeroDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHeroById(Guid heroId)
    {
        var hero = await _heroService.GetHeroByIdAsync(heroId);

        if (hero is not null) 
            return Ok(hero);
        
        _logger.LogWarning("No hero was found using the given id: {heroId}.", heroId);
        
        return NotFound($"No hero was found using the given id: {heroId}.");
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(HeroDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHero([FromBody] HeroDto newHero)
    {
        try
        {
            var hero = await _heroService.AddHeroAsync(newHero);
            
            return Created(nameof(GetHeroById), new { heroId = hero.Id});
        }
        catch (HeroAlreadyCreatedException exception)
        {
            _logger.LogError(exception.Message);
            return BadRequest(new { message = exception.Message, hero_id = exception.HeroId });
        }
    }
}