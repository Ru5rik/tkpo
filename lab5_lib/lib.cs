namespace lab5_lib
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
        private Elevator elevator;
        public Idle(Elevator elevator)
        {
            this.elevator = elevator;
        }
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
        private Elevator elevator;
        private int destination;
        private int current;
        public Movement(Elevator elevator, int destination)
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
        private Elevator elevator;
        public Overloaded(Elevator elevator)
        {
            this.elevator = elevator;
        }
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
        private Elevator elevator;
        public NoPower(Elevator elevator)
        {
            this.elevator = elevator;
        }
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
        private Elevator elevator;
        public Crash(Elevator elevator)
        {
            this.elevator = elevator;
        }
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
    public class Elevator
    {
        public int CurrentLevel { get; set; }
        public double BlackoutChance { get; private set; }
        public int MaxWeight { get; private set; }
        public IState State { get; set; }
        public Elevator(int maxWeight, double blackoutChance)
        {
            CurrentLevel = 1;
            MaxWeight = maxWeight;
            BlackoutChance = blackoutChance;
            State = new Idle(this);
        }
        public string CallTo(int level)
        {
            return State.CallTo(level);
        }
        public string Load(int weight)
        {
            return State.Load(weight);
        }
        public string Unload()
        {
            return State.Unload();
        }
        public string PowerRestore()
        {
            return State.PowerRestore();
        }
    }
}
