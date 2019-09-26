namespace Wheel_of_Azure
{
    public interface ICategorizedPhrases
    {
        string category { get; set; }

        string GetPhrase(string cat);
        void RandomizeCat();
    }
}