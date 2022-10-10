FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/rentACar/WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["src/corePackages/Core.Mailing/Core.Mailing.csproj", "Core.Mailing/"]
COPY ["src/rentACar/Application/Application.csproj", "Application/"]
COPY ["src/corePackages/Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["src/corePackages/Core.CrossCuttingConcers/Core.CrossCuttingConcerns.csproj", "Core.CrossCuttingConcers/"]
COPY ["src/corePackages/Core.Security/Core.Security.csproj", "Core.Security/"]
COPY ["src/corePackages/Core.Persistence/Core.Persistence.csproj", "Core.Persistence/"]
COPY ["src/corePackages/Core.ElasticSearch/Core.ElasticSearch.csproj", "Core.ElasticSearch/"]
COPY ["src/rentACar/Domain/Domain.csproj", "Domain/"]
COPY ["src/rentACar/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["src/rentACar/Persistence/Persistence.csproj", "Persistence/"]
RUN dotnet restore "src/rentACar/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
