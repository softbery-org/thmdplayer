// Version: 1.0.0.136
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
        private IPlayer _player = new Player();

        public IPlayer Player { get => _player; set => _player = value; }

        public SettingsWindow()
        {
            InitializeComponent();

            _fontsListComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
            _fontsListComboBox.SelectedItem = App.Config.SubtitleConfig.FontFamily;
            _fontSizeTextBox.Text = App.Config.SubtitleConfig.FontSize.ToString();//_player.SubtitleControl.FontSize.ToString();
            _shadowSubtitleCheckBox.IsChecked = App.Config.SubtitleConfig.Shadow.Visible;
        }

        public SettingsWindow(IPlayer player) : this()
        {
            _player = player;

            _player.SubtitleControl.FontSize = App.Config.SubtitleConfig.FontSize;
            _player.SubtitleControl.FontFamily = App.Config.SubtitleConfig.FontFamily;
            _player.SubtitleControl.ShowShadow = App.Config.SubtitleConfig.Shadow.Visible;
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close() ;
        }

        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            var p = _player as Player;
            p.SubtitleControl.FontSize = double.Parse(_fontSizeTextBox.Text);
            p.SubtitleControl.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
            p.SubtitleControl.ShowShadow = _shadowSubtitleCheckBox.IsChecked == true;

            // Config
            App.Config.SubtitleConfig.FontSize = double.Parse(_fontSizeTextBox.Text);
            App.Config.SubtitleConfig.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
            App.Config.SubtitleConfig.Shadow.Visible = _shadowSubtitleCheckBox.IsChecked == true;
            App.Config.SaveToFile("config.json");

            p.SubtitleControl.UpdateSubtitleStyle();
        }

        private void _fontsListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _test.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;

            var p = _player as Player;

            p.SubtitleControl.FontSize = double.Parse(_fontSizeTextBox.Text);
            p.SubtitleControl.FontFamily = (FontFamily)_fontsListComboBox.SelectedItem;
            p.SubtitleControl.ShowShadow = _shadowSubtitleCheckBox.IsChecked == true;

            p.SubtitleControl.UpdateSubtitleStyle();
        }

        private void ColorPicker_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _shadowColor.Text = _colorPicker.Selected.Hex();
        }
    }
}
