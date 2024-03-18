using AutoMapper;
using InkInsight.API.Controllers;
using InkInsight.API.Dto;
using InkInsight.API.Entities;
using InkInsight.API.Mappers;
using InkInsight.API.Persistences;
using InkInsight.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkInsight.UnitTests.Tests
{
    public class ReviewControllerTests
    {
        private readonly DbContextOptions<InkInsightDbContext> _dbContextOptions;
        private readonly ILogger<ReviewController> _logger;
        private readonly IMapper _mapper;

        public ReviewControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InkInsightDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _logger = Substitute.For<ILogger<ReviewController>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReviewProfile>();
            });
            _mapper = config.CreateMapper();
        }
        [Fact]
        public void GetReviewTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new ReviewController(context, _mapper, _logger);
                SeedTestData(context);

                var result = controller.GetAll();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var reviews = Assert.IsAssignableFrom<IEnumerable<Review>>(okResult.Value);
                Assert.True(reviews.Count() >= 3);
                ClearDatabase(context);
            }
        }

        [Fact]
        public void PostReviewTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new ReviewController(context, _mapper, _logger);
                var review = new ReviewDTO { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), BookId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Date = DateTime.Now.AddDays(4), Rating = 4, Text = "Text Review 4", UserId = Guid.Parse("00000000-0000-0000-0000-000000000002") };
                SeedTestData(context);

                var result = controller.Post(review);

                Assert.IsType<CreatedAtActionResult>(result);
                Assert.True(context.Reviews.Count() >= 4);
                ClearDatabase(context);
            }
        }

        [Fact]
        public void PutReviewTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new ReviewController(context, _mapper, _logger);
                var review = new ReviewDTO { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), BookId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Date = DateTime.Now.AddDays(11), Rating = 11, Text = "Text Review 1.1", UserId = Guid.Parse("00000000-0000-0000-0000-000000000001") };
                SeedTestData(context);

                var result = controller.Put(review);
                var rev = context.Reviews.Find(review.Id);

                Assert.IsType<NoContentResult>(result);
                Assert.True(rev.Text == review.Text && rev.Rating == review.Rating && rev.Date == review.Date);
                ClearDatabase(context);
            }
        }

        [Fact]
        public void DeleteReviewTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new ReviewController(context, _mapper, _logger);
                var id =  Guid.Parse("00000000-0000-0000-0000-000000000001");
                SeedTestData(context);

                var result = controller.Delete(id);
                var rev = context.Reviews.Find(id);

                Assert.IsType<NoContentResult>(result);
                Assert.True(rev == null);
                Assert.True(context.Reviews.Count() < 3);
                ClearDatabase(context);
            }
        }

        private static void SeedTestData(InkInsightDbContext context)
        {
            var users = new List<User>
            {
                new User { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "User 1", Email = "email1@email.com", Password = "Password 1" },
                new User { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "User 2", Email = "email2@email.com", Password = "Password 2" },
                new User { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "User 3", Email = "email3@email.com", Password = "Password 3" }
            };

            var books = new List<Book>
            {
                new Book { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Book 1", Author = "Author 1", Description = "Description 1" },
                new Book { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Book 2", Author = "Author 2", Description = "Description 2" },
                new Book { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Book 3", Author = "Author 3", Description = "Description 3" }
            };

            var reviews = new List<Review>
            {
                new Review { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Book = books[0], BookId = books[0].Id, Date = DateTime.Now.AddDays(1), Rating = 1, Text = "Text Review 1", User = users[0], UserId = users[0].Id },
                new Review { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Book = books[1], BookId = books[1].Id, Date = DateTime.Now.AddDays(2), Rating = 2, Text = "Text Review 2", User = users[1], UserId = users[1].Id },
                new Review { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Book = books[2], BookId = books[2].Id, Date = DateTime.Now.AddDays(3), Rating = 3, Text = "Text Review 2", User = users[2], UserId = users[2].Id }
            };
            context.Users.AddRange(users);
            context.Books.AddRange(books);
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }

        private static void ClearDatabase(InkInsightDbContext context)
        {
            var reviews = context.Reviews;
            var books = context.Books;
            var users = context.Users;
            context.Reviews.RemoveRange(reviews);
            context.Books.RemoveRange(books);
            context.Users.RemoveRange(users);
            context.SaveChanges();
        }
    }
}
