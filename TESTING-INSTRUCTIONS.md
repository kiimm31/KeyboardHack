# Quick Testing Guide - MapleStory Space Key Test

## ✅ Phase 1 Implementation Complete!

The application has been successfully implemented and builds without errors. It's now ready for testing with MapleStory.

## What Was Changed

### Core Changes
1. **Replaced InputSimulator** with Windows SendInput API
2. **Updated target process** from "Notepad" to "MapleStory"
3. **Simplified to space key test** - sends space every 2 seconds
4. **Added comprehensive logging** for debugging

### Technical Implementation
- Created `WindowsApiHelper.cs` with SendInput P/Invoke
- Updated `KeyboardService.cs` to use Windows API
- Simplified `KeyboardOrchestratorBackgroundService.cs`
- Removed `GregsStack.InputSimulatorStandard` dependency

## How to Test (5 Minutes)

### Step 1: Launch MapleStory
```
1. Start MapleStory game client
2. Log into a character
3. Position in a safe area (no monsters)
4. Keep game window visible
```

### Step 2: Run the Application
Open PowerShell in the project folder:
```powershell
cd Keyboardhack\KeyboardHackerMan
dotnet run
```

### Step 3: Observe Results
Watch for these console messages:
```
✅ "Found process: MapleStory"
✅ "Sending space key to MapleStory..."
✅ "Space key sent successfully - character should jump!"
```

And in the game:
```
✅ Character should jump every 2 seconds
```

## What Success Looks Like

### Console Output
```
info: Starting KeyboardOrchestratorBackgroundService...
info: Looking for process: MapleStory
info: Found process: MapleStory
info: ExecuteAsync started
info: Sending space key to MapleStory...
info: Space key sent successfully - character should jump!
```

### In-Game Behavior
- Character jumps every 2 seconds
- No interruption to gameplay
- Smooth, consistent jumps

## Troubleshooting

### ❌ "Process not found: MapleStory"

**Problem**: Application can't find MapleStory process

**Solutions**:
1. Make sure MapleStory is actually running
2. Check Task Manager → Details tab for exact process name
3. If process name is different (e.g., "MapleStory2" or "maplestory.exe"), update:
   
   Open: `Keyboardhack\KeyboardHackerMan\Services\KeyboardOrchestratorBackgroundService.cs`
   
   Line 17: Change `"MapleStory"` to the correct name
   ```csharp
   private string _processName = "MapleStory"; // Change this
   ```

### ❌ Key Sent But Character Doesn't Jump

**Problem**: Logs show success but no jump in game

**Possible Causes**:
1. Anti-cheat blocking simulated input
2. Game not in focus
3. Character is in a state that prevents jumping

**Solutions**:
1. Try running PowerShell as Administrator:
   - Right-click PowerShell → "Run as Administrator"
   - Navigate to project folder
   - Run `dotnet run`

2. Manually click on MapleStory window to ensure focus

3. Make sure character is on the ground (can jump)

### ❌ Build Errors

If you get build errors:
```powershell
dotnet restore
dotnet clean
dotnet build
```

## Configuration Options

### Change Key Press Interval
File: `KeyboardOrchestratorBackgroundService.cs`, Line ~71
```csharp
await Task.Delay(2000, stoppingToken);  // 2000ms = 2 seconds
```
Change `2000` to desired milliseconds (e.g., `1000` for 1 second)

### Change Key Being Pressed
File: `KeyboardOrchestratorBackgroundService.cs`, Line ~66
```csharp
bool result = _keyboardService.Press(WindowsApiHelper.VirtualKeyCode.SPACE, 100);
```

Available keys:
- `VirtualKeyCode.SPACE` - Space bar
- `VirtualKeyCode.LEFT` - Left arrow
- `VirtualKeyCode.RIGHT` - Right arrow
- `VirtualKeyCode.UP` - Up arrow
- `VirtualKeyCode.DOWN` - Down arrow
- `VirtualKeyCode.VK_X` - X key

### Stop the Application
Press `Ctrl+C` in the PowerShell window

## Expected Results

| Test | Expected Result | Status |
|------|----------------|---------|
| Build succeeds | 0 errors | ✅ PASS |
| Application starts | No exceptions | ⏳ TEST |
| Finds MapleStory | Log shows "Found process" | ⏳ TEST |
| Sends space key | Log shows "successfully" | ⏳ TEST |
| Character jumps | Visual confirmation | ⏳ TEST |

## After Testing

### If It Works ✅
Report success with:
- Console log output
- Video/screenshot of character jumping
- Any observations about behavior

### If It Doesn't Work ❌
Provide:
- Full console log output
- Exact MapleStory version/region
- Process name from Task Manager
- Any error messages
- Screenshot of Task Manager Details tab

## Important Notes

⚠️ **Disclaimer**: This is for educational/testing purposes only. Using automation in online games may violate Terms of Service.

⚠️ **Anti-Cheat**: MapleStory may have anti-cheat that detects this. Test at your own risk.

✅ **Safety**: This tool only sends keyboard input - no memory manipulation or game modification.

## Next Steps

Once testing is complete:
1. **If successful**: Move to Phase 2 (alternative input methods)
2. **If unsuccessful**: Implement fallback mechanisms or adjust approach
3. **Document results**: Update PLAN-001 with findings

## Need Help?

Check these files for more info:
- `Keyboardhack/KeyboardHackerMan/README.md` - Full documentation
- `.plans/IMPLEMENTATION-COMPLETE.md` - Technical details
- `.plans/PLAN-001-Progress.md` - Implementation progress

---

**Quick Commands**:
```powershell
# Navigate to project
cd Keyboardhack\KeyboardHackerMan

# Run application
dotnet run

# Stop application
Ctrl+C

# Rebuild if needed
dotnet clean
dotnet build
```

**Status**: ✅ Ready for Testing  
**Estimated Test Time**: 5 minutes  
**Required**: MapleStory running
