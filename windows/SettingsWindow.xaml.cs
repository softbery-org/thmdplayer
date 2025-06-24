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

                _player.SubtitleControl.FontSize = App.Config.SubtitleConfig.FontSize;
                _player.SubtitleControl.FontFamily = App.Config.SubtitleConfig.FontFamily;
                _player.SubtitleControl.Shadow = App.Config.SubtitleConfig.ShadowSubtitle;
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
                _player.SubtitleControl.FontSize = double.Parse(_fontSizeTextBox.Text);
                _player.SubtitleControl.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
                _player.SubtitleControl.Shadow = _shadowSubtitleCheckBox.IsChecked == true;

                _player.SubtitleControl.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
                /*_player.SubtitleControl.UpdateSubtitleStyle();
                _player.SubtitleControl.SetSubtitleFontFamily((FontFamily)_fontsListComboBox.SelectedItem);*/
            }

            // Config
            App.Config.SubtitleConfig.FontSize = double.Parse(_fontSizeTextBox.Text);
            App.Config.SubtitleConfig.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
            App.Config.SubtitleConfig.ShadowSubtitle = _shadowSubtitleCheckBox.IsChecked == true;
            App.Config.SaveToFile("config.json");

            this.Close();
        }

        private void _fontsListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _test.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
        }
    }
}
