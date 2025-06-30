// Version: 0.1.0.0
using Microsoft.Win32;
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
using ThmdPlayer.Core.medias;

namespace ThmdPlayer.windows
{
    /// <summary>
    /// Logika interakcji dla klasy EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private Uri _videoUri;
        private Core.medias.MediaEditor _mediaEditor;

        public EditorWindow()
        {
            InitializeComponent();
        }

        public EditorWindow(Uri uri)
        {
            InitializeComponent();

            var media = new Core.medias.Media(uri.LocalPath);
            _videoUri = media.Uri;
            _mediaEditor = new Core.medias.MediaEditor(media.Uri);
            _inputTextBox.Text = media.Uri.LocalPath;
            _outputTextBox.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(_videoUri.LocalPath), $"{System.IO.Path.GetFileNameWithoutExtension(media.Uri.LocalPath)}_cut{System.IO.Path.GetExtension(media.Uri.LocalPath)}");
            _startTimeTextBox.Text = "00:00:00";
            _endTimeTextBox.Text = media.Duration.ToString(); // Default end time set to 10 seconds
        }

        public EditorWindow(Media media)
        {
            InitializeComponent();
            _videoUri = media.Uri;
            _mediaEditor = new Core.medias.MediaEditor(media.Uri);
            _inputTextBox.Text = media.Uri.LocalPath;
            _outputTextBox.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(media.Uri.LocalPath), $"{System.IO.Path.GetFileNameWithoutExtension(_videoUri.LocalPath)}_cut{System.IO.Path.GetExtension(media.Uri.LocalPath)}");
            _startTimeTextBox.Text = "00:00:00";
            _endTimeTextBox.Text = media.Duration.ToString(); // Default end time set to 10 seconds
        }

        private void _btnCut_Click(object sender, RoutedEventArgs e)
        {
            var cut = _mediaEditor.CutVideo(_outputTextBox.Text, TimeSpan.Parse(_startTimeTextBox.Text), TimeSpan.Parse(_endTimeTextBox.Text));
            if (cut)
            {
                MessageBox.Show("Video cut successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Logger.Log.Log(LogLevel.Info, new string[] { "File", "Console" }, $"Video cut successfully: {_outputTextBox.Text}");
            }
            else
            {
                MessageBox.Show("Failed to cut video.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Log.Log(LogLevel.Error, new string[] { "File", "Console" }, $"Failed to cut video: {_outputTextBox.Text}");
            }
        }

        private void _btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                _videoUri = new Uri(ofd.FileName);
                _mediaEditor = new Core.medias.MediaEditor(_videoUri);
                _inputTextBox.Text = _videoUri.LocalPath;
            }
        }
    }
}
