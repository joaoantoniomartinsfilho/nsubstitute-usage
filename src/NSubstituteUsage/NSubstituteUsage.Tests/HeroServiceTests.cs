using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstituteUsage.API.Dtos;
using NSubstituteUsage.API.Repositories;
using NSubstituteUsage.API.Services;
using Xunit;

namespace NSubstituteUsage.Tests;

public class HeroServiceTests
{
    private readonly HeroService _sut;
    private readonly IHeroRepository _heroRepository = Substitute.For<IHeroRepository>();
    
    public HeroServiceTests()
    {
        _sut = new HeroService(_heroRepository);
    }

    [Fact]
    public async Task GetHeroByIdAsync_ShouldReturnAHero_WhenAHeroExists()
    {
        //Arrange
        var heroId = Guid.NewGuid();
        var heroName = "Batman";
        var heroAlterEgo = "Bruce Wayne";
        
        var mockedHero = new HeroDto
        {
            Id = heroId,
            Name = heroName,
            AlterEgo = heroAlterEgo
        };
        
        _heroRepository.GetHeroByIdAsync(heroId).Returns(mockedHero);

        //Act
        var hero = await _sut.GetHeroByIdAsync(heroId);

        //Assert
        Assert.Equal(hero.Id, heroId);
        Assert.Equal(hero.Name, heroName);
        Assert.Equal(hero.AlterEgo, heroAlterEgo);
    }
    
    [Fact]
    public async Task GetAllHeroesAsync_ShouldReturn5Heroes_WhenHeroesExists()
    {
        //Arrange
        var totalHeroes = 5;
        var heroes = new List<HeroDto>()
        {
            new (),
            new (),
            new (),
            new (),
            new (),
        };
        
        _heroRepository.GetAllHeroesAsync().Returns(heroes);

        //Act
        var hero = await _sut.GetAllHeroesAsync();

        //Assert
        Assert.Equal(hero.Count(), totalHeroes);
        await _heroRepository.Received(1).GetAllHeroesAsync();
    }
}