using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;

namespace Swarm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChartValues<ObservablePoint> ChartParticles = new ChartValues<ObservablePoint>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Main function
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInputs()) return;

            int FunctionID = FunctionSelector.SelectedIndex;
            int DimensionsCount = int.Parse(DimensionsInput.Text);

            Swarm swarm = new Swarm(int.Parse(ParticlesCountInput.Text),
                                    DimensionsCount,
                                    double.Parse(FPInput.Text),
                                    double.Parse(FGInput.Text),
                                    FunctionID,
                                    new List<int>() { int.Parse(MaxX.Text), int.Parse(MaxY.Text) },
                                    new List<int>() { int.Parse(MinX.Text), int.Parse(MinY.Text) });

            for (int i = 0; i < int.Parse(IterationsCountInput.Text); i++)
            {
                swarm.NextIteration();
            }

            if (DimensionsCount==2)
            {
                ChartParticles.Clear();
                foreach (Particle particle in swarm.Particles)
                {
                    ChartParticles.Add(new ObservablePoint(particle.Position[0], particle.Position[1]));
                }
            }

            int counter = 0;
            foreach (List<double> position in swarm.GetResult())
            {
                ResultOutput.Text += "Point #" + counter + "(";
                foreach (double coordinate in position)
                {
                    ResultOutput.Text += coordinate + ", ";
                }
                switch (FunctionID)
                {
                    case 0:
                        ResultOutput.Text += "), Value: " + Functions.GetSphereValue(position) + "\n";
                        break;
                    case 1:
                        ResultOutput.Text += "), Value: " + Functions.GetRastriginValue(position) + "\n";
                        break;
                    case 2:
                        ResultOutput.Text += "), Value: " + Functions.GetSchwefelValue(position) + "\n";
                        break;
                }
            }
        }

        /// <summary>
        /// Checks is input correct
        /// </summary>
        /// <returns>True = input correct. False = input incorrect</returns>
        private bool CheckInputs()
        {
            string DemensionsError = "Dimensions count must be a positive integer!\n";
            string FPcoefError = "FP coeficient must be positive double!\n";
            string FGcoefError = "FG coeficient must be positive double!\n";
            string FP_FGSumError = "FP + FG must be more than 4\n";
            string ParticlesCountError = "Particles count must be a positive integer!\n";
            string IterationsCountError = "Iterations count should must be a positive integer!\n";
            string ErrorMessage = "";

            if (!uint.TryParse(DimensionsInput.Text, out _))
            {
                ErrorMessage += DemensionsError;
                DimensionsInput.BorderBrush = Brushes.Red;
            }
            if (!double.TryParse(FPInput.Text, out double FP) && FP < 0)
            {
                ErrorMessage += FPcoefError;
                FPInput.BorderBrush = Brushes.Red;
            }
            if (!double.TryParse(FGInput.Text, out double FG) && FG < 0)
            {
                ErrorMessage += FGcoefError;
                FGInput.BorderBrush = Brushes.Red;
            }
            if (FP + FG < 4)
            {
                ErrorMessage += FP_FGSumError;
                FPInput.BorderBrush = Brushes.Red;
                FGInput.BorderBrush = Brushes.Red;
            }
            if (!uint.TryParse(ParticlesCountInput.Text, out _))
            {
                ErrorMessage += ParticlesCountError;
                ParticlesCountInput.BorderBrush = Brushes.Red;
            }
            if (!uint.TryParse(IterationsCountInput.Text, out _))
            {
                ErrorMessage += IterationsCountError;
                IterationsCountInput.BorderBrush = Brushes.Red;
            }

            if (ErrorMessage.Length != 0)
            {
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                DimensionsInput.BorderBrush = Brushes.Gray;
                FPInput.BorderBrush = Brushes.Gray;
                FGInput.BorderBrush = Brushes.Gray;
                ParticlesCountInput.BorderBrush = Brushes.Gray;
                IterationsCountInput.BorderBrush = Brushes.Gray;
                return true;
            }
        }

        private void CheckCorrect_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _) && !char.IsDigit(e.Text[0]) && e.Text[0] != ',' && e.Text[0] != '-';
        }
    }
}
