# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos de solução e projetos
COPY HQ.sln ./
COPY HQ.Api/HQ.Api.csproj ./HQ.Api/
COPY HQ.Application/HQ.Application.csproj ./HQ.Application/
COPY HQ.Domain/HQ.Domain.csproj ./HQ.Domain/
COPY HQ.Infra/HQ.Infra.csproj ./HQ.Infra/

# Restaurar dependências
RUN dotnet restore

# Copiar todo o código e publicar
COPY . .
RUN dotnet publish HQ.Api/HQ.Api.csproj -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "HQ.Api.dll"]
