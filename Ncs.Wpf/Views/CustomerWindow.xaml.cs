using System.Windows;
using System.Windows.Threading;

namespace Ncs.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private DispatcherTimer _timer;
        public CustomerWindow()
        {
            InitializeComponent();
            StartClock();
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
    }
}
