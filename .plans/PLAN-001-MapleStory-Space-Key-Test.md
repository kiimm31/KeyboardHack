# PLAN-001: MapleStory Space Key Test Implementation

## Overview
Create a proof-of-concept to send a single space keypress to MapleStory application to make the character jump, bypassing the current input method limitations.

## Problem Analysis
Based on code analysis and research:

### Current Issues
1. **Input Method Mismatch**: Current implementation uses `GregsStack.InputSimulatorStandard` which relies on Windows message queue
2. **Game Input Handling**: MapleStory likely uses DirectInput or Raw Input for better performance
3. **Process Targeting**: Hardcoded "Notepad" process name instead of "MapleStory"
4. **Input Validation**: Games often validate input source and timing

### Root Cause
MapleStory uses a different input handling mechanism than standard Windows applications, making the current `InputSimulatorStandard` library ineffective.

## Research Findings
- **SendInput API**: Most reliable for game automation (low-level input)
- **PostMessage/SendMessage**: Standard Windows message queue (current method)
- **DirectInput**: Game-specific input handling
- **Raw Input**: Hardware-level input processing

## Implementation Strategy

### Phase 1: Direct Windows API Implementation
Replace `GregsStack.InputSimulatorStandard` with direct Windows API calls using `SendInput` function.

### Phase 2: Process Targeting Fix
Update hardcoded process name from "Notepad" to "MapleStory".

### Phase 3: Input Method Testing
Implement multiple input methods to find the most effective approach.

## Technical Specifications

### Required Changes

#### 1. Update KeyboardService.cs
- Replace `GregsStack.InputSimulatorStandard` with direct Windows API
- Implement `SendInput` function via P/Invoke
- Add alternative input methods (PostMessage, SendMessage)

#### 2. Update KeyboardOrchestratorBackgroundService.cs
- Change hardcoded process name from "Notepad" to "MapleStory"
- Add process validation for MapleStory
- Implement single space key test functionality

#### 3. Add Windows API Declarations
- Add P/Invoke declarations for SendInput, PostMessage, SendMessage
- Add INPUT structure definitions
- Add Virtual Key Code mappings

#### 4. Create Test Endpoint
- Add simple test endpoint to trigger single space keypress
- Remove complex attack/heal sequences for POC

### Risk Analysis

#### High Risk
- **Anti-Cheat Detection**: MapleStory may detect automated input
- **Input Blocking**: Game may block non-physical input sources

#### Medium Risk
- **Process Access**: May need elevated permissions
- **Window Focus**: Game may have multiple windows/child windows

#### Low Risk
- **Performance Impact**: Minimal for single keypress
- **System Compatibility**: Standard Windows API calls

### Mitigation Strategies
1. **Multiple Input Methods**: Implement fallback mechanisms
2. **Timing Control**: Add configurable delays between attempts
3. **Process Validation**: Ensure correct process targeting
4. **Error Handling**: Graceful failure with detailed logging

## BDD Test Specifications

### Feature: MapleStory Space Key Input
```gherkin
Feature: Send space key to MapleStory
  As a user
  I want to send a space keypress to MapleStory
  So that my character jumps

  Scenario: Successful space key input
    Given MapleStory is running
    And the application is focused
    When I send a space keypress
    Then the character should jump
    And no errors should occur

  Scenario: Process not found
    Given MapleStory is not running
    When I attempt to send a space keypress
    Then an error should be logged
    And the operation should fail gracefully

  Scenario: Input method fallback
    Given MapleStory is running
    And the primary input method fails
    When I send a space keypress
    Then the fallback method should be used
    And the character should jump
```

## Implementation Checklist

### Phase 1: Core Implementation
- [x] Add Windows API P/Invoke declarations
- [x] Create new KeyboardService with SendInput implementation
- [x] Update KeyboardOrchestratorBackgroundService process name
- [x] Add single space key test functionality
- [x] Remove complex attack/heal sequences for POC

### Phase 2: Input Method Alternatives
- [ ] Implement PostMessage input method
- [ ] Implement SendMessage input method
- [ ] Add input method selection logic
- [ ] Create fallback mechanism

### Phase 3: Process Management
- [ ] Add MapleStory process detection
- [ ] Implement process validation
- [ ] Add window focus verification
- [ ] Create process monitoring

### Phase 4: Testing & Validation
- [ ] Create unit tests for input methods
- [ ] Add integration tests for process targeting
- [ ] Implement manual testing procedures
- [ ] Add logging and debugging capabilities

### Phase 5: Error Handling
- [ ] Add comprehensive error handling
- [ ] Implement retry mechanisms
- [ ] Add detailed logging
- [ ] Create user feedback system

## File Structure Changes

### New Files
- `KeyboardHackerMan/Helper/WindowsApiHelper.cs` - Windows API declarations
- `KeyboardHackerMan/Services/InputMethodService.cs` - Input method abstraction
- `KeyboardHackerMan/Models/InputResult.cs` - Input operation results

### Modified Files
- `KeyboardHackerMan/Services/KeyboardService.cs` - Replace with Windows API
- `KeyboardHackerMan/Services/KeyboardOrchestratorBackgroundService.cs` - Update process name
- `KeyboardHackerMan/Startup.cs` - Register new services

### Dependencies
- Remove: `GregsStack.InputSimulatorStandard`
- Add: Direct Windows API calls (no external dependencies)

## Success Criteria
1. **Primary**: Single space keypress makes MapleStory character jump
2. **Secondary**: No errors in application logs
3. **Tertiary**: Process correctly targets MapleStory application
4. **Validation**: Visual confirmation of character jumping

## Finalization Action
- [ ] **MANDATORY**: Test the implementation with MapleStory running and verify character jumps when space key is sent

## Timeline
- **Phase 1**: 2-3 hours (Core implementation)
- **Phase 2**: 1-2 hours (Alternative methods)
- **Phase 3**: 1 hour (Process management)
- **Phase 4**: 1 hour (Testing)
- **Phase 5**: 30 minutes (Error handling)

**Total Estimated Time**: 5.5-7.5 hours

