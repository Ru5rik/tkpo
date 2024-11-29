using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public interface IState
    {
        string CallTo(int level);
        string Load(int weight);
        string Unload();
        string PowerRestore();
    }
    public class Idle : IState
    {
        private ElevatorModel elevator;
        public Idle(ElevatorModel elevator)
        {
            this.elevator = elevator;
        }
        public override string ToString() => "Ожидание";
        public string CallTo(int level)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= elevator.BlackoutChance)
            {
                elevator.State = new NoPower(elevator);
                return "Произошло отключение электроэнергии";
            }
            elevator.State = new Movement(elevator, level);
            return $"Лифт вызван на этаж {level}";
        }

        public string Load(int weight)
        {
            if (weight >= elevator.MaxWeight)
            {
                elevator.State = new Overloaded(elevator);
                return "Лифт перегружен";
            }
            return $"Груз был загружен на {weight} кг";
        }
        public string Unload()
        {
            return "Лифт был разгружен";
        }
        public string PowerRestore()
        {
            return "Лифт в исправном состоянии";
        }
    }
    public class Movement : IState
    {
        private ElevatorModel elevator;
        private int destination;
        private int current;
        public Movement(ElevatorModel elevator, int destination)
        {
            this.elevator = elevator;
            this.destination = destination;
            current = 1;

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                elevator.CurrentLevel = destination;
                elevator.State = new Idle(elevator);
            });
        }
        public override string ToString() => "Движение";
        public string CallTo(int level)
        {
            return "Лифт уже вызван";
        }

        public string Load(int weight)
        {
            return "Невозможно загрузить груз во время движения";
        }
        public string Unload()
        {
            return "Невозможно разгрузить во время движения";
        }
        public string PowerRestore()
        {
            return "Лифт в исправном состоянии";
        }
    }
    public class Overloaded : IState
    {
        private ElevatorModel elevator;
        public Overloaded(ElevatorModel elevator)
        {
            this.elevator = elevator;
        }
        public override string ToString() => "Перегружен";
        public string CallTo(int level)
        {
            elevator.State = new Crash(elevator);
            return "Перегруженный лифт сломался при вызове";
        }

        public string Load(int weight)
        {
            return "Невозможно загрузить. Лифт перегружен.";
        }

        public string Unload()
        {
            elevator.State = new Idle(elevator);
            return "Лифт был разгружен";
        }
        public string PowerRestore()
        {
            return "Лифт в исправном состоянии";
        }
    }
    public class NoPower : IState
    {
        private ElevatorModel elevator;
        public NoPower(ElevatorModel elevator)
        {
            this.elevator = elevator;
        }
        public override string ToString() => "Нет притания";
        public string CallTo(int level)
        {
            return "Нет питания, лифт не может быть вызван";
        }

        public string Load(int weight)
        {
            return "Нет питания, лифт не может быть загружен";
        }

        public string Unload()
        {
            return "Нет питания, лифт не может быть разгружен";

        }
        public string PowerRestore()
        {
            elevator.State = new Idle(elevator);
            return "Питание восстановлено. Лифт снова работает";
        }
    }
    public class Crash : IState
    {
        private ElevatorModel elevator;
        public Crash(ElevatorModel elevator)
        {
            this.elevator = elevator;
        }
        public override string ToString() => "Сломан";
        public string CallTo(int level)
        {
            return "Лифт сломан, не может быть вызван";

        }
        public string Load(int weight)
        {
            return "Лифт сломан, не может быть загружен";
        }
        public string Unload()
        {
            return "Лифт сломан, не может быть разгружен";
        }
        public string PowerRestore()
        {
            return "Лифт сломан, необходимо исправить повреждения";
        }
    }
    public class ElevatorModel : INotifyPropertyChanged
    {
        private int _currentLevel;
        public int CurrentLevel
        {
            get { return _currentLevel; }
            set
            {
                _currentLevel = value;
                OnPropertyChanged(nameof(CurrentLevel));
            }
        }
        private IState _state;
        public IState State
        {
            get { return _state; }

            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public int MaxWeight { get; private set; }
        public double BlackoutChance { get; private set; }
        public ObservableCollection<string> Logs { get; set; }

        public ElevatorModel(int maxWeight, double blackoutChance)
        {
            CurrentLevel = 1;
            MaxWeight = maxWeight;
            BlackoutChance = blackoutChance;
            Logs = new();
            State = new Idle(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CallTo(int level)
        {
            Logs.Add(State.CallTo(level));
        }
        public void Load(int weight)
        {
            Logs.Add(State.Load(weight));
        }
        public void Unload()
        {
            Logs.Add(State.Unload());
        }
        public void PowerRestore()
        {
            Logs.Add(State.PowerRestore());
        }
    }
}
