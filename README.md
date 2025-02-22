# Library Management API

## 📌 Overview

The **Library Management API** is a book management system that provides full **CRUD operations** for books while enforcing authorization rules. Only registered users with the appropriate roles can **add, update, or delete books**, while others have **read-only access**.

## ✨ Features

- **User Authentication & Authorization** (Register, Login, JWT-based security)
- **Book Management (CRUD)**
  - Create, update, delete books (Admins and authorized users only)
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
dotnet ef database update --startup-project ../Library.API
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

| Method   | Endpoint         | Access | Description                                                  |
| -------- | ---------------- | ------ | -------------------------------------------------------------|
| `GET`    | `/books`         | Public                   | Get list of books with filters ans sorting |
| `GET`    | `/books/{title}` | Public                   | Get details of a single book               |
| `POST`   | `/books/book`    | Admin & Authorized user  | Add a new book                             |
| `PUT`    | `/books/{id}`    | Admin & Authorized user  | Update book details                        |  
| `DELETE` | `/books/{title}` | Admin & Authorized user  | Delete a book                              |

## 🛡️ Authorization

- JWT-based authentication is used.
- Only **admin users** can **create, update, or delete books**.
- All users can **view books**.

## 🤝 Contribution

Contributions are welcome! Feel free to open **issues** or submit **pull requests**.

## 📜 License

This project is **open-source** under the **MIT License**.

