using NSubstitute;
using Microsoft.EntityFrameworkCore;
using InkInsight.API.Persistences;
using InkInsight.API.Controllers;
using InkInsight.API.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;


namespace InkInsight.UnitTests.Tests
{
    public class BookControllerTests
    {
        private readonly DbContextOptions<InkInsightDbContext> _dbContextOptions;
        private readonly ILogger<BookController> _logger;

        public BookControllerTests()
        {
            // Configurar um banco de dados em memória para testes
            _dbContextOptions = new DbContextOptionsBuilder<InkInsightDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Configurar um substituto para o ILogger
            _logger = Substitute.For<ILogger<BookController>>();
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