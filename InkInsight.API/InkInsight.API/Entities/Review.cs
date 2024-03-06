namespace InkInsight.API.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}
