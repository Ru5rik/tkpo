using static System.Net.Mime.MediaTypeNames;

namespace lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("Книга1", "Содержимое книги");
            book.Read();

            Magazine magazine = new Magazine("Журнал1", "Содержимое журналал", "Выпуск 1");
            magazine.Read();
            Console.WriteLine($"Журнал имеет номер {magazine.Number}");

            TextBook studyBook = new TextBook("Учебник1", "Содержимое учебника", "Общее назначение");
            studyBook.Read();
            studyBook.Study();
        }
    }
    interface IRead
    {
        public void Read();
    }
    interface IStudy
    {
        public void Study();
    }
    abstract class Print : IRead
    {
        protected string title;
        protected string text;
        public Print(string title, string text)
        {
            this.text = text;
            this.title = title;
            Console.WriteLine($"Создание \"{this.title}\"");
        }
        public void Read()
        {
            Console.WriteLine($"Чтение {title}: {text}");
        }
    }
    class Book : Print
    {
        public Book(string title, string text) : base(title, text)
        {
        }
    }
    class Magazine : Print
    {
        public string Number { get; private set; }
        public Magazine(string title, string text, string number) : base(title, text)
        {
            Number = number;
        }
    }
    class TextBook : Book, IStudy
    {
        public string Specialization { get; set; }
        public TextBook(string title, string text, string specialization) : base(title, text)
        {
            Specialization = specialization;
        }

        public void Study()
        {
            Console.WriteLine($"Изучение учебника \"{title}\" на тему: {Specialization}");
        }
    }
}
