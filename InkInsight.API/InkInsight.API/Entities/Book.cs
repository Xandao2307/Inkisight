﻿using System.Text.Json.Serialization;

namespace InkInsight.API.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Book()
        {
        }

        public Book(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

 
    }
}
