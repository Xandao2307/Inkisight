namespace InkInsight.API.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User()
        {
        }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() 
        {
            return $"Id: {Id}\nName: {Name}";
        }
    }
}
