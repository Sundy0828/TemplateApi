# TemplateApi

[![codecov](https://codecov.io/gh/Sundy0828/TemplateApi/graph/badge.svg?token=OHTU9777VF)](https://codecov.io/gh/Sundy0828/TemplateApi)

A lightweight REST API starter built with an N-Tier architecture. Includes MongoDB integration, a NuGet client project, and a unit test project, providing a clean foundation for modular backend development.

---

## Table of Contents

-   [Setup](#setup)
-   [API Configuration](#api-configuration)
-   [Run the API](#run-the-api)
-   [Common Commands](#common-commands)

---

## Setup

Make sure you have [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) installed.

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

## API Configuration

Inside the `TemplateApi` folder, include these configuration files:

### `appsettings.json`

```
{
  "MongoDb": {
    "ConnectionString": "mongodb+srv://xxx:xxx@xxx.mongodb.net/",
    "DatabaseName": "TemplateApiDb"
  }
}
```

> Replace sensitive information (`xxx`) with your credentials.

---

## Run the API

Navigate to the API project:

```
cd TemplateApi
dotnet run
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