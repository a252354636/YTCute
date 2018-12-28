FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY YTCute.ApiGateways/YTCute.Ocelot.ApiGw/YTCute.Ocelot.ApiGw.csproj YTCute.ApiGateways/YTCute.Ocelot.ApiGw/
RUN dotnet restore YTCute.ApiGateways/YTCute.Ocelot.ApiGw/YTCute.Ocelot.ApiGw.csproj
COPY . .
WORKDIR /src/YTCute.ApiGateways/YTCute.Ocelot.ApiGw
RUN dotnet build YTCute.Ocelot.ApiGw.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish YTCute.Ocelot.ApiGw.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YTCute.Ocelot.ApiGw.dll"]
