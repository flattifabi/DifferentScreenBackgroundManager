using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImBack
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> backgrounds = new List<string>
        {
            @"C:\Users\fabif\OneDrive\Desktop\Köfler\Resources\asdf.jpg",
            @"C:\Users\fabif\OneDrive\Desktop\Köfler\Resources\asd1.jpg",
            @"C:\Users\fabif\OneDrive\Desktop\Köfler\Resources\asdf.jpg"
        };

        public MainWindow()
        {
            InitializeComponent();
            LoadBackgrounds();
            LoadScreens();
        }

        private void LoadBackgrounds()
        {
            foreach (string background in backgrounds)
            {
                comboBoxBackgrounds.Items.Add(background);
            }
        }

        private void ApplyBackgrounds()
        {
            Screen[] screens = Screen.AllScreens;

            if (screens.Length > backgrounds.Count)
            {
                System.Windows.MessageBox.Show("Es sind nicht genügend Hintergründe vorhanden.");
                return;
            }

            for (int i = 0; i < screens.Length; i++)
            {
                string selectedBackground = backgrounds[i];

                SetScreenBackground(screens[i], selectedBackground);
            }
        }

        private void SetScreenBackground(Screen screen, string backgroundPath)
        {
            string backgroundFullPath = System.IO.Path.GetFullPath(backgroundPath);

            bool result = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, backgroundFullPath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
            if (result)
            {
                System.Windows.MessageBox.Show($"Der Hintergrund für den Bildschirm {screen.DeviceName} wurde erfolgreich geändert.");
            }
            else
            {
                int error = Marshal.GetLastWin32Error();
                System.Windows.MessageBox.Show($"Fehler beim Ändern des Hintergrunds für den Bildschirm {screen.DeviceName}. Fehlercode: {error}");
            }
        }

        private const int SPI_SETDESKWALLPAPER = 0x0014;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            ApplyBackgrounds();
        }
        private void LoadScreens()
        {
            Screen[] screens = Screen.AllScreens;

            foreach (Screen screen in screens)
            {
                listBoxScreens.Items.Add(screen.DeviceName);
            }
        }
    }
}
