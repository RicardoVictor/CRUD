#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CRUD.API/CRUD.API.csproj", "src/CRUD.API/"]
COPY ["src/CRUD.Application/CRUD.Application.csproj", "src/CRUD.Application/"]
COPY ["src/CRUD.Infra.Data/CRUD.Infra.Data.csproj", "src/CRUD.Infra.Data/"]
COPY ["src/CRUD.Domain/CRUD.Domain.csproj", "src/CRUD.Domain/"]
RUN dotnet restore "src/CRUD.API/CRUD.API.csproj"
COPY . .
WORKDIR "/src/src/CRUD.API"
RUN dotnet build "CRUD.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRUD.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUD.API.dll"]
