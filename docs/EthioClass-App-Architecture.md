# EthioClass App Architecture

## 🎓 EthioClass App - Software Architecture Document

### 1. 👋 Introduction

EthioClass is a school management platform designed to give Ethiopian private schools a simple, trusted, and localized digital experience for admins, teachers, and parents.

The goal of EthioClass is not only to build software but to create a platform that understands how Ethiopian schools actually grade, communicate, and bill — not a foreign system adapted after the fact.

This document describes the initial software architecture, technology choices, security approach, development workflow, and deployment strategy.

The architecture is designed with the following principles:

- ✅ Keep it simple
- 🏗 Build a strong foundation
- 🧠 Avoid unnecessary complexity
- 📈 Design for future scalability
- 💎 Prioritize data correctness and reliability (grades and fees must never silently be wrong)

### 2. 🧱 Architecture Approach

#### 🧩 Modular Monolith Architecture

EthioClass will start as a **modular monolithic application**.

Instead of introducing microservices from the beginning, the backend will be developed as a well-structured modular system where each business domain (students, grading, fees, communication) is isolated behind its own service and repository layer.

This approach provides:

- ⚡ Faster development
- 🧰 Easier maintenance
- 🚀 Simple deployment
- 💸 Lower infrastructure cost
- 🧭 Clear business separation

As the school base grows (multiple schools, higher traffic), individual modules — most likely the SMS notification worker and the report-card PDF generator — can be extracted into independent services when there is a real business or technical need.

### 3. 🗺 High-Level System Architecture

```
                              ┌────────────┐
                              │   Users    │
                              │(Admin/     │
                              │Teacher/    │
                              │Parent)     │
                              └─────┬──────┘
                                    │
                 ┌──────────────────┼──────────────────┐
                 │                                      │
          ┌──────▼───────┐                    ┌─────────▼────────┐
          │ React Admin/  │                    │  Parent Web View  │
          │ Teacher Portal│                    │  (lightweight)    │
          └──────┬───────┘                    └─────────┬────────┘
                 │                                       │
                 └───────────────────┬───────────────────┘
                                     │ HTTPS REST API (versioned: /api/v1, /api/v2)
                                     │
                    ┌────────────────▼─────────────────┐
                    │        Linux VM / App Service      │
                    │  ┌───────────────────────────────┐ │
                    │  │        Nginx Reverse Proxy      │ │
                    │  └───────────────┬───────────────┘ │
                    │  ┌───────────────▼───────────────┐ │        ┌───────────────────────┐
                    │  │     ASP.NET Core Web API        │─┼───────▶  Background Jobs Queue │
                    │  │  (Modular Monolith, C#/.NET)    │ │        │  (Hangfire / BullMQ-  │
                    │  │  MediatR: Commands + Queries    │ │        │   equivalent)         │
                    │  └───────────────┬───────────────┘ │        └───────────┬───────────┘
                    └──────────────────┼─────────────────┘                    │
                                       │                                      │
                     ┌─────────────────┼─────────────────┐         ┌──────────▼──────────┐
                     │                                     │        │   Worker Process     │
              ┌──────▼──────┐                     ┌────────▼───┐    │  - SMS notifications │
              │ PostgreSQL   │                     │   Redis    │    │  - PDF report cards  │
              │ (EF Core)    │                     │(Cache+Jobs)│    │  - Scheduled reminders│
              └─────────────┘                     └────────────┘    └──────────────────────┘

                    ┌───────────────────── Deployment ─────────────────────┐
                    │  Developer → Git Repository → Docker Image → VM/Host  │
                    └────────────────────────────────────────────────────┘
```

### 4. 🧰 Technology Stack

#### 🖥️ Admin & Teacher Portal

**⚛️ React**

A React-based web portal will be developed for school admins and teachers to manage records and enter grades.

The portal will support:
- 👥 Student and staff management
- 📝 Grade entry per class/subject
- 📊 Reports and dashboards
- ⚙️ School configuration (terms, subjects, fee schedules)

#### 📱 Parent-Facing View

A lightweight, mobile-friendly web view (not a full native app for the MVP) so parents on any phone can check their child's record without installing anything — the Telegram bot remains the primary notification channel; this web view is the optional detail view for parents who want more than a bot message provides.

#### 🧠 Backend API

**🪺 ASP.NET Core (C#/.NET)**

ASP.NET Core will be used as the backend framework — the same stack used to build the TMS training platform this project's engineering practices are drawn from.

Reasons:
- 🟦 Strong typing end-to-end (C#)
- 🧱 Clean layered architecture (Controllers → Application/MediatR → Domain → Data)
- 🧩 Built-in dependency injection
- 🧱 Modular structure via feature folders (Students, Grading, Fees, Notifications)
- 🏢 Enterprise-level maintainability, proven at scale
- 📐 Native support for API versioning (`Asp.Versioning`) — critical since report-card and fee logic will evolve after schools are live
- 🧵 CQRS via MediatR — grade calculation, fee updates, and SMS triggers each live in their own small, testable handler, not a single sprawling service

The backend will expose a REST API consumed by both the admin/teacher portal and the parent-facing view.

#### 🗄 Database

**🐘 PostgreSQL (via Entity Framework Core)**

PostgreSQL will be used as the primary database.

Reasons:
- 🧱 Strong relational integrity — grades, fees, and attendance are inherently relational (student → term → subject → grade), not document-shaped
- 🛡 ACID transactions — a fee payment or grade entry must never be partially saved
- 📈 Mature, well-understood scaling path
- 🧾 Native support for row-versioning (optimistic concurrency) — needed when two staff members might edit the same student record

#### ⚡ Cache and Background Processing

**🔴 Redis**

Redis will be used for:
1. ⚡ Application caching (e.g., school configuration, subject lists — data that rarely changes within a term)
2. 🧵 Background job queuing

The implementation will use:
- 🔌 `StackExchange.Redis` for Redis communication
- 🐂 Hangfire (or a lightweight custom `BackgroundService`, matching patterns already used in the TMS project) for scheduled/queued job execution

### 5. 🔐 Authentication Strategy

#### 🔑 Credential-Based Authentication (Email/Phone + Password)

Unlike a consumer app targeting individual users, EthioClass's users are school staff and parents who are provisioned by the school admin — so simple email/phone + password login fits better than social OAuth for this audience.

Authentication flow:

**🪪 JWT Authentication**

After successful login:
- 🧾 Backend generates a JWT access token (and refresh token)
- 🔒 The web client stores the token securely (httpOnly cookie preferred over local storage)
- 🛡 Protected APIs require JWT authentication

Only endpoints requiring identity enforce authentication. Public resources (e.g., a school's public landing/contact info, if offered) remain accessible without authentication.

### 6. 🛂 Authorization Strategy

#### 🎭 Role-Based Access Control (RBAC)

Authorization is handled using roles. RBAC ensures each user can only access data relevant to their role and their own school (tenant).

Examples:
- 🧑‍💼 **School Admin** — full access within their own school only
- 👩‍🏫 **Teacher** — their assigned classes and subjects only
- 💰 **Bursar** — fee records only, no grade access
- 👪 **Parent** — their own child's record only, read-only

Every query is additionally scoped by `SchoolId` (tenant isolation) before any role check — a parent or teacher can never see another school's data, regardless of role.

### 7. 🧊 Redis Caching Strategy

Redis will be used to improve performance by reducing repeated, unnecessary database queries for data that barely changes within a term.

#### 📦 What will be cached?

The caching strategy focuses on data that:
- 🔁 Is frequently requested (e.g., subject lists, class rosters)
- 🐢 Changes infrequently (rarely mid-term)
- 🎯 Does not require real-time accuracy

#### ⏳ Cache Expiration Strategy

| Data | TTL |
|---|---|
| Subject/class configuration | 12 hours |
| Term/calendar data | 6 hours |
| Fee schedule per term | 1 hour |
| Student grade records | Not cached — always live (accuracy matters more than speed here) |

**Grades and fee balances are deliberately never cached** — a parent or bursar checking a balance must always see the current, correct value, never a stale cached one.

#### 🧹 Cache Invalidation

When data changes:
1. 📝 Update the database
2. 🧽 Remove the related cache key
3. 🔄 Next request loads fresh data
4. 💾 Store the updated result in Redis

### 8. 🧵 Background Job Processing

Some tasks should never block an API response.

Background jobs will handle:
- ⏰ Telegram bot notifications (absence, fee due dates, report-card-ready) — the default channel, sent via the Telegram Bot API at no per-message cost
- 📲 SMS fallback — triggered only for parents with no registered Telegram account, and for emergency/school-closure broadcasts where guaranteed delivery outweighs cost
- 📧 Bulk report card PDF generation for an entire class
- 📈 Future analytics/aggregation jobs (term pass-rate trends, etc.)

**Flow:** an API request (e.g., "generate report cards for Grade 5") enqueues a job and returns immediately; the worker processes it asynchronously. For each parent, the notification handler checks `ParentNotificationChannel` (Telegram vs. SMS) before sending, so the same job can silently mix channels without the caller needing to know which parents have Telegram. This keeps the API responsive even for a task affecting 40+ students at once, and keeps per-message SMS cost limited to the minority of parents who actually need it.

**Cost rationale:** SMS gateway pricing for Ethiopia runs meaningfully higher per message than local carrier rates, and that cost scales linearly with every parent and every notification. Defaulting to Telegram (free, unlimited) and reserving SMS for the unreachable minority keeps the platform's variable cost from growing directly with student count — a material consideration for a subscription-priced SaaS product with thin school-budget margins.

### 9. 🛡 Security Strategy

Security is considered from the beginning, not bolted on later.

Implemented protections:

**🔐 Authentication**
- 🔑 Email/phone + password, hashed with a strong algorithm (e.g., BCrypt/Argon2)
- 🪪 JWT authentication with short-lived access tokens + refresh tokens

**🛂 Authorization**
- 🎭 RBAC, combined with mandatory tenant (`SchoolId`) scoping on every query

**🧰 API Security**
- 🌐 CORS configuration, restricted to known frontend origins
- 🧷 CSRF protection on cookie-based flows
- 🧼 Input sanitization / output encoding
- 🧯 SQL injection prevention — enforced structurally via EF Core parameterized queries, never raw string-concatenated SQL
- ✅ Request validation via FluentValidation on every command
- 🧱 Secure response headers

**🏗 Infrastructure Security**
- 🔒 Secrets via environment variables / a secrets manager, never committed to source
- 🔐 HTTPS enforced everywhere
- 🧱 Nginx hardened configuration

### 10. 📚 API Documentation

API documentation will be maintained using **Scalar**, generated directly from OpenAPI attributes on the controllers (`[EndpointSummary]`, `[ProducesResponseType]`, etc.) — the same documentation-as-code approach used in the TMS project, so the docs can never silently drift from the real behavior.

Documentation includes:
- 🧩 All API endpoints, grouped by resource (Students, Grades, Fees, Notifications)
- 🧾 Request/response schema examples
- 🔐 Authentication requirements per endpoint
- 📦 Versioned documents (v1/v2) once the API evolves past the first release

This allows smooth collaboration between frontend and backend development, and gives pilot schools' technical contacts (if any) a real reference.

### 11. 🚀 Deployment Architecture

#### 🏗 Infrastructure

Deployment will use:
- 🐳 Docker (containerized API + worker)
- 🧱 Nginx (reverse proxy, HTTPS termination)
- ☁️ A Linux VM (DigitalOcean/Azure) or managed App Service
- 🐘 Managed PostgreSQL (e.g., Azure Database for PostgreSQL, or a managed VM instance for the first pilot schools)

### 12. 🧑‍💻 Development Workflow

#### 🧰 Development Tools

**🧑‍💻 Visual Studio / VS Code with C# Dev Kit**

Used for:
- 🖼 Backend development
- 🧹 Refactoring
- 🧠 Code assistance
- ⚡ Faster implementation

**🤖 Claude**

Used for:
- 🎨 Architecture and design feedback
- 🧾 Documentation drafting
- 🧪 Debugging support and code review
- 🧩 QA assistance

AI tools are used as engineering assistants to improve productivity and quality — not as a replacement for understanding the underlying architecture.

### 13. 🎯 Design Philosophy

- 🧠 The goal of EthioClass is not to build the most complex architecture.
- ✅ The goal is to build the right architecture — one that's honest about grading and fees, since those two things being wrong is what would actually lose a school's trust.
- 🧱 A simple, maintainable, and correctly-scoped foundation lets the team focus on what matters most:
  - 💝 Giving Ethiopian school administrators, teachers, and parents a system they can actually trust with their students' records.