using NSubstitute;
using Microsoft.EntityFrameworkCore;
using InkInsight.API.Persistences;
using InkInsight.API.Controllers;
using InkInsight.API.Entities;
using InkInsight.API.Dto;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using InkInsight.API.Mappers;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace InkInsight.UnitTests.Tests
{
    public class BookControllerTests
    {
        private readonly DbContextOptions<InkInsightDbContext> _dbContextOptions;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        public BookControllerTests()
        {
            // Configurar um banco de dados em memória para testes
            _dbContextOptions = new DbContextOptionsBuilder<InkInsightDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Configurar um substituto para o ILogger
            _logger = Substitute.For<ILogger<BookController>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void GetBookTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new BookController(context, null, _logger);
                SeedTestData(context);

                var result = controller.Get();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
                Assert.True(context.Books.Count() >= 3);

            }
        }

        [Fact]
        public void PostBookTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new BookController(context, _mapper, _logger);
                var book = new BookDTO { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Book 1", Author = "Author 1", Description = "Description 1" };

                var result = controller.Post(book);

                Assert.IsType<CreatedAtActionResult>(result);
                Assert.True(context.Books.Count() >= 1);
            }
        }

        [Fact]
        public void PutBookTest()
        {

            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new BookController(context, _mapper, _logger);
                var book = new BookDTO { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Book 1.1", Author = "Author 1.1", Description = "Description 1.1" };
                SeedTestData(context);

                var result = controller.Put(book);
                var newBook = context.Books.Find(book.Id);

                Assert.IsType<NoContentResult>(result);
                Assert.True(newBook.Name == book.Name && newBook.Author == book.Author && newBook.Description == book.Description);
            }

        }

        [Fact]
        public void DeleteBookTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new BookController(context, _mapper, _logger);
                var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
                SeedTestData(context);

                var result = controller.Delete(id);
                var newBook = context.Books.Find(id);

                Assert.IsType<NoContentResult>(result);
                Assert.True(newBook == null);
            }

        }

        private static void SeedTestData(InkInsightDbContext context)
        {
            var books = new List<Book>
            {
                new Book { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Book 1", Author = "Author 1", Description = "Description 1" },
                new Book { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Book 2", Author = "Author 2", Description = "Description 2" },
                new Book { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Book 3", Author = "Author 3", Description = "Description 3" }
            };
            context.AddRange(books);
            context.SaveChanges();
        }
    }
}