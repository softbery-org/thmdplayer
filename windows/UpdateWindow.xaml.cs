// Version: 1.0.0.136
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThmdPlayer.Core;
using ThmdPlayer.Core.logs;

namespace ThmdPlayer.windows
{
    /// <summary>
    /// Logika interakcji dla klasy UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        private async void _buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var updater = new Core.Updates.Updater("http://thmdplayer.softbery.org/version.txt"))
            {
                updater.UpdateAvailable += (s, ex) => Logger.Log.Log(LogLevel.Warning, new string[] { "File", "Console" }, $"New version available: {ex.NewVersion}");
                updater.ProgressChanged += (s, ex) =>
                {
                    Console.WriteLine($"Progress: {ex.ProgressPercentage}%");
                    _progressBar.Value = ex.ProgressPercentage;
                    _textBoxLog.Text += $"Progress: {ex.ProgressPercentage}%\n";
                    _labelPrectange.Content = $"{ex.ProgressPercentage}%";
                };
                updater.UpdateCompleted += (s, ex) => Console.WriteLine("Download completed!");
                updater.UpdateFailed += (s, ex) => Console.WriteLine($"Error: {ex.Message}");

                if (await updater.CheckForUpdatesAsync())
                {
                    await updater.DownloadUpdateAsync("http://thmdplayer.softbery.org/update.rar");
                    //updater.ApplyUpdate();
                }
            }
        }
    }
}
