FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
WORKDIR /app/BlogSabrina
RUN dotnet publish -c Release -o /app/out
WORKDIR /app/out
ENV ASPNETCORE_URLS=http://+:10000
ENTRYPOINT ["dotnet", "BlogSabrina.dll"]