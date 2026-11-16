DalilAcademy.API - LMS Backend (PayPal Integration)

An ASP.NET Core 10 Web API for the Dalil Academy Learning Management System (LMS),
extended from the core LMS project using Clean Architecture, 
EF Core with PostgreSQL, JWT Authentication, and a PayPal Sandbox Payment Gateway (`Orders API v2`).


Key Features & Architecture

- Clean Architecture:Strict separation of concerns across `Core` (Entities, Enums, Interfaces, DTOs), 
`Infra` (EF Core DbContext, Repositories, `PayPalPaymentGateway`, `PaymentService`),
and `API` (Controllers, `GlobalExceptionHandler`).
-Enrollment Status & Payment Tracking:
1-Enrollment Statuses:Stored as strings in PostgreSQL (`HasConversion<string>()`) with `Pending`
and `Paid` states. Paid enrollments cannot be deleted.
2-One-to-Many Relationship: A single `Enrollment` can have multiple independent `Payment` 
attempts (`Created`, `Completed`, `Failed`), each tracked with a unique `PayPalOrderId`.
- Backend-Driven Pricing & Currency Conversion:** Prevents client-side price tampering by
fetching the exact course price from the database and converting currency using the fixed 
rate: `1 JOD = 1.41 USD`.
- PayPal Orders API v2 Flow:
  1. Authenticate via OAuth 2.0 Client Credentials to obtain a Bearer Token.
  2. Create a PayPal Order (valid for 3 hours) and return order details / Approval URL.
  3. Capture the payment and verify `COMPLETED` status before updating `Payment` to `Completed` and `Enrollment` to `Paid`.
- Global Error Handling:** Standardized HTTP status codes (`200 OK`, `400 Bad Request`, `404 Not Found`, `409 Conflict` for already paid enrollments, `503 Service Unavailable` when PayPal is unreachable).

-Bonus Features Implemented

In addition to the core LMS requirements, the following optional bonus features are fully integrated:
1- Course Search & Filtering (`GET /api/Courses?search=...`):** Allows users to efficiently search and filter available courses by keywords alongside pagination.
2- Enrollment Payment History (`GET /api/enrollments/{id}/payments`):** Retrieves the complete payment history for a specific enrollment, returning all associated payment attempts sorted chronologically from newest to oldest.
 Tech Stack

1-Framework: ASP.NET Core 10 Web API
2-Database: PostgreSQL & Entity Framework Core
3-HTTP Client: Built-in `HttpClient` & `System.Net.Http.Json` (Typed HttpClient)
4-Security: JWT Authentication, BCrypt, and .NET User Secrets
5-Documentation: Swagger /Postman



 Security & User Secrets Setup

No sensitive keys or credentials are stored in `appsettings.json` or committed to the repository. 
All secrets are managed locally via .NET User Secrets.

Run the following commands inside the `DalilAcademy.API` project directory before running the app:


dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;
Database=DalilAcademyDb;Username=postgres;Password=YOUR_DB_PASSWORD"
dotnet user-secrets set "Jwt:Key" "YOUR_SUPER_SECRET_JWT_KEY"
dotnet user-secrets set "PayPal:ClientId" "YOUR_PAYPAL_SANDBOX_CLIENT_ID"
dotnet user-secrets set "PayPal:Secret" "YOUR_PAYPAL_SANDBOX_CLIENT_SECRET"
dotnet user-secrets set "PayPal:BaseUrl" "[https://api-m.sandbox.paypal.com]
(https://api-m.sandbox.paypal.com)"


- Getting Started & Running the Project
Prerequisites.NET 10 PostgreSQL PayPal Developer Sandbox account
Installation Steps
1-Clone the repository:Bashgit clone [https://github.com/abdallah-Frehat/DalilAcademy.API.git]
(https://github.com/abdallah-Frehat/DalilAcademy.API.git) cd DalilAcademy.API
2-Configure User Secrets:
Set up your database connection string, JWT key, and PayPal Sandbox credentials as shown in the section above.
Database Setup & Migrations

Apply Entity Framework Core migrations to set up your PostgreSQL database schema:

1- database update
-Run the Application:

1-Open the solution in Visual Studio.

2-Click the Run 

3-Explore and test the endpoints directly through the Swagger UI (e.g., https://localhost:7012/swagger)

 -Main API EndpointsMethodEndpointDescriptionPOST/api/Auth/registerRegister a new user (Admin or Student)
 POST/api/Auth/loginAuthenticate user and receive a JWT Bearer tokenGET/api/CoursesRetrieve available
 coursesPOST/api/Payments/create-orderFetch course price from DB,
 convert (1 JOD = 1.41 USD),
 and create a PayPal OrderPOST/api/Payments/capture-orderCapture PayPal Order,
 mark Payment Completed, 
 and update Enrollment to Paid.
