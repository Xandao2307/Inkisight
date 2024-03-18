using AutoMapper;
using InkInsight.API.Configurations;
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
    public class UserControllerTests
    {
        private readonly DbContextOptions<InkInsightDbContext> _dbContextOptions;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public UserControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InkInsightDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _logger = Substitute.For<ILogger<UserController>>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            _mapper = config.CreateMapper();

            const string secret = "e9fe51f94eadabf54dbf2fbbd57188b9abee436e";
            var configuration = Substitute.For<JwtConfiguration>();
            configuration.JwtSecret = secret;
            _tokenService = new TokenService(configuration);
        }

        [Fact]
        public void GetUserTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new UserController(context, _mapper, _tokenService, _logger);
                SeedTestData(context);

                var result = controller.Get();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var users = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
                Assert.True(users.Count() >= 3);
                ClearDatabase(context);
            }
        }

        [Fact]
        public void PostUserTest() 
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new UserController(context, _mapper, _tokenService, _logger);
                var user = new UserDTO { Name = "User 1", Email = "email1@email.com", Password = "Password 1" };

                var result = controller.Post(user);

                Assert.IsType<CreatedAtActionResult>(result);
                Assert.True(context.Users.Count() >= 1);
                ClearDatabase(context);
            }
        }

        [Fact]
        public void LoginUserTest()
        {
            using (var context = new InkInsightDbContext(_dbContextOptions))
            {
                var controller = new UserController(context, _mapper, _tokenService, _logger);
                var user = new UserDTO { Name = "User 1", Email = "email1@email.com", Password = "Password 1" };
                controller.Post(user);
                OkObjectResult? okResult = controller.Login(user.Email, user.Password) as OkObjectResult;
               
                Assert.IsType<OkObjectResult>(okResult);
                var token = okResult.Value.ToString();
                Assert.IsType<string>(token);
                Assert.NotNull(token);
                Assert.True(_tokenService.ValidateToken(token));
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
            context.AddRange(users);
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
