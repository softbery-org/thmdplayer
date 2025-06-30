// Version: 0.1.0.0
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using NAudio.Wave;
using ThmdPlayer.Core.controls;
namespace ThmdPlayer.windows
{
    /// <summary>
    /// Logika interakcji dla klasy SpectrumWindow.xaml
    /// </summary>
    public partial class SpectrumWindow : Window
    {
        public SpectrumWindow()
        {
            InitializeComponent();
            ThmdPlayer.Core.controls.AudioSpectrumControl spectrumControl = new ThmdPlayer.Core.controls.AudioSpectrumControl();
            this.Content = spectrumControl;
        }
    }
}
