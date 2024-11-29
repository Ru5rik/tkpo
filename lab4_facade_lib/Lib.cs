using lab4_facade_lib.subsystem;

namespace lab4_facade_lib
{
    namespace subsystem
    {
        public abstract class Voucher
        {
            public abstract double Price { get; }
        }
        public class BeachVoucher : Voucher
        {
            public override double Price => 5000;
        }
        public class ExcursionVoucher : Voucher
        {
            public override double Price => 3499;
        }
        public class DownhillVoucher : Voucher
        {
            public override double Price => 9500;
        }
    }
    public enum DietEnum
    {
        TwoTime,
        ThreeTime,
        AllInclusive
    }
    public class Facade
    {
        private subsystem.Voucher voucher;
        private Dictionary<string, double> countries = new()
        {
            {"Россия", 1},
            {"Беларусь", 1.1},
            {"Норвегия", 1.5},
            {"Египет", 1.25}
        };
        private Dictionary<DietEnum, double> diets = new()
        {
            {DietEnum.TwoTime, 1000},
            {DietEnum.ThreeTime, 1500},
            {DietEnum.AllInclusive, 2500}
        };
        public Facade(subsystem.Voucher voucher)
        {
            this.voucher = voucher;
        }
        public double GetPrice(int duraction, string country, int raiting, DietEnum diet)
        {
            return voucher.Price * countries[country] * duraction * ((double)raiting / 5 + 1)
                + duraction * diets[diet];
        }
    }
}
