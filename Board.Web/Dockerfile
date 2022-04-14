#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN apt-get update && apt-get install -y curl && apt-get clean
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["Board.Web/Board.Web.csproj", "Board.Web/"]
COPY ["Board.Infrastructure.Jwt/Board.Infrastructure.Jwt.csproj", "Board.Infrastructure.Jwt/"]
COPY ["Board.Domain.DTO/Board.Domain.DTO.csproj", "Board.Domain.DTO/"]
COPY ["Board.Domain/Board.Domain.csproj", "Board.Domain/"]
COPY ["Board.Application/Board.Application.csproj", "Board.Application/"]
COPY ["Boards.Application.Queries/Boards.Application.Queries.csproj", "Boards.Application.Queries/"]
COPY ["Board.Infrastructure.Files/Board.Infrastructure.Files.csproj", "Board.Infrastructure.Files/"]
COPY ["Board.Infrastructure.Repository/Board.Infrastructure.Repository.csproj", "Board.Infrastructure.Repository/"]
COPY ["Boards.Application.Commands/Boards.Application.Commands.csproj", "Boards.Application.Commands/"]
RUN dotnet restore "Board.Web/Board.Web.csproj"
COPY . .
WORKDIR "/src/Board.Web"
RUN dotnet build "Board.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Board.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Board.Web.dll"]