using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bee_Colony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Colony? colony; // Colony object
        public ChartValues<ObservablePoint> ChartParticles { get; private set; } // Collection for output chart


        public MainWindow()
        {
            InitializeComponent();
            ChartParticles = new ChartValues<ObservablePoint>();
            DataContext = this;
        }

        /// <summary>
        /// It kills colony ;(
        /// </summary>
        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ChartParticles.Clear();
            colony = null;
        }

        /// <summary>
        /// More iterations
        /// </summary>
        private void StartClick(object sender, RoutedEventArgs e)
        {
            if (colony == null)
            {
                try
                {
                    CreateColony();
                }
                catch (Exception)
                {
                    return;
                }
            }
            if (!int.TryParse(IterationsInput.Text, out int iterationsCount))
            {
                MessageBox.Show("Incorrect iterations count", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            for (int i = 0; i < iterationsCount; i++)
            {
                colony?.Exploration();
            }

            DisplayScoutsPositions();
        }

        /// <summary>
        /// One iteration
        /// </summary>
        private void OneIterationClick(object sender, RoutedEventArgs e)
        {
            if (colony == null)
            {
                try
                {
                    CreateColony();
                }
                catch (Exception)
                {
                    return;
                }
            }
            colony?.Exploration();
            DisplayScoutsPositions();
        }

        /// <summary>
        /// Displays scout positions in output chart
        /// </summary>
        private void DisplayScoutsPositions()
        {
            foreach (Scout scout in colony.Scouts)
            {
                ChartParticles.Add(new ObservablePoint(scout.Position[0], scout.Position[1]));

            }
        }

        /// <summary>
        /// Accepts only digits, minus, dot and semi
        /// </summary>
        private void CheckCorrect_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _) &&
                        !char.IsDigit(e.Text[0]) &&
                        e.Text[0] != '-' &&
                        e.Text[0] != '.' &&
                        e.Text[0] != ';';
        }

        /// <summary>
        /// Creates colony and checks input
        /// </summary>
        private void CreateColony()
        {
            List<double>? position = GetStartPosition();
            if (position == null)
            {
                MessageBox.Show("Incorrect coordinate input!\n" +
                        "Example:\n" +
                        "0;0 or 5.7;0.7", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception("Incorrect position");
            }
            if (!int.TryParse(SeachRadiusInput.Text, out int radius))
            {
                MessageBox.Show("Incorrect radius!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception("Incorrect radius");
            }
            if (!int.TryParse(ScoutsCountInput.Text, out int scoutsCount))
            {
                MessageBox.Show("Incorrect scouts count!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception("Incorrect scouts count");
            }
            if (!int.TryParse(AreasCountInput.Text, out int areasCount))
            {
                MessageBox.Show("Incorrect areas count", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception("Incorrect areas count");
            }

            colony = new Colony(position, radius, scoutsCount, areasCount, FunctionSelector.SelectedIndex);
        }

        /// <summary>
        /// Parse start position coordinates from input textbox
        /// </summary>
        /// <returns>List of statr position coordinates</returns>
        private List<double>? GetStartPosition()
        {
            string[] position = ColonyPositionInput.Text.Split(";");
            List<double> result = new List<double>();

            foreach (string coordinate in position)
            {
                if (double.TryParse(coordinate, out double parsedCoordinate))
                {
                    result.Add(parsedCoordinate);
                }
                else
                {
                    return null;
                }
            }
            return result;
        }

        /// <summary>
        /// Changes chart bounds
        /// </summary>
        private void Bounds_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (sender is TextBox _sender)
            {
                switch (_sender.Name)
                {
                    case "MaxX":
                        if (double.TryParse(_sender.Text, out double MaxX))
                        {
                            OutputChart.AxisX[0].MaxValue = MaxX;
                        }
                        break;
                    case "MaxY":
                        if (double.TryParse(_sender.Text, out double MaxY))
                        {
                            OutputChart.AxisY[0].MaxValue = MaxY;
                        }
                        break;
                    case "MinX":
                        if (double.TryParse(_sender.Text, out double MinX))
                        {
                            OutputChart.AxisX[0].MinValue = MinX;
                        }
                        break;
                    case "MinY":
                        if (double.TryParse(_sender.Text, out double MinY))
                        {
                            OutputChart.AxisY[0].MinValue = MinY;
                        }
                        break;
                    default:
                        e.Handled = true;
                        break;
                }
            }
        }
    }
}
