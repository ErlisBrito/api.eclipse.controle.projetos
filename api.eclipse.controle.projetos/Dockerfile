#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api.eclipse.controle.projetos/api.eclipse.controle.projetos.csproj", "api.eclipse.controle.projetos/"]
COPY ["api.eclipse.controle.projetos.Application/api.eclipse.controle.projetos.Application.csproj", "api.eclipse.controle.projetos.Application/"]
COPY ["api.eclipse.controle.projetos.Common/api.eclipse.controle.projetos.Common.csproj", "api.eclipse.controle.projetos.Common/"]
COPY ["api.eclipse.controle.projetos.Domain/api.eclipse.controle.projetos.Domain.csproj", "api.eclipse.controle.projetos.Domain/"]
COPY ["api.eclipse.controle.projetos.CrossCutting/api.eclipse.controle.projetos.CrossCutting.IoC.csproj", "api.eclipse.controle.projetos.CrossCutting/"]
COPY ["api.eclipse.controle.projetos.Data/api.eclipse.controle.projetos.Data.csproj", "api.eclipse.controle.projetos.Data/"]
RUN dotnet restore "./api.eclipse.controle.projetos/api.eclipse.controle.projetos.csproj"
COPY . .
WORKDIR "/src/api.eclipse.controle.projetos"
RUN dotnet build "./api.eclipse.controle.projetos.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./api.eclipse.controle.projetos.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.eclipse.controle.projetos.dll"]