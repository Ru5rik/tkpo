using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ElevatorModel _elevatorModel;
        public MainWindow()
        {
            InitializeComponent();

            _elevatorModel = new(250, 0.3d);

            DataContext = _elevatorModel;
        }

        private void CallClick(object sender, RoutedEventArgs e)
        {
            _elevatorModel.CallTo(Convert.ToInt32(LevelTB.Text));
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            _elevatorModel.Load(Convert.ToInt32(WeightTB.Text));
        }

        private void UnloadClick(object sender, RoutedEventArgs e)
        {
            _elevatorModel.Unload();
        }
        private void PowerOnClick(object sender, RoutedEventArgs e)
        {
            _elevatorModel.PowerRestore();
        }
    }
}