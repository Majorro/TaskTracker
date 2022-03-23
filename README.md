# Task Tracker

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/majorro/TaskTracker/TaskTracker%20CI?label=codechecks)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Majorro_TaskTracker&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Majorro_TaskTracker)
[![Mayhem for API](https://mayhem4api.forallsecure.com/api/v1/api-target/majorro/majorro-tasktracker/badge/icon.svg?scm_branch=master)](https://mayhem4api.forallsecure.com/majorro/majorro-tasktracker/latest-job?scm_branch=master)


This is an ASP.NET Core Web Api app that provides Rest API for task tracking. Api documentation generates using Swagger, more info can be found in [Usage](#usage).

## Technologies used

1. .NET 6
2. C# 10
3. ASP.NET Core 6 Web Api
4. Entity Framework Core 6 with Code-First approach
5. PosgreSQL
6. Swagger 3
7. Docker

## Prerequisites

1. [Docker](https://docs.docker.com/get-docker/) to build and run the app locally.

## Building and running

1. Simply run the following command from the root of the repository. You can also add `-d` key to run it in the background:
```bash
docker compose up --build
```

## Usage

When the app is running, http://localhost:8080/ can be opened to explore swagger api documentation and try the api methods.

## License

[MIT](LICENSE)
