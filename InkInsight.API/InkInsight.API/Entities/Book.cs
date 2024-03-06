namespace InkInsight.API.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Book()
        {
        }

        public Book(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"id: {Id}\nName: {Name}\nDescription: {Description}";
        }
    }
}
