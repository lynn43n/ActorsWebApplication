namespace ActorsWebApplication.Models
{
    public class DuplicateRankException : Exception
    {
        public DuplicateRankException(string message) : base(message)
        {
        }

    }
}
