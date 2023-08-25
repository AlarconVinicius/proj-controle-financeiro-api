FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/1-Api/ProjControleFinanceiro.Api/ProjControleFinanceiro.Api.csproj", "src/1-Api/ProjControleFinanceiro.Api/"]
COPY ["src/2-Domain/ProjControleFinanceiro.Domain/ProjControleFinanceiro.Domain.csproj", "src/2-Domain/ProjControleFinanceiro.Domain/"]
COPY ["src/4-Entities/ProjControleFinanceiro.Entities/ProjControleFinanceiro.Entities.csproj", "src/4-Entities/ProjControleFinanceiro.Entities/"]
COPY ["src/3-Data/ProjControleFinanceiro.Data/ProjControleFinanceiro.Data.csproj", "src/3-Data/ProjControleFinanceiro.Data/"]
COPY ["src/5-Identity/ProjControleFinanceiro.Identity/ProjControleFinanceiro.Identity.csproj", "src/5-Identity/ProjControleFinanceiro.Identity/"]
RUN dotnet restore "src/1-Api/ProjControleFinanceiro.Api/ProjControleFinanceiro.Api.csproj"
COPY . .
WORKDIR "/src/src/1-Api/ProjControleFinanceiro.Api"
RUN dotnet build "ProjControleFinanceiro.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjControleFinanceiro.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjControleFinanceiro.Api.dll"]