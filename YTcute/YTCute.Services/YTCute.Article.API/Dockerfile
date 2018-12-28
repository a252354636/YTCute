FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY YTCute.Services/YTCute.Article.API/YTCute.Article.API.csproj YTCute.Services/YTCute.Article.API/
RUN dotnet restore YTCute.Services/YTCute.Article.API/YTCute.Article.API.csproj
COPY . .
WORKDIR /src/YTCute.Services/YTCute.Article.API
RUN dotnet build YTCute.Article.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish YTCute.Article.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YTCute.Article.API.dll"]
