### Movies and Directors Project - README

---

#### **Project Overview**
This project is a web application built with ASP.NET Core MVC, showcasing the relationship between movies and their respective directors. It allows users to manage movies, directors, and their relational data through a simple interface. The project demonstrates key functionalities like authentication, authorization, CRUD operations, and data relationships.

---

#### **Technologies Used**
- **Backend**: ASP.NET Core MVC
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Frontend**: Razor Views with Bootstrap for UI
- **Authentication**: Cookie-based authentication with roles (Admin/User)
- **Dependency Injection**: Built-in ASP.NET Core DI
- **Session Management**: ASP.NET Core Session

---

#### **Features**
1. **Directors Management**
   - Add, edit, delete, and list directors.
   - Manage director details such as name, surname, and retirement status.

2. **Movies Management**
   - Add, edit, delete, and list movies.
   - Link movies to their respective directors.
   - View movie details, including name, release date, total revenue, and director information.

3. **Authentication & Authorization**
   - Role-based access control (Admin/User roles).
   - Login and logout functionality.
   - Restrict movie and director management to Admin users.

4. **Error Handling**
   - Validation for entity relationships (e.g., preventing deletion of a director linked to movies).
   - Display appropriate error messages to users.

---

#### **Setup Instructions**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/movies-directors.git
   cd movies-directors
   ```

2. **Configure the Database**
   - Make sure PostgreSQL is installed and running.
   - Create a new database (e.g., `moviesdb`).
   - Update the connection string in `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "Db": "Host=localhost;Port=5432;Database=moviesdb;Username=your_username;Password=your_password"
     }
     ```

3. **Apply Migrations**
   Run the following commands to apply database migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Seed Data (Optional)**
   - Seed the database with sample directors and movies using the provided services or SQL commands.

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the Application**
   Open your browser and navigate to:
   ```
   http://localhost:5103
   ```

---

#### **Project Structure**

- **Controllers**: Handles HTTP requests and maps them to views or services.
  - `MoviesController`
  - `DirectorsController`
  - `UsersController`
  - `RolesController`

- **Models**: Represents database entities and their relationships.
  - `Movie`, `Director`, `User`, `Role`

- **Services**: Contains business logic for CRUD operations and validations.
  - `MoviesService`
  - `DirectorsService`

- **Views**: Razor Views for each controller (e.g., `Movies/Index.cshtml`, `Directors/Create.cshtml`).

- **DAL**: Data Access Layer with Entity Framework DbContext (`Db`).

---

#### **Key Files**

1. **`Program.cs`**
   - Configures services (DI, authentication, session, DbContext).
   - Middleware pipeline setup.

2. **`AppSettings.json`**
   - Contains configuration for connection strings and other app settings.

3. **`Models/`**
   - Defines data models and view models.

4. **`Controllers/`**
   - Implements logic for handling HTTP requests.

---

#### **Authentication & Authorization**

1. **Authentication**
   - Cookie-based authentication is used.
   - Login and logout endpoints available in `UsersController`.

2. **Authorization**
   - Admin-only access to director and movie management.
   - Authorization is implemented with `[Authorize(Roles = "Admin")]`.

---

#### **Sample Data**

**Directors**
| Name             | Surname     | IsRetired |
|------------------|-------------|-----------|
| Christopher      | Nolan       | No        |
| Quentin          | Tarantino   | No        |
| Francis Ford     | Coppola     | Yes       |

**Movies**
| Name          | Release Date | Total Revenue | Director          |
|---------------|--------------|---------------|-------------------|
| Inception     | 2010-07-16   | $829,895,144  | Christopher Nolan |
| Pulp Fiction  | 1994-10-14   | $213,928,762  | Quentin Tarantino |
| The Godfather | 1972-03-24   | $134,966,411  | Francis Ford Coppola |

---

#### **Future Improvements**
- Add search and filter functionality for movies and directors.
- Implement advanced reporting on movie revenues.
- Add localization support for multiple languages.

---

#### **License**
This project is licensed under the MIT License.

---

#### **Contributors**
- **Your Name**: [GitHub Profile](https://github.com/buseclk12)

Feel free to customize this file further to suit your project's specific details!
