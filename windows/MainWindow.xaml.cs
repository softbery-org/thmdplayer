// Version: 1.0.0.134
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ThmdPlayer.Core;
using ThmdPlayer.Core.Logs;

namespace ThmdPlayer.windows
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ThmdPlayer.Core.controls.Player _player;
        private CancellationToken cancellationTokenSource = new CancellationToken(false);
        private Rectangle _shortcutsTab;

        public MainWindow()
        {
            App.AddLog(LogLevel.Info, "Starting application.");

            _shortcutsTab = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(32, 0, 0, 0)), // Transparentny czarny ustawiono w aRGB: przeŸroczystoœæ: 32%; rgb: 0,0,0 czerñ
                Width = 200,
                Height = 200,
                Visibility = Visibility.Collapsed
            };

            _player = new ThmdPlayer.Core.controls.Player();

            ControlBarButtonClick();

            InitializeComponent();

            var c = this.Content as Grid;
            if (c != null)
            {
                c.Children.Add(_player);
            }

            _player.PlaylistView.SetGridColumns(new string[] { "Duration", "Name", "Position" });
            _player.PlaylistView.Add(new Core.medias.Media("F:/Filmy/A Minecraft Movie/Minecraft Film A Minecraft Movie 2025 - Filman cc - Filmy i Seri.mp4"), _player);
            _player.PlaylistView.Add(new Core.medias.Media("F:/Filmy/Dziki robot - The.Wild.Robot.2024.Pldub.Md.360P.Amzn.Web-Dl.H.265.Dd2.0-Fox.mkv.AVI", _player));
            _player.PlaylistView.Add(new Core.medias.Media("rtsp://admin:BHVZSL@192.168.88.243/h264_stream"), _player);
            
            //_player.Playlist.Add(new Core.medias.Media("https://luluvdo.com/e/036u92ask61z"), _player);

            _player.Play();

            this.Closing += OnClosing;
        }

        private void ControlBarButtonClick()
        {
            _player.ControlBar.BtnOptions.Click += (s, e) =>
            {
                var settingsWindow = new SettingsWindow(_player);
                settingsWindow.Owner = this;
                settingsWindow.ShowDialog();
                var grid = new Grid();
                grid.Background = new SolidColorBrush(Color.FromArgb(32, 0, 0, 0)); // Transparentny czarny ustawiono w aRGB: przeŸroczystoœæ: 32%; rgb: 0,0,0 czerñ
            };

            _player.ControlBar.BtnUpdate.Click += (s, e) =>
            {
                var updateWindow = new UpdateWindow();
                updateWindow.Owner = this;
                updateWindow.ShowDialog();
            };

            _player.ControlBar.BtnInfo.Click += (s, e) =>
            {
                var infoWindow = new AboutWindow();
                infoWindow.Owner = this;
                infoWindow.ShowDialog();
            };
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                App.Logger.Log(LogLevel.Info, new string[] { "File", "Console" }, $"Closing player.");

                cancellationTokenSource.ThrowIfCancellationRequested();
                try
                {
                    _player.Stop();
                    _player.Dispose();
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                Logger.Log.Log(LogLevel.Error, new string[] { "File", "Console" }, $"Error: {ex.Message}");
            }
            finally
            {
                Logger.Log.Log(LogLevel.Info, new string[] { "File", "Console" }, $"Player closed.");
                Logger.Log.Dispose();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (_player.FullScreen)
                    _player.FullScreen = false;
            }
            if (e.Key == Key.M)
            {
                if (_player.Mute)
                    _player.Mute = false;
                else
                    _player.Mute = true;
            }
            if (e.Key == Key.H)
            {
                if (_shortcutsTab.Visibility == Visibility.Visible)
                    _shortcutsTab.Visibility = Visibility.Collapsed;
                else
                    _shortcutsTab.Visibility = Visibility.Visible;
            }
            if ((e.Key == Key.N))
            {
                if (_player.SubtitleControl.Visibility == Visibility.Visible)
                {
                    _player.SubtitleControl.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _player.SubtitleControl.Visibility = Visibility.Visible;
                }
            }
            if (e.Key == Key.F)
            {
                if (_player.FullScreen)
                    _player.FullScreen = false;
                else
                    _player.FullScreen = true;
            }
            if (e.Key==Key.F1)
            {
                _player.Seek(TimeSpan.FromSeconds(60));
            }
            if (e.Key == Key.Up)
            {
                _player.Seek(TimeSpan.FromSeconds(60*5), Core.medias.SeekDirection.Forward);
            }
            if (e.Key == Key.Down)
            {
                _player.Seek(TimeSpan.FromSeconds(60*5), Core.medias.SeekDirection.Backward);
            }
            if (e.Key == Key.Right)
            {
                _player.Seek(TimeSpan.FromSeconds(5), Core.medias.SeekDirection.Forward);
            }
            if (e.Key == Key.Left)
            {
                _player.Seek(TimeSpan.FromSeconds(5), Core.medias.SeekDirection.Backward);
            }
        }
    }
}
