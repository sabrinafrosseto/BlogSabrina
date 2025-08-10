FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlogSabrina/BlogSabrina.csproj", "BlogSabrina/"]
RUN dotnet restore "BlogSabrina/BlogSabrina.csproj"
COPY . .
WORKDIR "/src/BlogSabrina"
RUN dotnet build "BlogSabrina.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogSabrina.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
ENTRYPOINT ["dotnet", "BlogSabrina.dll"]