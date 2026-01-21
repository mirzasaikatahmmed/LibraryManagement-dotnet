# Library Management System API

A comprehensive Library Management System built with ASP.NET Core Web API, following N-Tier architecture and RESTful principles.

## Project Overview

This project implements a complete library management system with CRUD operations and advanced features including:
- Book search and filtering
- Intelligent book recommendations
- Automatic late fee calculation
- Comprehensive statistics and reports

## Project Structure

```
LibraryManagement/
├── LibraryManagement.API/          # Presentation Layer (Web API Controllers)
├── LibraryManagement.BLL/          # Business Logic Layer (Services)
├── LibraryManagement.DAL/          # Data Access Layer (Entities, DbContext, Repository)
├── LibraryManagement.Common/       # Shared DTOs and Models
└── docker-compose.yml              # PostgreSQL Database Configuration
```

## Architecture

This project follows **3-Tier Architecture**:

1. **Presentation Layer (API)**: Handles HTTP requests/responses using controllers
2. **Business Logic Layer (BLL)**: Contains application logic and services
3. **Data Access Layer (DAL)**: Manages database operations using Entity Framework Core and Repository Pattern

## Technologies Used

- **Framework**: ASP.NET Core 10.0 Web API
- **Database**: PostgreSQL (Dockerized)
- **ORM**: Entity Framework Core 10.0 with Npgsql
- **AutoMapper**: For DTO mapping
- **Swagger/OpenAPI**: API documentation and testing
- **Architecture Pattern**: N-Tier Architecture with Generic Repository Pattern
- **Docker**: For PostgreSQL containerization

## Key Features

### CRUD Operations
- ✅ Books Management
- ✅ Members Management
- ✅ Loans Management
- ✅ Categories Management
- ✅ Authors Management
- ✅ Fines Management

### Beyond CRUD Functionalities (Project Requirements)

#### 1. Advanced Search and Filtering ✅
- Search books by title, author, category, publication year
- Filter by availability status
- **Endpoint**: `POST /api/books/search`

#### 2. Book Recommendation System ✅
- Recommends books based on member's borrowing history
- Analyzes preferred categories
- Suggests popular books for new members
- **Endpoint**: `GET /api/books/recommendations/{memberId}`

#### 3. Late Fee Calculation Workflow ✅
- Automatically calculates fines for overdue books ($5 per day)
- Updates fine amounts dynamically
- Processes all overdue loans in batch
- **Endpoint**: `POST /api/loans/process-overdue-fines`

#### 4. Reports and Analytics ✅
- Library overview statistics
- Most borrowed books
- Member borrowing patterns
- Overdue reports
- **Endpoints**: `/api/statistics/*`

## Setup and Installation

### Prerequisites
- .NET 10.0 SDK
- Docker and Docker Compose

### Step 1: Start PostgreSQL Database

```bash
docker compose up -d
```

This creates a PostgreSQL database with:
- **Host**: localhost:5432
- **Database**: LibraryDB
- **Username**: admin
- **Password**: admin123

### Step 2: Navigate to API Project

```bash
cd LibraryManagement.API
```

### Step 3: Create and Apply Migrations

```bash
dotnet ef migrations add InitialCreate --project ../LibraryManagement.DAL --startup-project .

dotnet ef database update --project ../LibraryManagement.DAL --startup-project .
```

### Step 4: Run the Application

```bash
dotnet run
```

The API will be available at:
- **HTTP**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger

## API Endpoints

### Books (`/api/books`)
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Create new book
- `PUT /api/books/{id}` - Update book
- `DELETE /api/books/{id}` - Delete book
- `POST /api/books/search` - **Advanced search**
- `GET /api/books/recommendations/{memberId}` - **Get recommendations**

### Members (`/api/members`)
- `GET /api/members` - Get all members
- `GET /api/members/{id}` - Get member by ID
- `POST /api/members` - Create new member
- `PUT /api/members/{id}` - Update member
- `DELETE /api/members/{id}` - Delete member

### Loans (`/api/loans`)
- `GET /api/loans` - Get all loans
- `GET /api/loans/{id}` - Get loan by ID
- `POST /api/loans/borrow` - Borrow a book
- `POST /api/loans/{loanId}/return` - Return a book
- `GET /api/loans/overdue` - Get overdue loans
- `POST /api/loans/process-overdue-fines` - **Process overdue fines (workflow automation)**

### Statistics (`/api/statistics`)
- `GET /api/statistics/overview` - **Get library statistics**
- `GET /api/statistics/most-borrowed?topCount=10` - **Most borrowed books**
- `GET /api/statistics/member-statistics` - **Member borrowing statistics**
- `GET /api/statistics/overdue-report` - **Overdue report**

### Categories (`/api/categories`)
- Standard CRUD operations

### Authors (`/api/authors`)
- Standard CRUD operations

## Sample Requests

### Create Category
```json
POST /api/categories
{
  "name": "Science Fiction",
  "description": "Science fiction novels and stories"
}
```

### Create Author
```json
POST /api/authors
{
  "name": "Isaac Asimov",
  "email": "isaac@example.com",
  "bio": "American author and professor of biochemistry",
  "country": "USA"
}
```

### Create Book
```json
POST /api/books
{
  "title": "Foundation",
  "isbn": "978-0553293357",
  "publicationYear": 1951,
  "totalCopies": 5,
  "publisher": "Gnome Press",
  "description": "A classic science fiction novel",
  "categoryId": 1,
  "authorId": 1
}
```

### Create Member
```json
POST /api/members
{
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "+1234567890",
  "address": "123 Main St, City",
  "membershipType": "Premium"
}
```

### Borrow a Book
```json
POST /api/loans/borrow
{
  "bookId": 1,
  "memberId": 1,
  "loanDurationDays": 14
}
```

### Search Books (Advanced Filtering)
```json
POST /api/books/search
{
  "title": "Foundation",
  "category": "Science Fiction",
  "isAvailable": true
}
```

## Database Schema

### Core Entities
- **Book**: Stores book information with relationships to Category and Author
- **Category**: Book categories/genres
- **Author**: Author information
- **Member**: Library members with membership details
- **Loan**: Tracks book borrowing (connects Books and Members)
- **Fine**: Late fees associated with overdue loans

### Relationships
- Books → Category (Many-to-One)
- Books → Author (Many-to-One)
- Loans → Book (Many-to-One)
- Loans → Member (Many-to-One)
- Fines → Loan (Many-to-One)
- Fines → Member (Many-to-One)

All foreign keys use `Restrict` delete behavior for data integrity.

## Project Requirements Compliance

### ✅ 1. Web API Project
- RESTful API implementation
- JSON request/response format
- Proper HTTP methods (GET, POST, PUT, DELETE)

### ✅ 2. N-Tier Architecture
- **Presentation Layer**: API Controllers
- **Business Logic Layer**: Services (BookService, LoanService, etc.)
- **Data Access Layer**: Repositories and DbContext
- Clear separation of responsibilities
- No inappropriate direct access between layers

### ✅ 3. Functional Requirements (4-5 Beyond CRUD)
1. **Advanced Search/Filtering** - BookService.SearchBooks()
2. **Book Recommendation System** - BookService.GetRecommendationsForMember()
3. **Workflow Automation** - LoanService.ProcessOverdueFines()
4. **Reports & Analytics** - StatisticsService (4 different reports)

### ✅ 4. Technical Requirements
- Entity Framework Core for database interaction
- PostgreSQL relational database
- Modular and maintainable code
- Best coding practices
- DTOs with validation (Data Annotations)
- AutoMapper for mapping
- Generic Repository Pattern
- Dependency Injection

## Error Handling and Validation

- All DTOs include Data Annotations
- Automatic ModelState validation via `[ApiController]`
- Proper HTTP status codes:
  - **200 OK**: Successful retrieval
  - **201 Created**: Resource created
  - **204 No Content**: Successful update/delete
  - **400 Bad Request**: Validation errors
  - **404 Not Found**: Resource not found

## Testing with Swagger

1. Run the application
2. Navigate to http://localhost:5000/swagger
3. Explore all endpoints
4. Test requests directly from the browser
5. View request/response schemas

## Best Practices Implemented

- DTOs for API communication (no direct entity exposure)
- AutoMapper for clean object mapping
- Generic Repository Pattern for DRY principle
- Dependency Injection for loose coupling
- Service layer for business logic
- Validation attributes on DTOs
- Proper HTTP status codes
- RESTful URL structure
- Swagger documentation

## Project Author

**Author**: Mirza Saikat Ahmmed

Created for the ADVANCED PROGRAMMING WITH .NET course project.

## License

This project is for educational purposes.
