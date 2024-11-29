namespace lab3_factory_method
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShapeCreator factory = new RandomShapeCreator();

            Shape shape = factory.CreateShape();
            Console.WriteLine($"Создана фигура: {shape.Name}, Клеток: {shape.CellsCount}");

            shape = factory.CreateShape();
            Console.WriteLine($"Создана фигура: {shape.Name}, Клеток: {shape.CellsCount}");

            shape = factory.CreateShape();
            Console.WriteLine($"Создана фигура: {shape.Name}, Клеток: {shape.CellsCount}");
        }
    }

    public abstract class Shape()
    {
        public abstract string Name { get; }
        public abstract int CellsCount { get; }
    }
    public class Square : Shape
    {
        public override string Name => "Square";
        public override int CellsCount => 4;
    }

    public class Line : Shape
    {
        public override string Name => "Line";
        public override int CellsCount => 4;
    }

    public class TShape : Shape
    {
        public override string Name => "TShape";
        public override int CellsCount => 4;
    }

    public class LShape : Shape
    {
        public override string Name => "LShape";
        public override int CellsCount => 4;
    }

    public class SuperSquare : Shape
    {
        public override string Name => "SuperSquare";
        public override int CellsCount => 9;
    }

    public class SuperLine : Shape
    {
        public override string Name => "SuperLine";
        public override int CellsCount => 10;
    }
    public abstract class ShapeCreator
    {
        public abstract Shape CreateShape();
    }
    public class RandomShapeCreator : ShapeCreator
    {
        private Random random = new Random();
        private List<Func<Shape>> shapes = new List<Func<Shape>>
        {
            () => new Square(),
            () => new Line(),
            () => new TShape(),
            () => new LShape(),
            () => new SuperSquare(),
            () => new SuperLine()
        };

        public override Shape CreateShape()
        {
            var index = random.Next(shapes.Count);
            return shapes[index]();
        }
    }
}
