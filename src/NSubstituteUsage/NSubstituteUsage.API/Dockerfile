FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NSubstituteUsage.API/NSubstituteUsage.API.csproj", "NSubstituteUsage.API/"]
RUN dotnet restore "NSubstituteUsage.API/NSubstituteUsage.API.csproj"
COPY . .
WORKDIR "/src/NSubstituteUsage.API"
RUN dotnet build "NSubstituteUsage.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSubstituteUsage.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSubstituteUsage.API.dll"]
