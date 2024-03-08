using System.Text.Json.Serialization;

namespace InkInsight.API.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        public int? Rating { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
