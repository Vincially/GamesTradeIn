# Transaction State Machine (Sale, Trade, and Rental)

This state machine diagram illustrates the dynamic lifecycle of a transaction within **GamesTradeIn**. It maps how transaction statuses change based on user actions and system triggers, highlighting financial and inventory operations.

```mermaid
stateDiagram-v2
    [*] --> Pending : Proposal Created by Proposer
    
    note right of Pending
        The digital game status transitions to 'PendingTransaction'
        and becomes locked in the owner's inventory.
    end note

    Pending --> Rejected : Receiver Rejects Proposal
    Pending --> Canceled : Proposer Cancels Proposal
    Pending --> Approved : Receiver Accepts Proposal

    state Approved {
        [*] --> ProcessingPayment : If Type is Sale or Paid Rental
        [*] --> Released : If Type is Trade or Free Rental
        
        ProcessingPayment --> BalanceDebited : Validates and Debits Proposer's Wallet
        ProcessingPayment --> PaymentFailed : Insufficient Funds
    }

    PaymentFailed --> Canceled : System cancels due to lack of funds
    BalanceDebited --> Released : Transaction Authorized

    state Released {
        [*] --> AccessGranted : API reveals DigitalKey/Credentials on the PWA
    }

    Released --> Completed : If Type is Sale or Trade (End of Flow)
    Released --> ActiveRental : If Type is Rental

    state ActiveRental {
        [*] --> GameInUse : Game status transitions to 'Borrowed'
    }

    ActiveRental --> Returned : Rental period expires OR user returns access
    
    note right of Returned
        The game status transitions back to 'Available'.
        If it was a paid rental, the escrowed amount
        is finally credited to the game owner's Wallet.
    end note

    Returned --> [*]
    Completed --> [*]
    Rejected --> [*]
    Canceled --> [*]
```