using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
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
    public async Task GetAllHeroesAsync_ShouldReturn5Heroes_WhenHeroesExists()
    {
        //Arrange
        var mockedHeroes = new List<HeroDto>
        {   
            new(),new(),new(),new(),new()
        };
        _heroRepository.GetAllHeroesAsync().Returns(mockedHeroes);
        
        //Act
        var heroes = await _sut.GetAllHeroesAsync();
        
        //Assert
        Assert.Equal(heroes.Count(), mockedHeroes.Count);
    }
    
    [Fact]
    public async Task AddHeroAsync_ShouldReturnAHero_WhenHeroIsCreated()
    {
        //Arrange
        var heroName = "Batman";
        var heroId = Guid.NewGuid();
        var heroAltergo = "Bruce Wayne";
        
        var newHero = new HeroDto()
        {
            Id = heroId,
            Name = heroName,
            AlterEgo = heroAltergo
        };
        
        _heroRepository.GetHeroByIdAsync(newHero.Id).ReturnsNull();
        _heroRepository.AddHeroAsync(newHero).Returns(newHero);
        
        //Act
        var heroCreated = await _sut.AddHeroAsync(newHero);
        
        //Assert
        Assert.Equal(heroCreated.Id, newHero.Id);
        Assert.Equal(heroCreated.Name, newHero.Name);
        Assert.Equal(heroCreated.AlterEgo, newHero.AlterEgo);
        await _heroRepository.Received(1).GetHeroByIdAsync(newHero.Id);
    }
    
    [Fact]
    public async Task GetByIdHeroAsync_ShouldReturnAHero_WhenHeroExists()
    {
        //Arrange
        var heroName = "Superman";
        var heroId = Guid.NewGuid();
        var heroAltergo = "Clark Kent";
        
        var mockedHeroFromDatabase = new HeroDto()
        {
            Id = heroId,
            Name = heroName,
            AlterEgo = heroAltergo
        };
        
        _heroRepository.GetHeroByIdAsync(mockedHeroFromDatabase.Id).Returns(mockedHeroFromDatabase);
        
        //Act
        var heroFromDatabase = await _sut.GetHeroByIdAsync(heroId);
        
        //Assert
        Assert.Equal(heroFromDatabase.Id, mockedHeroFromDatabase.Id);
        Assert.Equal(heroFromDatabase.Name, mockedHeroFromDatabase.Name);
        Assert.Equal(heroFromDatabase.AlterEgo, mockedHeroFromDatabase.AlterEgo);
        await _heroRepository.Received(1).GetHeroByIdAsync(mockedHeroFromDatabase.Id);
    }
    
    
    
}