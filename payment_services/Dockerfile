FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["payment_services.csproj", "./"]
RUN dotnet restore "./payment_services.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "payment_services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "payment_services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "payment_services.dll"]
