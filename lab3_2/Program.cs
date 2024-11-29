namespace lab3_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
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
    }
}
