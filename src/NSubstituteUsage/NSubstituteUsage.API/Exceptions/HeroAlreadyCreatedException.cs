namespace NSubstituteUsage.API.Exceptions;

public class HeroAlreadyCreatedException : Exception
{
    public readonly Guid HeroId;
    
    public HeroAlreadyCreatedException(string exceptionMessage, Guid heroId) : base(exceptionMessage)
    {
        HeroId = heroId;
    }
}