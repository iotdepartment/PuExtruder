# Etapa de build con SDK 6.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

# Asegúrate de que el nombre del proyecto sea correcto
RUN dotnet restore WebApplication4.csproj
RUN dotnet publish WebApplication4.csproj -c Release -o out

# Etapa de runtime con ASP.NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApplication4.dll"]
