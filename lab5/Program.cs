using lab5_lib;

namespace lab5
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Elevator elevator = new Elevator(250, 0.3d);

            Console.WriteLine(elevator.CallTo(3));
            Console.WriteLine(elevator.PowerRestore());
            Console.WriteLine(elevator.Load(150));

            await Task.Delay(1100);

            Console.WriteLine(elevator.Load(150));
            Console.WriteLine(elevator.CallTo(1));
            Console.WriteLine(elevator.PowerRestore());

            await Task.Delay(1100);

            Console.WriteLine(elevator.Load(250));
            Console.WriteLine(elevator.Load(25));
            Console.WriteLine(elevator.Unload());

            Console.WriteLine(elevator.CallTo(2));
            Console.WriteLine(elevator.PowerRestore());
            
            await Task.Delay(1100);

            Console.WriteLine(elevator.Load(275));
            Console.WriteLine(elevator.CallTo(5));
        }
    }
}
