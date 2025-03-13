FROM home.freemanke.com:60012/freemanke/poetry-planet-dotnetsdk:9.0 AS build
WORKDIR /app
COPY . ./
ARG DESCRIPTION
ARG ASPNETCORE_ENVIRONMENT
RUN bash ./nuget.sh && dotnet publish -c Release -o /app/out ./src/PoetryPlanet.Web/PoetryPlanet.Web.csproj

FROM home.freemanke.com:60012/freemanke/poetry-planet-dotnetsdk:9.0 AS base
WORKDIR /app
ARG DESCRIPTION
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}
EXPOSE 80
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "PoetryPlanet.Web.dll"]