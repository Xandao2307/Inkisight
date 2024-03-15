using InkInsight.API.Utils;
using InkInsight.API.Services;
using InkInsight.API.Configurations;
using NSubstitute;

namespace InkInsight.UnitTests.Tests
{
    public class UtilsAndServicesTests
    {

        [Fact]
        public void HashUtilsCreateHashTest()
        {
            const string expectedHash = "7eb6e13a46dda0fd9997b9d58416e9dc72de967b1a0cfe2597db17f2fb7a5c41"; // = "TestValue"
            const string testValue = "TestValue";
            var result = HashUtils.CreateHash(testValue);

            Assert.NotNull(result);
            Assert.Equal(expectedHash, result);
        }

        [Fact]
        public void TokenServicesGenerateTokenAndValidateTokenTest()
        {
            const string value = "ValueTest";
            const string secret = "e9fe51f94eadabf54dbf2fbbd57188b9abee436e";
            var configuration = Substitute.For<JwtConfiguration>();
            configuration.JwtSecret = secret;
            var service = new TokenService(configuration);

            var result = service.GenerateToken(value);
            var isValid = service.ValidateToken(result);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(isValid);
        }
    }
}