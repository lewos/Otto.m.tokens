	#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#
#ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1
#
 ##EXPOSE 80
 ##EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["Office4U/Office4U.csproj", "Office4U/"]
#RUN dotnet restore "Office4U/Office4U.csproj"
#COPY . .
#WORKDIR "/src/Office4U"
#RUN dotnet build "Office4U.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Office4U.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
##ENV ASPNETCORE_URLS http://*:$PORT
##ENTRYPOINT ["dotnet", "MVP.API.dll"]
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet Office4U.dll
#
##CMD ["dotnet", "MVP.API.dll"]
#
##CMD ASPNETCORE_URLS=http://*:$PORT dotnet AlquilarMVP.dll
#
#

#-----------------------------

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
# copiar todos los proyectos al directorio workdir
COPY . .

RUN dotnet restore "./Otto.m.tokens/Otto.m.tokens.csproj"
RUN dotnet publish "./Otto.m.tokens/Otto.m.tokens.csproj" -c Release -o /app

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

#EXPOSE 5000
#CMD ["dotnet", "Otto.m.tokens.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Otto.m.tokens.dll