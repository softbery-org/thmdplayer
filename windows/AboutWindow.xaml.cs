// Version: 1.0.0.100
using System.Windows;
using System.Windows.Media;
using ThmdPlayer.Core.images.svg;

namespace ThmdPlayer.windows
{
    /// <summary>
    /// Logika interakcji dla klasy AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            _image.Source = SvgImageConverter.ConvertSvgToImageSource("Resources/icons/svg/ad.svg", Brushes.Red, Brushes.Purple);
        }
    }
}
