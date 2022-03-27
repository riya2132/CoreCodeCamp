FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["CoreCodeCamp.csproj", "./"]
RUN dotnet restore "./CoreCodeCamp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CoreCodeCamp.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "CoreCodeCamp.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreCodeCamp.dll"]