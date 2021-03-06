#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Skyline.WebMvc/Skyline.WebMvc.csproj", "src/Skyline.WebMvc/"]
COPY ["src/Skyline.Infrastructure/Skyline.Infrastructure.csproj", "src/Skyline.Infrastructure/"]
COPY ["src/Skyline.ApplicationCore/Skyline.ApplicationCore.csproj", "src/Skyline.ApplicationCore/"]
RUN dotnet restore "src/Skyline.WebMvc/Skyline.WebMvc.csproj"
COPY . .
WORKDIR "/src/src/Skyline.WebMvc"
RUN dotnet build "Skyline.WebMvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Skyline.WebMvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skyline.WebMvc.dll"]
