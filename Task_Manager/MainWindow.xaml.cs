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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Вынести в отдельную функцию, когда будет куча изменений персонолизации
            ITheme theme = _paletteHelper.GetTheme();
            if (!Settings.Default.ThemeToggle)
            {
                theme.SetBaseTheme(Theme.Light);
                themeToggle.IsChecked = false;
            }
            else
            {
                themeToggle.IsChecked = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            //Сохраняет актуальный GreedSplitter
            _paletteHelper.SetTheme(theme);
            GridLength gridLength = new GridLength(Settings.Default.GridSplitter);
            Spliter.Width = gridLength;
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {

            Settings.Default.GridSplitter = Spliter.ActualWidth;
            Settings.Default.Save();
            Application.Current.MainWindow.Close();
        }

        private void Turn_Button(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Expand_Button(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new();

        private void Toggle_Theme(object sender, RoutedEventArgs e)
        {
            ITheme theme = _paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
                Settings.Default.ThemeToggle = false;
                Settings.Default.Save();
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
                Settings.Default.ThemeToggle = true;
                Settings.Default.Save();
            }

            _paletteHelper.SetTheme(theme);
        }

    }
}
