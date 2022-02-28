FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG DB_CONNECTION_STRING

# ????
# ENV ASPNETCORE_ENVIRONMENT Docker
ENV Logging__Console__FormatterName=Simple
ENV DB_CONNECTION_STRING=$DB_CONNECTION_STRING

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TaskTracker/TaskTracker.csproj", "TaskTracker/"]
RUN dotnet restore "TaskTracker/TaskTracker.csproj"
COPY "src/" .
WORKDIR "/src/TaskTracker"
RUN dotnet build "TaskTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskTracker.csproj" -c Release -o /app/publish
COPY "src/TaskTracker/efbundle" /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "TaskTracker.dll"]
# for heroku deployment
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TaskTracker.dll \ 
    && chmod +x efbundle \
    && ./efbundle