using LiveCharts.Defaults;
using LiveCharts;
using System.Windows;

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
    }
}
