FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY TestDbContext.Api/TestDbContext.Api.csproj ./TestDbContext.Api/
RUN dotnet restore TestDbContext.Api

# copy everything else and build app
COPY TestDbContext.Api/ ./TestDbContext.Api

WORKDIR /app/TestDbContext.Api
RUN dotnet publish --configuration Debug --output out --verbosity normal
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0 AS runtime
WORKDIR /app
COPY --from=build /app/TestDbContext.Api/out ./
ENTRYPOINT ["dotnet", "TestDbContext.Api.dll"]
