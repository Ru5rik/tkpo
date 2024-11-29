namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Метод \"Абстрактная фабрика\"");
            Console.WriteLine("Создание игрового ПК");
            ComputerFactory gamingPCFactory = new GamingPCFactory();
            IProcessor gamingPCProcessor = gamingPCFactory.CreateProcessor();
            IRAM gamingPCRAM = gamingPCFactory.CreateRAM();
            IGraphicsCard gamingPCGraphicsCard = gamingPCFactory.CreateGraphicsCard();

            gamingPCProcessor.Process();
            gamingPCRAM.LoadData();
            gamingPCGraphicsCard.RenderGraphics();

            Console.WriteLine("\nСоздание рабочего ПК");
            ComputerFactory workstationPCFactory = new WorkstationPCFactory();
            IProcessor workstationPCProcessor = workstationPCFactory.CreateProcessor();
            IRAM workstationPCRAM = workstationPCFactory.CreateRAM();
            IGraphicsCard workstationPCGraphicsCard = workstationPCFactory.CreateGraphicsCard();

            workstationPCProcessor.Process();
            workstationPCRAM.LoadData();
            workstationPCGraphicsCard.RenderGraphics();

            #region Фабричный метод

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nМетод \"Фабричный метод\"");
            Console.WriteLine("Создание игрового ПК");
            ComputerStore gamingPCStore = new GamingPCStore();
            gamingPCStore.OrderComputer();

            Console.WriteLine("\nСоздание рабочего ПК");
            ComputerStore workstationPCStore = new WorkstationPCStore();
            workstationPCStore.OrderComputer();

            #endregion

            #region Строитель

            Console.ForegroundColor = ConsoleColor.Red;
            

            #endregion
            Console.ResetColor();

        }
    }
    public interface IProcessor
    {
        void Process();
    }
    public interface IRAM
    {
        void LoadData();
    }
    public interface IGraphicsCard
    {
        void RenderGraphics();
    }
    public class IntelProcessor : IProcessor
    {
        public void Process()
        {
            Console.WriteLine("Intel процессор вычисляет");
        }
    }
    public class AMDProcessor : IProcessor
    {
        public void Process()
        {
            Console.WriteLine("AMD процессор вычисляет");
        }
    }
    public class DDR4RAM : IRAM
    {
        public void LoadData()
        {
            Console.WriteLine("Загрузка данных из DDR4 RAM");
        }
    }
    public class DDR5RAM : IRAM
    {
        public void LoadData()
        {
            Console.WriteLine("Загрузка данных из DDR5 RAM");
        }
    }
    public class NvidiaGraphicsCard : IGraphicsCard
    {
        public void RenderGraphics()
        {
            Console.WriteLine("Рендер с помощью видеокарты Nvidia");
        }
    }
    public class RadeonGraphicsCard : IGraphicsCard
    {
        public void RenderGraphics()
        {
            Console.WriteLine("Рендер с помощью видеокарты Radeon graphics");
        }
    }

    public abstract class ComputerFactory
    {
        public abstract IProcessor CreateProcessor();
        public abstract IRAM CreateRAM();
        public abstract IGraphicsCard CreateGraphicsCard();
    }
    public class GamingPCFactory : ComputerFactory
    {
        public override IProcessor CreateProcessor()
        {
            return new IntelProcessor();
        }
        public override IRAM CreateRAM()
        {
            return new DDR5RAM();
        }
        public override IGraphicsCard CreateGraphicsCard()
        {
            return new NvidiaGraphicsCard();
        }
    }
    public class WorkstationPCFactory : ComputerFactory
    {
        public override IProcessor CreateProcessor()
        {
            return new AMDProcessor();
        }
        public override IRAM CreateRAM()
        {
            return new DDR4RAM();
        }
        public override IGraphicsCard CreateGraphicsCard()
        {
            return new RadeonGraphicsCard();
        }
    }

    #region Фабричный метод

    public abstract class AComputer
    {
        public IProcessor Processor { get; set; }
        public IRAM RAM { get; set; }
        public IGraphicsCard GraphicsCard { get; set; }

        public abstract void DisplayConfiguration();
    }

    public class GamingPC : AComputer
    {
        public GamingPC()
        {
            Processor = new IntelProcessor();
            RAM = new DDR4RAM();
            GraphicsCard = new NvidiaGraphicsCard();
        }

        public override void DisplayConfiguration()
        {
            Console.WriteLine("Конфигурация игрового ПК:");
            Processor.Process();
            RAM.LoadData();
            GraphicsCard.RenderGraphics();
        }
    }

    public class WorkstationPC : AComputer
    {
        public WorkstationPC()
        {
            Processor = new AMDProcessor();
            RAM = new DDR5RAM();
            GraphicsCard = new RadeonGraphicsCard();
        }

        public override void DisplayConfiguration()
        {
            Console.WriteLine("Конфигурация рабочего ПК:");
            Processor.Process();
            RAM.LoadData();
            GraphicsCard.RenderGraphics();
        }
    }

    public abstract class ComputerStore
    {
        public abstract AComputer CreateComputer();

        public void OrderComputer()
        {
            var computer = CreateComputer();
            computer.DisplayConfiguration();
        }
    }

    public class GamingPCStore : ComputerStore
    {
        public override AComputer CreateComputer()
        {
            return new GamingPC();
        }
    }

    public class WorkstationPCStore : ComputerStore
    {
        public override AComputer CreateComputer()
        {
            return new WorkstationPC();
        }
    }

    #endregion

    #region Строитель

   

    #endregion
}
