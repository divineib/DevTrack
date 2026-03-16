# DevTrack Project Status

Simple project checklist for tracking progress across Parts I-IV.

> [!IMPORTANT]
> This file is checklist-only on purpose. It is used to track implementation status and pending requirements.

## 1) Phase Progress

- [x] Part I: System definition and objective
- [x] Part II: MVC shell setup and initial interface
- [ ] Part III: Core components and data-backed views
- [ ] Part IV: Final build, deployment, and presentation deliverables

## 2) Completed Work (Part II)

- [x] Created ASP.NET Core MVC project with .NET 10
- [x] Added primary pages: Overview, Dashboard, Projects, Admin, Privacy
- [x] Implemented shared layout and navigation shell
- [x] Added responsive, modern UI styling
- [x] Added dark/light theme toggle with saved preference
- [x] Added starter animation behaviors for interface transitions
- [x] Added inline code comments and placeholder notes for upcoming phases
- [x] Initialized Git and committed baseline project state

## 3) Part III Scope (Core Components)

### A. Data and Architecture

- [x] Add Entity Framework Core DbContext
- [x] Define core domain models (Project, Skill, Category, Review, Profile)
- [x] Create and apply initial migrations
- [x] Add development seed data

### B. Authentication and Authorization

- [x] Add ASP.NET Core Identity
- [x] Build Register/Login/Logout flows
- [x] Configure role model (Student, Admin)
- [x] Restrict admin endpoints and views by role

### C. Core MVC Features and CRUD

- [ ] Implement data-backed controller/actions for projects
- [ ] Implement full CRUD for projects
- [ ] Implement skills management workflow
- [ ] Replace demo metrics/cards with live database queries
- [ ] Add model validation and user-facing error handling

### D. External API Integration

- [ ] Integrate GitHub API for repository metadata
- [ ] Persist linked repository data
- [ ] Display synced repository data in project views

## 4) Final Submission Readiness (Part IV)

- [ ] Complete MVC application aligned with the project objective
- [ ] Complete full CRUD operations connected to the database
- [ ] Complete third-party API integration
- [ ] Verify responsive behavior on desktop and mobile
- [ ] Deploy application and verify public URL
- [ ] Finalize GitHub repository documentation
- [ ] Prepare a 5-10 minute screencast (code walkthrough + live demo)

## 5) Deployment Direction

- **Primary target:** Azure App Service (strong .NET support)
- **Alternative option:** Heroku (only if student plan/resources are confirmed and tested)

Current plan: proceed with Azure unless Heroku student tooling is already validated for this project.

> [!CAUTION]
> Do not mark deployment tasks as complete until the live URL is tested end-to-end for CRUD and role-based access behavior.