FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["Monio.API/Monio.API.csproj", "Monio.API/"]
COPY ["Monio.Application/Monio.Application.csproj", "Monio.Application/"]
COPY ["Monio.Domain/Monio.Domain.csproj", "Monio.Domain/"]
COPY ["Monio.Infrastructure/Monio.Infrastructure.csproj", "Monio.Infrastructure/"]

RUN dotnet restore "Monio.API/Monio.API.csproj"

COPY . .

WORKDIR "/src/Monio.API"

RUN dotnet publish "Monio.API.csproj" \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Monio.API.dll"]