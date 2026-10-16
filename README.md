# Product Management System (Technical Assessment)

A web-based Product Management application built with **ASP.NET Core (.NET 10)** featuring **MVC Views**, **RESTful Web APIs**, and **JWT Bearer Authentication**.

---

## 🚀 Features

- **Authentication & Authorization**:
  - User Registration & Login with JWT (JSON Web Token) generation.
  - Protected API endpoints using `[Authorize]` attributes.
  - Client-side route protection (auto-redirect to login if unauthenticated).
- **Product Management (CRUD)**:
  - **Create**: Add new products with validations (Name, Description, Price).
  - **Read**: View list of products in a clean, responsive table.
  - **Update**: Edit existing product details via modal form.
  - **Delete**: Remove products with confirmation modal.
  - **Filter & Search**: Search products by name and filter by minimum/maximum price range.
- **Enterprise Standards**:
  - Global Exception Handling Middleware (`GlobalExceptionMiddleware`) returning standard JSON errors.
  - Structured Logging using **Serilog** (Console & File logging under `Logs/`).
  - Separation of concerns: MVC Controllers for Views and API Controllers for data endpoints.

---

## 🛠️ Tech Stack

- **Framework**: [.NET 10 SDK](https://dotnet.microsoft.com/download) / ASP.NET Core MVC & Web API
- **Authentication**: JWT Bearer (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Logging**: Serilog (`Serilog.AspNetCore`, `Serilog.Sinks.Console`, `Serilog.Sinks.File`)
- **Frontend**: Razor Views, HTML5, Bootstrap 5, Vanilla JavaScript (Fetch API)
- **Data Storage**: In-Memory DataStore (`ProductManagement.Common.DataStore`)

---

## 📋 Prerequisites

Before running this project, ensure you have:
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (or compatible .NET runtime) installed.
- Git (optional, for cloning).
- Any modern web browser (Chrome, Edge, Firefox).

Verify your .NET installation by running:
```powershell
dotnet --version
```

---

## 🏃 How to Run Locally

### 1. Clone the Repository
```bash
git clone <repository-url>
cd ProductManagement
```

### 2. Navigate to the Project Directory
```powershell
cd ProductManagement
```

### 3. Restore Dependencies & Build
```powershell
dotnet restore
dotnet build
```

### 4. Run the Application
```powershell
dotnet run
```

Once started, the console will display:
```text
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5032
```

### 5. Access via Browser
Open your browser and navigate to:
👉 **[http://localhost:5032](http://localhost:5032)**

---

## 🔑 Default Test Credentials

For quick evaluation, a default account is pre-configured:

| Role | Username | Password |
|---|---|---|
| Administrator | `admin` | `password123` |

> *You can also create a new account using the **Register** page.*

---

## 📚 API Documentation

### Authentication Endpoints

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/auth/register` | None | Register a new user (`{ username, password }`) |
| `POST` | `/api/auth/login` | None | Authenticate and obtain JWT token |

#### Example Login Request:
```json
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "password123"
}
```
**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

### Product Endpoints (Protected by JWT)
> *Include header `Authorization: Bearer <your_jwt_token>` for all `/api/products` requests.*

| Method | Endpoint | Query Params | Description |
|---|---|---|---|
| `GET` | `/api/products` | `name`, `minPrice`, `maxPrice` | List all products with optional filters |
| `GET` | `/api/products/{id}` | - | Get product detail by ID |
| `POST` | `/api/products` | - | Create new product |
| `PUT` | `/api/products/{id}` | - | Update product by ID |
| `DELETE` | `/api/products/{id}` | - | Delete product by ID |

#### Example Create Product Payload:
```json
POST /api/products
Content-Type: application/json
Authorization: Bearer <token>

{
  "name": "Mechanical Keyboard",
  "description": "RGB Blue Switch",
  "price": 450000
}
```

---

## 📁 Project Structure

```text
ProductManagement/
├── .gitignore                      # Git ignore rules for .NET
├── README.md                       # Documentation & running guide
└── ProductManagement/              # ASP.NET Core Project
    ├── Common/
    │   ├── DataStore.cs            # In-memory mock database
    │   └── GlobalExceptionMiddleware.cs # Error handling middleware
    ├── Controllers/
    │   ├── AuthController.cs       # Auth MVC Views & API
    │   ├── ProductsController.cs   # Product REST API endpoints
    │   └── ProductsViewController.cs# Product MVC View Controller
    ├── Models/
    │   ├── AuthModels.cs           # User & DTO models
    │   └── Products.cs             # Product entity model
    ├── Views/
    │   ├── Auth/
    │   │   ├── Login.cshtml        # Login page UI
    │   │   └── Register.cshtml     # Registration page UI
    │   ├── ProductsView/
    │   │   └── Index.cshtml        # Product Catalog UI & JS CRUD
    │   └── Shared/
    │       └── _Layout.cshtml      # Master layout & navbar
    ├── appsettings.json            # Configuration & JWT Secret Key
    ├── Program.cs                  # App entry point & dependency injection
    └── ProductManagement.csproj    # Project file & NuGet packages
```

---

## 👨‍💻 Author
Technical Assessment Submission - Product Management Application.
