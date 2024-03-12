using System.Text.Json.Serialization;

namespace InkInsight.API.Dto
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public Guid ReviewId { get; set; }

        public UserDTO()
        {
        }
    }
}
