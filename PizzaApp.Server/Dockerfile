#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PizzaApp.Server/PizzaApp.Server.csproj", "PizzaApp.Server/"]
RUN dotnet restore "PizzaApp.Server/PizzaApp.Server.csproj"
COPY . .
WORKDIR "/src/PizzaApp.Server"
RUN dotnet build "PizzaApp.Server.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaApp.Server.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaApp.Server.dll"]