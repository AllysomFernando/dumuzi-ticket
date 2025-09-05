FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["DumuziTickets.csproj", "."]
RUN dotnet restore "DumuziTickets.csproj"

COPY . .
RUN dotnet build "DumuziTickets.csproj" -c Release -o /app/build
RUN dotnet publish "DumuziTickets.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5166
ENV ASPNETCORE_URLS=http://+:5166

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DumuziTickets.dll"]
