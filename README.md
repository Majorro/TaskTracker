# Task Tracker

![GitHub branch checks state](https://img.shields.io/github/checks-status/majorro/TaskTracker/master?label=branchchecks)
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
5. Swagger 3

## Prerequisites

1. [Docker](https://docs.docker.com/get-docker/) to build and run the app locally.

## Building and running

1. Simply run the following command from the root of the repository. You can also add `-d` key to run it in the background:
```bash
docker compose up --build
```

## Usage

When the app is running, http://localhost:8080/ can be opened to explore swagger api documentation and try api methods.

## License

MIT License

Copyright (c) 2022 Andrey Plekhov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
