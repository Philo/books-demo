FROM mcr.microsoft.com/dotnet/sdk:6.0 AS prep
WORKDIR /app
COPY ./src /src/
RUN mkdir /proj && cd /src && \
    find . -type f -a \( -iname "*.sln" -o -iname "*.csproj" -o -iname "nuget.config" \) \
    -exec cp --parents "{}" ../proj/ \;

FROM prep AS build
WORKDIR /src
COPY --from=prep ./proj .
RUN dotnet restore ./Books.Service.Web/Books.Service.Web.csproj
COPY --from=prep ./src .
RUN dotnet publish Books.Service.Web/Books.Service.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "Books.Service.Web.dll"]
