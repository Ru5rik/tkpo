using lab4_facade_lib;
using lab4_facade_lib.subsystem;

namespace lab4_facade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Стоимости путевок");
            Facade facade = new Facade(new BeachVoucher());
            Console.WriteLine($"Пляжный отдых в Египте:\t\t{facade.GetPrice(7, "Египет", 4, DietEnum.TwoTime):F2} рублей");

            facade = new Facade(new ExcursionVoucher());
            Console.WriteLine($"Экскурсию в Россию:\t\t{facade.GetPrice(7, "Россия", 4, DietEnum.ThreeTime):F2} рублей");
            Console.WriteLine($"Экскурсию в Беларусь:\t\t{facade.GetPrice(5, "Беларусь", 3, DietEnum.AllInclusive):F2} рублей");

            facade = new Facade(new DownhillVoucher());
            Console.WriteLine($"Горнолыжный спуск в Норвегии:\t{facade.GetPrice(4, "Норвегия", 5, DietEnum.AllInclusive):F2} рублей");
        }
    }
}
