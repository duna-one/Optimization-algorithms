using LiveCharts.Defaults;
using LiveCharts;
using System.Windows;
using System.Windows.Input;

namespace Bee_Colony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ChartValues<ObservablePoint> ChartParticles { get; private set; } = new ChartValues<ObservablePoint>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {

        }

        private void StartClick(object sender, RoutedEventArgs e)
        {

        }

        private void OneIterationClick(object sender, RoutedEventArgs e)
        {

        }

        private void CheckCorrect_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _) && !char.IsDigit(e.Text[0]) && e.Text[0] != ',' && e.Text[0] != '-';
        }
    }
}
