<<<<<<< Updated upstream
# RecordStore
=======
# ASP.NET RecordStore Web API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Table of Contents
- [Introduction](#introduction)
- [Technologies](#technologies)
- [Features](#features)
- [Setup](#setup)
- [Usage](#usage)
- [Endpoints](#endpoints)
  - [Example Endpoint Usage](#example-endpoint-usage)
- [License](#license)
- [To-Do](#to-do)

---

## Introduction
This project is an **ASP.NET Web API** that serves as a web API for a record store. 

It is built to demonstrate clean architecture, RESTful principles, test driven development and efficient data handling.

RecordStore was developed as a solo project during Northcoders software development bootcamp in C#.

---

## Technologies
- **Language:** C#
- **Framework:** [ASP.NET Core 8.0](https://learn.microsoft.com/en-us/aspnet/core)
- **Tools:** [Entity Framework Core](https://learn.microsoft.com/en-us/ef/), [NUnit](https://nunit.org/), [Moq](https://github.com/moq/moq4), [Fluent Assertions](https://fluentassertions.com/)
- **Database:** SQL Server

---

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

## Features
- RESTful API design.
- CRUD operations for Albums.
- Filtered read operations for Albums.
- Read operations for Genres and Artists.

---

## Setup
1. Clone the repository:
   ```pwsh
   git clone https://github.com/DrBenSullivan/RecordStore.git
   cd ./RecordStore/
   ```

2. Install dependencies:
   ```pwsh
   dotnet restore
   ```

3. Update database connection strings.

4. Apply migrations:
   ```pwsh
   dotnet ef database update
   ```

5. Run the application:
   ```pwsh
   dotnet run
   ```

The API will be accessible at http://localhost:5034

---

### Usage
- Use a tool such as Postman or cURL to test the endpoints

---

## Endpoints

### Resource: Albums

| HTTP Method | Endpoint           | Description        |
|-------------|--------------------|--------------------|
| `GET`       | `/api/albums`      | Get all albums     |
| `GET`       | `/api/albums/{id}` | Get album by ID    |
| `POST`      | `/api/albums`      | Create new album   |
| `PUT`       | `/api/albums/{id}` | Update album by ID |
| `DELETE`    | `/api/albums/{id}` | Delete album by ID |

### Resource: Artists

| HTTP Method | Endpoint                   | Description                 |
|-------------|----------------------------|-----------------------------|
| `GET`       | `/api/artists`             | Get all artists             |
| `GET`       | `/api/artists/{id}`        | Get artist by ID            |
| `GET`       | `/api/artists/{id}/albums` | Get all albums by artist ID |

### Resource: Genres

| HTTP Method | Endpoint           | Description     |
|-------------|--------------------|-----------------|
| `GET`       | `/api/genres`      | Get all genres  |
| `GET`       | `/api/genres/{id}` | Get genre by ID |

---

#### Query Parameters for `GET /api/albums`

The `GET /api/albums` endpoint supports optional query parameters to filter the list of albums. These parameters can be combined to customise the results.

#### Query Parameters:

| Parameter    | Type    | Description                                                                   |
|--------------|---------|-------------------------------------------------------------------------------|
| `inStock`    | `bool`  | Filter albums by stock availability (`true` for in-stock, `false` otherwise). |
| `releaseYear`| `int`   | Filter albums by their release year.                                          |
| `genreId`    | `int`   | Filter albums by their genre ID.                                              |

#### Usage:

The query parameters are optional. If no query parameters are provided, the endpoint returns all albums.

---

### Example Endpoint Usage

#### Get Album by ID
**Request:**
```pwsh
Invoke-RestMethod -Uri "http://localhost:5034/api/albums/1" -Method Get
```

**Response:**
```json
{
  "albumId": 1,
  "albumTitle": "Abbey Road",
  "artist": "The Beatles",
  "releaseYear": 1969,
  "genre": "Rock",
  "stockQuantity": 100
}
```

#### Post Album
**Required Fields for POST:**
- `title` (string): The title of the album.
- `artistId` (integer): The ID of the artist (must exist in the database).
- `releaseYear` (integer): The year the album was released.
- `genre` (integer): The ID of the genre (must exist in the database).

**Request:**
```pwsh
Invoke-RestMethod -Uri "http://localhost:5034/api/albums" -Method Post -Body @{
    title = "The Beatles"
    artistId = 1
    releaseYear = 1968
    genre = 1
} -ContentType "application/json"
```

**Response:**
```json
{
  "albumId": 11,
  "albumTitle": "The Beatles",
  "artist": "The Beatles",
  "releaseYear": 1968,
  "genre": "Rock",
  "stockQuantity": 0
}
```

---

## License
This project is licensed under the [MIT License](./LICENSE.txt).

---

## To-Do

1. **Error Handling**:
   - Implement comprehensive error responses (e.g., 404 for missing records, 400 for invalid input)
2. **Get Album by Title**:
   - Add further filtering query parameter to endpoint `/api/albums` to retrieve albums with a given title
3. **Search Albums by Title**:
   - Add a query parameter `/api/albums?title=Abbey%20Road`.
4. **Swagger Documentation**:
   - Add XML documentation for Swagger UI
5. **Improve Unit Test Coverage & Integration Tests**
   - Cover all endpoints with unit tests
   - Implement integration tests
>>>>>>> Stashed changes
