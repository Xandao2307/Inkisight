using System.Text.Json.Serialization;

namespace InkInsight.API.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public User()
        {
        }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
