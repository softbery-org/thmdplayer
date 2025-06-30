// Version: 1.0.0.126
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThmdPlayer.Core;
using ThmdPlayer.Core.configuration;
using ThmdPlayer.Core.helpers;
using ThmdPlayer.Core.logs;

namespace ThmdPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<string> _categories = new List<string> { "Console", "File" };
       

        public static Config Config { get; set; } = Config.Instance;

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
            Logger.Log.Log(LogLevel.Info, new string[] {"File", "Console" }, "Główna logika aplikacji...");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
