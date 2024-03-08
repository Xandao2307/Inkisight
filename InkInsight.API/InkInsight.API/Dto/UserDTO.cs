namespace InkInsight.API.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid ReviewId { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
