using Ncs.Wpf.Views;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Ncs.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ThemeSettingFile = "theme.config"; // Save user preference
        private DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            LoadSavedTheme();
            StartClock();
        }

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSelector.SelectedItem is ComboBoxItem selectedTheme)
            {
                string themeName = selectedTheme.Content.ToString();
                ApplyTheme(themeName);
                SaveThemeSetting(themeName);
            }
        }

        private void ApplyTheme(string themeName)
        {
            ResourceDictionary theme = new ResourceDictionary();

            if (themeName == "Dark Theme")
                theme.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
            else if (themeName == "Red Theme")
                theme.Source = new Uri("Themes/RedTheme.xaml", UriKind.Relative);
            else
                theme.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);

            // Apply the selected theme
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(theme);
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Themes/ThemeStyle.xaml", UriKind.Relative) });
        }

        private void SaveThemeSetting(string themeName)
        {
            File.WriteAllText(ThemeSettingFile, themeName);
        }

        private void LoadSavedTheme()
        {
            if (File.Exists(ThemeSettingFile))
            {
                string savedTheme = File.ReadAllText(ThemeSettingFile);
                if (savedTheme == "Dark Theme")
                {
                    ThemeSelector.SelectedIndex = 1;
                }
                else if (savedTheme == "Red Theme")
                {
                    ThemeSelector.SelectedIndex = 2;
                }
                else
                {
                    ThemeSelector.SelectedIndex = 0;
                }
                ApplyTheme(savedTheme);
            }
        }
        private void StartClock()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += UpdateDateTime;
            _timer.Start();
        }

        private void UpdateDateTime(object sender, EventArgs e)
        {
            DateTimeText.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy - HH:mm:ss");
        }

        private void ImageCustomer_Click(object sender, MouseButtonEventArgs e)
        {
            var customerPage = new CustomerWindow
            {
                WindowState = WindowState.Maximized
            };
            customerPage.Show();
        }

        private void ImageCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            ApplyMouseHoverEffect(ImageCustomer, scale: 1.1, Cursors.Hand);
        }

        private void ImageCustomer_MouseLeave(object sender, MouseEventArgs e)
        {
            ApplyMouseHoverEffect(ImageCustomer, scale: 1.0, Cursors.Arrow);
        }

        private void ApplyMouseHoverEffect(UIElement element, double scale, Cursor cursor)
        {
            if (element is FrameworkElement fe)
            {
                fe.RenderTransform = new ScaleTransform(scale, scale);
                fe.RenderTransformOrigin = new Point(0.5, 0.5);
                fe.Cursor = cursor;
            }
        }

        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            var adminPage = new AdminWindow
            {
                WindowState = WindowState.Maximized
            };
            adminPage.Show();
        }
    }

}