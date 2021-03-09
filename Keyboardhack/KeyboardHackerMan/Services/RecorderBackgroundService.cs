using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;


namespace KeyboardHackerMan.Services
{
    public class RecorderBackgroundService : BackgroundService
    {
        private readonly RepeaterSettings _repeaterSettings;

        public RecorderBackgroundService(RepeaterSettings repeaterSettings)
        {
            _repeaterSettings = repeaterSettings;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_repeaterSettings.StartRecording)
                {

                }
            }

            return Task.CompletedTask;
        }
    }
}
