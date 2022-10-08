FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
 EXPOSE 80
 EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
 COPY . .

COPY ["DiscussionNet.sln", "DiscussionNet"]
COPY ["/src/DiscussionNet.Domain/", "DiscussionNet.Domain/"]
COPY ["/src/DiscussionNet.Application/", "DiscussionNet.Application/"]
COPY ["/src/DiscussionNet.Persistence/", "DiscussionNet.Persistence/"]
COPY ["/src/DiscussionNet.Infrastructure/", "DiscussionNet.Infrastructure/"]
COPY ["/src/DiscussionNet.WebApi/", "DiscussionNet.WebApi/"]


RUN dotnet restore "/src/DiscussionNet.WebApi/DiscussionNet.WebApi.csproj"


COPY . .
WORKDIR "/src/"
RUN dotnet build "DiscussionNet.WebApi" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscussionNet.WebApi" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscussionNet.WebApi.dll"]