namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITarget target = new GasTankObjAdapter(2.6, 1, 28);
            Console.WriteLine(target.GetData());
            target.ModifMass(0.75d);
            Console.WriteLine(target.GetData());

            int t0 = 30, dt = 60;
            Console.WriteLine($"Разница давления при изменении температуры с {t0} по {dt}: {target.CalculateDp(t0, dt):F3} Па");
        }
    }

    public interface ITarget
    {
        double CalculateDp(int t0, int dT);
        void ModifMass(double dm);
        string GetData();
    }
    class GasTank
    {
        private const double R = 8.314;
        private const double K = 273.15;

        private double volume;
        // кг
        internal double mass;
        // г/моль
        private double molar;

        public GasTank(double volume, double mass, double molar)
        {
            this.volume = volume;
            this.mass = mass;
            this.molar = molar;
        }
        public GasTank() : this(1, 1, 1)
        {

        }
        public double GetPressure(int t) => AmountOfMatter() * R * (t + K) / volume;
        private double AmountOfMatter() => mass * 1000 / molar;
        public override string ToString() =>
                $"Информация о газовом баллоне\n" +
                $"Объем:\t\t\t{volume}\tм^3\n" +
                $"Масса газа:\t\t{mass}\tкг\n" +
                $"Молярная масса:\t\t{molar}\tг/моль\n";
    }
    public class GasTankObjAdapter : ITarget
    {
        private GasTank gasTank;
        public GasTankObjAdapter(double volume, double mass, double molar)
        {
            gasTank = new(volume, mass, molar);
        }
        public double CalculateDp(int t0, int dt) => gasTank.GetPressure(dt) - gasTank.GetPressure(t0);
        public string GetData() => gasTank.ToString();
        public void ModifMass(double dm) => gasTank.mass += dm;
    }
}
