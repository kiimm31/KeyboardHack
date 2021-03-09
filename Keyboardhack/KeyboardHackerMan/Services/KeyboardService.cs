using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;

namespace KeyboardHackerMan.Services
{
    public class KeyboardService : IKeyboardService
    {
        private readonly IInputSimulator _simulator;

        public KeyboardService(IInputSimulator simulator)
        {
            _simulator = simulator;
        }

        public void Press(VirtualKeyCode command, int duration = 100)
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
