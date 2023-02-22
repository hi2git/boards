#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install nodejs
RUN apt-get -y install build-essential

WORKDIR /src
COPY . .
WORKDIR "/src/Boards.Front.API"

FROM build AS publish
RUN dotnet publish "Boards.Front.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boards.Front.API.dll"]