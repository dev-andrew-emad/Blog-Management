# Blog Management System API

Backend API built with **ASP.NET Core** using a 3-Layer Clean Architecture (API / Business / Data).  
This project manages blogs with posts, comments, replies, likes, and user authentication with roles.

---

## Features

- **Authentication & Authorization**
  - JWT Authentication (Login / Register)
  - Role-based Access Control (Admin / User)
  

- **Blog Management**
  - Create, Read, Update, Delete Posts
  - Add, Edit, Delete Comments & Replies
  - Like system for Posts, Comments, and Replies
  - Transaction-safe delete operations

- **Database**
  - Entity Framework Core (EF Core)
  - SQL Server backend
  - Relationships with Restrict delete behavior

- **Additional**
  - RESTful API endpoints
  - Proper error handling and validation
  - Secure password hashing

---

## Project Structure

BlogManagement/
│
├─ API/ # ASP.NET Core Web API layer
├─ Business/ # Business logic layer
├─ Data/ # EF Core data access layer
├─ Models/ # Entity models
├─ Migrations/ # EF Core migrations
└─ README.md


---

## Getting Started

### Prerequisites

- [.NET 6 or 7 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- Visual Studio 2022 or VS Code

### Setup

```bash
git clone 
cd your-repo
Configure the connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BlogDB;Trusted_Connection=True;"
}
Apply migrations:

dotnet ef database update
Run the API:

dotnet run
API Endpoints (Examples)
POST /api/auth/register – Register a new user

POST /api/auth/login – Login and get JWT

GET /api/posts – List all posts

POST /api/posts – Create a new post 

POST /api/comments – Add comment to a post

DELETE /api/posts/{id} – Delete a post with all comments, replies, and likes (transaction-safe)

Full endpoint list is available in Swagger UI when running locally.

Security
Passwords are hashed and never stored in plain text

JWT tokens are used for authentication

Role and permission-based access enforced at controller level

Notes
All delete operations are Restrict, not Cascade, so related entities are manually handled in code

Transactions are used to ensure consistency

EF Core is used for all database operations

