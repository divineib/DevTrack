# DevTrack Project Status

This is my working project tracker for DevTrack. I am using it to document what I have completed, what I am currently building, and what I still need to finish for Parts III and IV.

## 1) Phase Progress

- [x] Part I: I completed the system definition and objective
- [x] Part II: I completed the MVC shell setup and initial interface
- [ ] Part III: I still need to implement core components and data-backed views
- [ ] Part IV: I still need to complete final build, deployment, and presentation deliverables

## 2) Completed Work (Part II)

- [x] I created the ASP.NET Core MVC project with .NET 10
- [x] I added the primary pages: Overview, Dashboard, Projects, Admin, Privacy
- [x] I implemented a shared layout and navigation shell
- [x] I added responsive, modern UI styling
- [x] I added a dark/light theme toggle with saved preference
- [x] I added starter animation behaviors for interface transitions
- [x] I added inline code comments and placeholder notes for upcoming phases
- [x] I initialized Git and committed the baseline project state

## 3) Part III Scope (Core Components)

### A. Data and Architecture

- [ ] I need to add Entity Framework Core DbContext
- [ ] I need to define core domain models (Project, Skill, Category, Review, Profile)
- [ ] I need to create and apply initial migrations
- [ ] I need to add development seed data

### B. Authentication and Authorization

- [ ] I need to add ASP.NET Core Identity
- [ ] I need to build Register/Login/Logout flows
- [ ] I need to configure role model (Student, Admin)
- [ ] I need to restrict admin endpoints and views by role

### C. Core MVC Features and CRUD

- [ ] I need to implement data-backed controller/actions for projects
- [ ] I need to implement full CRUD for projects
- [ ] I need to implement skills management workflow
- [ ] I need to replace demo metrics/cards with live database queries
- [ ] I need to add model validation and user-facing error handling

### D. External API Integration

- [ ] I need to integrate GitHub API for repository metadata
- [ ] I need to persist linked repository data
- [ ] I need to display synced repository data in project views

## 4) Weekly Status Update Template

I can use this format for class check-ins:

### Current Progress and Demonstration

- Summary of completed work:
- Demonstration evidence (live walkthrough, screenshots, or short recording):

### Next Week Objectives

- Planned milestones:
- Target timeline:

### Assistance Needed

- Technical blockers:
- Requirement clarifications:

## 5) Final Submission Readiness (Part IV)

- [ ] I need to complete the MVC application aligned with the project objective
- [ ] I need to complete full CRUD operations connected to the database
- [ ] I need to complete third-party API integration
- [ ] I need to verify responsive behavior on desktop and mobile
- [ ] I need to deploy the application and verify the public URL
- [ ] I need to finalize the GitHub repository documentation
- [ ] I need to prepare a 5-10 minute screencast (code walkthrough + live demo)

## 6) Deployment Direction

- **Primary target I plan to use:** Azure App Service (strong .NET support)
- **Alternative option:** Heroku (only if student plan/resources are confirmed and tested)

My current plan is to proceed with Azure unless Heroku student tooling is already validated for this project.