#  DalilAcademy.API - LMS Backend

A robust ASP.NET Core 10 Web API for a Learning Management System (LMS) built with Clean Architecture,
JWT Authentication, dynamic file uploads, EF Core with PostgreSQL, and Third-Party API Integration.



##  Features

- **Clean Architecture:** Separation of concerns using Core, Infra, and API layers.
- **Authentication & Authorization:** Secure JWT token implementation with role-based policies
(Admin / Student).
- **Course Management:** Full CRUD operations using DTO mappings.
- **Dynamic File Uploads:** Upload image files directly to static 
`wwwroot/images/Courses/` with max-size (2MB) and extension validation.
- **External API Integration:** Currency conversion built with 
`HttpClient` connecting to **ExchangeRate-API**.
- **Database & Persistence:** PostgreSQL integration via Entity Framework Core migrations.


##  Tech Stack

- **Framework:** ASP.NET Core 10 Web API
- **Database:** PostgreSQL & EF Core
- **Authentication:** JWT (JSON Web Tokens) & BCrypt Hashing
- **Documentation:** OpenAPI / Swagger
- **External Integration:** ExchangeRate-API via `HttpClient`



##  Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [PostgreSQL](https://www.postgresql.org/)

### Installation & Setup

1. **Clone the repository:**
  
   git clone [https://github.com/YOUR_USERNAME/DalilAcademy.API.git](https://github.com/YOUR_USERNAME/DalilAcademy.API.git)
   cd DalilAcademy.API