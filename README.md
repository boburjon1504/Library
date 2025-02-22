# Library Management API

## 📌 Overview

The **Library Management API** is a book management system that provides full **CRUD operations** for books while enforcing authorization rules. Only registered users with the appropriate roles can **add, update, or delete books**, while others have **read-only access**.

## ✨ Features

- **User Authentication & Authorization** (Register, Login, JWT-based security)
- **Book Management (CRUD)**
  - Create, update, delete books (Admin users only)
  - View books (All users)
- **Filtering & Pagination**
  - Filter by year range (MinYear, MaxYear)
  - Sort by various properties: `Popularity`, `Title`, `PublishedYear`, `Author`, `ViewsCount`
  - Supports both **ascending and descending** sorting

## 🛠️ Technologies Used

- **ASP.NET Core Web API** (Backend)
- **SQL Server** (Database)
- **Entity Framework Core** (ORM)
- **AutoMapper** (Object Mapping)
- **FluentValidation** (Validation Framework)
- **JWT Authentication** (Security)

## 🚀 Installation & Setup

### 1️⃣ Prerequisites

- .NET SDK 9.0+
- SQL Server

### 2️⃣ Clone the Repository

```sh
git clone https://github.com/boburjon1504/Library.git
cd Library
```

### 3️⃣ Configure the Database

Update `appsettings.json` with your **SQL Server connection string**:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=LibraryDB;Trusted_Connection=True;"
}
```

Run migrations to set up the database:

```sh
dotnet ef database update
```

### 4️⃣ Run the Application

```sh
dotnet run
```

## 📡 API Endpoints

### 🔐 Authentication

| Method | Endpoint         | Description     |
| ------ | ---------------- | --------------- |
| `POST` | `/auth/register` | Register a user |
| `POST` | `/auth/login`    | Login & get JWT |

### 📚 Book Management

| Method   | Endpoint         | Access | Description                    |
| -------- | ---------------- | ------ | ------------------------------ |
| `GET`    | `/books`         | Public | Get list of books with filters |
| `GET`    | `/books/{title}` | Public | Get details of a single book   |
| `POST`   | `/books`         | Admin  | Add a new book                 |
| `PUT`    | `/books/{id}`    | Admin  | Update book details            |
| `DELETE` | `/books/{title}` | Admin  | Delete a book                  |

## 🛡️ Authorization

- JWT-based authentication is used.
- Only **admin users** can **create, update, or delete books**.
- All users can **view books**.

## 📖 Sorting & Filtering Example

To get books sorted by `Title` in **descending order**:

```sh
GET /books?MaxYear=0&MinYear=9000&IncludeDeletedBooks=true&SortBy=Popularity&IsAscending=true&CurrentPage=1
```


## 🤝 Contribution

Contributions are welcome! Feel free to open **issues** or submit **pull requests**.

## 📜 License

This project is **open-source** under the **MIT License**.

