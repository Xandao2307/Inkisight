using AutoMapper;
using InkInsight.API.Controllers;
using InkInsight.API.Mappers;
using InkInsight.API.Persistences;
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
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;

        public ReviewControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InkInsightDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _logger = Substitute.For<ILogger<BookController>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReviewProfile>();
            });
            _mapper = config.CreateMapper();
        }
        [Fact]
        public void GetReviewTest()
        {

        }
    }
}
