#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Skyline.Console.WebMvc/Skyline.Console.WebMvc.csproj", "src/Skyline.Console.WebMvc/"]
COPY ["src/Skyline.Console.Infrastructure/Skyline.Console.Infrastructure.csproj", "src/Skyline.Console.Infrastructure/"]
COPY ["src/Skyline.Console.ApplicationCore/Skyline.Console.ApplicationCore.csproj", "src/Skyline.Console.ApplicationCore/"]
RUN dotnet restore "src/Skyline.Console.WebMvc/Skyline.Console.WebMvc.csproj"
COPY . .
WORKDIR "/src/src/Skyline.Console.WebMvc"
RUN dotnet build "Skyline.Console.WebMvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Skyline.Console.WebMvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skyline.Console.WebMvc.dll"]
