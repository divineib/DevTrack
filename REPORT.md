# DevTrack - Final Project Report

**Course:** Dynamic Web Page Development  
**Project:** DevTrack - Student Developer Portfolio Tracker  
**Framework:** ASP.NET Core MVC (.NET 10)  
**Database:** SQLite via Entity Framework Core  

---

## 1. Project Overview

DevTrack is a web application built to help software engineering students track their coursework projects, map technical skills, and monitor portfolio growth over time. It provides role-based workspaces where students manage their own projects and administrators review submissions across all users.

Key capabilities:

- Full CRUD operations for managing portfolio projects
- Skill tagging with category grouping
- GitHub API integration for automatic repository metadata sync
- Role-based access control (Student and Admin roles)
- Responsive dark/light theme with mobile-friendly navigation
- Database seeding for development testing

---

## 2. Tech Stack

| Layer | Technology | Rationale |
|-------|-----------|-----------|
| **Backend** | ASP.NET Core MVC (.NET 10) | Server-side MVC with Razor views, minimal hosting model |
| **Language** | C# | Strongly typed, first-class .NET support |
| **Views** | Razor (`.cshtml`) | HTML + C# templating with tag helpers and partials |
| **Styling** | Custom CSS with CSS Variables | Responsive grid/flexbox layout, dark/light theming via custom properties |
| **Client Scripts** | Vanilla JavaScript | Theme toggle, mobile nav, scroll animations — no framework overhead |
| **Database** | SQLite | Lightweight, zero-config relational database suitable for local dev |
| **ORM** | Entity Framework Core | Code-first migrations, LINQ queries, relationship configuration |
| **Auth** | ASP.NET Core Identity | Cookie-based authentication, role management (Student/Admin) |
| **Validation** | jQuery Validate + Unobtrusive | Client-side form validation paired with server-side model annotations |
| **External API** | GitHub REST API v3 | Repository metadata sync (name, description, last updated) |

## 3. External Integrations

| Service | Purpose |
|---------|---------|
| **GitHub API** | Fetches repository metadata (name, description, stars, last update) when a user links a GitHub URL to a project |
| **ASP.NET Core Identity** | Manages user registration, login/logout, password hashing, and role assignment (Student/Admin) |

---

## 4. Visual Studio Project Structure

```
DevTrack/
├── DevTrack.slnx                        # Solution file
├── DevTrack.Web/                         # Main web project
│   ├── Program.cs                        # App entry point and service configuration
│   ├── DevTrack.Web.csproj               # Project file with NuGet references
│   ├── appsettings.json                  # Connection string and app config
│   │
│   ├── Controllers/                      # MVC Controllers
│   │   ├── HomeController.cs             # Landing, Dashboard, Admin, Privacy
│   │   ├── AccountController.cs          # Register, Login, Logout
│   │   └── ProjectsController.cs         # Project CRUD + Skill management
│   │
│   ├── Models/                           # Domain entities
│   │   ├── AppUser.cs                    # Identity user extension
│   │   ├── Project.cs                    # Core project entity
│   │   ├── ProjectStatus.cs             # Status enum
│   │   ├── Skill.cs                     # Skill entity
│   │   ├── Category.cs                  # Skill/project category
│   │   ├── ProjectSkill.cs             # Many-to-many join entity
│   │   ├── Review.cs                    # Admin review record
│   │   ├── StudentProfile.cs           # Student profile extension
│   │   └── ErrorViewModel.cs            # Error page model
│   │
│   ├── ViewModels/                       # Presentation-layer models
│   │   ├── Account/
│   │   │   ├── LoginViewModel.cs
│   │   │   └── RegisterViewModel.cs
│   │   └── Projects/
│   │       ├── ProjectFormViewModel.cs
│   │       └── ProjectsIndexViewModel.cs
│   │
│   ├── Views/                            # Razor views
│   │   ├── Home/
│   │   │   ├── Index.cshtml              # Public landing page
│   │   │   ├── Dashboard.cshtml          # Student dashboard
│   │   │   ├── Admin.cshtml              # Admin review center
│   │   │   ├── Projects.cshtml           # Redirect shell
│   │   │   └── Privacy.cshtml            # Privacy & data use
│   │   ├── Account/
│   │   │   ├── Login.cshtml
│   │   │   ├── Register.cshtml
│   │   │   └── AccessDenied.cshtml
│   │   ├── Projects/
│   │   │   ├── Index.cshtml              # Project list with skills
│   │   │   ├── Create.cshtml             # New project form
│   │   │   ├── Edit.cshtml               # Edit project form
│   │   │   └── _ProjectForm.cshtml       # Shared form partial
│   │   └── Shared/
│   │       ├── _Layout.cshtml            # App shell layout
│   │       ├── _ValidationScriptsPartial.cshtml
│   │       └── Error.cshtml
│   │
│   ├── Data/                             # Database layer
│   │   ├── ApplicationDbContext.cs       # EF Core DbContext
│   │   └── DbSeeder.cs                  # Development seed data
│   │
│   ├── Services/                         # External integrations
│   │   ├── IGitHubRepoService.cs        # Interface for GitHub API
│   │   ├── GitHubRepoService.cs         # Implementation
│   │   └── GitHubRepoInfo.cs            # DTO
│   │
│   ├── Migrations/                       # EF Core migrations
│   │
│   └── wwwroot/                          # Static assets
│       ├── css/site.css                  # Custom theme and responsive styles
│       ├── js/site.js                    # Theme toggle, mobile nav, animations
│       └── lib/                          # Vendored libraries (jQuery, Bootstrap)
```

---

## 5. MVC Component Breakdown

### 5a. Models

The domain model layer defines seven entity classes that map to SQLite tables through Entity Framework Core.

**`Project`** is the central entity. Each project belongs to an `AppUser` (via `OwnerId`), optionally references a `Category`, and connects to multiple `Skill` records through the `ProjectSkill` join table. Projects also store GitHub metadata fields (`GitHubRepoUrl`, `GitHubRepoName`, `GitHubLastSyncedUtc`) populated by the external API integration.

```csharp
public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }

    [StringLength(200)]
    public string? GitHubRepoUrl { get; set; }

    [StringLength(120)]
    public string? GitHubRepoName { get; set; }

    public DateTime? GitHubLastSyncedUtc { get; set; }

    public string OwnerId { get; set; } = string.Empty;
    public AppUser? Owner { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
```

**`AppUser`** extends `IdentityUser` to include a `DisplayName` and an optional `StudentProfile` navigation (one-to-one relationship enforced by a unique index on `UserId`).

**`ProjectSkill`** implements a many-to-many relationship between `Project` and `Skill` using a composite primary key configured in the DbContext's `OnModelCreating`:

```csharp
builder.Entity<ProjectSkill>()
    .HasKey(ps => new { ps.ProjectId, ps.SkillId });
```

**`ProjectStatus`** is an enum with four states: `Planned`, `InProgress`, `Completed`, and `OnHold`.

**Entity Relationships:**

```
AppUser (1) ──── (*) Project
Project (1) ──── (*) ProjectSkill (*) ──── (1) Skill
Project (*) ──── (1) Category
Category (1) ──── (*) Skill
Project (1) ──── (*) Review
AppUser (1) ──── (0..1) StudentProfile
```

### 5b. Views

All views use the Razor view engine (`.cshtml` files) with tag helpers enabled via `_ViewImports.cshtml`. The shared `_Layout.cshtml` provides the application shell: a sticky translucent navigation bar, content area, and footer.

**Layout features:**
- Responsive hamburger menu that activates below 980px screen width
- Role-aware navigation: Dashboard and Projects links appear only for authenticated users; the Admin link appears only for users in the Admin role
- Dark/light theme toggle with preference saved to `localStorage`
- Authentication-aware header showing either sign-in/register buttons or the user's name with a logout form

**View organization:**
- **Home views** serve the public landing page (with live database metrics), the authenticated student dashboard (with project activity and KPI cards), the admin review center (with moderation feed), and the privacy page
- **Account views** provide clean, centered forms for registration and login, plus an access-denied page
- **Project views** include a list page with inline skill management, and a shared `_ProjectForm` partial used by both Create and Edit views. The form partial includes category and skill selection, status dropdown, and a GitHub URL input field
- **Shared partials** include the validation scripts partial (jQuery + jQuery Validate + Unobtrusive Validation) and the error page

### 5c. Controllers

**`HomeController`** — Handles the five main pages of the application shell.

| Action | Auth | Description |
|--------|------|-------------|
| `Index` | Public | Landing page with live user/project/skill counts from database |
| `Dashboard` | `[Authorize]` | Student workspace with per-user project metrics (scoped to own projects unless Admin) |
| `Projects` | `[Authorize]` | Redirects to `ProjectsController.Index` for the real CRUD page |
| `Admin` | `[Authorize(Roles = "Admin")]` | Review center with pending/flagged/approved counts and moderation feed |
| `Privacy` | Public | Data use and access control information |

**`AccountController`** — Manages the authentication lifecycle.

| Action | Auth | Description |
|--------|------|-------------|
| `Register` GET/POST | `[AllowAnonymous]` | Creates a new user, assigns the Student role, and signs in |
| `Login` GET/POST | `[AllowAnonymous]` | Authenticates via `SignInManager.PasswordSignInAsync` |
| `Logout` POST | `[Authorize]` | Signs out and redirects to the landing page |
| `AccessDenied` | `[AllowAnonymous]` | Shown when a user tries to access a restricted page |

**`ProjectsController`** — Full CRUD operations for projects, protected by `[Authorize]` at the class level.

| Action | HTTP | Description |
|--------|------|-------------|
| `Index` | GET | Lists projects with eager-loaded categories and skills. Scoped to the current user's projects unless Admin. |
| `Create` | GET/POST | Renders a form with category/skill dropdowns. On POST, saves the project, syncs GitHub metadata if URL provided, and links selected skills. |
| `Edit` | GET/POST | Loads the existing project for owner or Admin. Updates fields and re-syncs GitHub metadata. |
| `Delete` | POST | Removes a project. Owner-or-Admin enforcement via `CanAccessProject()`. |
| `AddSkill` | POST | Creates a new skill (duplicate-checked) for the global skill pool. |

The ownership check is a private helper method:

```csharp
private bool CanAccessProject(Project project)
{
    if (User.IsInRole("Admin"))
        return true;

    var userId = User.FindFirst("...nameidentifier")?.Value;
    return !string.IsNullOrWhiteSpace(userId) && project.OwnerId == userId;
}
```

---

## 6. Key and Complex Code Sections

### 6a. Application Startup (Program.cs)

The application uses the minimal hosting model. Services are registered in this order:

1. **Entity Framework Core** with SQLite (`Data Source=devtrack.db`)
2. **ASP.NET Core Identity** with `AppUser` and `IdentityRole`, configured with relaxed password rules suitable for a class project
3. **Cookie authentication** paths pointing to `/Account/Login` and `/Account/AccessDenied`
4. **Typed HttpClient** for `IGitHubRepoService` with the GitHub API base address and required headers
5. **MVC** via `AddControllersWithViews()`

The middleware pipeline enables HTTPS redirection, routing, authentication, authorization, static asset serving, and the default MVC route pattern `{controller=Home}/{action=Index}/{id?}`. The `DbSeeder` runs at the end of startup to ensure migrations are applied and seed data exists.

### 6b. Database Context and Relationships

`ApplicationDbContext` extends `IdentityDbContext<AppUser>`, inheriting all ASP.NET Core Identity tables (users, roles, claims, tokens) while adding six domain `DbSet` properties. The `OnModelCreating` override configures:

- The composite primary key on `ProjectSkill` for the many-to-many relationship
- Cascade delete behavior so removing a project also removes its skill associations
- A unique index on `StudentProfile.UserId` to enforce the one-to-one relationship with `AppUser`

### 6c. GitHub API Integration

The `GitHubRepoService` is registered as a typed `HttpClient` and implements `IGitHubRepoService`. When a user provides a GitHub repository URL on a project form, `ProjectsController.TryApplyGitHubMetadataAsync` parses the URL to extract the owner and repo name, calls the GitHub REST API at `repos/{owner}/{repo}`, and stores the returned repository name and sync timestamp on the project. If the user left the project description blank, the GitHub repository description is used as a fallback.

The URL parsing logic (`TryParseGitHubRepo`) validates that the URL is an absolute URI on `github.com` with at least two path segments (owner and repo), stripping any `.git` suffix.

### 6d. Database Seeding

`DbSeeder.SeedCoreDataAsync` runs at startup and performs three operations:

1. Applies any pending EF Core migrations via `MigrateAsync()`
2. Creates the `Student` and `Admin` roles if they don't exist
3. Seeds three categories and four skills (linked to categories) if the tables are empty

This ensures the application always has a functional baseline for development and testing.

### 6e. Role-Based Authorization

Authorization is implemented at three levels:

- **Controller-level**: `[Authorize]` on `ProjectsController` requires authentication for all project operations
- **Action-level**: `[Authorize(Roles = "Admin")]` on the Admin action restricts access to the admin role
- **Code-level**: `CanAccessProject()` checks ownership before edit/delete operations, allowing admins to bypass the check
- **View-level**: The layout conditionally renders navigation links based on `User.Identity.IsAuthenticated` and `User.IsInRole("Admin")`

---

## 7. Responsive Design Approach

The application uses a custom CSS architecture built with CSS custom properties (variables) for theming and standard CSS Grid/Flexbox for layout. No CSS framework is used for the active UI — Bootstrap is vendored but not loaded.

### Theme System

Two complete color palettes are defined via CSS custom properties on `:root` (dark, default) and `html[data-theme="light"]` (light). All colors throughout the stylesheet reference these variables, enabling instant theme switching via a single `data-theme` attribute change on the `<html>` element. The user's preference is persisted in `localStorage`.

### Responsive Breakpoints

Two media query breakpoints handle the responsive layout:

**At 980px and below:**
- The desktop navigation links are hidden
- A hamburger menu button appears, toggling the nav open/closed
- KPI cards and panels shift from 3-column and 6-column spans to 6-column

**At 700px and below:**
- The topbar stacks vertically with the auth controls below the brand/nav area
- All grid elements become full-width (12-column span)
- Auth forms expand to full width
- Skills checklists switch from 2-column to single-column
- List items stack vertically for readability

### Mobile Navigation

The hamburger menu is implemented with a CSS-only animated icon (three bars that transform into an X when active) and a JavaScript toggle that manages the `open` class and `aria-expanded` attribute for accessibility. Navigation links automatically close when clicked.

### Typography

Font sizes use `clamp()` for fluid scaling between breakpoints (e.g., `clamp(1.6rem, 4vw, 2.35rem)` for hero headings), avoiding abrupt size jumps.

---

## 8. How to Run the Project Locally

1. Ensure .NET 10 SDK is installed
2. Clone or extract the project directory
3. Open a terminal in the `DevTrack.Web/` folder
4. Run:
   ```
   dotnet restore
   dotnet run
   ```
5. The app will apply migrations, seed data, and start on `https://localhost:5001` (or the port shown in terminal output)
6. Register a new account to explore the Student role
7. The seeded Admin account (if configured) can access the Admin review center

---

*Report prepared for Dynamic Web Page Development - Final Project Submission*
