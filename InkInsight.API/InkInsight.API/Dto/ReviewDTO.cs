using InkInsight.API.Entities;

namespace InkInsight.API.Dto
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int? Rating { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
