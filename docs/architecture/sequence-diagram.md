# Transaction Acceptance Sequence Diagram (Paid Rental Flow)

This sequence diagram illustrates the HTTP communication, database transactions, and custom telemetry processing when a user accepts a paid rental proposal. It highlights how the React PWA handles responses and how the .NET API enforces Domain invariants and ACID compliance.

```mermaid
sequenceDiagram
    autonumber
    actor Receiver as Receiver (Game Owner)
    participant PWA as React PWA (Frontend)
    participant API as .NET API (Backend)
    participant DB as PostgreSQL Database

    Receiver->>PWA: Clicks "Accept Rental Proposal"
    
    PWA->>API: POST /api/transactions/{id}/approve<br/>[Authorization: Bearer JWT]
    note over PWA, API: Injects Client-side telemetry triggers if necessary
    
    activate API
    API->>DB: Begin Database Transaction
    
    API->>DB: Fetch Transaction & Aggregates (User, GameInventory, Wallets)
    activate DB
    DB-->>API: Return Entities & Aggregate Roots
    deactivate DB

    note over API: Domain Invariant Check:<br/>Ensure GameStatus == PendingTransaction
    note over API: Domain Invariant Check:<br/>Ensure Proposer's Wallet has enough Balance

    API->>API: Execute transaction.Approve()
    API->>API: Execute proposerWallet.Debit(rentalPrice)
    note over API: Funds are now held in Escrow by the Transaction Aggregate

    API->>API: Execute gameInventory.MarkAsBorrowed(dueDate)

    API->>DB: Persist Changes (Transaction Status, Wallet Balance, Game Status)
    activate DB
    DB-->>API: Confirm SaveChangesAsync()
    deactivate DB
    
    API->>DB: Commit Transaction
    
    note over API: Process Telemetry:<br/>Calculate execution time for X-Response-Time-Ms
    
    API-->>PWA: 200 OK<br/>{ "status": "ActiveRental", "digitalKey": "EncryptedData..." }<br/>[Custom Header -> X-Response-Time-Ms: 14]
    deactivate API
    
    activate PWA
    note over PWA: Interceptor reads X-Response-Time-Ms<br/>Updates Performance Metrics UI
    PWA-->>Receiver: Renders Success Screen & reveals Digital Key/Credentials
    deactivate PWA
```

# Wishlist Matchmaking and Telemetry Sequence Diagram (Query/Read Flow)

This diagram illustrates the high-performance read path within **GamesTradeIn**. It highlights how the React PWA background workers interact with .NET Minimal APIs using Dapper for ultra-fast SQL queries, and how client-server telemetry headers are captured to monitor latency.

```mermaid
sequenceDiagram
    autonumber
    actor User as Authenticated Player
    participant SW as PWA Service Worker (Background)
    participant PWA as React UI (Frontend)
    participant API as .NET Minimal API (Backend)
    participant DB as PostgreSQL (Read Replica/Store)

    loop Every 30 Seconds (Smart Polling)
        SW->>API: GET /api/users/me/wishlist/matches<br/>[If-None-Match: "W/v2-hash"]
        activate API
        
        note over API: High-Performance Read Path:<br/>Bypasses EF Core Change Tracker
        
        API->>DB: Execute Raw/Dapper Query (SQL JOIN)<br/>WHERE wishlist.title = inventory.title
        activate DB
        DB-->>API: Return DTO List
        deactivate DB

        note over API: Computes X-Response-Time-Ms header
        
        alt Data hasn't changed (Cache Hit)
            API-->>SW: 304 Not Modified<br/>[X-Response-Time-Ms: 2]
        else New Matches Found (Cache Miss)
            API-->>SW: 200 OK + JSON Payload<br/>[X-Response-Time-Ms: 8]
            SW->>PWA: Broadcast: "NEW_MATCHES_FOUND"
            PWA->>User: Triggers Toast Notification / UI Update
        end
        deactivate API
    end
```