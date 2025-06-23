// Version: 1.0.0.118
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThmdPlayer.Core.configuration;
using ThmdPlayer.Core.helpers;
using ThmdPlayer.Core.Logs;

namespace ThmdPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<string> _categories = new List<string> { "Console", "File" };
        private static Core.Logs.AsyncLogger _logger { get; set; } = new Core.Logs.AsyncLogger();

        public static Config Config { get; set; } = Config.Instance;
        public static Core.Logs.AsyncLogger Logger { get => _logger; set => _logger = value; }

        private void StartLogs()
        {
            _logger = new AsyncLogger
            {
                MinLogLevel = Config.LogLevel,
                CategoryFilters =
                {
                    ["Console"] = true,
                    ["File"] = true
                },
            };

            _logger.AddSink(new CategoryFilterSink(
                new FileSink("Logs", "log", new TextFormatter()), new[] { "File" }));

            _logger.AddSink(new CategoryFilterSink(
                new ConsoleSink(formatter: new TextFormatter()), new[] { "Console" }));

            _logger.Log(Core.Logs.LogLevel.Info, "File", $"Save to log files");
            _logger.Log(Core.Logs.LogLevel.Info, "Console", "Console logs just started.");
            _logger.Log(Core.Logs.LogLevel.Info, "File", _logger.GetMetrics().ToString());
            _logger.Log(Core.Logs.LogLevel.Info, "Console", _logger.GetMetrics().ToString());
        }

        public static void AddLog(Core.Logs.LogLevel level, string message, string[] category = null, Exception exception = null)
        {
            if (category != null)
                _categories.AddRange(category);

            _logger.Log(level, _categories.ToArray(), message, exception);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var options = new List<HelpOption>
            {
            new HelpOption(
                flags: new List<string> { "--server", "-s" },
                description: "Włącza lub wyłącza tryb serwera",
                hasValue: true
            ),
            new HelpOption(
                flags: new List<string> { "--client", "-c" },
                description: "Włącza lub wyłącza tryb clienta",
                hasValue: true
            ),
            new HelpOption(
                flags: new List<string> { "--port", "-p" },
                description: "Określa port serwera",
                hasValue: true,
                valueType: "int"
            ),
            new HelpOption(
                flags: new List<string> { "-h", "--help" },
                description: "Wyświetla pomoc"
            )
            };

            var help = new HelpGenerator(
                "thmdplayer",
                "Przykładowa aplikacja z zaawansowanym systemem pomocy",
                options
            );

            help.CheckHelp(e.Args);

            // Przetwarzanie argumentów...
            Console.WriteLine("Główna logika aplikacji...");

            StartLogs();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
