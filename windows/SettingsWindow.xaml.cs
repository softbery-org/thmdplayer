// Version: 1.0.0.135
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ThmdPlayer.Core.controls;
using ThmdPlayer.Core;
using ThmdPlayer.Core.Interfaces;
using System.Windows.Controls;

namespace ThmdPlayer.windows
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private IPlayer _player;

        public IPlayer Player { get => _player; set => _player = value; }

        public SettingsWindow()
        {
            InitializeComponent();

            _fontsListComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
            _fontsListComboBox.SelectedItem = App.Config.SubtitleConfig.FontFamily;
            _fontSizeTextBox.Text = App.Config.SubtitleConfig.FontSize.ToString();//_player.SubtitleControl.FontSize.ToString();
            _shadowSubtitleCheckBox.IsChecked = App.Config.SubtitleConfig.ShadowSubtitle;
        }

        public SettingsWindow(IPlayer player) : this()
        {
            InitializeComponent();

            if (_player != null)
            {
                _player = player;

                _player.SubtitleControl.SubtitleSize = App.Config.SubtitleConfig.FontSize;
                _player.SubtitleControl.FontFamily = App.Config.SubtitleConfig.FontFamily;
                _player.SubtitleControl.SubtitleShadow = App.Config.SubtitleConfig.ShadowSubtitle;
            }
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close() ;
        }

        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_player != null)
            {
                _player.SubtitleControl.SubtitleSize = double.Parse(_fontSizeTextBox.Text);
                _player.SubtitleControl.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
                _player.SubtitleControl.SubtitleShadow = _shadowSubtitleCheckBox.IsChecked == true;

                _player.SubtitleControl.UpdateSubtitleStyle();
            }

            // Config
            App.Config.SubtitleConfig.FontSize = double.Parse(_fontSizeTextBox.Text);
            App.Config.SubtitleConfig.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
            App.Config.SubtitleConfig.ShadowSubtitle = _shadowSubtitleCheckBox.IsChecked == true;
            App.Config.SaveToFile("config.json");
        }
    }
}
