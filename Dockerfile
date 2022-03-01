FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG DB_HOSTNAME
ARG DB_HOSTPORT
ARG DB_LOGIN
ARG DB_PASSWORD

ENV DB_HOSTNAME=$DB_HOSTNAME
ENV DB_HOSTPORT=$DB_HOSTPORT
ENV DB_LOGIN=$DB_LOGIN
ENV DB_PASSWORD=$DB_PASSWORD
ENV Logging__Console__FormatterName=Simple

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