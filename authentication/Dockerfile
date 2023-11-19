FROM mcr.microsoft.com/dotnet/aspnet:7.0.14-jammy-amd64 AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 5062/tcp
EXPOSE 3306
EXPOSE 3307

FROM mcr.microsoft.com/dotnet/sdk:7.0.404-1-jammy-amd64 AS build
WORKDIR /src
COPY ["authentication.csproj", "."]
RUN dotnet restore "authentication.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "authentication.csproj" -c Release -o /app/build

FROM build AS publish
COPY ["authentication.csproj", "."]
RUN dotnet publish "authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/Templates /app/Templates
ARG SENDGRID_API_KEY
ENV SendGridApiKey=$SENDGRID_API_KEY
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "authentication.dll"]
