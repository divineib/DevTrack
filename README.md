# <div align="center">DevTrack</div>

<div align="center">
  Student developer portfolio and project tracking system built with ASP.NET Core MVC.
</div>

<p align="center">
  <a href="#"><img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=fff" alt=".NET 10" /></a>
  <a href="#"><img src="https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?logo=dotnet&logoColor=fff" alt="ASP.NET Core MVC" /></a>
  <a href="#"><img src="https://img.shields.io/badge/Status-Complete-1a7f37" alt="Status" /></a>
  <a href="#"><img src="https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white" alt="C#" /></a>
  <a href="#"><img src="https://img.shields.io/badge/Razor-512BD4?logo=dotnet&logoColor=fff" alt="Razor" /></a>
  <a href="#"><img src="https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=fff" alt="SQLite" /></a>
  <a href="#"><img src="https://img.shields.io/badge/CSS-639?logo=css&logoColor=fff" alt="CSS" /></a>
  <a href="#"><img src="https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=000" alt="JavaScript" /></a>
</p>

---

## Overview

DevTrack is a web application that helps software engineering students track coursework projects, tag technical skills, sync GitHub repository metadata, and present portfolio progress in a clean, role-based workspace. Students manage their own projects while administrators review submissions across all users.

## Features

- Full CRUD for projects with category and skill tagging
- GitHub API integration for automatic repository metadata sync
- Role-based access control (Student and Admin roles)
- Student dashboard with live project metrics and activity feed
- Admin review center with moderation feed and KPI cards
- Responsive design with mobile hamburger navigation
- Dark/light theme toggle with saved preference
- Client-side and server-side form validation
- Database seeding for development testing

## Tech Stack

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

## External Integrations

| Service | Purpose |
|---------|---------|
| **GitHub API** | Fetches repository metadata (name, description, stars, last update) when a user links a GitHub URL to a project |
| **ASP.NET Core Identity** | Manages user registration, login/logout, password hashing, and role assignment (Student/Admin) |

## Project Structure

```text
DevTrack/
├── DevTrack.slnx                        # Solution file
├── DevTrack.Web/                         # Main web project
│   ├── Program.cs                        # Entry point and service configuration
│   ├── DevTrack.Web.csproj               # Project file with NuGet packages
│   ├── appsettings.json                  # Connection string and config
│   │
│   ├── Controllers/                      # MVC Controllers
│   │   ├── HomeController.cs             # Landing, Dashboard, Admin, Privacy
│   │   ├── AccountController.cs          # Register, Login, Logout
│   │   └── ProjectsController.cs         # Project CRUD + Skill management
│   │
│   ├── Models/                           # Domain entities
│   │   ├── AppUser.cs                    # Identity user extension
│   │   ├── Project.cs                    # Core project entity
│   │   ├── ProjectStatus.cs             # Status enum (Planned, InProgress, etc.)
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
│   │   ├── Home/                         # Landing, Dashboard, Admin, Privacy
│   │   ├── Account/                      # Login, Register, AccessDenied
│   │   ├── Projects/                     # Index, Create, Edit, _ProjectForm
│   │   └── Shared/                       # _Layout, _ValidationScriptsPartial, Error
│   │
│   ├── Data/                             # Database layer
│   │   ├── ApplicationDbContext.cs       # EF Core DbContext
│   │   └── DbSeeder.cs                  # Seed data (roles, categories, skills)
│   │
│   ├── Services/                         # External integrations
│   │   ├── IGitHubRepoService.cs        # GitHub API interface
│   │   ├── GitHubRepoService.cs         # Implementation
│   │   └── GitHubRepoInfo.cs            # DTO
│   │
│   ├── Migrations/                       # EF Core database migrations
│   │
│   └── wwwroot/                          # Static assets
│       ├── css/site.css                  # Theme tokens, responsive styles
│       ├── js/site.js                    # Theme toggle, mobile nav, animations
│       └── lib/                          # Vendored libraries (jQuery, Bootstrap)
│
├── REPORT.md                             # Final project report (source)
├── REPORT.pdf                            # Final project report (PDF)
└── PROJECT_STATUS.md                     # Phase progress checklist
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run Locally

```bash
cd DevTrack.Web
dotnet restore
dotnet run
```

Open the URL shown in the terminal (e.g. `http://localhost:5092`).

On first run the app automatically applies migrations and seeds starter data (roles, categories, skills).

### Test Accounts

Register a new account to explore the **Student** role. To access the **Admin** review center, assign the Admin role to a user via the seeded role system.

## Documentation

| File | Description |
|------|-------------|
| [`REPORT.md`](./REPORT.md) | Full project report — structure, MVC breakdown, key code, responsive design |
| [`REPORT.pdf`](./REPORT.pdf) | PDF version of the report for submission |
| [`PROJECT_STATUS.md`](./PROJECT_STATUS.md) | Phase progress checklist (Parts I–IV) |
