# DevTrack Project Status

This file tracks implementation status, upcoming milestones, and preparation for project check-ins.

## Overall Phase Status

- [x] Part I: System definition and objective document
- [x] Part II: MVC shell setup and initial UI/pages
- [ ] Part III: Core components and data-backed views
- [ ] Part IV: Final submission (deployment, demo, complete feature set)

## Completed in Part II

- [x] Created .NET 10 ASP.NET Core MVC project structure
- [x] Implemented multi-page navigation shell (Overview, Dashboard, Projects, Admin, Privacy)
- [x] Added responsive, modern interface styling
- [x] Implemented dark/light theme toggle with saved preference
- [x] Added starter interaction animations
- [x] Added code comments and placeholders for upcoming implementation
- [x] Initialized Git repository and committed baseline

## Part III Objectives (Core Components)

### Architecture and Data

- [ ] Add Entity Framework Core DbContext
- [ ] Define core models (User profile, Project, Skill, Category, Review)
- [ ] Create and apply initial database migrations
- [ ] Seed starter data for development/testing

### Authentication and Authorization

- [ ] Add ASP.NET Core Identity
- [ ] Implement Register/Login/Logout views and flows
- [ ] Define role setup (Student, Admin)
- [ ] Restrict admin routes to Admin role

### CRUD and MVC Completion

- [ ] Implement ProjectsController with full CRUD
- [ ] Implement Skills management (create/read/update/delete)
- [ ] Connect dashboard cards and lists to real data
- [ ] Add validation and user-friendly error handling

### API Integration

- [ ] Integrate GitHub API for repository metadata
- [ ] Store linked repository details in database
- [ ] Display synced repository info in project views

## Weekly Status Update Template

Use this format for class updates:

### 1. Current Progress and Demonstration

- Summary of completed work:
- Demo evidence (live, screenshots, or video):

### 2. Next Week Objectives

- Planned milestones:
- Target timeline:

### 3. Request for Assistance

- Technical blockers:
- Clarifications needed:

## Part IV Final Submission Readiness Checklist

- [ ] Full MVC app aligned with original concept
- [ ] CRUD fully functional with database
- [ ] Third-party API integration complete
- [ ] Responsive UI verified on multiple screen sizes
- [ ] Application deployed and public URL available
- [ ] GitHub repository updated with documentation
- [ ] 5-10 minute screen-recorded walkthrough completed

## Deployment Decision Notes

- Azure: best alignment with .NET ecosystem and long-term professional relevance
- Heroku: possible option if student plan access is available, but .NET workflows may be less direct than Azure

Current recommendation: prioritize Azure App Service for final submission unless Heroku student credits/resources are already set up and tested.