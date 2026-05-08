FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo el archivo de proyecto principal y restaura dependencias
COPY Challenge.WebApi/Challenge.WebApi.csproj Challenge.WebApi/
RUN dotnet restore Challenge.WebApi/Challenge.WebApi.csproj

# Copia el resto de los archivos
COPY . .

# Publica solo el proyecto WebApi
RUN dotnet publish Challenge.WebApi/Challenge.WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Challenge.WebApi.dll"]