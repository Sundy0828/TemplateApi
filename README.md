# TemplateApi

[![codecov](https://codecov.io/gh/Sundy0828/TemplateApi/graph/badge.svg?token=OHTU9777VF)](https://codecov.io/gh/Sundy0828/TemplateApi)

A lightweight REST API starter built with an N-Tier architecture. Includes MongoDB integration, a NuGet client project, and a unit test project, providing a clean foundation for modular backend development.

---

## Table of Contents

-   [Prerequisites](#prerequisites)
-   [Setup](#setup)
-   [API Configuration](#api-configuration)
-   [Run the API](#run-the-api)
-   [Common Commands](#common-commands)

---

## Prerequisites

Make sure you have these installed:
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

Verify installation:

```
dotnet --version
docker --version
docker compose version
```

---

## Setup

Clone the repo and navigate to the solution:

```
git clone https://github.com/Sundy0828/TemplateApi.git
cd TemplateApi
```

Restore packages:

```
dotnet restore
```

---

## Run the API

From the repository root where docker-compose.yml lives:

```
docker compose up --build
```

By default, the API will run at:

-   `http://localhost:5070`

Swagger UI is available at `http://localhost:5070/swagger` for exploring the endpoints.

---

## Common Commands

### Format the code

```
dotnet format
```

> This uses the formatting rules from the `.editorconfig` file

### Run tests

```
cd TemplateApi.Tests
dotnet test
```