FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY YTCute.Services/YTCute.Identity.API/YTCute.Identity.API.csproj YTCute.Services/YTCute.Identity.API/
RUN dotnet restore YTCute.Services/YTCute.Identity.API/YTCute.Identity.API.csproj
COPY . .
WORKDIR /src/YTCute.Services/YTCute.Identity.API
RUN dotnet build YTCute.Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish YTCute.Identity.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YTCute.Identity.API.dll"]
