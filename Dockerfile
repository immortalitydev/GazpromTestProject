FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GazpromTestProject.sln", "."]
COPY ["src/Domain/Domain/Domain.csproj", "src/Domain/Domain/"]
COPY ["src/Application/Application/Application.csproj", "src/Application/Application/"]
COPY ["src/Infrastructure/Infrastructure/Infrastructure.csproj", "src/Infrastructure/Infrastructure/"]
COPY ["src/Web/Web/Web.csproj", "src/Web/Web/"]
RUN dotnet restore "GazpromTestProject.sln"
COPY . .
RUN dotnet publish "src/Web/Web/Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
