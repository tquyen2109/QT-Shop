FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QTShop.Category/QTShop.Category.csproj", "QTShop.Category/"]
RUN dotnet restore "QTShop.Category/QTShop.Category.csproj"
COPY . .
WORKDIR "/src/QTShop.Category"
RUN dotnet build "QTShop.Category.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QTShop.Category.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QTShop.Category.dll"]
