using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using System;

namespace KeyboardHackerMan
{
    public class KeyboardService : IKeyboardService
    {
        private readonly IInputSimulator _simulator;

        public KeyboardService(IInputSimulator simulator)
        {
            _simulator = simulator;
        }

        public void Press(VirtualKeyCode command, int duration)
        {
            _simulator.Keyboard.KeyDown(command).Sleep(duration).KeyUp(command);
        }

        public void Sleep(int duration = 100)
        {
            _simulator.Keyboard.Sleep(duration);
        }
    }

    public interface IKeyboardService
    {
        void Press(VirtualKeyCode command, int duration = 100);

        void Sleep(int duration = 100);
    }
}
