using lab5_lib;
using System.Collections.ObjectModel;
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

namespace lab5_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Elevator elevator;
        private Dictionary<Type, Action> StateToText;
        public ObservableCollection<string> Logs { get; set; } = new();
        public int Level { get; set; } = 1;
        public int Weight { get; set; } = 50;

        public MainWindow()
        {
            InitializeComponent();

            StateToText = new()
            {
                { typeof(Idle), () => {StateTB.Text = "Ожидание"; StateTB.Foreground = Brushes.Green; } },
                { typeof(Movement), async () => {StateTB.Text ="Движение"; StateTB.Foreground = Brushes.Blue; await Task.Delay(1000); Update(); }},
                { typeof(NoPower), () => {StateTB.Text = "Нет притания"; StateTB.Foreground = Brushes.Yellow; }},
                { typeof(Overloaded), () => {StateTB.Text = "Перегружен"; StateTB.Foreground = Brushes.Orange; }},
                { typeof(Crash), () => {StateTB.Text = "Сломан"; StateTB.Foreground = Brushes.Red; }},
            };

            elevator = new Elevator(250, 0.2d);
            Update();

            DataContext = this;
        }

        private void Update()
        {
            StateToText[elevator.State.GetType()]();
            LevelTB.Text = elevator.CurrentLevel.ToString();
            WeightTB.Text = elevator.MaxWeight.ToString("0 кг");
            ChanceTB.Text = elevator.BlackoutChance.ToString("P2");
        }

        private void CallClick(object sender, RoutedEventArgs e)
        {
            Logs.Add(elevator.CallTo(Level));
            Update();
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            Logs.Add(elevator.Load(Weight));
            Update();
        }

        private void UnloadClick(object sender, RoutedEventArgs e)
        {
            Logs.Add(elevator.Unload());
            Update();
        }
        private void PowerOnClick(object sender, RoutedEventArgs e)
        {
            Logs.Add(elevator.PowerRestore());
            Update();
        }
    }
}