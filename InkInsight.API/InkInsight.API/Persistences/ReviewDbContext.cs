using InkInsight.API.Entities;

namespace InkInsight.API.Persistences
{
    public class ReviewDbContext
    {
        public List<Review> Reviews { get; set; }

        public ReviewDbContext()
        {
            Reviews = new List<Review>();
        }
    }
}
