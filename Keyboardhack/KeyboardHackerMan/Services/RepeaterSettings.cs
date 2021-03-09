using GregsStack.InputSimulatorStandard.Native;
using System.Collections.Generic;
using System.Linq;

namespace KeyboardHackerMan.Services
{
    public class RepeaterSettings
    {
        public bool IsActive { get; set; } = false;
        public List<VirtualKeyCode> RepeaterSequence { get; set; } = new List<VirtualKeyCode>();
        public string ProcessName { get; set; }
        public bool StartRecording { get; set; } = false;

        public bool CanStart()
        {
            return IsActive && !string.IsNullOrWhiteSpace(ProcessName) && RepeaterSequence.Any();
        }
    }
}
