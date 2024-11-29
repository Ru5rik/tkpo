namespace lab3_abstract_factory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Film film = new Film(new RUFilmFactory());

            film.Watch();
            Console.WriteLine("\n");

            film.ChangeLanguage(new ENGFilmFactory());

            film.Watch();
            Console.WriteLine("\n");

            film.ChangeLanguage(new RUFilmFactory());
            film.Watch();
        }
    }
    public class Film
    {
        private IAudio audio;
        private ISubtitles subtitles;

        public Film(FilmFactory factory)
        {
            audio = factory.CreateAudio();
            subtitles = factory.CreateSubtitles();
        }

        public void Watch()
        {
            audio.Play();
            subtitles.Show();
        }

        public void ChangeLanguage(FilmFactory factory)
        {
            audio = factory.CreateAudio();
            subtitles = factory.CreateSubtitles();
        }
    }
    public interface IAudio
    {
        void Play();
    }
    public interface ISubtitles
    {
        void Show();
    }
    public class RUAudio : IAudio
    {
        public void Play()
        {
            Console.WriteLine("Звучит русская озвучка");
        }
    }
    public class ENGAudio : IAudio
    {
        public void Play()
        {
            Console.WriteLine("Звучит английская озвучка");
        }
    }
    public class RUSubtitles : ISubtitles
    {
        public void Show()
        {
            Console.WriteLine("Показаны русские субтитры");
        }
    }
    public class ENGSubtitles : ISubtitles
    {
        public void Show()
        {
            Console.WriteLine("Показаны английские субтитры");
        }
    }
    public abstract class FilmFactory
    {
        public abstract IAudio CreateAudio();
        public abstract ISubtitles CreateSubtitles();
    }
    public class RUFilmFactory : FilmFactory
    {
        public override IAudio CreateAudio()
        {
            return new RUAudio();
        }

        public override ISubtitles CreateSubtitles()
        {
            return new RUSubtitles();
        }
    }
    public class ENGFilmFactory : FilmFactory
    {
        public override IAudio CreateAudio()
        {
            return new ENGAudio();
        }

        public override ISubtitles CreateSubtitles()
        {
            return new ENGSubtitles();
        }
    }
}
