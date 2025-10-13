# Implementation Flow Diagram

## Current vs Target Architecture

### Current Flow (Not Working)
```
User Request → KeyboardOrchestratorBackgroundService → KeyboardService → InputSimulatorStandard → Windows Message Queue → MapleStory (BLOCKED)
```

### Target Flow (Working)
```
User Request → KeyboardOrchestratorBackgroundService → New KeyboardService → SendInput API → Direct Input → MapleStory (SUCCESS)
```

## Implementation Flow

```mermaid
flowchart TD
    A[User Triggers Space Key Test] --> B[KeyboardOrchestratorBackgroundService]
    B --> C{MapleStory Process Found?}
    C -->|No| D[Log Error & Exit]
    C -->|Yes| E[Focus MapleStory Window]
    E --> F[New KeyboardService.SendInput]
    F --> G[Windows API SendInput Call]
    G --> H{Input Successful?}
    H -->|Yes| I[Character Jumps - SUCCESS]
    H -->|No| J[Try PostMessage Fallback]
    J --> K{PostMessage Successful?}
    K -->|Yes| I
    K -->|No| L[Try SendMessage Fallback]
    L --> M{SendMessage Successful?}
    M -->|Yes| I
    M -->|No| N[Log All Methods Failed]
    
    style A fill:#e1f5fe
    style I fill:#c8e6c9
    style D fill:#ffcdd2
    style N fill:#ffcdd2
```

## Input Method Comparison

```mermaid
graph LR
    A[InputSimulatorStandard] --> B[Windows Message Queue]
    C[SendInput API] --> D[Direct Input]
    E[PostMessage] --> F[Window Message]
    G[SendMessage] --> H[Synchronous Message]
    
    B --> I[MapleStory - BLOCKED]
    D --> J[MapleStory - SUCCESS]
    F --> K[MapleStory - MAYBE]
    H --> L[MapleStory - MAYBE]
    
    style I fill:#ffcdd2
    style J fill:#c8e6c9
    style K fill:#fff3e0
    style L fill:#fff3e0
```

## Process Flow for Space Key Test

```mermaid
sequenceDiagram
    participant U as User
    participant K as KeyboardOrchestrator
    participant W as WindowsHelper
    participant S as KeyboardService
    participant API as Windows API
    participant M as MapleStory
    
    U->>K: Trigger Space Key Test
    K->>W: Find MapleStory Process
    W-->>K: Process Handle
    K->>W: Focus Window
    W-->>K: Focus Complete
    K->>S: Send Space Key
    S->>API: SendInput(SPACE)
    API->>M: Direct Input
    M-->>API: Input Received
    API-->>S: Success
    S-->>K: Success
    K-->>U: Character Jumps
```

