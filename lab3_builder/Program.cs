using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lab3_builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Builder builder = new XmlBuilder(File.ReadAllLines("input.txt"));
            Director director = new Director(builder);
            director.Construct();
            builder.GetResult();
        }
    }
    public class Article
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string Hash { get; set; }

    }

    public abstract class Builder
    {
        protected Article article = new Article();
        public abstract void BuildTitle();
        public abstract void BuildAuthor();
        public abstract void BuildText();
        public abstract void BuildHash();
        public abstract void GetResult();
    }

    public class XmlBuilder : Builder
    {
        private string[] file;
        private Article rawArticle;
        public XmlBuilder(string[] file)
        {
            this.file = file;
            rawArticle = new();
        }

        private string Tagged(string tag, string value) => $"<{tag}>{value}</{tag}>";
        public override void BuildTitle()
        {
            rawArticle.Title = file.FirstOrDefault("TITLE");
            article.Title = Tagged("title", rawArticle.Title);
        }

        public override void BuildAuthor()
        {
            rawArticle.Author = file.Skip(1).FirstOrDefault("AUTHOR");
            article.Author = Tagged("author", rawArticle.Author);
        }

        public override void BuildText()
        {
            rawArticle.Text = string.Join("\n", file.Skip(2).Take(file.Length - 3));
            article.Text = Tagged("text", rawArticle.Text);
        }
        public override void BuildHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string hash = string.Join("", 
                    sha256.ComputeHash(
                        Encoding.UTF8.GetBytes($"{rawArticle.Title}\n{rawArticle.Author}\n{rawArticle.Text}"))
                    .Select(x => x.ToString("x2")));
                article.Hash = Tagged("hash", hash);

                rawArticle.Hash = file.LastOrDefault("");
                if (hash == rawArticle.Hash)
                    Console.WriteLine($"Хэш суммы совпадают: {hash}");
                else Console.WriteLine($"Хэш суммы не совпадают:\ntxt: {rawArticle.Hash}\nxml: {hash}");
            }
        }
        public override void GetResult()
        {
            File.WriteAllText("output.xml", "<?xml version=\"1.1\" encoding=\"UTF-8\" ?>" +
                Tagged("article", string.Join("", article.Title, article.Author, article.Text, article.Hash)));
        }
    }
    public class Director
    {
        private Builder builder;

        public Director(Builder builder)
        {
            this.builder = builder;
        }

        public void Construct()
        {
            builder.BuildTitle();
            builder.BuildAuthor();
            builder.BuildText();
            builder.BuildHash();
        }
    }
}
