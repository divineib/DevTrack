# <div align="center">DevTrack</div>

<div align="center">
  Student developer portfolio and project tracking system built with ASP.NET Core MVC.
</div>

<p align="center">
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=fff" alt=".NET 10" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?logo=dotnet&logoColor=fff" alt="ASP.NET Core MVC" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/Status-Complete-1a7f37" alt="Status" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white" alt="C#" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/Razor-512BD4?logo=dotnet&logoColor=fff" alt="Razor" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=fff" alt="SQLite" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/Tailwind_CSS-v4-38bdf8?logo=tailwindcss&logoColor=fff" alt="Tailwind CSS v4" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=000" alt="JavaScript" /></a>
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
- Database seeding with demo data for local exploration

## Tech Stack

| Layer | Technology | Rationale |
|-------|-----------|-----------|
| **Backend** | ASP.NET Core MVC (.NET 10) | Server-side MVC with Razor views, minimal hosting model |
| **Language** | C# | Strongly typed, first-class .NET support |
| **Views** | Razor (`.cshtml`) | HTML + C# templating with tag helpers and partials |
| **Styling** | Tailwind CSS v4 + liquid glass | Utility-first build (`Styles/app.css` → `wwwroot/css/site.css`); frosted panels, semantic tokens; accessible focus/hover patterns aligned with shadcn-style UX (this app is Razor/MVC — [shadcn/ui](https://ui.shadcn.com/) is React-only and is not bundled) |
| **Client Scripts** | Vanilla JavaScript | Theme toggle, mobile nav, scroll animations — no framework overhead |
| **Database** | SQLite | Lightweight, zero-config relational database suitable for local dev |
| **ORM** | Entity Framework Core | Code-first migrations, LINQ queries, relationship configuration |
| **Auth** | ASP.NET Core Identity | Cookie-based authentication, role management (Student/Admin) |
| **Validation** | jQuery Validate + Unobtrusive | Client-side form validation paired with server-side model annotations |
| **External API** | GitHub REST API v3 | Repository metadata sync (name, description, last updated) |

## External Integrations

| Service | Purpose |
|---------|---------|
| **GitHub API** | Fetches repository metadata when a user links a GitHub URL to a project |
| **ASP.NET Core Identity** | User registration, login/logout, password hashing, and roles (Student/Admin) |

## Project Structure

```text
DevTrack/
├── DevTrack.slnx                 # Solution file
├── dotnet-tools.json             # dotnet-ef tool manifest (for migrations)
├── DevTrack.Web/
│   ├── Program.cs
│   ├── DevTrack.Web.csproj
│   ├── appsettings.json
│   ├── Controllers/              # Home, Account, Projects
│   ├── Models/                   # Domain entities
│   ├── ViewModels/               # Form and list view models
│   ├── Views/                    # Razor pages
│   ├── Data/                     # DbContext + seed data
│   ├── Services/                 # GitHub API client
│   ├── Migrations/               # EF Core migrations
│   ├── Styles/                   # Tailwind source (`app.css`)
│   ├── package.json              # Tailwind CLI scripts (devDependency)
│   └── wwwroot/                  # Built CSS, JS, client libraries
```

## Repository notes (for contributors)

- **SQLite database files are not on GitHub.** Patterns `*.db`, `*.db-shm`, and `*.db-wal` are in `.gitignore`, so your local `devtrack.db` / `devtrack.dev.db` (and any WAL files) stay on your machine only. There is **no database in this repository**—cloners get a **new empty database** on first run; migrations create tables and the seeder adds **generic demo data** (shared demo accounts and a placeholder GitHub link), not your personal records.
- **`node_modules/`** is ignored. The repo includes **`package.json`** and **`package-lock.json`** so `npm ci` or `npm install` is reproducible.
- **`wwwroot/css/site.css`** is the **built** Tailwind output and is **committed** so others can run `dotnet run` **without** Node.js. After you change `Styles/app.css`, run `npm run build:css` and commit the updated `site.css` so the UI stays in sync on GitHub.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS recommended) — **only if** you edit Tailwind source under `DevTrack.Web/Styles/`

### Run locally

From the repository root (the folder that contains `DevTrack.slnx`):

```bash
git clone https://github.com/divineib/DevTrack.git
cd DevTrack
cd DevTrack.Web
dotnet restore
dotnet run
```

Open **`http://localhost:5092`** in your browser (default **HTTP** profile from `launchSettings.json`; use **http**, not https, unless you run with the **https** profile and trust the ASP.NET dev certificate).

On first run, the app applies migrations and seeds roles, categories, skills, demo users, sample projects, and reviews.

### Tailwind CSS (optional)

Only needed when changing styles:

```bash
cd DevTrack.Web
npm install
npm run build:css
```

- **`npm run watch:css`** — rebuilds `wwwroot/css/site.css` whenever `Styles/app.css` changes (use while editing styles).
- If the UI looks unstyled or stale after a pull, run `npm run build:css` again and hard-refresh the browser (**Development** builds use strict cache headers for `.css` / `.js`).

### Troubleshooting

| Issue | What to try |
|-------|-------------|
| Styles look wrong after `git pull` | `cd DevTrack.Web && npm install && npm run build:css`, then hard-refresh |
| Port **5092** already in use | Stop the other process using that port or change `applicationUrl` in `Properties/launchSettings.json` |
| Database errors after switching branches | Delete local `*.db` files in `DevTrack.Web`, run again so migrations + seed recreate data |

### Demo accounts (after seed)

Password for both: **`DevTrack1`**

| Email | Role |
|-------|------|
| `student@devtrack.local` | Student |
| `admin@devtrack.local` | Admin |

You can also register a new account; new users receive the **Student** role by default.

### EF Core migrations (optional)

If you change models, from the **repository root** (folder that contains `DevTrack.slnx`):

```bash
dotnet tool restore
dotnet ef migrations add YourMigrationName --project DevTrack.Web
```

---

## License

This project was created for coursework. Use and modify as you like.
