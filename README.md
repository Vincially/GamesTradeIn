# GamesTradeIn

An open-source, high-performance digital game trading, selling, and rental platform. This project showcases enterprise-grade software engineering practices, featuring a **.NET Backend** leveraging DDD and Clean Architecture, and a **React PWA Frontend**.

---

## Key Features & Business Rules
* **Digital-Only Ecosystem:** Only for digital game licenses, keys, and access across platforms (PS5, Xbox Series S/X, Switch, Steam, Epic, GoG).
* **Unified Rental System:** Allows users to rent digital access for a specified duration, supporting both premium (paid via virtual wallet) and free tier (loan) options.
* **Secured Wallet (Escrow):** Financial transactions are held in a secure domain escrow during active rentals and only released upon successful return validation.
* **Smart Wishlist Matchmaking:** Automatic background matching between users' wishlists and available inventory titles.

---

## Tech Stack & Local Environment

The entire ecosystem is containerized for seamless local development:
* **Backend:** .NET 10, C#, Minimal APIs, Entity Framework Core, Dapper, MediatR.
* **Frontend:** React, Vite, TypeScript, TailwindCSS, Service Workers (PWA).
* **Database:** PostgreSQL (with strict ACID compliance).
* **Infrastructure:** Docker & Docker Compose.

---

## System Architecture & Diagrams

This project is strictly documented using **Mermaid.js**. You can view our deep-dive technical specifications inside the `docs/architecture/` folder.

### [1. System Context & Containers (C4 Model - Level 2)](docs/architecture/c4-containers.md)
Illustrates the boundaries of the system, component responsibilities, and how data flows across the React PWA, .NET API, and PostgreSQL.

### [2. Domain Model & Tactical DDD](docs/architecture/domain-model.md)
Represents the core domain, enforcing conceptual consistency boundaries through Aggregate Roots, Entities, and Value Objects.

### [3. Transaction State Machine](docs/architecture/state-machine.md)
Tracks the dynamic lifecycle of a transaction (Sale, Trade, or Rental), illustrating how domain states transition securely based on user actions and invariants.

### [4. Technical Workflows (Sequence Diagrams)](docs/architecture/sequence-diagram.md)
We maintain detailed sequence diagrams mapping critical execution paths:

- The Command Path (Write): Demonstrates transactional ACID safety and DDD invariant checks during paid rental approvals. (See docs/architecture/sequence-diagram.md)
- The Query Path (Read): Demonstrates high-performance reads using Dapper, bypassing EF Change Tracker, and optimizing bandwidth with HTTP ETag Caching (If-None-Match).