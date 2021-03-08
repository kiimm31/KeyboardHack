using GregsStack.InputSimulatorStandard.Native;
using KeyboardHackerMan.Helper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeyboardHackerMan.Services
{
    public class KeyboardOrchestratorBackgroundService : BackgroundService
    {
        private readonly IKeyboardService _keyboardService;
        private readonly ILogger<KeyboardOrchestratorBackgroundService> _logger;
        private readonly Process _processor;

        private static int _repeatTillUsePot = 500;

        private int _repeated = 0;

        private static List<VirtualKeyCode> _attackKeyCommands = new List<VirtualKeyCode>()
        {
            VirtualKeyCode.LEFT,
            VirtualKeyCode.VK_X,
            VirtualKeyCode.RIGHT,
            VirtualKeyCode.VK_X
        };

        private static List<VirtualKeyCode> _healKeyCommands = new List<VirtualKeyCode>()
        {
           VirtualKeyCode.DELETE,
           VirtualKeyCode.DELETE,
           VirtualKeyCode.DELETE,
           VirtualKeyCode.END,
           VirtualKeyCode.END
        };

        private string _processName = "Notepad";

        public KeyboardOrchestratorBackgroundService(IKeyboardService keyboardService, ILogger<KeyboardOrchestratorBackgroundService> logger)
        {
            _keyboardService = keyboardService;
            _logger = logger;
            _logger.LogInformation("Starting...");
            var process = Process.GetProcessesByName(_processName);

            if (process.Any())
            {
                _logger.LogInformation($"Found Processor... {_processName}");
                _processor = process.First();
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (IsProcessorRunning())
                {
                    ExecuteCommands(_attackKeyCommands);
                    _repeated++;
                }

                if (NeedToUsePotion())
                {
                    _logger.LogDebug("Healing...");
                    _repeated = 0;
                    ExecuteCommands(_healKeyCommands);
                }
            }

            return Task.CompletedTask;
        }

        private bool NeedToUsePotion()
        {
            return _repeated == _repeatTillUsePot;
        }

        private void FocusWindow()
        {
            WindowsHelper.FocusWindow(_processor);
        }

        private void ExecuteCommands(List<VirtualKeyCode> keyCommands)
        {
            FocusWindow();
            foreach (var keyCommand in keyCommands)
            {
                _keyboardService.Press(keyCommand);
                _keyboardService.Sleep();
            }
        }

        private bool IsProcessorRunning()
        {
            return _processor != null && _processor.Responding && _processor.HasExited == false;
        }
    }
}
