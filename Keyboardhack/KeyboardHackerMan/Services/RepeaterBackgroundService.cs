using KeyboardHackerMan.Helper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KeyboardHackerMan.Services
{
    public class RepeaterBackgroundService : BackgroundService
    {
        private readonly RepeaterSettings _settings;
        private readonly ILogger<RepeaterBackgroundService> _logger;
        private readonly IKeyboardService _keyboard;

        public RepeaterBackgroundService(RepeaterSettings settings, ILogger<RepeaterBackgroundService> logger, IKeyboardService keyboard)
        {
            _settings = settings;
            _logger = logger;
            _keyboard = keyboard;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (_settings.CanStart())
                {
                    WindowsHelper.FocusWindow(_settings.ProcessName);

                    ExecuteCommands();
                }
            }

            return Task.CompletedTask;
        }

        private void ExecuteCommands()
        {
            foreach (var virtualKeyCode in _settings.RepeaterSequence)
            {
                _keyboard.Press(virtualKeyCode);
                _keyboard.Sleep();
            }
        }
    }
}
